using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.ConfigFramework;
using AowEmailWrapper.Localization;
using AowEmailWrapper.Classes;
using Mozilla.Autoconfig;

namespace AowEmailWrapper.Helpers
{
    public class AutoconfigurationHelper
    {
        public static AccountConfigValues MapMechanismResponse(
            MechanismResponse response, 
            string emailAddress, 
            string password, 
            ServerType incomingServerPreference)
        {
            AccountConfigValues mappedAccount;

            try
            {
                mappedAccount = new AccountConfigValues();

                EmailProvider provider = response.ClientConfig.EmailProvider;

                if (response.IsGuess)
                {
                    mappedAccount.IsGuess = true;
                    TestAllEmailServers(provider, 5000); //Excludes servers that fail and determines socket type for Plain/TLS ports
                }

                if (provider.IncomingServers.Count > 0 &&
                    provider.OutgoingServers.Count > 0)
                {
                    mappedAccount.EmailProvider = provider.ID;
                    mappedAccount.Name = provider.DisplayName;
                    mappedAccount.PollingConfig = ChooseIncomingServer(provider, emailAddress, incomingServerPreference);
                    mappedAccount.PollingConfig.PasswordTrue = password;
                    mappedAccount.SmtpConfig = ChooseOutgoingServer(provider, emailAddress);
                    mappedAccount.SmtpConfig.UsePollingCredentials = mappedAccount.PollingConfig.Username == mappedAccount.SmtpConfig.Username;

                    if (!mappedAccount.SmtpConfig.UsePollingCredentials)
                    {
                        mappedAccount.SmtpConfig.PasswordTrue = password;
                    }
                }
                else
                {
                    mappedAccount = null;
                }
            }
            catch(Exception ex)
            {
                mappedAccount = null;
            }

            return mappedAccount;
        }

        private static PollingConfigValues ChooseIncomingServer(EmailProvider provider, string emailAddress, ServerType incomingServerPreference)
        {
            PollingConfigValues returnVal;

            IncomingServer chosenImap = GetServerTypeByMaxPort(provider.IncomingServers, ServerType.IMAP);
            IncomingServer chosenPop = GetServerTypeByMaxPort(provider.IncomingServers, ServerType.POP3);

            switch (incomingServerPreference)
            { 
                case ServerType.IMAP:
                    returnVal = MapIncomingServer(chosenImap != null ? chosenImap : chosenPop, emailAddress);
                    break;
                case ServerType.POP3:
                    returnVal = MapIncomingServer(chosenPop != null ? chosenPop : chosenImap, emailAddress);
                    break;
                default:
                    if (chosenImap != null && chosenPop != null)
                    {
                        if (chosenImap.Port < 900 && chosenPop.Port > 900)
                        {
                            returnVal = MapIncomingServer(chosenPop, emailAddress);
                        }
                        else
                        {
                            returnVal = MapIncomingServer(chosenImap, emailAddress);
                        }
                    }
                    else if (chosenImap != null && chosenPop == null)
                    {
                        returnVal = MapIncomingServer(chosenImap, emailAddress);
                    }
                    else if (chosenImap == null && chosenPop != null)
                    {
                        returnVal = MapIncomingServer(chosenPop, emailAddress);
                    }
                    else
                    {
                        returnVal = null;
                    }
                    break;
            }

            return returnVal;
        }

        private static IncomingServer GetServerTypeByMaxPort(List<IncomingServer> servers, ServerType type)
        {
            IncomingServer returnVal = null;

            List<IncomingServer> serversByType = servers.FindAll(srv => srv.Type == type);
            if (serversByType.Count > 0)
            {
                int maxPort = serversByType.Max(srv => srv.Port);
                returnVal = serversByType.Find(srv => srv.Port == maxPort);
            }

            return returnVal;
        }

