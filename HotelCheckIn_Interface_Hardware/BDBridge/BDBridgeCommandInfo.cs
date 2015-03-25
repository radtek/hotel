using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.BDBridge
{
    public class BDBridgeCommandInfo
    {
        private int command;
        public int Command
        {
            set { command = value; }
            get { return command; }
        }
        int op;
        public int Op
        {
            set { op = value; }
            get { return op; }
        }
        int hostCommand;
        public int HostCommand
        {
            set { hostCommand = value; }
            get { return hostCommand; }
        }
        int status;
        public int Status
        {
            set { status = value; }
            get { return status; }
        }
        int errorCode;
        public int ErrorCode
        {
            set { errorCode = value; }
            get { return errorCode; }
        }
        int oStatus;
        public int OStatus
        {
            set { oStatus = value; }
            get { return oStatus; }
        }
        int oErrorCode;
        public int OErrorCode
        {
            set { oErrorCode = value; }
            get { return oErrorCode; }
        }
        int near_end;
        public int Near_end
        {
            set { near_end = value; }
            get { return near_end; }
        }
        int[] exit = new int[4];
        public int[] Exit
        {
            set { exit = value; }
            get { return exit; }
        }
        int[] reject = new int[4];
        public int[] Reject
        {
            set { reject = value; }
            get { return reject; }
        }
        int[] qty = new int[4];
        public int[] Qty
        {
            set { qty = value; }
            get { return qty; }
        }
        int mode;
        public int Mode
        {
            set { mode = value; }
            get { return mode; }
        }
        long[] value1 = new long[7];
        public long[] Value
        {
            set { value1 = value; }
            get { return value1; }
        }
        bool isBusy;
        public bool IsBusy
        {
            set { isBusy = value; }
            get { return isBusy; }
        }
        int socketCreate;
        public int SocketCreate
        {
            set { socketCreate = value; }
            get { return socketCreate; }
        }
    }
}
