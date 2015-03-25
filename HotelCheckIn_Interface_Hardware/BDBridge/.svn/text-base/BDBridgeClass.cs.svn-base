using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

//出钞模块，端口：4950
namespace HotelCheckIn_Interface_Hardware.BDBridge
{
    public class BDBridgeClass
    {
        private BDBridgeCommandInfo COM = new BDBridgeCommandInfo();
        private Socket conn = null;
        private int iStatus = 0; //机器状态代码
        private int iErrorCode = 0; //错误代码

        public int IStatus
        {
            get
            {
                return iStatus;
            }
        }

        public int IErrorCode
        {
            get
            {
                return iErrorCode;
            }
        }

        public int Lval { get; set; }

        public BDBridgeClass()
        {
            int i;
            COM.Command = 0;
            COM.HostCommand = 0;
            COM.ErrorCode = 0;
            COM.Status = 0;
            COM.OErrorCode = 0;
            COM.OStatus = 0;
            COM.Mode = 3;
            COM.SocketCreate = -99;

            for (i = 0; i < 4; i++)
            {
                COM.Exit[i] = 0;
                COM.Reject[i] = 0;
            }
        }

        /// <summary>
        /// Socket联接
        /// </summary>
        /// <param name="hostIP"></param>
        /// <param name="hostPort">端口号：4930</param>
        public void SocketOpen(string hostIP, int hostPort)
        {

            conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (conn.Connected == true)
            {
                conn.Close();
            }
            conn.Connect(hostIP, hostPort);
        }

