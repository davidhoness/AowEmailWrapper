using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;

using EricDaugherty.CSES.Net;
using EricDaugherty.CSES.SmtpServer;
using AowEmailWrapper.CSES;
using AowEmailWrapper.Classes;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Controls;
using AowEmailWrapper.Pollers;
using AowEmailWrapper.Games;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Localization;

using Lesnikowski.Mail;

using Microsoft.Win32;

namespace AowEmailWrapper
{
    public enum IconState
    { 
        None = 1,
        Normal,
        Sending,
        Checking,
        EmailWaiting,
        CheckEmail
    }

    public partial class Main : Form
    {
        #region String Constants

        private const string WINDOWS_REG_STARTUP_LOCATION = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string WrapperWriteAccessMessageBoxKey = "msgWrapperWriteAccess";
        private const string WrapperArchiveGameMessageBoxKey = "msgWrapperArchiveGame";
        private const string WrapperCannotActivateAccountMessageBoxKey = "msgWrapperCannotActivateAccount";
        private const string WrapperEmailSentSuccessKey = "msgWrapperEmailSentSuccess";
        private const string WrapperEmailSentFailedKey = "msgWrapperEmailSentFailed";
        private const string WrapperRestartRequiredKey = "msgWrapperRestartRequired";
        private const string WrapperPollFailedKey = "msgWrapperPollFailed";
        private const string WrapperGamesWaitingKey = "msgWrapperGamesWaiting";

        private const string Menu_Show_Tag = "menuItemShow";
        private const string Menu_Accounts_Tag = "menuItemAccounts";
        private const string Menu_Poll_Tag = "menuItemPollNow";
        private const string Menu_Exit_Tag = "menuItemExit";

        #endregion

        #region Private Members

        private Icon _baseIcon = null;
        private SimpleServer _theServer;
        private BasePoller _poller;
        private static AowGameManager _gameManager;
        private static SmtpSender _smtpSender;

        private Config _wrapperConfig;
        private ActivityList _activityLog;

        private StartedTaskWatcher _startedGameWatcher;
        private EventHandler _shutDownEvent;
        private EventHandler _activityLogRefresh;
        private bool _closeCancel = true;

        private ContextMenu _contextMenu;

        private MenuItem _menuAccounts;
        private MenuItem _menuShow;
        private MenuItem _menuPoll;
        private MenuItem _menuExit;

        #endregion