        private static PollingConfigValues MapIncomingServer(IncomingServer input, string emailAddress)
        {
            PollingConfigValues returnVal = new PollingConfigValues();

            returnVal.Username = input.GetUsernameFormatted(emailAddress);

            returnVal.UsePolling = true;
            returnVal.PollInterval = 10;

            switch (input.Type)
            {
                case ServerType.IMAP:
                    returnVal.EmailType = EmailType.IMAP;
                    break;
                case ServerType.POP3:
                    returnVal.EmailType = EmailType.POP3;
                    if (input.Pop3 != null && input.Pop3.CheckInterval != null)
                    {
                        returnVal.PollInterval = input.Pop3.CheckInterval.Minutes;
                    }
                    break;
            }

            returnVal.Server = input.Hostname;
            returnVal.Port = input.Port;

            switch (input.SocketType)
            {
                case SocketType.Plain:
                    returnVal.SSLType = SSLType.None;
                    break;
                case SocketType.SSL:
                    returnVal.SSLType = SSLType.SSL;
                    break;
                case SocketType.STARTTLS:
                    returnVal.SSLType = SSLType.TLS;
                    break;
            }

            return returnVal;
        }

        private static SmtpConfigValues ChooseOutgoingServer(EmailProvider provider, string emailAddress)
        {
            SmtpConfigValues returnVal = null;

            int maxPort = provider.OutgoingServers.Max(srv => srv.Port);
            OutgoingServer chosenServer = provider.OutgoingServers.Find(srv => srv.Port == maxPort);

            if (chosenServer != null)
            {
                returnVal = MapOutgoingServer(chosenServer, emailAddress);
            }

            return returnVal;
        }

        private static SmtpConfigValues MapOutgoingServer(OutgoingServer input, string emailAddress)
        {
            SmtpConfigValues returnVal = new SmtpConfigValues();

            returnVal.Username = input.GetUsernameFormatted(emailAddress);

            returnVal.Authentication = input.Authentication != AuthenticationType.None;
            returnVal.BCCMyself = false;
            returnVal.EmailAddress = emailAddress;
            returnVal.SmtpServer = input.Hostname;
            returnVal.Port = input.Port;

            switch (input.SocketType)
            {
                case SocketType.Plain:
                    returnVal.SmtpSSLType = SSLType.None;
                    break;
                case SocketType.SSL:
                    returnVal.SmtpSSLType = SSLType.SSL;
                    break;
                case SocketType.STARTTLS:
                    returnVal.SmtpSSLType = SSLType.TLS;
                    break;
            }

            return returnVal;
        }

        private static void TestAllEmailServers(EmailProvider provider, int timeOutMs)
        {
            List<TimeOutServerTest> incomingServerTests = new List<TimeOutServerTest>();
            List<TimeOutServerTest> outgoingServerTests = new List<TimeOutServerTest>();

            provider.IncomingServers.ForEach(server => incomingServerTests.Add(new TimeOutServerTest(server)));
            provider.OutgoingServers.ForEach(server => outgoingServerTests.Add(new TimeOutServerTest(server)));

            incomingServerTests.ForEach(test => test.Test(timeOutMs));
            outgoingServerTests.ForEach(test => test.Test(timeOutMs));

            System.Threading.Thread.Sleep(timeOutMs + 100); //Wait for tests to finish

            List<IncomingServer> incomingSuccess = new List<IncomingServer>();
            List<OutgoingServer> outgoingSuccess = new List<OutgoingServer>();

            incomingServerTests.FindAll(test => test.IsSuccess).ForEach(test => incomingSuccess.Add(test.IncomingServer));
            outgoingServerTests.FindAll(test => test.IsSuccess).ForEach(test => outgoingSuccess.Add(test.OutgoingServer));

            provider.IncomingServers = incomingSuccess;
            provider.OutgoingServers = outgoingSuccess;

            incomingServerTests.ForEach(test => test.Dispose());
            outgoingServerTests.ForEach(test => test.Dispose());
        }
    }
}