        /// <summary>
        /// Socket关闭
        /// </summary>
        public void SocketClose()
        {
            if (conn != null && conn.Connected == true)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Socket指令操作
        /// </summary>
        /// <param name="command"></param>
        public void SocketCommand(int command)
        {
            COM.Command = command;
        }

        /// <summary>
        /// Socket通信读写数据
        /// </summary>
        public void RW_Data()
        {
            byte[] args = new byte[36];
            bool renew, lchk;
            long lval;
            int bytes;

            args[0] = Convert.ToByte(COM.Command);
            if (COM.Command == 1)
            {
                args[4] = 0;
                args[8] = Convert.ToByte(Lval);
            }
            bytes = conn.Send(args, args.Length, 0);
            if (bytes >= 36)
                bytes = conn.Receive(args, args.Length, 0);
            if (bytes >= 36)
            { // 9 int = 9 * 4 byte =>36byte
                renew = false;
                COM.ErrorCode = 0;
                COM.Near_end = 0;
                COM.Status = args[0];
                switch (args[0])
                {
                    case 0: COM.Command = COM.HostCommand;
                        COM.ErrorCode = 0;
                        break;
                    case 1: COM.Command = 0;
                        //  renew = true;
                        if (COM.HostCommand == 1)
                        {
                            COM.HostCommand = 0;
                        }
                        break;
                    case 2: COM.Command = 0;
                        if (COM.HostCommand == 1) COM.HostCommand = 0;
                        break;
                    case 4:
                    case 5: COM.Command = 2;
                        renew = true;
                        break;
                    case 8:
                        COM.Near_end = args[4];
                        break;
                    case 9: COM.ErrorCode = args[4];
                        COM.Mode = args[8];
                        break;
                    default: COM.HostCommand = 0;
                        COM.Command = 0;
                        break;
                }
                if (renew == true)
                {
                    lchk = false;
                    if (args[4] != COM.Exit[0]) { COM.Exit[0] = args[4]; lchk = true; }
                    if (args[8] != COM.Reject[0]) { COM.Reject[0] = args[8]; lchk = true; }
                    if (args[12] != COM.Exit[1]) { COM.Exit[1] = args[12]; lchk = true; }
                    if (args[16] != COM.Reject[1]) { COM.Reject[1] = args[16]; lchk = true; }
                    if (args[20] != COM.Exit[2]) { COM.Exit[2] = args[20]; lchk = true; }
                    if (args[24] != COM.Reject[2]) { COM.Reject[2] = args[24]; lchk = true; }
                    if (args[28] != COM.Exit[3]) { COM.Exit[3] = args[28]; lchk = true; }
                    if (args[32] != COM.Reject[3]) { COM.Reject[3] = args[32]; lchk = true; }
                    //ShowValue();
                }
                if (COM.SocketCreate != 0)
                {
                    //this->toolStripStatusLabel1->Text = "CH_Bridge Connection";
                    //this->toolStripStatusLabel1->ForeColor = System::Drawing::Color::Green; 
                    if (args[0] == 0) COM.SocketCreate = 0;
                }
                if (COM.OStatus != COM.Status || COM.OErrorCode != COM.ErrorCode)
                {
                    //ShowStatus();
                    iErrorCode = COM.ErrorCode;
                    iStatus = COM.Status;
                    COM.OStatus = COM.Status;
                    COM.OErrorCode = COM.ErrorCode;
                }
            }
            else
            { //closesocket(ConnectSocket);
                //ConnectSocket = INVALID_SOCKET;
                //SocketConn = -1;
            }
        }


        public string ShowStatus(int tagErrorCode, int tagStatus)
        {
            if (tagErrorCode == 0)
            {
                switch (tagStatus)
                {
                    case 0: return "Normal";
                    case 1: return "DISPENSING";
                    case 2: return "Dispense Command Ready";
                    case 4: return "Dispense End";
                    case 5: return "Dispense End ( Error)";
                    case 8: return "Near-End Detect ==> Code : " + COM.Near_end.ToString();
                    case 99: return "Command Error";
                }
            }
            else
            {
                if (COM.Mode == 0)
                {
                    switch (tagErrorCode)
                    {
                        case 1: return "Sensor DIVERT is fail!...";
                        case 2: return "Sensor SONAR is fail!...";
                        case 3: return "Sensor REJECT is fail!...";
                        case 4: return "Sensor EXIT is fail!...";
                        case 5: return "REJECT_TRAY exist!...";
                        case 6: return "Sensor PATH1 is fail!...";
                        case 7: return "Sensor PATH2 is fail!...";
                        case 8: return "Sensor PATH3 is fail!...";
                        case 9: return "Sensor PATH4 is fail!...";
                        case 13: return "Cassette1 Sensor CST_IN is fail!...";
                        case 14: return "Cassette1 Sensor CHECK is fail!...";
                        case 15: return "Cassette1 exists not in the postion.!...";
                        case 16: return "Cassette1 PickUp is End.!...";
                        case 17: return "Cassette2 Sensor CST_IN is fail!...";
                        case 18: return "Cassette2 Sensor CHECK is fail!...";
                        case 19: return "Cassette2 Sensor CHECK is fail!...";
                        case 20: return "Cassette2 exists not in the postion.!...";
                        case 21: return "Cassette2 PickUp is End.!...";
                        case 22: return "Cassette3 Sensor CHECK is fail!...";
                        case 23: return "Cassette3 exists not in the postion.!...";
                        case 24: return "Cassette3 PickUp is End.!...";
                        case 25: return "Cassette4 Sensor CST_IN is fail!...";
                        case 26: return "Cassette4 Sensor CHECK is fail!...";
                        case 27: return "Cassette4 exists not in the postion.!...";
                        case 28: return "Cassette4 PickUp is End.!...";
                        case 33: return "Feeding Time-out between CHECK Sensor and SONAR Sensor!...";
                        case 34: return "Feeding Time-out between SONAR Sensor and DIVERT Sensor!...";
                        case 35: return "Feeding Time-out between DIVERT Sensor and EXIT Sensor!...";
                        case 36: return "Feeding Time-out between DIVERT Sensor and REJECT Sensor!...";
                        case 37: return "A Note Is Staying at EXT Sensor!...";
                        case 38: return "Ejecting the Note Suspected as Rejected!...";
                        case 39: return "Abnormal Note Management (Flow Processing Error)!...";
                        case 40: return "Abnormal Note Management (Flow Processing Error)!...";
                        case 41: return "Rejecting the Note Suspected as Ejected!...";
                        case 43: return "Detecting Notes on the Path Before Start of Pick-up!...";
                        case 44: return "Too Many Pick-up Events During Dispensing from One Cash Cassette!...";
                        case 45: return "Too Many Rejects During Dispensing from One Cash Cassette!...";
                        case 46: return "Abnormal Termination During Purge Execution!...";
                        case 49: return "Detecting Trouble in Motor or Slit Sensor Before Dispensing!...";
                        case 50: return "Not Detecting Reject Tray before Start or for Operation!...";
                        case 51: return "Failed to Calibrate Sensors!...";
                        case 52: return "More Banknotes than the Requested are Dispensed!...";
                        case 53: return "Dispensing is Not Terminated within 90 Seconds!...";
                        case 54: return "Recognizing Abnormal Command!...";
                        case 55: return "Recognizing Abnormal Parameters on the Command!...";
                        case 56: return "Download Sequence is incorrect.!...";
                        case 57: return "Failure of Write!...";
                        case 58: return "Not to Give Verify command on Reset after Downloading Program!...";
                        case 59: return "Failure of Writing EEPROM!...";
                        case 60: return "Mismatches Checksum of EEPROM on Writing EEPROM!...";
                        case 61: return "Error in Dispense Serial Number or Identification Number of Dispense!...";
                        case 64: return "Sonar Sensor is Always On.!...";
                        case 65: return "Divert Sensor is Always On.!...";
                        case 66: return "Exit Sensor is Always On.!...";
                        case 67: return "Reject Sensor is Always On.!...";
                        case 72: return "Sonar Sensor is Always Off.!...";
                        case 73: return "Divert Sensor is Always Off.!...";
                        case 74: return "Exit Sensor is Always Off.!...";
                        case 75: return "Reject Sensor is Always Off.!...";
                        case 80: return "Path1 Sensor is Always On.!...";
                        case 81: return "Check1 Sensor is Always On.!...";
                        case 82: return "CST_IN1 Sensor is Always On.!...";
                        case 83: return "Path2 Sensor is Always On.!...";
                        case 84: return "Check2 Sensor is Always On.!...";
                        case 85: return "CST_IN2 Sensor is Always On.!...";
                        case 86: return "Path3 Sensor is Always On.!...";
                        case 87: return "Check3Sensor is Always On.!...";
                        case 88: return "CST_IN3Sensor is Always On.!...";
                        case 89: return "Path4 Sensor is Always On.!...";
                        case 90: return "Check4Sensor is Always On.!...";
                        case 91: return "CST_IN4Sensor is Always On.!...";
                        case 96: return "Path1 Sensor is Always Off.!...";
                        case 97: return "Check1 Sensor is Always Off.!...";
                        case 98: return "CST_IN1 Sensor is Always Off.!...";
                        case 99: return "Path2 Sensor is Always Off.!...";
                        case 100: return "Check2 Sensor is Always Off.!...";
                        case 101: return "CST_IN2 Sensor is Always Off.!...";
                        case 102: return "Path3 Sensor is Always Off.!...";
                        case 103: return "Check3 Sensor is Always Off.!...";
                        case 104: return "CST_IN3 Sensor is Always Off.!...";
                        case 105: return "Path4 Sensor is Always Off.!...";
                        case 106: return "Check4 Sensor is Always Off.!...";
                        case 107: return "CST_IN4 Sensor is Always Off.!...";
                        case 112: return "Banknote Pick Up Error in the Cassette1 on NEAREND State!...";
                        case 113: return "Banknote Pick Up Error in the Cassette2 on NEAREND State!...";
                        case 114: return "Banknote Pick Up Error in the Cassette3on NEAREND State!...";
                        case 115: return "Banknote Pick Up Error in the Cassette4on NEAREND State!...";
                        case 116: return "Jamming or sensor failure in the Cash Cassette1!...";
                        case 117: return "Jamming or sensor failure in the Cash Cassette2!...";
                        case 118: return "Jamming or sensor failure in the Cash Cassette3!...";
                        case 119: return "Jamming or sensor failure in the Cash Cassette4!...";
                        case 120: return "Not Detecting Cash Cassette1 before Start or for Operation!...";
                        case 121: return "Not Detecting Cash Cassette2 before Start or for Operation!...";
                        case 122: return "Not Detecting Cash Cassette3before Start or for Operation!...";
                        case 123: return "Not Detecting Cash Cassette4before Start or for Operation!...";
                        case 124: return "Cash Cassette1 is Near-End (In Case of Near End Detection Mode)!...";
                        case 125: return "Cash-Cassette2 is Near-End (In Case of Near End Detection Mode)!...";
                        case 126: return "Cash-Cassette3is Near-End (In Case of Near End Detection Mode)!...";
                        case 127: return "Cash-Cassette4is Near-End (In Case of Near End Detection Mode)!...";
                        case 128: return "Pick-up Error in Cassette1 ( Banknotes exist in Cash Cassette1)!...";
                        case 129: return "Pick-up Error in Cassette2 ( Banknotes exist in Cash Cassette2)!...";
                        case 130: return "Pick-up Error in Cassette3 ( Banknotes exist in Cash Cassette3)!...";
                        case 131: return "Pick-up Error in Cassette4 ( Banknotes exist in Cash Cassette4)!...";
                        case 160: return "Detect Note in Cassette 1 Check Sensor!...";
                        case 161: return "Detect Note in Cassette 2 Check Sensor or Path 2!...";
                        case 162: return "Detect Note in Cassette 3 Check Sensor or Path 3!...";
                        case 163: return "Detect Note in Cassette 4 Check Sensor or Path 4!...";
                        case 231: return "OFF LINE!...";
                        default:
                            return "Unknow Error!...";
                    }
                }
                else if (COM.Mode == 1)
                {
                    switch (tagErrorCode)
                    {
                        case 1: return "Sensor DIV-L is fail!...";
                        case 2: return "Sensor DIV-R is fail!...";
                        case 3: return "Sensor REJECT BOX CHECK(RB sensor) is fail!...";
                        case 4: return "Sensor EXT is fail!...";
                        case 5: return "Reject cassette is fail!...";
                        case 6: return "Sensor SOL is fail!...";
                        case 7: return "Sensor RVDT Start_L is fail!...";
                        case 8: return "Sensor RVDT Start_R is fail!...";
                        case 13: return "CASSETTE1 Sensor CHK-L is fail!...";
                        case 14: return "CASSETTE1 Sensor CHK-R is fail!...";
                        case 15: return "CASETTE1 BOX exists in the postion.!...";
                        case 16: return "CASETTE1 CLU is fail!...";
                        case 17: return "CASSETTE2 Sensor CHK-L is fail!...";
                        case 18: return "CASSETTE2 Sensor CHK-R is fail!...";
                        case 19: return "CASETTE2 BOX exists in the postion.!...";
                        case 20: return "CASETTE2 CLU is fail!...";
                        case 21: return "CASSETTE3 Sensor CHK-L is fail!...";
                        case 22: return "CASSETTE3 Sensor CHK-R is fail!...";
                        case 23: return "CASETTE3 BOX exists in the postion.!...";
                        case 24: return "CASETTE3 CLU is fail!...";
                        case 25: return "CASSETTE4 Sensor CHK-L is fail!...";
                        case 26: return "CASSETTE4 Sensor CHK-R is fail!...";
                        case 27: return "CASETTE4 BOX exists in the postion.!...";
                        case 28: return "CASETTE4 CLU is fail!...";
                        case 33: return "Banknote Pick Up Error!...";
                        case 34: return "TimeOut on the path between CHK Sensor and RVDT Start Sensor!...";
                        case 35: return "TimeOut on the path between DIV Sensor and EXT Sensor!...";
                        case 36: return "TimeOut on the path between DVT Sensor and RJT Sensor!...";
                        case 37: return "A note Staying at EXT Sensor!...";
                        case 38: return "Ejecting the note suspected as rejected!...";
                        case 39: return "Abnormal note management (Flow Processing Error Inside)!...";
                        case 40: return "Abnormal note management (Flow Processing Error Inside)!...";
                        case 41: return "Jamming on EJT Sensor!...";
                        case 42: return "Jamming on EXT Sensor!...";
                        case 43: return "Detecting notes on the path before start of pick-up!...";
                        case 44: return "Dispensing too many notes for one transaction!...";
                        case 45: return "Rejecting too many notes for one transaction!...";
                        case 46: return "Abnormal termination during purge execution!...";
                        case 64: return "Detecting sensor trouble or abnormal material before start!...";
                        case 65: return "Detecting sensor trouble or abnormal material before start!...";
                        case 66: return "Detecting trouble of solenoid operation before dispense!...";
                        case 67: return "Detecting trouble in motor or slit sensor before dispense!...";
                        case 68: return "Detecting no Cassette1!...";
                        case 69: return "Detecting Near-end status in the cassette requested to dispense!...";
                        case 70: return "Detecting no reject tray before start or for operation!...";
                        case 71: return "Failed to calibrate sensors!...";
                        case 72: return "Jamming or sensor failure in the Cash Cassette!...";
                        case 73: return "More banknotes than the requested are dispensed!...";
                        case 74: return "TimeOut on the path between RVDT Start Sensor and DIV Sensor!...";
                        case 75: return "Dispensing is not terminated within 90 seconds.!...";
                        case 76: return "Detecting no Cassette2 (effective only for ECDM-200)!...";
                        case 80: return "Recognizing abnormal Command!...";
                        case 81: return "Recognizing abnormal Parameters on the command!...";
                        case 82: return "Not to give Verify command on Reset after downloading program!...";
                        case 83: return "Failure of writing on program area!...";
                        case 84: return "Failure of Verify!...";
                        case 231: return "OFF LINE!...";
                        default:
                            return "Unknow Error!...";
                    }
                }
                else if (COM.Mode == 2)
                {
                    switch (tagErrorCode)
                    {
                        case 1: return "Sensor DIV-L is fail!...";
                        case 2: return "Sensor DIV-R is fail!...";
                        case 3: return "Sensor REJECT BOX CHECK(RB sensor) is fail!...";
                        case 4: return "Sensor EXT is fail!...";
                        case 5: return "Reject cassette is fail!...";
                        case 6: return "Sensor SOL is fail!...";
                        case 7: return "Sensor RVST-L is fail!...";
                        case 8: return "Sensor RVST-R is fail!...";
                        case 9: return "Sensor CAM WHEEL is fail!...";
                        case 10: return "Sensor CAM CLOSE POINT is fail!...";
                        case 11: return "Sensor CAM NOTE-L is fail!...";
                        case 12: return "Sensor CAM NOTE-R is fail!...";
                        case 13: return "CASSETTE1 Sensor CHK-L is fail!...";
                        case 14: return "CASSETTE1 Sensor CHK-R is fail!...";
                        case 15: return "CASSETTE1 is fail!...";
                        case 16: return "CASSETTE1 Sensor CB is fail!...";
                        case 17: return "CASSETTE2 Sensor CHK-L is fail!...";
                        case 18: return "CASSETTE2 Sensor CHK-R is fail!...";
                        case 19: return "CASSETTE2 is fail!...";
                        case 20: return "CASSETTE2 Sensor CB is fail!...";
                        case 21: return "CASSETTE3 Sensor CHK-L is fail!...";
                        case 22: return "CASSETTE3 Sensor CHK-R is fail!...";
                        case 23: return "CASSETTE3 is fail!...";
                        case 24: return "CASSETTE3 Sensor CB is fail!...";
                        case 25: return "CASSETTE4 Sensor CHK-L is fail!...";
                        case 26: return "CASSETTE4 Sensor CHK-R is fail!...";
                        case 27: return "CASSETTE4 is fail!...";
                        case 28: return "CASSETTE4 Sensor CB is fail!...";
                        case 33: return "Banknote Pick Up Error!...";
                        case 34: return "TimeOut on the path between CHK Sensor and RVDT Start Sensor!...";
                        case 35: return "TimeOut on the path between DIV Sensor and EXT Sensor!...";
                        case 36: return "TimeOut on the path between DVT Sensor and RJT Sensor!...";
                        case 37: return "A note Staying at EXT Sensor!...";
                        case 38: return "Ejecting the note suspected as rejected!...";
                        case 39: return "Abnormal note management (Flow Processing Error Inside)!...";
                        case 40: return "Abnormal note management (Flow Processing Error Inside)!...";
                        case 41: return "Jamming on EJT Sensor!...";
                        case 42: return "Jamming on EXT Sensor!...";
                        case 43: return "Detecting notes on the path before start of pick-up!...";
                        case 44: return "Dispensing too many notes for one transaction!...";
                        case 45: return "Rejecting too many notes for one transaction!...";
                        case 46: return "Abnormal termination during purge execution!...";
                        case 64: return "Detecting sensor trouble or abnormal material before start!...";
                        case 65: return "Detecting sensor trouble or abnormal material before start!...";
                        case 66: return "Detecting trouble of solenoid operation before dispense!...";
                        case 67: return "Detecting trouble in motor or slit sensor before dispense!...";
                        case 68: return "Detecting no cassette0 requested to dispense banknotes!...";
                        case 69: return "Detecting Near-end status in the cassette requested to dispense!...";
                        case 70: return "Detecting no reject tray before start or for operation!...";
                        case 71: return "Failed to calibrate sensors!...";
                        case 72: return "Jamming or sensor failure in the Cash Cassette!...";
                        case 73: return "More banknotes than the requested are dispensed!...";
                        case 74: return "TimeOut on the path between RVDT Start Sensor and DIV Sensor!...";
                        case 75: return "Dispensing is not terminated within 90 seconds.!...";
                        case 76: return "Detecting no cassette1 requested to dispense banknotes!...";
                        case 80: return "Recognizing abnormal Command!...";
                        case 81: return "Recognizing abnormal Parameters on the command!...";
                        case 82: return "Not to give Verify command on Reset after downloading program!...";
                        case 83: return "Failure of writing on program area!...";
                        case 231: return "OFF LINE!...";
                        default:
                            return "Unknow Error!...";
                    }
                }
                else
                {
                    switch (tagErrorCode)
                    {
                        case 1: return "CHK SENSOR 1 is fail!...";
                        case 2: return "CHK SENSOR 2 is fail!...";
                        case 3: return "DIV SENSOR 1 is fail!...";
                        case 4: return "DIV SENSOR 2 is fail!...";
                        case 5: return "EJT SENSOR is fail!...";
                        case 6: return "EXIT SENSOR is fail!...";
                        case 7: return "SOL SENSOR is fail!...";
                        case 8: return "CASSETTE0 SENSOR is fail!...";
                        case 9: return "CASSETTE1 SENSOR is fail!...";
                        case 10: return "CHK SENSOR 3 is fail!...";
                        case 11: return "CHK SENSOR 4 is fail!...";
                        case 12: return "REJECT TRAY S/W!...";
                        case 13: return "Cassette1 Sensor CST_IN is fail!...";
                        case 14: return "Cassette1 Sensor CHECK is fail!...";
                        case 15: return "Cassette1 exists not in the postion.!...";
                        case 16: return "Cassette1 PickUp is End.!...";
                        case 17: return "Cassette2 Sensor CST_IN is fail!...";
                        case 18: return "Cassette2 Sensor CHECK is fail!...";
                        case 19: return "Cassette2 Sensor CHECK is fail!...";
                        case 20: return "Cassette2 exists not in the postion.!...";
                        case 21: return "Cassette2 PickUp is End.!...";
                        case 22: return "Cassette3 Sensor CHECK is fail!...";
                        case 23: return "Cassette3 exists not in the postion.!...";
                        case 24: return "Cassette3 PickUp is End.!...";
                        case 25: return "Cassette4 Sensor CST_IN is fail!...";
                        case 26: return "Cassette4 Sensor CHECK is fail!...";
                        case 27: return "Cassette4 exists not in the postion.!...";
                        case 28: return "Cassette4 PickUp is End.!...";
                        case 50: return "Pickup error!...";
                        case 51: return "JAM at CHK1,2 Sensor!...";
                        case 52: return "Overflow bill!...";
                        case 53: return "JAM at EXIT Sensor or EJT Sensor!...";
                        case 54: return "JAM at DIV Sensor!...";
                        case 55: return "Undefined command!...";
                        case 56: return "Bill- End!...";
                        case 58: return "Counting Error (between CHK3,4 Sensor and DIV Sensor)!...";
                        case 59: return "Note request error!...";
                        case 60: return "Counting Error(between DIV Sensor and EJT Sensor)!...";
                        case 61: return "Counting Error(between EJT Sensor and EXIT Sensor)!...";
                        case 63: return "Reject Tray is not recognized!...";
                        case 64: return "Lower Bill-End!...";
                        case 65: return "Motor Stop!...";
                        case 66: return "JAM at Div Sensor!...";
                        case 67: return "Timeout (From DIV Sensor to EJT Sensor)!...";
                        case 68: return "Over Reject!...";
                        case 69: return "Upper Cassette is not recognized!...";
                        case 70: return "Lower Cassette is not recognized!...";
                        case 71: return "Dispensing timeout!...";
                        case 72: return "JAM at EJT Sensor!...";
                        case 73: return "Diverter solenoid or SOL Sensor error!...";
                        case 74: return "SOL Sensor error!...";
                        case 76: return "JAM at CHK3,4 Sensor!...";
                        case 78: return "Purge error (Jam at Div Sensor)!...";
                        case 231: return "OFF LINE!...";
                        default:
                            return "Unknow Error!...";
                    }
                }
            }
            return "";
        }


    }
}
