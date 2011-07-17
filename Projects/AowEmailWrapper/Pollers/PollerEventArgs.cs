using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.Pollers
{
    public enum PollState
    {
        Begin,
        End
    }

    public class PollerEventArgs
    {
        private bool _emailRecieved = false;
        private PollState _theState = PollState.Begin;
        private Exception _ex = null;

        public PollState PollState
        {
            get { return _theState; }
            set { _theState = value; }
        }

        public bool EmailRecieved
        {
            get { return _emailRecieved; }
            set { _emailRecieved = value; }
        }

        public Exception Exception
        {
            get { return _ex; }
            set { _ex = value; }
        }

        public PollerEventArgs(PollState theState, bool emailRecieved)
            : this(theState, emailRecieved, null)
        { }

        public PollerEventArgs(PollState theState, bool emailRecieved, Exception ex)
        {
            _theState = theState;
            _emailRecieved = emailRecieved;
            _ex = ex;
        }
    }
}
