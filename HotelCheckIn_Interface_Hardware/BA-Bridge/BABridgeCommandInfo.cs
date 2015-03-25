using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.BA_Bridge
{
    public class BABridgeCommandInfo
    {
        private int command;
        public int Command
        {
            get { return command; }
            set { command = value; }
        }
        private int op;
        public int Op
        {
            get { return op; }
            set { op = value; }
        }
        private int hostCommand;
        public int HostCommand
        {
            get { return hostCommand; }
            set { hostCommand = value; }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int errorCode;
        public int ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }
        private int oStatus;
        public int OStatus
        {
            get { return oStatus; }
            set { oStatus = value; }
        }
        private int oErrorCode;
        public int OErrorCode
        {
            get { return oErrorCode; }
            set { oErrorCode = value; }
        }
        private long[] value1 = new long[7];
        public long[] Value
        {
            get { return value1; }
            set { value1 = value; }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }
        private int socketCreate;
        public int SocketCreate
        {
            get { return socketCreate; }
            set { socketCreate = value; }
        }
    }
}
