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
using Microsoft.SqlServer.MessageBox;
using EricDaugherty.CSES.Net;
using EricDaugherty.CSES.SmtpServer;
using AowEmailWrapper.ASG;
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
        private const string WrapperResendToKey = "msgWrapperResendTo";

        private const string MainFormTitleTemplate = "{0} - {1}";

        private const string Menu_Show_Tag = "menuItemShow";
        private const string Menu_Accounts_Tag = "menuItemAccounts";
        private const string Menu_Poll_Tag = "menuItemPollNow";
        private const string Menu_Exit_Tag = "menuItemExit";

        private const string GameSmtpServerTemplate = "127.0.0.1:{0}";
        private const string WrapperAutostartTemplate = "\"{0}\" {1}";
        private const string WrapperRestartTemplate = "{0} {1}";

        private const string ButtonKeyOK = "buttonOK";
        private const string ButtonKeyCancel = "buttonCancel";
        private const string ButtonKeyResend = "buttonResend";

        #endregion

        #region Private Members

        private Icon _baseIcon = null;
        private SimpleServer _theServer;
        private BasePoller _poller;
        private static AowGameManager _gameManager;
        private static SmtpSender _smtpSender;

        private Config _wrapperConfig;
        private ActivityList _activityLog;

        private StartedTaskWatcher _aow1GameWatcher;
        private StartedTaskWatcher _aow2GameWatcher;
        private StartedTaskWatcher _aowSmGameWatcher;

        private EventHandler _shutDownEvent;
        private EventHandler _maximizeEvent;
        private EventHandler _activityLogRefresh;
        private bool _closeCancel = true;
        private bool _isNewConfig = false;
        private bool _configNeedsSave = false;
        private bool _configChangeTracking = false;
        private int _showingExceptionCount = 0;

        private ContextMenu _contextMenu;

        private MenuItem _menuAccounts;
        private MenuItem _menuShow;
        private MenuItem _menuPoll;
        private MenuItem _menuExit;

        #endregion

        #region Properties
        
        //To hide from alt-tab when minimized
        protected override CreateParams CreateParams
        {
            get
            {
                //Turn off WS_EX_CONTROLPARENT style bit
                CreateParams cp = base.CreateParams;
                int bit = ExtendedWindowStyles.WS_EX_CONTROLPARENT;
                int test = cp.ExStyle & bit;
                if (test.Equals(bit))
                {
                    cp.ExStyle ^= bit;
                }
                return cp;
            }
        }

        protected bool ConfigNeedsSave
        {
            get { return _configNeedsSave; }
            set
            {
                _configNeedsSave = value;
                cmdSave.Enabled = _configNeedsSave;
                cmdSave.BackColor = cmdSave.Enabled ? Color.IndianRed : this.BackColor;
            }
        }

        protected bool ConfigChangeTracking
        {
            get { return _configChangeTracking; }
            set
            {
                _configChangeTracking = value;
                EventHandler configNeedsSave = new EventHandler(OnConfigNeedsSave);

                if (_configChangeTracking)
                {
                    accountsConfig.Config_Changed += configNeedsSave;
                    preferencesConfig.Config_Changed += configNeedsSave;
                }
                else
                {
                    accountsConfig.Config_Changed -= configNeedsSave;
                    preferencesConfig.Config_Changed -= configNeedsSave;
                }
            }
        }

        //The Wrapper Exe is left in memory if we shut down while an ExceptionMessageBox is up
        protected bool OkayToShutDown
        {
            get { return _showingExceptionCount.Equals(0); }
        }

        #endregion

        public Main()
        {
            _wrapperConfig = DataManagerHelper.LoadConfig(out _isNewConfig);

            LoadTranslations();

            InitializeComponent();

            Translator.TranslateForm(this);

            _gameManager = new AowGameManager();

            if (!_gameManager.CheckWriteAccess())
            {
                MessageBox.Show(Translator.Translate(WrapperWriteAccessMessageBoxKey, _gameManager.GetEmailInFolderList()), Translator.Translate(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            lblVersion.Text = string.Format(lblVersion.Text, ConfigHelper.BuildVersion);

            _baseIcon = notifyIcon.Icon;

            SetIcon(IconState.Normal);

            accountsConfig.AccountsTemplates = DataManagerHelper.LoadAccountTemplates();

            LoadActivityLog();

            LoadConfig();

            CreateContextMenu();

            BindCustomEvents();

            CheckNotifyIconState(true);

            this.FormClosing += new FormClosingEventHandler(Main_FormClosing);

            Splash.CloseForm();
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

                if (_aow1GameWatcher != null)
                {
                    _aow1GameWatcher.Stop();
                }
                if (_aow2GameWatcher != null)
                {
                    _aow2GameWatcher.Stop();
                }
                if (_aowSmGameWatcher != null)
                {
                    _aowSmGameWatcher.Stop();
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
            SaveConfig(true);
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

        private void LoadConfig()
        {
            try
            {
                if (!_isNewConfig)
                {
                    this.WindowState = FormWindowState.Minimized;
                    Minimized();
                }

                if (_wrapperConfig != null)
                {
                    if (_wrapperConfig.PreferencesConfig != null)
                    {
                        preferencesConfig.Config = _wrapperConfig.PreferencesConfig;
                    }

                    if (_wrapperConfig.AccountsList != null)
                    {
                        accountsConfig.Config = _wrapperConfig.AccountsList;
                        ActivateAccount(_wrapperConfig.AccountsList.StartUpAccount); //This will turn on Config Change Tracking
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_isNewConfig &&
                _wrapperConfig != null &&
                _wrapperConfig.AccountsList != null &&
                _wrapperConfig.AccountsList.Accounts != null &&
                _wrapperConfig.AccountsList.Accounts.Count.Equals(0))
            {
                _isNewConfig = false;
                accountsConfig.Add();
            }
        }

        private void SaveConfig(bool reActivate)
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
                        panelLocalMessageStore.Visible = theAccount.PollingConfig.EmailType.Equals(EmailType.POP3);
                    }
                }

                //Preferences
                PreferencesConfigValues preferencesConfigValues = preferencesConfig.Config;
                if (preferencesConfigValues != null)
                {
                    string keyName = Translator.Translate(this.Name);
                    if (preferencesConfigValues.Autostart)
                    {
                        string keyValue = string.Format(WrapperAutostartTemplate, Application.ExecutablePath, ConfigHelper.AUTOSTART_CMD_PARAM);
                        RegistryHelper.SetValue(Registry.CurrentUser, WINDOWS_REG_STARTUP_LOCATION, keyName, keyValue);
                    }
                    else
                    {
                        RegistryHelper.DeleteValue(Registry.CurrentUser, WINDOWS_REG_STARTUP_LOCATION, keyName);
                    }
                }

                _wrapperConfig.PreferencesConfig = preferencesConfigValues;

                DataManagerHelper.SaveConfig(_wrapperConfig);
                ConfigNeedsSave = false;

                bool activateSuccess = false;

                if (reActivate)
                {
                    activateSuccess = ActivateAccount(_wrapperConfig.AccountsList.ActiveAccount);
                }

                if (preferencesConfigValues != null &&
                    !preferencesConfigValues.LanguageCode.Equals(Translator.CurrentLanguageCode) &&
                    activateSuccess)
                {
                    if (MessageBox.Show(Translator.Translate(WrapperRestartRequiredKey), Translator.Translate(this.Name), MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                    {
                        RestartWrapper();
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

        #endregion

        #region Custom Events

        private void RestartWrapper()
        {
            string thisProcessId = Process.GetCurrentProcess().Id.ToString();
            _closeCancel = false;
            this.Close();
            Process.Start(Application.ExecutablePath, string.Format(WrapperRestartTemplate, ConfigHelper.RESTART_CMD_PARAM, thisProcessId));
        }

        private void BindCustomEvents()
        {
            _shutDownEvent += new EventHandler(ShutDown);
            _maximizeEvent += new EventHandler(Maximize);
            _activityLogRefresh += new EventHandler(ActivityLogRefresh);
            _gameManager.OnGameSaved += new AowGameSavedEventHandler(OnAowGameSaved);

            accountsConfig.Account_Activated += new AccountActivatedEventHandler(Account_Activated);
            accountsConfig.Config_Changed += new EventHandler(Rebuild_Account_Menu);

            activityListView.OnDoubleClick += new ActivityListViewEventHandler(ActivityListViewDoubleClicked);
            activityListView.OnListChanged += new EventHandler(ActivityLogChanged);
            activityListView.OnDeleteClick += new ActivityListViewEventHandler(ActivityListViewGamesDeleted);
            activityListView.OnMarkAsEnded += new ActivityListViewEventHandler(ActivityListViewGamesMarkedAsEnded);
            activityListView.OnResendClick += new ActivityListViewEventHandler(ActivityListViewResend);
        }

        private void ShutDown(object sender, EventArgs e)
        {
            if (OkayToShutDown)
            {
                _closeCancel = false;
                this.Close();
            }
        }

        private void StartedGameWatchCompleted(object sender, AowGameType gameType)
        {
            switch (gameType)
            {
                case AowGameType.Aow1:
                    _aow1GameWatcher = null;
                    break;
                case AowGameType.Aow2:
                    _aow2GameWatcher = null;
                    break;
                case AowGameType.AowSm:
                    _aowSmGameWatcher = null;
                    break;
            }
        }

        private void OnConfigNeedsSave(object sender, EventArgs e)
        {
            ConfigNeedsSave = true;
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
                        notifyIcon.ShowBalloonTip(5000, Translator.Translate(this.Name), Translator.Translate(WrapperGamesWaitingKey, fileCount.ToString()), ToolTipIcon.Info);
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
            string defaultText = Translator.Translate(this.Name);
            notifyIcon.Text = !string.IsNullOrEmpty(status) ? string.Format("{0}: {1}", defaultText, status) : defaultText;
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
            this.SuspendLayout();

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Visible = false;
            }

            this.ResumeLayout();
        }

        private void Maximize(object sender, EventArgs e)
        {
            Maximize();
        }

        private void Maximize()
        {
            this.SuspendLayout();

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
                this.Visible = true;
            }
            
            this.Activate();
            this.ResumeLayout();
        }

        private void ShowException(Exception ex)
        {
            this.Invoke(new EventHandler(this.Maximize));

            ExceptionMessageBox box = new ExceptionMessageBox(ex);

            box.Caption = Translator.Translate(this.Name);
            box.SetButtonText(Translator.Translate(ButtonKeyOK));
            box.DefaultButton = ExceptionMessageBoxDefaultButton.Button1;
            box.Symbol = ExceptionMessageBoxSymbol.Error;
            box.Buttons = ExceptionMessageBoxButtons.Custom;

            ShowExceptionMessageBox(ref box);
            box = null;
        }

        private void ShowExceptionMessageBox(ref ExceptionMessageBox box)
        {
            _showingExceptionCount++;
            box.Show(this);
            _showingExceptionCount--;
        }

        private void StartGame(AowGame theGame)
        {
            switch (theGame.GameType)
            {
                case AowGameType.Aow1:
                    if (_aow1GameWatcher == null)
                    {
                        _aow1GameWatcher = new StartedTaskWatcher(theGame, new StartedTaskCompleteEventHandler(StartedGameWatchCompleted));
                        _aow1GameWatcher.Start();
                    }
                    break;
                case AowGameType.Aow2:
                    if (_aow2GameWatcher == null)
                    {
                        _aow2GameWatcher = new StartedTaskWatcher(theGame, new StartedTaskCompleteEventHandler(StartedGameWatchCompleted));
                        _aow2GameWatcher.Start();
                    }
                    break;
                case AowGameType.AowSm:
                    if (_aowSmGameWatcher == null)
                    {
                        _aowSmGameWatcher = new StartedTaskWatcher(theGame, new StartedTaskCompleteEventHandler(StartedGameWatchCompleted));
                        _aowSmGameWatcher.Start();
                    }
                    break;
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
                        else
                        {
                            //The connection should be good
                            RetrySendFailures();
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

                    if (form.ShowDialog(this).Equals(DialogResult.OK))
                    {
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
            }
        }

        #endregion

        #region Outgoing Email

        private void StartServer(int thePort)
        {
            try
            {
                _theServer = new SimpleServer(thePort, ProcessSMTPRequest);
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
                    ResendHelper.Save(theEmail);
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
            if (_closeCancel)
            {
                this.Activate();

                if (theResponse.IsSuccess)
                {
                    SmtpSendSuccess(theResponse);
                }
                else
                {
                    SmtpSendFailure(theResponse);
                }

                GC.Collect();

                CheckNotifyIconState();
            }
        }

        private void SmtpSendSuccess(SmtpSendResponse theResponse)
        {
            if (theResponse.IsSuccess)
            {
                if (_wrapperConfig.AccountsList.ActiveAccount != null &&
                    _wrapperConfig.AccountsList.ActiveAccount.SmtpConfig != null &&
                    !_wrapperConfig.AccountsList.ActiveAccount.SmtpConfig.Verified)
                {
                    _wrapperConfig.AccountsList.ActiveAccount.SmtpConfig.Verified = true;
                    DataManagerHelper.SaveConfig(_wrapperConfig);
                }

                notifyIcon.ShowBalloonTip(15000, theResponse.GameEmail.Subject, Translator.Translate(WrapperEmailSentSuccessKey, theResponse.GameEmail.To[0].Address), ToolTipIcon.Info);
                if (_wrapperConfig.PreferencesConfig != null && _wrapperConfig.PreferencesConfig.PlaySoundOnSend)
                {
                    PlaySound(ConfigHelper.SentSound);
                }

                if (theResponse.GameEmail.Attachments.Count > 0)
                {
                    MimeData theAttachment = theResponse.GameEmail.Attachments[0];
                    Activity theActivity = UpdateActivitySent(theAttachment);

                    if (_wrapperConfig.PreferencesConfig != null && _wrapperConfig.PreferencesConfig.CopyToEmailOut)
                    {
                        try
                        {
                            _gameManager.CopyToEmailOut(theAttachment, _gameManager.GetGameByType(theActivity.GameType));
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
            }
        }

        private void SmtpSendFailure(SmtpSendResponse theResponse)
        {
            if (!theResponse.IsSuccess)
            {
                MimeData theAttachment = theResponse.GameEmail.Attachments[0];
                UpdateActivitySendError(theAttachment);

                if (_wrapperConfig.AccountsList.ActiveAccount != null &&
                    _wrapperConfig.AccountsList.ActiveAccount.SmtpConfig != null &&
                    _wrapperConfig.AccountsList.ActiveAccount.SmtpConfig.Verified)
                {
                    //Just show Baloon error
                    notifyIcon.ShowBalloonTip(15000, theResponse.GameEmail.Subject, theResponse.Exception.Message, ToolTipIcon.Error);
                    theResponse.Dispose();
                }
                else
                {
                    //Show full Message Box error
                    ShowSmtpExceptionMessageBox(theResponse);
                }
            }
        }

        private void ShowSmtpExceptionMessageBox(SmtpSendResponse theResponse)
        {
            RaiseEvent(_maximizeEvent, this, new EventArgs());

            string errorMessage = Translator.Translate(WrapperEmailSentFailedKey, theResponse.GameEmail.Subject, theResponse.GameEmail.To[0].Address);
            ApplicationException showException = new ApplicationException(errorMessage, theResponse.Exception);

            ExceptionMessageBox box = new ExceptionMessageBox(showException);
            box.Caption = Translator.Translate(this.Name);

            box.SetButtonText(Translator.Translate(ButtonKeyResend), Translator.Translate(ButtonKeyCancel));
            box.DefaultButton = ExceptionMessageBoxDefaultButton.Button1;

            box.Symbol = ExceptionMessageBoxSymbol.Question;
            box.Buttons = ExceptionMessageBoxButtons.Custom;

            ShowExceptionMessageBox(ref box);

            if (box.CustomDialogResult.Equals(ExceptionMessageBoxDialogResult.Button1) && _smtpSender != null)
            {
                _smtpSender.SendMessage(theResponse.GameEmail);
            }
            else
            {
                theResponse.Dispose();
            }

            box = null;
        }

        private void RetrySendFailures()
        {
            List<Activity> toRetry = _activityLog.GetRetryActivities();
            if (toRetry.Count > 0 && _smtpSender != null)
            {
                foreach (Activity activity in toRetry)
                {
                    if (ResendHelper.CanResend(activity.FileName))
                    {
                        _smtpSender.SendMessage(ResendHelper.Load(activity.FileName));
                    }
                }
            }
        }

        #endregion

        #region Accounts

        private void Account_Activated(object sender, AccountConfigValues theAccount, bool dirty)
        {
            ActivateAccount(theAccount);

            if (dirty)
            {
                //We have unsaved data
                SaveConfig(false);
            }
        }

        private bool ActivateAccount(AccountConfigValues account)
        {
            bool success = false;

            //Don't switch accounts if a send is in progress
            if ((_smtpSender != null && _smtpSender.IsSending))
            {
                success = false;
                MessageBox.Show(Translator.Translate(WrapperCannotActivateAccountMessageBoxKey, account.Name), Translator.Translate(this.Name), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (account != null &&
                !string.IsNullOrEmpty(account.Name))
            {
                ConfigChangeTracking = false;

                _wrapperConfig.AccountsList.ActiveAccountName = account.Name;

                if (account.PollingConfig != null)
                {
                    panelLocalMessageStore.Visible = account.PollingConfig.EmailType.Equals(EmailType.POP3);
                }

                //Belt and braces for Game to Wrapper communication
                int listenPort = PreferencesConfigValues.GameWrapperDataPortDefault;

                if (_wrapperConfig.PreferencesConfig != null &&
                    _wrapperConfig.PreferencesConfig.GameWrapperDataPort > 0)
                {
                    listenPort = _wrapperConfig.PreferencesConfig.GameWrapperDataPort;
                }

                if (_theServer == null)
                {
                    //Start Wrapper SMTP Server 
                    StartServer(listenPort);
                }
                else if (_theServer != null &&
                    _theServer.IsRunning &&
                    !_theServer.Port.Equals(listenPort))
                {
                    //They changed the port, the SMTP Server needs a restart
                    StopServer();
                    StartServer(listenPort);
                }

                if (account.SmtpConfig != null)
                {
                    _gameManager.SetEmailConfigAll(AppDataHelper.CheckEmail.FullName, account.SmtpConfig.EmailAddress, string.Format(GameSmtpServerTemplate, _theServer.Port));
                }

                StopPolling();

                CreateSmtpSender(account.SmtpConfig, account.PollingConfig);

                if (account.PollingConfig.UsePolling &&
                    !string.IsNullOrEmpty(account.PollingConfig.Username) &&
                    !string.IsNullOrEmpty(account.PollingConfig.Password) &&
                    !string.IsNullOrEmpty(account.PollingConfig.Server))
                {
                    StartPolling(account.PollingConfig, _wrapperConfig.PreferencesConfig);
                }

                success = true;

                if (account.SmtpConfig != null &&
                    !string.IsNullOrEmpty(account.SmtpConfig.EmailAddress))
                {
                    this.Text = string.Format(MainFormTitleTemplate, Translator.Translate(this.Name), account.SmtpConfig.EmailAddress);
                }
                else
                {
                    this.Text = Translator.Translate(this.Name);
                }

                accountsConfig.Refresh();

                ConfigChangeTracking = true;
            }

            return success;
        }

        #endregion

        #region Activity Log

        //Raised by the AowGameManager class
        private void OnAowGameSaved(object sender, AowGameSavedEventArgs e)
        {
            ResendHelper.Delete(e.FileName); //Avoids the user resending the previous turn by mistake

            Activity lastActivity = _activityLog.GetLastActivityByFileName(e.FileName);

            if (lastActivity != null)
            {
                _activityLog.Activities.Remove(lastActivity);
            }

            Activity newActivity = new Activity(e);
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

        private void ActivityListViewGamesDeleted(object sender, List<Activity> list)
        {
            if (list != null && list.Count > 0)
            {
                try
                {
                    list.ForEach(deletedActivity =>
                    {
                        TurnLogger.DeleteLog(deletedActivity.FileName);
                        ResendHelper.Delete(deletedActivity.FileName);
                        _gameManager.DeleteGame(deletedActivity.GameType, deletedActivity.FileName);
                    });
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    Trace.Flush();
                    ShowException(ex);
                }
            }
        }

        private void ActivityListViewGamesMarkedAsEnded(object sender, List<Activity> list)
        {
            if (list != null && list.Count > 0)
            {
                try
                {
                    if (MessageBox.Show(Translator.Translate(WrapperArchiveGameMessageBoxKey, ConfigHelper.EndedFolder), Translator.Translate(this.Name), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        list.ForEach(endedActivity => _gameManager.ArchiveEndedGame(endedActivity.GameType, endedActivity.FileName, ConfigHelper.EndedFolder));
                    }

                    list.ForEach(endedActivity =>
                    {
                        TurnLogger.DeleteLog(endedActivity.FileName);
                        ResendHelper.Delete(endedActivity.FileName);
                    });
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    Trace.Flush();
                    ShowException(ex);
                }
            }
        }

        private void ActivityListViewResend(object sender, List<Activity> list)
        {
            if (list != null && list.Count > 0 && _smtpSender!=null)
            {
                try
                {
                    foreach (Activity activity in list)
                    {
                        IMail theEmail = ResendHelper.Load(activity.FileName);
                        if (theEmail != null && theEmail.To.Count > 0)
                        {
                            string newToAddress = theEmail.To[0].Address;

                            Image gameTypeImage = null;
                            string gameType = activity.GameType.ToString();
                            if (imageListIcons.Images.IndexOfKey(gameType) >= 0)
                            {
                                gameTypeImage = imageListIcons.Images[gameType];
                            }

                            if (InputBox.Show(activity.FileName, Translator.Translate(WrapperResendToKey), ref newToAddress, gameTypeImage).Equals(DialogResult.OK))
                            {
                                if (!newToAddress.Equals(theEmail.To[0].Address, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    theEmail.To.Clear();
                                    theEmail.To.Add(new Lesnikowski.Mail.Headers.MailBox(newToAddress));
                                }

                                _smtpSender.SendMessage(theEmail);

                                CheckNotifyIconState();
                                ResendHelper.Save(theEmail);
                            }
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

        private Activity UpdateActivitySent(MimeData theAttachment)
        {
            Activity theActivity = null;
            theActivity = _activityLog.GetLastActivityByFileName(theAttachment.FileName);

            if (theActivity != null)
            {
                theActivity.Status = ActivityState.Sent;
                if (theActivity.GameType.Equals(AowGameType.Unknown))
                {
                    using (ASGFileInfo theASG = new ASGFileInfo(theAttachment))
                    {
                        theActivity.GameType = theASG.GameType;
                    }
                }
            }
            else
            {
                using (ASGFileInfo theASG = new ASGFileInfo(theAttachment))
                {
                    theActivity = new Activity(
                        ActivityState.Sent,
                        theASG.GameType,
                        theASG.FileNameTrue,
                        theASG.MapTitle,
                        theASG.TurnNumber.ToString());
                }

                _activityLog.Activities.Add(theActivity);
            }

            RaiseEvent(_activityLogRefresh, this, new EventArgs());

            return theActivity;
        }

        private void UpdateActivitySendError(MimeData theAttachment)
        {
            Activity theActivity = null;
            theActivity = _activityLog.GetLastActivityByFileName(theAttachment.FileName);

            if (theActivity != null)
            {
                theActivity.Status = ActivityState.Error;
            }
            else
            {
                using (ASGFileInfo theASG = new ASGFileInfo(theAttachment))
                {
                    theActivity = new Activity(
                        ActivityState.Error,
                        theASG.GameType,
                        theASG.FileNameTrue,
                        theASG.MapTitle,
                        theASG.TurnNumber.ToString());
                }

                _activityLog.Activities.Add(theActivity);
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
            _menuPoll.Enabled = (_poller != null &&
                _wrapperConfig != null &&
                _wrapperConfig.AccountsList != null &&
                _wrapperConfig.AccountsList.ActiveAccount != null &&
                _wrapperConfig.AccountsList.ActiveAccount.PollingConfig != null &&
                _wrapperConfig.AccountsList.ActiveAccount.PollingConfig.UsePolling);

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