        public Main()
        {
            bool isNewConfig = false;
            _wrapperConfig = DataManagerHelper.LoadConfig(out isNewConfig);

            LoadTranslations();

            InitializeComponent();

            Translator.TranslateForm(this);

            _gameManager = new AowGameManager();

            if (!_gameManager.CheckWriteAccess())
            {
                MessageBox.Show(Translator.Translate(WrapperWriteAccessMessageBoxKey, _gameManager.GetEmailInFolderList()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            lblVersion.Text = string.Format(lblVersion.Text, ConfigHelper.BuildVersion);

            _baseIcon = notifyIcon.Icon;

            SetIcon(IconState.Normal);

            LoadActivityLog();

            LoadConfig(isNewConfig);
            
            CreateContextMenu();

            BindCustomEvents();

            CheckNotifyIconState(true);

            this.FormClosing += new FormClosingEventHandler(Main_FormClosing);

            StartServer();
        }

        #region Form Events

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_closeCancel && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                e.Cancel = false;

                if (_theServer != null && _theServer.IsRunning)
                {
                    StopServer();
                }

                if (_poller != null)
                {
                    _poller.Stop();
                }

                if (_startedGameWatcher != null)
                {
                    _startedGameWatcher.Stop();
                }

                SetIcon(IconState.None);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Minimized();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Maximize();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        #endregion

        #region Load / Save Config

        private void LoadTranslations()
        {
            if (_wrapperConfig != null &&
                _wrapperConfig.PreferencesConfig != null)
            {
                //This will make non supported regional settings default to English language
                string loadedLanguageCode = Translator.SetLanguage(_wrapperConfig.PreferencesConfig.LanguageCode, DataManagerHelper.LoadLanguages());
                _wrapperConfig.PreferencesConfig.LanguageCode = loadedLanguageCode;
            }
        }

        private void LoadConfig(bool isNewConfig)
        {
            try
            {
                if (!isNewConfig)
                {
                    this.WindowState = FormWindowState.Minimized;
                    Minimized();
                }

                if (_wrapperConfig != null)
                {
                    if (_wrapperConfig.AccountsList != null)
                    {
                        accountsConfig.Config = _wrapperConfig.AccountsList;
                        ActivateAccount(_wrapperConfig.AccountsList.ActiveAccount);
                    }

                    if (_wrapperConfig.PreferencesConfig != null)
                    {
                        preferencesConfig.Config = _wrapperConfig.PreferencesConfig;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                ShowException(ex);
            }
        }

        private void SaveConfig()
        {
            try
            {
                //Accounts
                AccountConfigValuesList accountConfigValuesList = accountsConfig.Config;
                if (accountConfigValuesList != null)
                {
                    _wrapperConfig.AccountsList = accountConfigValuesList;
                    CreateAccountMenu(_menuAccounts, _wrapperConfig.AccountsList);

                    if (accountConfigValuesList.ActiveAccount != null)
                    {
                        AccountConfigValues theAccount = accountConfigValuesList.ActiveAccount;
                        _gameManager.SetEmailConfigAll(AppDataHelper.CheckEmail.FullName, theAccount.SmtpConfig.EmailAddress, "localhost");

                        panelLocalMessageStore.Visible = theAccount.PollingConfig.EmailType.Equals(EmailType.POP3);
                    }
                }

                //Preferences
                PreferencesConfigValues preferencesConfigValues = preferencesConfig.Config;
                if (preferencesConfigValues != null)
                {
                    if (preferencesConfigValues.Autostart)
                    {
                        RegistryHelper.SetValue(Registry.CurrentUser, WINDOWS_REG_STARTUP_LOCATION, this.Text, string.Format("\"{0}\" {1}", Application.ExecutablePath, ConfigHelper.AUTOSTART_CMD_PARAM));
                    }
                    else
                    {
                        RegistryHelper.DeleteValue(Registry.CurrentUser, WINDOWS_REG_STARTUP_LOCATION, this.Text);
                    }
                }

                _wrapperConfig.PreferencesConfig = preferencesConfigValues;

                DataManagerHelper.SaveConfig(_wrapperConfig);

                ActivateAccount(_wrapperConfig.AccountsList.ActiveAccount);

                if (preferencesConfigValues != null &&
                    !preferencesConfigValues.LanguageCode.Equals(Translator.CurrentLanguageCode))
                {
                    MessageBox.Show(Translator.Translate(WrapperRestartRequiredKey), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                ShowException(ex);
            }
        }

        #endregion

        #region Custom Events

        private void BindCustomEvents()
        {
            _shutDownEvent += new EventHandler(ShutDown);
            _activityLogRefresh += new EventHandler(ActivityLogRefresh);
            _gameManager.OnGameSaved += new AowGameSavedEventHandler(OnAowGameSaved);

            EventHandler configNeedsSave = new EventHandler(Config_Needs_Save);

            accountsConfig.Account_Activated += new AccountActivatedEventHandler(Account_Activated);
            accountsConfig.Config_Changed += new EventHandler(Rebuild_Account_Menu);
            accountsConfig.Config_Changed += configNeedsSave;
            
            preferencesConfig.Config_Changed += configNeedsSave;

            activityListView.OnDoubleClick += new ActivityListViewEventHandler(ActivityListViewDoubleClicked);
            activityListView.OnListChanged += new EventHandler(ActivityLogChanged);
            activityListView.OnMarkAsEnded += new ActivityListViewEventHandler(ActivityListViewGamesMarkedAsEnded);
        }

        private void ShutDown(object sender, EventArgs e)
        {
            _closeCancel = false;
            this.Close();
        }

        private void StartedGameWatchCompleted(StartedTaskWatcher sender, Process theProcess)
        {
            _startedGameWatcher = null;
        }

        private void Config_Needs_Save(object sender, EventArgs e)
        {
            cmdSave.BackColor = Color.IndianRed;
        }

        #endregion

        #region Private Utility Methods

        private void PlaySound(string theFile)
        {
            if (!string.IsNullOrEmpty(theFile))
            {
                System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
                string file = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), theFile);
                if (System.IO.File.Exists(file))
                {
                    myPlayer.SoundLocation = file;
                    myPlayer.Play();
                    myPlayer.Dispose();
                    myPlayer = null;
                }
            }
        }

        private void CheckNotifyIconState()
        {
            CheckNotifyIconState(false);
        }

        private void CheckNotifyIconState(bool showBaloon)
        {
            IconState theState = IconState.Normal;

            if (_smtpSender != null && _smtpSender.IsSending)
            {
                SetIcon(IconState.Sending);
            }
            else if (_poller != null && _poller.IsPolling)
            {
                SetIcon(IconState.Checking);
            }
            else
            {
                int fileCount = _activityLog.GetUnSentActivitiesCount();
                if (fileCount > 0)
                {
                    theState = IconState.EmailWaiting;

                    if (showBaloon)
                    {
                        this.Activate();
                        notifyIcon.ShowBalloonTip(5000, this.Text, Translator.Translate(WrapperGamesWaitingKey, fileCount.ToString()), ToolTipIcon.Info);
                    }
                }

                SetIcon(theState);
            }
        }

        private void RaiseEvent(EventHandler theDelegate, object sender, EventArgs e)
        {
            if (theDelegate != null)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(theDelegate, sender, e);
                }
                else
                {
                    theDelegate(sender, e);
                }
            }
        }

        private void SetIcon(IconState theState)
        {
            Icon theIcon = null;
            string status = null;

            switch (theState)
            {
                case IconState.Normal:
                    theIcon = _baseIcon;
                    status = null;
                    break;
                case IconState.Sending:
                    theIcon = GetIcon(theState.ToString());
                    status = Translator.TranslateEnum(theState);
                    break;
                case IconState.Checking:
                    theIcon = GetIcon(theState.ToString());
                    status = Translator.TranslateEnum(theState);
                    break;
                case IconState.EmailWaiting:
                    theIcon = GetIcon(theState.ToString());
                    status = Translator.TranslateEnum(theState);
                    break;
                default:
                    status = null;
                    theIcon = null;
                    break;
            }

            notifyIcon.Text = !string.IsNullOrEmpty(status) ? string.Format("{0}: {1}", this.Text, status) : this.Text;
            notifyIcon.Icon = theIcon;
        }

        private Icon GetIcon(string theKey)
        {
            Icon returnVal = null;

            if (imageListIcons.Images.ContainsKey(theKey))
            {
                returnVal = Icon.FromHandle(((Bitmap)imageListIcons.Images[theKey]).GetHicon());
            }

            return returnVal;
        }

        private void Minimized()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void Maximize()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (_activityLog != null && 
                    _activityLog.Activities != null && 
                    _activityLog.Activities.Count > 0)
                {
                    tabControlMain.SelectedTab = tabControlMain.TabPages["tabActivity"];
                }
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Activate();
            }
        }

        private void ShowException(Exception ex)
        {
            string error = string.Concat(ex.Message, "\r\n\r\n", ex.StackTrace);
            Clipboard.SetText(error);
            MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StartGame(AowGame theGame)
        {
            if (_startedGameWatcher == null)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = theGame.ExeFile;
                proc.StartInfo.WorkingDirectory = theGame.Root.FullName;

                proc.Start();

                _startedGameWatcher = new StartedTaskWatcher(proc, new StartedTaskCompleteEventHandler(StartedGameWatchCompleted));
                new Thread(new ThreadStart(_startedGameWatcher.Start)).Start();
            }
            else
            {
                //already running
            }
        }

        #endregion

        #region Incoming Email

        private void StartPolling(PollingConfigValues pollingConfigValues, PreferencesConfigValues preferencesConfigValues)
        {
            switch (pollingConfigValues.EmailType)
            {
                case EmailType.IMAP:
                    _poller = new ImapPoller(
                        pollingConfigValues.Server,
                        pollingConfigValues.Port,
                        pollingConfigValues.UseSSL,
                        pollingConfigValues.Username,
                        pollingConfigValues.PasswordTrue,
                        pollingConfigValues.PollInterval,
                        preferencesConfigValues.SaveFolder,
                        _gameManager);
                    break;
                case EmailType.POP3:
                    _poller = new Pop3Poller(
                        pollingConfigValues.Server,
                        pollingConfigValues.Port,
                        pollingConfigValues.UseSSL,
                        pollingConfigValues.Username,
                        pollingConfigValues.PasswordTrue,
                        pollingConfigValues.PollInterval,
                        preferencesConfigValues.SaveFolder,
                        _gameManager);
                    break;
            }

            _poller.OnEmailEvent += new PollerEmailEventHandler(PollerEmailEvent);
            _poller.Start();
        }

        private void StopPolling()
        {
            if (_poller != null)
            {
                _poller.Stop();
            }
        }

        private void PollerEmailEvent(object sender, PollerEventArgs e)
        {
            if (_closeCancel)
            {
                switch (e.PollState)
                {
                    case PollState.Begin:
                        SetIcon(IconState.Checking);
                        break;
                    case PollState.End:
                        if (e.EmailRecieved)
                        {
                            if (_wrapperConfig.PreferencesConfig != null && _wrapperConfig.PreferencesConfig.PlaySoundOnEmail)
                            {
                                PlaySound(ConfigHelper.NotifySound);
                            }
                            Thread.Sleep(50);
                            RaiseEvent(_activityLogRefresh, this, new EventArgs());
                            GC.Collect();
                        }
                        if (e.Exception != null)
                        {
                            notifyIcon.ShowBalloonTip(15000, Translator.Translate(WrapperPollFailedKey), e.Exception.Message, ToolTipIcon.Error);
                        }
                        CheckNotifyIconState();
                        break;
                }
            }
        }

        private void cmdMessageStore_Click(object sender, EventArgs e)
        {
            if (_wrapperConfig != null)
            {
                AccountConfigValues theAccount = _wrapperConfig.AccountsList.ActiveAccount;

                if (theAccount != null &&
                    !string.IsNullOrEmpty(theAccount.PollingConfig.Username) &&
                    !string.IsNullOrEmpty(theAccount.PollingConfig.Server))
                {
                    MessageStore form = new MessageStore(theAccount.PollingConfig.Username, theAccount.PollingConfig.Server);
                    form.OnReDownload += new EventHandler(MessageStoreReDownload);
                    form.ShowDialog(this);
                }
            }
        }

        private void MessageStoreReDownload(object sender, EventArgs e)
        {
            if (_wrapperConfig != null)
            {
                AccountConfigValues theAccount = _wrapperConfig.AccountsList.ActiveAccount;

                if (_poller != null)
                {
                    _poller.PollNow();
                }
                else if (theAccount.PollingConfig != null)
                {
                    StartPolling(theAccount.PollingConfig, _wrapperConfig.PreferencesConfig);
                }
            }
        }

        #endregion

        #region Outgoing Email

        private void StartServer()
        {
            try
            {
                _theServer = new SimpleServer(25, ProcessSMTPRequest);
                new Thread(new ThreadStart(_theServer.Start)).Start();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                ShowException(ex);
            }
        }

        private void StopServer()
        {
            if (_theServer != null)
            {
                _theServer.Stop();
                _theServer = null;
            }
        }

        public void ProcessSMTPRequest(Socket socket)
        {
            SMTPProcessor theSmtpProcessor = null;

            try
            {
                SmtpSpool smtpSpooler = new SmtpSpool();

                theSmtpProcessor = new SMTPProcessor(string.Concat(Environment.MachineName, ".com"), new AnyRecipientFilter(), smtpSpooler);

                SetIcon(IconState.Sending);

                theSmtpProcessor.ProcessConnection(socket);

                IMail theEmail = smtpSpooler.SpooledEmail;

                if (theEmail != null)
                {
                    _smtpSender.SendMessage(theEmail);
                }

                theSmtpProcessor.Dispose();
                theSmtpProcessor = null;

                CheckNotifyIconState();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                ShowException(ex);
            }
            finally
            {
                theSmtpProcessor = null;
            }
        }

        private void CreateSmtpSender(SmtpConfigValues smtpConfig, PollingConfigValues pollingConfig)
        {
            if (smtpConfig != null && pollingConfig != null)
            {
                bool create = true;

                if (_smtpSender != null && _smtpSender.IsSending)
                {
                    create = false;
                }
                else if (_smtpSender != null)
                {
                    _smtpSender.Dispose();
                    _smtpSender = null;
                }

                if (create)
                {
                    if (smtpConfig.Authentication)
                    {
                        _smtpSender = new SmtpSender(
                            smtpConfig.SmtpServer,
                            smtpConfig.Port,
                            (smtpConfig.UsePollingCredentials) ? pollingConfig.Username : smtpConfig.Username,
                            (smtpConfig.UsePollingCredentials) ? pollingConfig.PasswordTrue : smtpConfig.PasswordTrue,
                            smtpConfig.SmtpSSLType,
                            smtpConfig.BCCMyself);
                    }
                    else
                    {
                        _smtpSender = new SmtpSender(
                            smtpConfig.SmtpServer,
                            smtpConfig.Port,
                            smtpConfig.SmtpSSLType,
                            smtpConfig.BCCMyself);
                    }

                    _smtpSender.OnEmailSent += new SmtpSenderSentEventHandler(SmtpSenderSent);
                }
            }
        }

        private void SmtpSenderSent(object sender, SmtpSendResponse theResponse)
        {
            this.Activate();

            if (theResponse.IsSuccess)
            {
                notifyIcon.ShowBalloonTip(15000, theResponse.GameEmail.Subject, Translator.Translate(WrapperEmailSentSuccessKey, theResponse.GameEmail.To[0].Address), ToolTipIcon.Info);
                if (_wrapperConfig.PreferencesConfig != null && _wrapperConfig.PreferencesConfig.PlaySoundOnSend)
                {
                    PlaySound(ConfigHelper.SentSound);
                }

                if (theResponse.GameEmail.Attachments.Count > 0)
                {
                    MimeData theAttachment = theResponse.GameEmail.Attachments[0];
                    AowGame theGame = _gameManager.GetGameByFile(theAttachment.FileName);

                    UpdateActivitySent(theAttachment.FileName, theGame);

                    if (_wrapperConfig.PreferencesConfig != null && _wrapperConfig.PreferencesConfig.CopyToEmailOut)
                    {
                        try
                        {
                            _gameManager.CopyToEmailOut(theAttachment, theGame);
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError(ex.ToString());
                            Trace.Flush();
                            ShowException(ex);
                        }
                    }
                }

                theResponse.Dispose();
                
                GC.Collect();
            }
            else
            {
                DialogResult theResult = MessageBox.Show(
                    Translator.Translate(WrapperEmailSentFailedKey,
                    theResponse.GameEmail.Subject,
                    theResponse.GameEmail.To.ToString(),
                    theResponse.Error),
                    this.Text, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (theResult.Equals(DialogResult.Retry) && _smtpSender != null)
                {
                    _smtpSender.SendMessage(theResponse.GameEmail);
                }
                else
                {
                    theResponse.Dispose();
                }
            }

            CheckNotifyIconState();
        }

        #endregion

        #region Accounts

        private void Account_Activated(object sender, AccountConfigValues theAccount)
        {
            _wrapperConfig.AccountsList = accountsConfig.Config;
            ActivateAccount(theAccount);
        }

        private bool ActivateAccount(AccountConfigValues account)
        {
            bool success = false;

            //Don't switch accounts if a send is in progress
            if ((_smtpSender != null && _smtpSender.IsSending))
            {
                success = false;
                MessageBox.Show(Translator.Translate(WrapperCannotActivateAccountMessageBoxKey, account.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (account != null &&
                !string.IsNullOrEmpty(account.Name))
            {
                _wrapperConfig.AccountsList.ActiveAccountName = account.Name;

                if (account.PollingConfig != null)
                {
                    panelLocalMessageStore.Visible = account.PollingConfig.EmailType.Equals(EmailType.POP3);
                }

                if (account.SmtpConfig != null)
                {
                    _gameManager.SetEmailConfigAll(AppDataHelper.CheckEmail.FullName, account.SmtpConfig.EmailAddress, "localhost");
                }

                StopPolling();

                CreateSmtpSender(account.SmtpConfig, account.PollingConfig);

                if (account.PollingConfig.UsePolling &&
                    !string.IsNullOrEmpty(account.PollingConfig.Username) &&
                    !string.IsNullOrEmpty(account.PollingConfig.Password))
                {
                    StartPolling(account.PollingConfig, _wrapperConfig.PreferencesConfig);
                }

                success = true;

                //this.Text = string.Format(MainFormTitleTemplate, Translator.Translate(this.Name), account.Name);

                accountsConfig.Refresh();

                cmdSave.BackColor = this.BackColor;
            }

            return success;
        }

        #endregion

        #region Activity Log

        //Raised by the AowGameManager class
        private void OnAowGameSaved(object sender, AowGameSavedEventArgs e)
        {
            Activity lastActivity = _activityLog.GetLastActivityByFileName(e.FileName);

            if (lastActivity != null)
            {
                _activityLog.Activities.Remove(lastActivity);
            }

            Activity newActivity = new Activity(e.GameType, e.FileName, ActivityState.Received);
            _activityLog.Activities.Add(newActivity);
        }

        private void LoadActivityLog()
        {
            try
            {
                activityListView.SmallImageList = imageListIcons;
                _activityLog = DataManagerHelper.LoadActivityLog();
                activityListView.ActivityLog = _activityLog;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
                ShowException(ex);
            }
        }

        private void ActivityListViewDoubleClicked(object sender, List<Activity> list)
        {
            if (list != null && list.Count > 0)
            {
                AowGame theGame = _gameManager.GetGameByType(list[0].GameType);
                if (theGame != null)
                {
                    StartGame(theGame);
                }
            }
        }

        private void ActivityListViewGamesMarkedAsEnded(object sender, List<Activity> list)
        {
            if (list != null && list.Count > 0)
            {
                try
                {
                    if (MessageBox.Show(Translator.Translate(WrapperArchiveGameMessageBoxKey, ConfigHelper.EndedFolder), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (Activity ended in list)
                        {
                            _gameManager.ArchiveEndedGame(ended.GameType, ended.FileName, ConfigHelper.EndedFolder);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    Trace.Flush();
                    ShowException(ex);
                }
            }
        }

        private void UpdateActivitySent(string fileName, AowGame theGame)
        {
            Activity currentActivity = _activityLog.GetLastActivityByFileName(fileName);
            if (currentActivity != null)
            {
                currentActivity.Outwards = ActivityState.Sent;
                if (currentActivity.GameType.Equals(AowGameType.Unknown))
                {
                    currentActivity.GameType = _gameManager.GetGameByFile(fileName).GameType;
                }
            }
            else
            {                
                if (theGame != null)
                {
                    Activity newGame = new Activity(theGame.GameType, fileName, ActivityState.New, ActivityState.Sent);
                    _activityLog.Activities.Add(newGame);
                }
            }

            RaiseEvent(_activityLogRefresh, this, new EventArgs());
        }

        private void ActivityLogRefresh(object sender, EventArgs e)
        {
            activityListView.Refresh();
            DataManagerHelper.SaveActivityLog(_activityLog);
        }

        private void ActivityLogChanged(object sender, EventArgs e)
        {
            CheckNotifyIconState();
            DataManagerHelper.SaveActivityLog(_activityLog);
        }

        #endregion

        #region Context Menu

        private void CreateContextMenu()
        {
            EventHandler menuItemClickEvent = new EventHandler(menuItem_Click);
            _contextMenu = new ContextMenu();
            _contextMenu.Popup += new EventHandler(ContextMenu_Popup);

            _menuShow = new MenuItem(Translator.Translate(Menu_Show_Tag), menuItemClickEvent);
            _menuShow.Tag = Menu_Show_Tag;
            AddMenuItemToContextMenu(_menuShow);

            AddMenuItemToContextMenu(new MenuItem("-"));

            _menuAccounts = new MenuItem();            
            Image emailImage = imageListIcons.Images[IconState.EmailWaiting.ToString()];

            foreach (AowGame game in _gameManager.Games)
            {
                if (game.IsInstalled)
                {
                    string gameType = game.GameType.ToString();

                    IconMenuItem menuItem = new IconMenuItem(game.GameName, imageListIcons.Images[gameType], emailImage);
                    
                    menuItem.Name = gameType;
                    menuItem.Tag = menuItem.Name;
                    menuItem.Click += menuItemClickEvent;

                    AddMenuItemToContextMenu(menuItem);
                }
            }

            AddMenuItemToContextMenu(new MenuItem("-"));

            _menuAccounts = new MenuItem(Translator.Translate(Menu_Accounts_Tag));
            CreateAccountMenu(_menuAccounts, _wrapperConfig.AccountsList);
            AddMenuItemToContextMenu(_menuAccounts);

            _menuPoll = new MenuItem(Translator.Translate(Menu_Poll_Tag), menuItemClickEvent);
            _menuPoll.Tag = Menu_Poll_Tag;
            _menuPoll.Enabled = false;
            AddMenuItemToContextMenu(_menuPoll);

            _menuExit = new MenuItem(Translator.Translate(Menu_Exit_Tag), menuItemClickEvent);
            _menuExit.Tag = Menu_Exit_Tag;
            AddMenuItemToContextMenu(_menuExit);
          
            notifyIcon.ContextMenu = _contextMenu;
        }

        private void AddMenuItemToContextMenu(MenuItem theItem)
        {
            _contextMenu.MenuItems.Add(theItem);
            theItem.Index = _contextMenu.MenuItems.Count - 1;
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            switch (((MenuItem)sender).Tag.ToString())
            {
                case "Aow1":
                case "Aow2":
                case "AowSm":
                    AowGame theGame = _gameManager.Games.Find(game => game.GameType.ToString().Equals(((MenuItem)sender).Tag.ToString()));
                    StartGame(theGame);
                    break;
                case Menu_Show_Tag:
                    Maximize();
                    break;
                case Menu_Poll_Tag:
                    if (_poller != null)
                    {
                        _poller.PollNow();
                    }
                    break;
                case Menu_Exit_Tag:
                    RaiseEvent(_shutDownEvent, sender, new EventArgs());
                    break;
            }
        }

        private void CreateAccountMenu(MenuItem parent, AccountConfigValuesList accountsList)
        {
            if (parent != null)
            {
                foreach (MenuItem sub in parent.MenuItems)
                {
                    if (sub != null)
                    {
                        sub.Dispose();
                    }
                }

                parent.MenuItems.Clear();

                foreach (AccountConfigValues account in accountsList.Accounts)
                {
                    MenuItem sub = new MenuItem();
                    sub.Text = account.Name;
                    if (account.Equals(accountsList.ActiveAccount))
                    {
                        sub.Checked = true;
                    }
                    sub.Click += new EventHandler(AccountMenu_Click);
                    parent.MenuItems.Add(sub);
                }
            }
        }

        private void Rebuild_Account_Menu(object sender, EventArgs e)
        {
            CreateAccountMenu(_menuAccounts, _wrapperConfig.AccountsList);
        }

        private void AccountMenu_Click(object sender, EventArgs e)
        {
            MenuItem theMenu = (MenuItem)sender;
            AccountConfigValues theAccount = _wrapperConfig.AccountsList.GetAccountByName(theMenu.Text);
            if (theAccount != null)
            {
                ActivateAccount(theAccount);
            }
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            _menuPoll.Enabled = (_poller != null);

            foreach (AowGame game in _gameManager.Games)
            {
                if (game.IsInstalled)
                {
                    string gameType = game.GameType.ToString();
                    if (_contextMenu.MenuItems.ContainsKey(gameType))
                    {
                        IconMenuItem menuItem = (IconMenuItem)_contextMenu.MenuItems[gameType];

                        int unknownGameTypeActivities = _activityLog.GetUnknownGameTypeActivitiesCount();
                        int unSentActivities = _activityLog.GetUnSentActivitiesCountByGameType(game.GameType);

                        if (unknownGameTypeActivities > 0 &&
                            (game.GameType.Equals(AowGameType.Aow2) || game.GameType.Equals(AowGameType.AowSm)))
                        {
                            menuItem.ShowEndImage = true;
                            menuItem.EndImage = imageListIcons.Images[IconState.CheckEmail.ToString()];
                        }
                        else if (unSentActivities > 0)
                        {
                            menuItem.ShowEndImage = true;
                            menuItem.EndImage = imageListIcons.Images[IconState.EmailWaiting.ToString()];
                        }
                        else
                        {
                            menuItem.ShowEndImage = false;
                        }
                    }
                }
            }

            foreach (MenuItem sub in _menuAccounts.MenuItems)
            {
                sub.Checked = sub.Text.Equals(_wrapperConfig.AccountsList.ActiveAccountName, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion
    }
}
