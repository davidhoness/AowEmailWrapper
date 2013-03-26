using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mozilla.Autoconfig
{
    internal class MxGuessHandler
    {
        private const string PopTemplate1 = "pop.{0}";
        private const string PopTemplate2 = "pop3.{0}";
        private const string PopTemplate3 = "pop.mail.{0}";
        private const string PopTemplate4 = "pop3.mail.{0}";

        private const string ImapTemplate1 = "imap.{0}";
        private const string ImapTemplate2 = "imap.mail.{0}";

        private const string SmtpTemplate1 = "smtp.{0}";
        private const string SmtpTemplate2 = "smtp.mail.{0}";

        private const string MailTemplate = "mail.{0}";

        private const int PopPlain = 110;
        private const int PopSSL = 995;

        private const int ImapPlain = 143;
        private const int ImapSSL = 993;

        private const int SmtpPlain = 25;
        private const int SmtpSSL = 465;
        private const int SmtpTLS = 587;

        private const string DefaultUsernameFormat = "%EMAILADDRESS%";

        public static MechanismResponse GuessConfig(string domain)
        {
            MechanismResponse returnVal = new MechanismResponse();            

            Dictionary<string, int[]> guesses = GetGuesses();
            List<TimeOutSocket> sockets = new List<TimeOutSocket>();

            foreach (string key in guesses.Keys)
            {                
                string domainGuess = string.Format(key, domain);

                foreach (int port in guesses[key])
                {
                    sockets.Add(new TimeOutSocket(domainGuess, port));
                }
            }

            sockets.ForEach(socket => socket.Test(4000));

            System.Threading.Thread.Sleep(4100);            

            List<TimeOutSocket> openSockets = sockets.FindAll(socket => socket.IsSuccess);

            if (openSockets.Count > 1)
            {
                ClientConfig config = BuildClientConfig(openSockets, domain);

                if (config.EmailProvider.IncomingServers.Count > 0 &&
                    config.EmailProvider.OutgoingServers.Count > 0)
                {
                    returnVal.ClientConfig = config;
                    returnVal.ResponseType = MechanismResponseType.Success;
                    returnVal.Origin = MechanismOriginType.Guess;
                }
            }

            sockets.ForEach(socket => socket.Dispose());

            return returnVal;
        }

        private static Dictionary<string, int[]> GetGuesses()
        {
            Dictionary<string, int[]> returnVal = new Dictionary<string, int[]>();

            int[] popSockets = new int[] { PopPlain, PopSSL };
            int[] imapSockets = new int[] { ImapPlain, ImapSSL };
            int[] smtpSockets = new int[] { SmtpPlain, SmtpSSL, SmtpTLS };
            int[] allSockets = new int[] { PopPlain, PopSSL, ImapPlain, ImapSSL, SmtpPlain, SmtpSSL, SmtpTLS };

            returnVal.Add(PopTemplate1, popSockets);
            returnVal.Add(PopTemplate2, popSockets);
            returnVal.Add(PopTemplate3, popSockets);
            returnVal.Add(PopTemplate4, popSockets);
            returnVal.Add(ImapTemplate1, imapSockets);
            returnVal.Add(ImapTemplate2, imapSockets);
            returnVal.Add(SmtpTemplate1, smtpSockets);
            returnVal.Add(SmtpTemplate2, smtpSockets);
            returnVal.Add(MailTemplate, allSockets);
         
            return returnVal;
        }

        private static ClientConfig BuildClientConfig(List<TimeOutSocket> openSockets, string domain)
        {
            EmailProvider provider = new EmailProvider();
            provider.IncomingServers = new List<IncomingServer>();
            provider.OutgoingServers = new List<OutgoingServer>();

            foreach (TimeOutSocket socket in openSockets)
            {
                switch (socket.Port)
                {
                    case PopPlain:
                    case PopSSL:
                        provider.IncomingServers.Add(CreatePop(socket));
                        break;
                    case ImapPlain:
                    case ImapSSL:
                        provider.IncomingServers.Add(CreateImap(socket));
                        break;
                    case SmtpPlain:
                    case SmtpSSL:
                    case SmtpTLS:
                        provider.OutgoingServers.Add(CreateSmtp(socket));
                        break;
                }
            }

            provider.DisplayName = domain;
            provider.DisplayShortName = domain;
            provider.Domains = new List<string>();
            provider.Domains.Add(domain);

            return new ClientConfig() { Version = "1.1", EmailProvider = provider };
        }

        private static IncomingServer CreatePop(TimeOutSocket socket)
        {
            IncomingServer incoming = new IncomingServer();
            incoming.Type = ServerType.POP3;
            incoming.Port = socket.Port;
            incoming.Hostname = socket.Host;
            incoming.Authentication = AuthenticationType.PasswordClearText;
            incoming.UsernameFormat = DefaultUsernameFormat;

            switch (socket.Port)
            {
                case PopPlain:
                    incoming.SocketType = SocketType.Unknown;
                    break;
                case PopSSL:
                    incoming.SocketType = SocketType.SSL;
                    break;
            }

            return incoming;
        }

        private static IncomingServer CreateImap(TimeOutSocket socket)
        {
            IncomingServer incoming = new IncomingServer();
            incoming.Type = ServerType.IMAP;
            incoming.Port = socket.Port;
            incoming.Hostname = socket.Host;
            incoming.Authentication = AuthenticationType.PasswordClearText;
            incoming.UsernameFormat = DefaultUsernameFormat;

            switch (socket.Port)
            {
                case ImapPlain:
                    incoming.SocketType = SocketType.Unknown;
                    break;
                case ImapSSL:
                    incoming.SocketType = SocketType.SSL;
                    break;
            }

            return incoming;
        }

        private static OutgoingServer CreateSmtp(TimeOutSocket socket)
        {
            OutgoingServer outgoing = new OutgoingServer();
            outgoing.Type = ServerType.SMTP;
            outgoing.Port = socket.Port;
            outgoing.Hostname = socket.Host;
            outgoing.Authentication = AuthenticationType.PasswordClearText;
            outgoing.UsernameFormat = DefaultUsernameFormat;

            switch (socket.Port)
            {
                case SmtpSSL:
                    outgoing.SocketType = SocketType.SSL;
                    break;
                case SmtpPlain:
                case SmtpTLS:
                    outgoing.SocketType = SocketType.Unknown;
                    break;
            }

            return outgoing;
        }
    }
}
