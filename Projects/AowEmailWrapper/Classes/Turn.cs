using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.Classes
{
    public class Turn
    {
        private const char SPLIT_CHAR = ';';
        private const string IsoTimeFormat = "s";
        private const string TOSTRING_TEMPLATE = "{0}; {1}; {2}; {3}";

        private string _turnNumber;
        private string _utcTimeString;
        private string _email;
        private string _timeTakenString;

        public Turn(string turnNumber, string email, string previousTurnUtcString)
        {
            DateTime stampUtc = DateTime.Now.ToUniversalTime();

            _utcTimeString = stampUtc.ToString(IsoTimeFormat);

            _turnNumber = turnNumber;
            _email = email;

            if (!string.IsNullOrEmpty(previousTurnUtcString))
            {
                DateTime previousTurnUtc;
                if (DateTime.TryParse(previousTurnUtcString, out previousTurnUtc))
                {
                    TimeSpan timeTaken = stampUtc - previousTurnUtc;
                    _timeTakenString = TurnLogger.TimeSpanToString(timeTaken);
                }
            }
            else
            {
                _timeTakenString = string.Empty;
            }
        }

        public Turn(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] split = input.Split(SPLIT_CHAR);
                if (split.Length == 4)
                {
                    _turnNumber = split[0].Trim();
                    _email = split[1].Trim();
                    _utcTimeString = split[2].Trim();                    
                    _timeTakenString = split[3].Trim();
                }
            }
        }

        public string TurnNumber
        {
            get { return _turnNumber; }
            set { _turnNumber = value; }
        }

        public string UtcTimeString
        {
            get { return _utcTimeString; }
            set { _utcTimeString = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string TimeTaken
        {
            get { return _timeTakenString; }
            set { _timeTakenString = value; }
        }

        public override string ToString()
        {
            return string.Format(TOSTRING_TEMPLATE, _turnNumber, _email, _utcTimeString, _timeTakenString);
        }
    }
}
