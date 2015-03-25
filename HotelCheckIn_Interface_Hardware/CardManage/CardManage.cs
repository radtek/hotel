﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.exception;

namespace HotelCheckIn_Interface_Hardware.CardManage
{
    public class CardManage
    {
        /// <summary>
        /// 设置com端口
        /// </summary>
        public string ComPort { get; set; }

        /// <summary>
        /// 设置波特率
        /// </summary>
        public uint BaudRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CardManage()
        {
            ComPort = "com2";
            BaudRate = 9600;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comHandle">串口句柄</param>
        /// <returns>成功：true,失败：false</returns>
        public bool OpenComPort(ref IntPtr comHandle)
        {
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                return comHandle.ToInt32() != 0;
            }
            catch (Exception)
            {
                throw new BusinessException("打开串口失败！");
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="comHandle">串口句柄</param>
        /// <returns>成功：true,失败：抛出异常</returns>
        public bool CloseComPort(IntPtr comHandle)
        {
            try
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                return true;
            }
            catch (Exception)
            {
                throw new BusinessException("关闭串口失败！");
            }
        }

        ///  <summary>
        ///   复位命令
        ///  </summary>
        /// <param name="comHandle">串口句柄</param>
        /// <param name="b">
        ///  复位选项
        /// =0x30，初始化读卡器,有卡弹卡（不上传版本信息）
        /// =0x31，初始化读卡器,有卡回收（不上传版本信息）
        /// =0x32，初始化读卡器,有卡停在读磁卡位置（不上传版本信息）
        /// =0x33，初始化读卡器，有卡停在读IC 卡位置（不上传版本信息）
        /// =0x34，初始化读卡器，不动作(上传版本信息)
        ///  </param>
        ///  <returns></returns>
        public string Reset(IntPtr comHandle, byte b)
        {
            var recordInfo = new byte[200];
            try
            {
                int i = PackageK100Dll.M100A_EnterCard(comHandle, false, 0, 0x30, recordInfo);
                return "复位完成" + i;
            }
            catch (Exception)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw new BusinessException("复位异常！");
            }
        }

