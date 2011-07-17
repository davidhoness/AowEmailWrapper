using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.Games
{
    public class AowGameSavedEventArgs
    {
        private AowGameType _theGameType;
        private string _fileName;

        public AowGameType GameType
        {
            get { return _theGameType; }
            set { _theGameType = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public AowGameSavedEventArgs(AowGameType type, string fileName)
        {
            _theGameType = type;
            _fileName = fileName;
        }
    }
}
