using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mozilla.Autoconfig
{
    public class MechanismResponse
    {
        private MechanismResponseType _responseType;
        private ClientConfig _clientConfig = null;
        private Exception _exception = null;
        private bool _mxLookup = false;
        private MechanismOriginType _origin;

        public MechanismResponse()
        {
            _responseType = MechanismResponseType.NotFound;
        }

        public bool IsSuccess
        {
            get { return _responseType.Equals(MechanismResponseType.Success); }
        }

        public bool IsGuess
        {
            get { return _origin.Equals(MechanismOriginType.Guess); }
        }

        public bool MxLookup
        {
            get { return _mxLookup; }
            set { _mxLookup = value; }
        }

        public MechanismOriginType Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public MechanismResponseType ResponseType
        {
            get { return _responseType; }
            set { _responseType = value; }
        }

        public ClientConfig ClientConfig
        {
            get { return _clientConfig; }
            set { _clientConfig = value; }
        }

        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
    }
}