        /// <summary>
        /// 从卡盒到读写位置
        /// </summary>
        /// <returns></returns>
        public bool CardBoxPositionToRead()
        {
            var comHandle = new IntPtr();
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            int? data;
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("读取卡片位置失败！");
                }
                switch (cardStates[0])
                {
                    //通道无卡
                    case 48:
                        switch (cardStates[1])
                        {
                            case 48:
                                PackageK100Dll.M100A_CommClose(comHandle);
                                throw new BusinessException("卡箱无卡");
                            default:
                                //将卡槽卡片移动到读写卡位置
                                data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x35, recordInfo);
                                if (data != 0)
                                {
                                    PackageK100Dll.M100A_CommClose(comHandle);
                                    throw new BusinessException("移动卡片位置失败2");
                                }
                                PackageK100Dll.M100A_CommClose(comHandle);
                                return true;
                        }
                        break;
                    case 55:
                    case 50:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败1！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        return true;
                        break;
                    //通道有卡
                    default:
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("请取走卡片或者业务正在办理，请稍候！");
                }


            }
            catch (Exception ex)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw ex;
            }
        }

        /// <summary>
        /// 读写卡位置移动到出入卡口
        /// </summary>
        /// <returns></returns>
        public bool ReadPositionToEntrance()
        {
            var comHandle = new IntPtr();
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            int? data;
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("读取卡片位置失败！");
                }
                switch (cardStates[0])
                {
                    //通道无卡
                    case 48:
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("通道无卡！");
                    //读磁卡位置有卡
                    case 55:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x32, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        return true;

                    case 50:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("回收成功！");
                    //通道有卡
                    default:
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("请取走卡片或者业务正在办理，请稍候！");
                }
            }
            catch (Exception e)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw e;
            }

        }

        /// <summary>
        /// 出入口位置到读写卡位置
        /// </summary>
        /// <returns></returns>
        public bool EntrancePositionToRead()
        {
            var comHandle = new IntPtr();
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            int? data;
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("读取卡片位置失败！");
                }
                switch (cardStates[0])
                {
                    //通道无卡
                    case 48:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("回收成功！");
                    case 55:
                    case 50:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("回收成功！");
                    case 51:
                    case 52:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x30, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        return true;
                    //通道有卡
                    default:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("回收成功！");
                }
            }
            catch (Exception e)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw e;
            }
        }

        /// <summary>
        /// 读写卡位置到回收盒
        /// </summary>
        /// <returns></returns>
        public bool ReadPositionToRecoverBox()
        {
            var comHandle = new IntPtr();
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            int? data;
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("读取卡片位置失败！");
                }
                switch (cardStates[0])
                {
                    //通道无卡
                    case 48:
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("通道无卡！");
                    //读磁卡位置有卡
                    case 55:
                    case 50:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            throw new BusinessException("移动卡片位置失败！");
                        }
                        PackageK100Dll.M100A_CommClose(comHandle);
                        return true;
                    //通道有卡
                    default:
                        PackageK100Dll.M100A_CommClose(comHandle);
                        throw new BusinessException("请取走卡片或者业务正在办理，请稍候！");
                }
            }
            catch (Exception e)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw e;
            }
        }

        /// <summary>
        /// 读取卡片在机器里的位置
        /// </summary>
        /// <param name="cardStates">CardStates[0]，表示通道卡片位置，具体含义如下
        ///=0x30：通道无卡
        ///=0x31：读磁卡位置有卡
        ///=0x32：IC卡位置有卡
        ///=0x33：前端夹卡位置有卡
        ///=0x34：前端不夹卡位置有卡
        ///=0x35：卡不在标准位置(标准位置指的是上面5个位置
        ///（0x30~0x34）.当卡不在标准位置时，可以通过移动卡片
        ///命令将卡移动到标准位置)
        ///=0x36：卡片正在移动中
        ///=0x37：射频卡位置有卡
        ///CardStates[1]，表示卡箱卡片状态
        ///=0x30:卡箱无卡
        ///=0x31:卡箱卡片不足,提醒需要加卡
        ///=0x32:卡箱卡片足够</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        public int CheckCardPosition()
        {
            byte[] cardStates = new byte[2];
            string recordInfo;
            var _recordInfo = new byte[200];
            IntPtr comHandle = new IntPtr();
            bool bHasMacAddr = false;
            byte macAddr = 1;
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                var data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, _recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("读取卡位置失败！");
                }
                PackageK100Dll.M100A_CommClose(comHandle);
                return cardStates[0];
            }
            catch (Exception)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw;
            }
        }

        /// <summary>
        /// 自定义查询卡位置
        /// </summary>
        /// <param name="comHandle">串口句柄</param>
        /// <returns>返回卡位置
        ///=0x30：通道无卡
        ///=0x31：读磁卡位置有卡
        ///=0x32：IC卡位置有卡
        ///=0x33：前端夹卡位置有卡
        ///=0x34：前端不夹卡位置有卡
        ///=0x35：卡不在标准位置(标准位置指的是上面5个位置
        ///（0x30~0x34）.当卡不在标准位置时，可以通过移动卡片命令将卡移动到标准位置)
        ///=0x36：卡片正在移动中
        ///=0x37：射频卡位置有卡
        /// </returns>
        public byte CustomCheckCardPosition(IntPtr comHandle)
        {
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            try
            {
                var data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, recordInfo);
                if (data != 0)
                {
                    throw new BusinessException("读取卡位置失败！");
                }
                return cardStates[0];
            }
            catch (Exception e)
            {
                throw new BusinessException("读取卡位置异常！");
            }
        }

        /// <summary>
        /// 进卡设置，为立即返回方式，卡片一旦进入，则命令失效，卡片是否到具体位置，必须通过“读
        /// 取卡片在机器里的位置”命令来判断
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="enterType">具体有效值如下
        ///0x30: 禁止进卡(将取消先前设置好的进卡指令)
        ///0x31: 使能进卡，进卡后停卡在读磁卡位置
        ///0x32: 使能进卡，进卡后停卡在读IC卡位置
        ///0x33: 使能进卡，进卡后将卡回收到回收箱
        ///0x34: 使能进卡，进卡后停卡在前端夹卡位置
        ///0x35: 使能进卡，进卡后将卡弹出</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        public bool EnterCard()
        {
            var comHandle = new IntPtr();
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                var data = PackageK100Dll.M100A_EnterCard(comHandle, false, 0, 0x36, recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("！");
                }
                PackageK100Dll.M100A_CommClose(comHandle);
                return true;
                //recordInfo = Encoding.Default.GetString(_recordInfo);
            }
            catch (Exception)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw;
            }
        }

        ///  <summary>
        ///  卡片传动指令
        ///  </summary>
        ///  <param name="comHandle">已经打开的串口的句柄</param>
        ///  <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        ///  <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        ///  <param name="pm">卡片传动的选项，有效值如下：
        /// 0x30: 将卡片传动到读磁卡位置
        /// 0x31: 将卡片传动到IC卡位置
        /// 0x32: 将卡片传动到前端夹卡位置
        /// 0x33: 将卡片弹出
        /// 0x34: 将卡片回收到回收箱</param>
        /// <param name="position"></param>
        /// <returns>正确=0，错误=非0</returns>
        public int MoveCard(IntPtr comHandle, byte position)
        {
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    throw new BusinessException("打开串口失败！");
                }
                var data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, position, recordInfo);
                PackageK100Dll.M100A_CommClose(comHandle);
                return data;
            }
            catch (Exception)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                throw new BusinessException("打开串口异常！");
            }
        }



        /// <summary>
        /// 初始化机器
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool InitMachine(ref string message)
        {
            var comHandle = new IntPtr();
            var cardStates = new byte[2];
            var recordInfo = new byte[200];
            int? data;
            try
            {
                comHandle = PackageK100Dll.M100A_CommOpenWithBaud(ComPort, BaudRate);
                if (comHandle.ToInt32() == 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    message = "打开串口失败！";
                    return false;
                }
                data = PackageK100Dll.M100A_CheckCardPosition(comHandle, false, 0, cardStates, recordInfo);
                if (data != 0)
                {
                    PackageK100Dll.M100A_CommClose(comHandle);
                    message = "读取卡片位置失败";
                    return false;
                }
                switch (cardStates[0])
                {
                    //通道无卡
                    case 48:
                        break;
                    case 52:
                        break;
                    //通道有卡
                    default:
                        data = PackageK100Dll.M100A_MoveCard(comHandle, false, 0, 0x34, recordInfo);
                        if (data != 0)
                        {
                            PackageK100Dll.M100A_CommClose(comHandle);
                            message = "移动卡片位置失败！";
                            return false;
                        }
                        PackageK100Dll.M100A_EnterCard(comHandle, false, 0, 0x30, recordInfo);
                        break;
                }
                switch (cardStates[1])
                {
                    case 48:
                        PackageK100Dll.M100A_CommClose(comHandle);
                        message = "卡箱无卡";
                        return false;
                    default:
                        break;
                }
                PackageK100Dll.M100A_CommClose(comHandle);
                message = "打开串口成功！";
                return true;
            }
            catch (Exception e)
            {
                PackageK100Dll.M100A_CommClose(comHandle);
                message = "发生异常！" + e.Message;
                return false;
            }
        }

    }
}