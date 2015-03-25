using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

//入钞模块,端口：4930
namespace HotelCheckIn_Interface_Hardware.BA_Bridge
{
    public class BABridgeClass
    {
        private BABridgeCommandInfo COM = new BABridgeCommandInfo();
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

        public BABridgeClass()
        {
            COM.Command = 0;
            COM.HostCommand = 0;
            COM.ErrorCode = 0;
            COM.Status = 0;
            COM.OErrorCode = 0;
            COM.OStatus = 0;
            COM.SocketCreate = -99;
            COM.Op = 0;
            for (int i = 0; i < 7; i++)
            {
                COM.Value[i] = 0;
            }
        }

        private BABridgeClass(string hostIP, int hostPort, int command, int Op)
        {
            COM.Command = command;
            COM.HostCommand = 0;
            COM.ErrorCode = 0;
            COM.Status = 0;
            COM.OErrorCode = 0;
            COM.OStatus = 0;
            COM.SocketCreate = -99;
            COM.Op = Op;
            for (int i = 0; i < 7; i++)
            {
                COM.Value[i] = 0;
            }
            conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            conn.Connect(hostIP, hostPort);

            //ShowStatus();
            //ShowValue();
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
        /// <param name="Op"></param>
        public void SocketCommand(int command, int Op)
        {
            COM.Command = command;
            COM.Op = Op;
        }

        /// <summary>
        /// Socket通信读写数据
        /// </summary>
        public void RW_Data()
        {
            long lval;
            byte[] args = new byte[36];
            int bytes;
            bool renew, lchk;

            args[0] = Convert.ToByte(COM.Command);
            if (COM.Command == 1 && COM.Op == 1)
            {
                args[4] = 1;
                lval = Lval;
                byte[] b12 = BitConverter.GetBytes(Convert.ToInt32(lval % 10000));
                args[12] = b12[0];
                args[13] = b12[1];
                args[14] = b12[2];
                args[15] = b12[3];
                //args[12] = Convert.ToByte(lval % 10000);
                if (lval > 0) Convert.ToByte(lval /= 10000);
                //args[8] = Convert.ToByte(lval % 10000);
                byte[] b8 = BitConverter.GetBytes(Convert.ToInt32(lval % 10000));
                args[8] = b8[0];
                args[9] = b8[1];
                args[10] = b8[2];
                args[11] = b8[3];
            }

            bytes = conn.Send(args, args.Length, 0);
            if (bytes >= 36)
                bytes = conn.Receive(args, args.Length, 0);
            if (bytes >= 36)
            {
                renew = false;
                COM.ErrorCode = 0;
                COM.Status = args[0];
                switch (args[0])
                {
                    case 0: COM.Command = COM.HostCommand;
                        COM.ErrorCode = 0;
                        break;
                    case 1: COM.Command = 0;
                        renew = true;
                        if (COM.HostCommand == 1)
                        {
                            COM.HostCommand = 0;
                        }
                        else if (COM.HostCommand == 3) COM.Command = 3;
                        break;
                    case 2: COM.Command = 0;
                        if (COM.HostCommand == 1) COM.HostCommand = 0;
                        else if (COM.HostCommand == 3) COM.Command = 3;
                        break;
                    case 3: COM.Command = 0;
                        if (COM.HostCommand == 3) COM.HostCommand = 0;
                        break;
                    case 4:
                    case 5: COM.Command = 2;
                        renew = true;
                        break;
                    case 7: COM.Command = 8;
                        break;
                    case 8: COM.Command = 0;
                        break;
                    case 6:
                    case 9: COM.ErrorCode = args[4];
                        if (COM.HostCommand == 3) COM.Command = 3;
                        break;
                    default: COM.HostCommand = 0;
                        COM.Command = 0;
                        break;
                }
                if (renew == true)
                {
                    lchk = false;
                    //lval = args[4];
                    lval = BitConverter.ToInt32(args, 4);
                    lval = lval * 10000;
                    //lval = lval + args[8];
                    lval = lval + BitConverter.ToInt32(args, 8);
                    if (lval != COM.Value[6]) { COM.Value[6] = lval; lchk = true; }
                    //if (args[12] != COM.Value[0]) { COM.Value[0] = args[12]; lchk = true; }
                    if (BitConverter.ToInt32(args, 12) != COM.Value[0]) { COM.Value[0] = BitConverter.ToInt32(args, 12); lchk = true; }
                    //if (args[16] != COM.Value[1]) { COM.Value[1] = args[16]; lchk = true; }
                    if (BitConverter.ToInt32(args, 16) != COM.Value[1]) { COM.Value[1] = BitConverter.ToInt32(args, 16); lchk = true; }
                    //if (args[20] != COM.Value[2]) { COM.Value[2] = args[20]; lchk = true; }
                    if (BitConverter.ToInt32(args, 20) != COM.Value[2]) { COM.Value[2] = BitConverter.ToInt32(args, 20); lchk = true; }
                    //if (args[24] != COM.Value[3]) { COM.Value[3] = args[24]; lchk = true; }
                    if (BitConverter.ToInt32(args, 24) != COM.Value[3]) { COM.Value[3] = BitConverter.ToInt32(args, 24); lchk = true; }
                    //if (args[28] != COM.Value[4]) { COM.Value[4] = args[28]; lchk = true; }
                    if (BitConverter.ToInt32(args, 24) != COM.Value[4]) { COM.Value[4] = BitConverter.ToInt32(args, 28); lchk = true; }
                    //if (args[32] != COM.Value[5]) { COM.Value[5] = args[32]; lchk = true; }
                    if (BitConverter.ToInt32(args, 32) != COM.Value[5]) { COM.Value[5] = BitConverter.ToInt32(args, 32); lchk = true; }
                    if (lchk == true) ShowValue();
                }

                if (COM.OStatus != COM.Status || COM.OErrorCode != COM.ErrorCode)
                {
                    iErrorCode = COM.ErrorCode;
                    iStatus = COM.Status;
                    COM.OStatus = COM.Status;
                    COM.OErrorCode = COM.ErrorCode;
                }
            }
        }

        /// <summary>
        /// 获取收款状态
        /// </summary>
        /// <param name="tagErrorCode">错误代码</param>
        /// <param name="tagStatus">状态代码</param>
        public string ShowStatus(int tagErrorCode, int tagStatus)
        {
            if (tagErrorCode == 0)
            {
                switch (tagStatus)
                {
                    case 0: return "Normal";
                    case 1: return "ACCEPTING";
                    case 2: return "Accept Command Ready";
                    case 3: return "STOP Accept Command Read";
                    case 4: return "Accept End";
                    case 5: return "Accept End ( Error)";
                    case 7: return "Accept (Hold)";
                }
            }
            else
            {
                switch (tagErrorCode)
                {
                    case 1: return "STACKER FULL!...";
                    case 2: return "STACKER OPEN!...";
                    case 3: return "JAM IN ACCEPTOR!...";
                    case 4: return "JAM IN STACKER!...";
                    case 5: return "PAUSE!...";
                    case 6: return "CHEATED!...";
                    case 11: return "Stack Motor Failure!...";
                    case 12: return "Transport ( feed ) motor speed failure!...";
                    case 13: return "Transport ( feed ) motor failure!...";
                    case 14: return "Solenoid failure!...";
                    case 15: return "PB unit failure!...";
                    case 16: return "Cash Box Not Ready!...";
                    case 17: return "Validator head remove!...";
                    case 18: return "Boot ROM failure!...";
                    case 19: return "External ROM failure!...";
                    case 20: return "RAM failure!...";
                    case 21: return "External ROM writing failure!...";
                    case 70: return "Rejecting:Insertion error";
                    case 71: return "Rejecting:Mag error";
                    case 72: return "Rejecting:Reject action due to residual bills, etc.(in the head unit of ACCEPTOR)";
                    case 73: return "Rejecting:Correction error/ Magnification error";
                    case 74: return "Rejecting:Transport error";
                    case 75: return "Rejecting:Validation error of bill denomination";
                    case 76: return "Rejecting:Photo pattern error";
                    case 77: return "Rejecting:Photo level error";
                    case 78: return "Rejecting:Return by INHIBIT: Error of insertion direction / Error of bill denomination";
                    case 79: return "Rejecting:No command sent in response to ESCROW";
                    case 80: return "Rejecting:Operation error";
                    case 81: return "Rejecting:Reject action due to residual bills, etc. (in the stacker)";
                    case 82: return "Rejecting:Bill Length error";
                    case 83: return "Rejecting:Photo pattern error";
                    case 84: return "Rejecting:Genuine bill feature error";
                    case 99: return "ID-003 OFF LINE!...";
                    case 100: return "Unknow Error!...";
                }
            }
            return "";
        }

        /// <summary>
        /// 返回总金额
        /// </summary>
        /// <returns></returns>
        public string ShowValue()
        {
            return COM.Value[6].ToString();
        }
    }
}
