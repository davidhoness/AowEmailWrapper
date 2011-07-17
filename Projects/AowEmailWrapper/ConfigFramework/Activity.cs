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
        private ActivityState _inwards;
        private ActivityState _outwards;
        private string _dateTicks;

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

        [XmlAttribute("in")]
        public ActivityState Inwards
        {
            get { return _inwards; }
            set { _inwards = value; }
        }

        [XmlAttribute("out")]
        public ActivityState Outwards
        {
            get { return _outwards; }
            set { _outwards = value; }
        }

        [XmlAttribute("ticks")]
        public string DateTicks
        {
            get { return _dateTicks; }
            set { _dateTicks = value; }
        }

        public Activity()
        { }

        public Activity(AowGameType type, string fileName, ActivityState inwards)
            : this(type, fileName, inwards, ActivityState.None)
        { }

        public Activity(AowGameType type, string fileName, ActivityState inwards, ActivityState outwards)
        {
            _type = type;
            _fileName = fileName;
            _inwards = inwards;
            _outwards = outwards;

            _dateTicks = DateTime.Now.Ticks.ToString();
        }
    }
}
