using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AowEmailWrapper.Games;

namespace AowEmailWrapper.ConfigFramework
{
    public enum ActivityState
    {
        [XmlEnum(Name = "None")]
        None = 1,
        [XmlEnum(Name = "New")]
        New,
        [XmlEnum(Name = "Received")]
        Received,
        [XmlEnum(Name = "Sent")]
        Sent,
        [XmlEnum(Name = "Ended")]
        Ended
    }

    [XmlRoot("activity")]
    public class Activity
    {
        private AowGameType _type;
        private string _fileName;
        private ActivityState _status;
        private string _dateTicks;
        private string _mapTitle = string.Empty;
        private string _turnNo = string.Empty;

        [XmlAttribute("game_type")]
        public AowGameType GameType
        {
            get { return _type; }
            set { _type = value; }
        }

        [XmlAttribute("file_name")]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [XmlAttribute("map_title")]
        public string MapTitle
        {
            get { return _mapTitle; }
            set { _mapTitle = value; }
        }

        [XmlAttribute("turn")]
        public string TurnNumber
        {
            get { return _turnNo; }
            set { _turnNo = value; }
        }

        //Depricated
        [XmlAttribute("in")]
        public string Inwards
        {
            get { return null; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _status = Helpers.ConfigHelper.ParseEnumString<ActivityState>(value);
                }
            }
        }

        //Depricated
        [XmlAttribute("out")]
        public string Outwards
        {
            get { return null; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ActivityState theState = Helpers.ConfigHelper.ParseEnumString<ActivityState>(value);
                    if (!theState.Equals(ActivityState.None))
                    {
                        _status = theState;
                    }
                }
            }
        }

        [XmlAttribute("status")]
        public ActivityState Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [XmlAttribute("ticks")]
        public string DateTicks
        {
            get { return _dateTicks; }
            set { _dateTicks = value; }
        }

        public Activity()
        { }

        public Activity(AowGameSavedEventArgs e)
            : this(ActivityState.Received, e.GameType, e.FileName, e.MapTitle, e.TurnNumber)
        { }

        public Activity(ActivityState status, AowGameType type, string fileName, string mapTitle, string turnNo)
        {
            _status = status;
            _type = type;
            _fileName = fileName;
            _mapTitle = mapTitle;
            _turnNo = turnNo;
            _dateTicks = DateTime.Now.Ticks.ToString();
        }
    }
}
