using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AowEmailWrapper.Helpers;

namespace AowEmailWrapper.Classes
{
    public class TurnLogger
    {
        private const string TURN_LOG_STARTED = "Started";        
        private const string TURN_LOG_HEADER = "Turn Log\r\n--------------------------------------------------------\r\n";
        private const string TURN_LOG_DISABLED_MESSAGE = "Turn Log Disabled.";
        private const string TOTAL_TIMES_LOG_HEADER = "Total Times\r\n--------------------------------------------------------\r\n";
        private const string TIME_MINS_TEMPLATE = "{0} minutes";
        private const string TIME_HOURS_TEMPLATE = "{0} hours {1} minutes";
        private const string TIME_DAYS_TEMPLATE = "{0} days {1} hours {2} minutes";
        private const string TIME_TOTAL_PLAYER_TEMPLATE = "{0} = {1}. Turns: {2}. Average: {3}";
        private const string TIME_TOTAL_GAME_TEMPLATE = "Total game time: {0}";
        private const string TurnNumberRegExp = @"(?:[^\r]*\r){4}[^\d]*(\d+)"; //Match a decimal number after the 4th CR (on the 5th line), use the second group matched
        private const string TurnNumberTemplate = "Turn: {0}";
        
        private const string NEGATIVE_TIME = "Negative time";
        
