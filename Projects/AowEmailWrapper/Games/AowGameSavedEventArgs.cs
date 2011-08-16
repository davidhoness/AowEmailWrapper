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
        private string _gameTitle;
        private string _mapTitle;
        private string _turnNo;

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

        public string GameTitle
        {
            get { return _gameTitle; }
            set { _gameTitle = value; }
        }

        public string MapTitle
        {
            get { return _mapTitle; }
            set { _mapTitle = value; }
        }

        public string TurnNumber
        {
            get { return _turnNo; }
            set { _turnNo = value; }
        }

        public AowGameSavedEventArgs(AowGameType type, string fileName)
            : this(type, fileName, null, null, null)
        { }

        public AowGameSavedEventArgs(AowGameType type, string fileName, string gameTitle, string mapTitle, string turnNo)
        {
            _theGameType = type;
            _fileName = fileName;
            _gameTitle = gameTitle;
            _mapTitle = mapTitle;
            _turnNo = turnNo;
        }
    }
}
