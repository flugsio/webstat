using System;
using System.Collections.Generic;
using System.Text;

namespace webstat
{
    class TickEvent : IServiceTickEvent
    {
        #region IServiceTickEvent Members

        private DateTime _Timestamp;
        private Status _Status;

        public DateTime Timestamp
        {
            get { return _Timestamp; }
        }

        public Status Status
        {
            get { return _Status; }
        }

        #endregion

        public TickEvent(DateTime timeStamp, Status status)
        {
            _Timestamp = timeStamp;
            _Status = status;
        }
    }
}