        public static string LogTurn(string gameFileName, string email, string outgoingBodyText)
        {
            string theLog = TURN_LOG_DISABLED_MESSAGE;

            try
            {
                string oldLog = DataManagerHelper.LoadTurnLog(gameFileName);

                if (!string.IsNullOrEmpty(oldLog) && oldLog.IndexOf(TURN_LOG_HEADER) >= 0)
                {
                    theLog = UpdateLog(oldLog, email);
                }
                else
                {
                    theLog = CreateLog(email, GetTurnNumber(outgoingBodyText));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }

            return theLog;
        }

        private static int CompareFileInfoLastWriteTime(FileInfo x, FileInfo y)
        {
            return x.LastWriteTime.CompareTo(y.LastWriteTime);
        }

        private static string UpdateLog(string input, string email)
        {
            string returnVal = TURN_LOG_DISABLED_MESSAGE;
            string turnNumber = string.Empty;
            string previousTurnLog = string.Empty;

            if (!string.IsNullOrEmpty(input))
            {
                int turnLogHeaderIndex = input.IndexOf(TURN_LOG_HEADER);

                if (turnLogHeaderIndex > 0)
                {
                    turnNumber = GetTurnNumber(input.Substring(0, turnLogHeaderIndex));
                    previousTurnLog = input.Substring(input.IndexOf(TURN_LOG_HEADER));

                    if (!string.IsNullOrEmpty(previousTurnLog))
                    {
                        string headerRemoved = previousTurnLog.Replace(TURN_LOG_HEADER, string.Empty);

                        string[] lineSplit = headerRemoved.Split(new string[] { StringHelper.CrLf }, StringSplitOptions.RemoveEmptyEntries);
                        List<Turn> turnList = new List<Turn>();

                        foreach (string line in lineSplit)
                        {
                            if (TOTAL_TIMES_LOG_HEADER.Contains(line))
                            {
                                break;
                            }
                            turnList.Add(new Turn(line));
                        }

                        Turn thisTurn = new Turn(turnNumber, email, turnList[0].UtcTimeString);

                        turnList.Insert(0, thisTurn);

                        StringBuilder sb = new StringBuilder(TURN_LOG_HEADER);

                        foreach (Turn turn in turnList)
                        {
                            sb.Append(StringHelper.CrLf);
                            sb.Append(turn.ToString());
                        }

                        sb.Append(StringHelper.CrLf);

                        CalculateTotalTimes(ref sb, turnList);

                        returnVal = sb.ToString();
                    }
                }
            }

            return returnVal;
        }

        private static string CreateLog(string email, string turnNumber)
        {
            string returnVal = string.Empty;

            Turn thisTurn = new Turn(turnNumber, email, null);

            thisTurn.TimeTaken = TURN_LOG_STARTED;

            StringBuilder sb = new StringBuilder(TURN_LOG_HEADER);
            sb.Append(StringHelper.CrLf);
            sb.Append(thisTurn.ToString());
            returnVal = sb.ToString();

            return returnVal;
        }

        public static void SaveLog(string gameFileName, string emailBody)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailBody))
                {
                    DataManagerHelper.SaveTurnLog(gameFileName, emailBody);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();
            }
        }

        public static bool DeleteLog(string gameFileName)
        {
            return DataManagerHelper.DeleteTurnLog(gameFileName);
        }

        public static string TimeSpanToString(TimeSpan input)
        {
            string returnVal = string.Empty;

            if (input.Ticks < 0)
            {
                returnVal = NEGATIVE_TIME; //This can happen if someone's system clock is wrong
            }
            else
            {
                if (input.Days > 0)
                {
                    returnVal = string.Format(TIME_DAYS_TEMPLATE, input.Days.ToString(), input.Hours.ToString(), input.Minutes.ToString());
                }
                else if (input.Hours > 0)
                {
                    returnVal = string.Format(TIME_HOURS_TEMPLATE, input.Hours.ToString(), input.Minutes.ToString());
                }
                else if (input.Minutes > 0)
                {
                    returnVal = string.Format(TIME_MINS_TEMPLATE, input.Minutes.ToString());
                }
            }

            return returnVal;
        }

        private static string GetTurnNumber(string input)
        {
            string returnVal = string.Empty;
            Regex regEx = new Regex(TurnNumberRegExp);
            Match theTurnMatch = regEx.Match(input);
            if (theTurnMatch != null &&
                theTurnMatch.Groups != null &&
                theTurnMatch.Groups.Count.Equals(2))
            {
                returnVal = string.Format(TurnNumberTemplate, theTurnMatch.Groups[1].Value);
            }
            return returnVal;
        }

        private static void CalculateTotalTimes(ref StringBuilder sb, List<Turn> turnList)
        {
            if (turnList.Count > 1) //Calculate total times
            {
                Dictionary<string, TimeSpan> totalTimes = new Dictionary<string, TimeSpan>();
                TimeSpan totalGameTime = new TimeSpan();

                for (int i = turnList.Count - 2; i >= 0; i--) //Loop backwards
                {
                    Turn theTurn = turnList[i];
                    Turn thePreviousTurn = turnList[i + 1];

                    DateTime theTurnUtc, thePreviousTurnUtc;

                    if (DateTime.TryParse(theTurn.UtcTimeString, out theTurnUtc) &&
                        DateTime.TryParse(thePreviousTurn.UtcTimeString, out thePreviousTurnUtc))
                    {
                        TimeSpan timeTaken = theTurnUtc - thePreviousTurnUtc;
                        if (totalTimes.ContainsKey(theTurn.Email))
                        {
                            totalTimes[theTurn.Email] = totalTimes[theTurn.Email].Add((timeTaken.Ticks > 0) ? timeTaken : new TimeSpan());
                        }
                        else
                        {
                            totalTimes.Add(theTurn.Email, (timeTaken.Ticks > 0) ? timeTaken : new TimeSpan());
                        }

                        totalGameTime = totalGameTime.Add((timeTaken.Ticks > 0) ? timeTaken : new TimeSpan());
                    }
                }

                sb.Append(StringHelper.CrLf);
                sb.Append(TOTAL_TIMES_LOG_HEADER);

                foreach (string key in totalTimes.Keys)
                {
                    List<Turn> turns = turnList.FindAll(turn => turn.Email.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                    //The player who started the game should have the start turn discounted from the 
                    //mean average calculation since the game was not waiting for them then.

                    Turn startTurn = turns.Find(turn => turn.TimeTaken.Equals(TURN_LOG_STARTED, StringComparison.InvariantCultureIgnoreCase));

                    int turnCount = (startTurn != null) ? turns.Count - 1 : turns.Count;

                    if (turnCount > 0)
                    {
                        sb.Append(StringHelper.CrLf);
                        sb.Append(string.Format(TIME_TOTAL_PLAYER_TEMPLATE, key, TimeSpanToString(totalTimes[key]), turns.Count.ToString(), TimeSpanToString(TimeSpan.FromSeconds(totalTimes[key].TotalSeconds / turnCount))));
                    }
                    else
                    {
                        sb.Append(string.Format(TIME_TOTAL_PLAYER_TEMPLATE, key, "None", turns.Count.ToString(), "None"));
                    }
                }

                sb.Append(StringHelper.CrLf);
                sb.Append(StringHelper.CrLf);
                sb.Append(string.Format(TIME_TOTAL_GAME_TEMPLATE, TimeSpanToString(totalGameTime)));
            }
        }
    }
}
