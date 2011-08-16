using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AowEmailWrapper.ASG;
using AowEmailWrapper.Games;

namespace AowEmailWrapper.ConfigFramework
{
    [XmlRoot("activities")]
    public class ActivityList
    {
        private List<Activity> _activities;
        private const int MaxActivities = 100;

        [XmlElement("activity")]
        public List<Activity> Activities
        {
            get 
            {
                //Only keep 100 Activities, let them drop off the bottom after that
                if (_activities.Count > MaxActivities)
                {
                    _activities.Sort((a, b) => long.Parse(b.DateTicks).CompareTo(long.Parse(a.DateTicks)));
                    _activities.RemoveAll(item => long.Parse(item.DateTicks) < long.Parse(_activities[MaxActivities - 1].DateTicks));
                }
                return _activities;
            }
            set { _activities = value; }
        }

        public ActivityList()
        {
            _activities = new List<Activity>();
        }

        public Activity GetLastActivityByFileName(string fileName)
        {
            Activity returnVal = null;

            if (_activities != null)
            {
                List<Activity> fileNameMatches = _activities.FindAll(item =>
                    item.FileName.Contains(ASGFileInfo.SafeSearchFileName(fileName)) && ASGFileInfo.IsAsg(item.FileName));

                if (fileNameMatches != null && fileNameMatches.Count > 0)
                {
                    returnVal = fileNameMatches.Find(item => item.DateTicks.Equals(fileNameMatches.Max(maxTicks => maxTicks.DateTicks)));
                }
            }

            return returnVal;
        }

        public int GetUnSentActivitiesCount()
        {
            int returnVal = 0;

            List<Activity> unSentMatches = _activities.FindAll(item => item.Status.Equals(ActivityState.Received));

            if (unSentMatches != null & unSentMatches.Count > 0)
            {
                returnVal = unSentMatches.Count;
            }

            return returnVal;
        }

        public int GetUnSentActivitiesCountByGameType(AowGameType theType)
        {
            int returnVal = 0;

            List<Activity> unSentMatches = _activities.FindAll(item =>
                item.GameType.Equals(theType) && item.Status.Equals(ActivityState.Received));

            if (unSentMatches != null & unSentMatches.Count > 0)
            {
                returnVal = unSentMatches.Count;
            }

            return returnVal;
        }

        public int GetUnknownGameTypeActivitiesCount()
        {
            List<Activity> unknownMatches = _activities.FindAll(item => item.GameType.Equals(AowGameType.Unknown));
            return unknownMatches.Count;
        }
    }
}
