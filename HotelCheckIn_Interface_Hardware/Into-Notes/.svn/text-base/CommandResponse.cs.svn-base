using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.Into_Notes
{
    public class CommandResponse
    {
    }

    /// <summary>
    /// CONTROLLER->ACCEPTOR
    /// </summary>
    public enum StatusRequest
    {
        STATUS_REQUEST = 0X11
    }

    /// <summary>
    /// ACCEPTOR->CONTROLLER
    /// </summary>
    public enum Status
    {
        /// <SUMMARY>
        /// 等待插入纸币的待机状态及可运作状态
        /// </SUMMARY>
        ENABLE_IDLING = 0X11,

        /// <SUMMARY>
        /// 进行纸币装入及识别的状态
        /// </SUMMARY>
        ACCEPTING = 0X12,

        /// <SUMMARY>
        /// 纸币识别完毕，等待CONTROLLER命令的状态。(纸币保持在ACCEPTOR内部)
        /// 附加1字节的[ESCROW DATA](收入币种)。
        /// 在保管暨代付款状态下，当3秒以内ACCEPTOR无法接收[STATUS REQUEST]时，
        /// 及发送[ESCROW]后10秒以内未由CONTROLLER发送OPERATION指令时，退还纸币。
        /// </SUMMARY>
        ESCROW = 0X13,

        /// <SUMMARY>
        /// 根据来自CONTROLLER的OPERATION COMMAND[STACK-1]及[STACK-2]指令将搬运纸币及收纳在堆积输送器的状态。
        /// 由于搬运中的不适当，有时也会在搬运途中退还纸币。
        /// 在该退还时，状态将由STACKING变为REJECTING，所以此时需要中断交易。
        /// </SUMMARY>
        STACKING = 0X14,

        /// <SUMMARY>
        /// 纸币收入确认信号。
        /// ACCEPTOR对于[VEND VALID]，在由CONTROLLER发送[ACK]之前保持状态。
        /// CONTROLLER根据[VEND VALID]进行信用提升。
        /// </SUMMARY>
        VEND_VALID = 0X15,

        /// <SUMMARY>
        /// 收纳纸币，从[VEND VALID]到可收入下一张纸币([ENABLE]状态)之前的状态。
        /// </SUMMARY>
        STACKED = 0X16,

        /// <SUMMARY>
        /// ACCEPTOR 的纸币识别结果为不能收入的纸币及根据来自CONTROLLER的[INHIBIT]
        /// 指令退还纸币的状态。
        /// 附加1字节的[REJECT DATA](退还内容)。
        /// </SUMMARY>
        REJECTING = 0X17,

        /// <SUMMARY>
        /// 针对[ESCROW]根据来自CONTROLLER的[RETURN]指令退还纸币的状态。
        /// </SUMMARY>
        RETURNING = 0X18,

        /// <SUMMARY>
        /// 针对[ESCROW]根据来自CONTROLLER的[HOLD]指令将纸币保留在ACCEPTOR内部的状态。
        /// </SUMMARY>
        HOLDING = 0X19,

        /// <SUMMARY>
        /// 根据来自CONTROLLER的[INHIBIT]指令，ACCEPTOR禁止收入纸币的状态。
        /// 或者根据[ENABLE/DISABLE]指令、拨动开头，收入币种全部DISABLE的状态及根据
        /// [DIRECTION]指令，全部收入方向为INHIBIT的状态。
        /// </SUMMARY>
        DISABLE_INHIBIT = 0X1A,

        /// <SUMMARY>
        /// 根据来自CONTROLLER的[RESET]，ACCEPTOR实行初始化动作的状态。
        /// </SUMMARY>
        INITIALIZE = 0X1B,

        //-----------------------------------6-2-2通电状态---------------------------------------

        /// <SUMMARY>
        /// ACCEPTOR在电源ON时，ACCEPTOR内部正常的状态
        /// </SUMMARY>
        POWERUP = 0X40,

        /// <SUMMARY>
        /// 电源ON时，在ACCEPTOR头搬运部(可退还的位置)残留着纸币的状态。
        /// 根据来自CONTROLLER的[RESET]指令，ACCEPTOR退还纸币，实行初始化。
        /// CONTROLLER方面正在交易中时，取消交易
        /// </SUMMARY>
        POWER_UP_WITH_BILL_IN_ACCEPTOR = 0X41,

        /// <SUMMARY>
        /// 电源ON时，在堆积输送器搬运部内部(不能退还的位置)残留着纸币的状态。
        /// 根据来自CONTROLLER的[RESET]指令，ACCEPTOR收纳纸币，实行初始化。
        /// 在交易中的[VEND VALID]等待下接收到本STATUS时，因为通过[RESET]指令继续收纳该纸币等，
        /// 所以，作为CONTROLLER方面的处理，能够结束中断的交易，给予信用。
        /// 并且，在使用POWER RECOVERY OPTION时，发出[RESET]指令后，将等待收纳该纸币后的[VEND VALID]，
        /// 通过[VEND VALID]的确认给予信用，结束交易
        /// </SUMMARY>
        POWER_UP_WITH_BILL_IN_STACKER = 0X42,


        //-----------------------------------6-2-3错误状态---------------------------------------

        /// <SUMMARY>
        /// 堆积输送器箱牌装满状态
        /// </SUMMARY>
        STACKER_FULL = 0X43,

        /// <SUMMARY>
        /// 堆积输送器门打开或者未安装堆积输送器箱
        /// </SUMMARY>
        STACKER_OPEN_OR_STACKER_BOX_REMOVE = 0X44,

        /// <SUMMARY>
        /// ACCEPTOR内部发生阻塞
        /// </SUMMARY>
        JAM_IN_ACCEPTOR = 0X45,

        /// <SUMMARY>
        /// 堆积输送器搬运部发生阻塞。
        /// 收纳时发生异常。
        /// </SUMMARY>
        JAM_IN_STACKER = 0X46,

        /// <SUMMARY>
        /// 由于在第1枚纸币收纳中或者搬运中插入了第2枚纸币而使ACCEPTOR无法运作的状态。
        /// (取出第2枚纸币后开始搬运。)
        /// </SUMMARY>
        PAUSE = 0X47,

        /// <SUMMARY>
        /// ACCEPTOR受到了可能是恶作剧的行为。
        /// </SUMMARY>
        CHEATED = 0X48,

        /// <SUMMARY>
        /// 由于ACCEPTOR故障、异常或者未正常安装而无法正常运作的状态。
        /// 附加1字节的[FAILURE DATA]
        /// </SUMMARY>
        FAILURE = 0X49,

        /// <SUMMARY>
        /// 通信数据发生错误
        /// </SUMMARY>
        COMMUNICATION_ERROR = 0X4A,

        /// <SUMMARY>
        /// 来自CONTROLLER的指令不是有效指令。(在非接收该指令的状态时，该指令将作为不对应情况发送。)
        /// 请通过[STATUS REQUEST] 确认ACCEPTOR的状态。
        /// </SUMMARY>
        INVALID_COMMAND = 0X4B

    }

    /// <summary>
    /// CONTROLLER->ACCEPTOR
    /// </summary>
    public enum OperationCommands
    {
        //-----------------------------------6-3操作命令(CONTROLLER->ACCEPTOR)---------------------------------------

        /// <SUMMARY>
        /// 指在将ACCEPTOR 重置的指令。ACCEPTOR在任何状态时均接收。
        /// 在电源ON后(通电状态)必须发送。
        /// </SUMMARY>
        RESET = 0X40,

        /// <SUMMARY>
        /// 将牌保管暨代付款状态的纸币搬运、收纳在堆积输送器部。
        /// ACCEPTOR在纸币通过堆积输送器杅时处于[VEND VALID]状态。
        /// 仅在[ESCROW]状态时有效
        /// </SUMMARY>
        STACK_1 = 0X41,

        /// <SUMMARY>
        /// 将处于保管暨代付款状态的纸币搬运、收纳在堆积输送器部。
        /// ACCEPTOR 在收纳纸币(按下的位置)时处于[VEND VALID]状态。
        /// 仅在[ESCROW]状态时有效
        /// </SUMMARY>
        STACK_2 = 0X42,

        /// <SUMMARY>
        /// 退还处于保管暨代付款状态的纸币
        /// 仅在[ESCROW]状态时有效
        /// </SUMMARY>
        RETURN = 0X43,

        /// <SUMMARY>
        /// 将处于保管暨代付款状态的纸币保留10秒。
        /// </SUMMARY>
        HOLD = 0X44,

        /// <SUMMARY>
        /// 使ACCEPTOR的状态保持3秒。要继续保持状态，必须重新发送[WAIT]指令。
        /// </SUMMARY>
        WAIT = 0X45,
    }

    /// <summary>
    /// 发送operation command 返回的命令
    /// </summary>
    public enum ResponseToOperationCommand
    {
        ACK = 0X50,
        INVALID_COMMAND = 0X4B
    }

    /// <summary>
    /// ack肯定应答
    /// </summary>
    public enum Ack
    {
        /// <SUMMARY>
        /// [ACCEPTOR->CONTROLLER]
        /// 对于来自CONTROLLER的[OPERATION COMMAND]的响应
        /// [CONTROLLER->ACCEPTOR]
        /// 对于来自ACCEPTOR的[VEND VALID]的响应
        /// </SUMMARY>
        ACK = 0X50
    }

    /// <summary>
    /// 发送ack时返回的命令
    /// </summary>
    public enum PollRequest
    {
        ENQ = 0X05
    }

    /// <summary>
    /// CONTROLLER->ACCEPTOR
    /// </summary>
    public enum SettingCommands
    {
        /// <SUMMARY>
        /// 设定各个币种的收入。
        /// </SUMMARY>
        ENABLE_DISABLE = 0XC0,

        /// <SUMMARY>
        /// 设定各个币种的识别水平
        /// </SUMMARY>
        SECURITY = 0XC1,

        /// <SUMMARY>
        /// 设定ACCEPTOR的COMMUNICATION MODE
        /// </SUMMARY>
        COMMUNICATION_MODE = 0XC2,

        /// <SUMMARY>
        /// 设为暂时禁止收入ACCEPTOR
        /// </SUMMARY>
        INHIBIT = 0XC3,

        /// <SUMMARY>
        /// 设定纸币的收入方向
        /// </SUMMARY>
        DIRECTION = 0XC4,

        /// <SUMMARY>
        /// 设定ACCEPTOR的可选功能
        /// </SUMMARY>
        OPTIONAL_FUNCTION = 0XC5
    }

    /// <summary>
    /// 发送settingcommand的返回命令
    /// </summary>
    public enum ResponseToSettingCommand
    {
        /// <SUMMARY>
        /// 设定各个币种的收入。
        /// </SUMMARY>
        ENABLE_DISABLE = 0XC0,

        /// <SUMMARY>
        /// 设定各个币种的识别水平
        /// </SUMMARY>
        SECURITY = 0XC1,

        /// <SUMMARY>
        /// 设定ACCEPTOR的COMMUNICATION MODE
        /// </SUMMARY>
        COMMUNICATION_MODE = 0XC2,

        /// <SUMMARY>
        /// 设为暂时禁止收入ACCEPTOR
        /// </SUMMARY>
        INHIBIT = 0XC3,

        /// <SUMMARY>
        /// 设定纸币的收入方向
        /// </SUMMARY>
        DIRECTION = 0XC4,

        /// <SUMMARY>
        /// 设定ACCEPTOR的可选功能
        /// </SUMMARY>
        OPTIONAL_FUNCTION = 0XC5
    }

    /// <summary>
    /// CONTROLLER->ACCEPTOR
    /// </summary>
    public enum SettingStatusRequests
    {
        /// <SUMMARY>
        /// 各个币种的收入设定状态的发送要求
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// 将通过[ENABLE/DISABLE]指令及拨动开关设定的收入币种的状态行为2字节的[ENABLE/DISABLE DATA]来附加
        /// </SUMMARY>
        ENABLE_DISABLE = 0X80,

        SECURITY = 0X81,

        /// <SUMMARY>
        /// 每个个币种的各识别水平设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        COMMUNICATION_MODE = 0X82,

        /// <SUMMARY>
        /// ACCEPTOR的收入禁止设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        INHIBIT = 0X83,

        /// <SUMMARY>
        /// 纸币收入方向设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        DIRECTION = 0X84,

        /// <SUMMARY>
        /// ACCEPTOR的MODEL/ID/VERSION发送要求
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        VERSION_REQUEST = 0X88,

        /// <SUMMARY>
        /// ACCEPTOR的BOOT VERSION 发送要求
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        BOOT_VERSION_REQUEST = 0X89,

        /// <SUMMARY>
        /// OPTIONAL FUNCTION指令设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        OPTIONAL_FUNCTION = 0X85,

        /// <SUMMARY>
        /// [ESCROW DATA]的内容 (DENOMINATION DATA)的发送要求(PLUG&PLAY)功能
        /// 响应：DENOMINATION DATA(ACCEPTOR->CONTROLLER)
        /// 从[ESCROW DATA]61H依次作为连续的数据发送。
        /// </SUMMARY>
        CURRENCY_ASSIGN_REQUEST = 0X8A
    }

    /// <summary>
    /// 发送SettingStatusRequest返回的命令
    /// </summary>
    public enum SettingStatus
    {
        /// <SUMMARY>
        /// 各个币种的收入设定状态的发送要求
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// 将通过[ENABLE/DISABLE]指令及拨动开关设定的收入币种的状态行为2字节的[ENABLE/DISABLE DATA]来附加
        /// </SUMMARY>
        ENABLE_DISABLE = 0X80,

        SECURITY = 0X81,

        /// <SUMMARY>
        /// 每个个币种的各识别水平设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        COMMUNICATION_MODE = 0X82,

        /// <SUMMARY>
        /// ACCEPTOR的收入禁止设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        INHIBIT = 0X83,

        /// <SUMMARY>
        /// 纸币收入方向设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        DIRECTION = 0X84,

        /// <SUMMARY>
        /// OPTIONAL FUNCTION指令设定状态的发送要求。
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        OPTIONAL_FUNCTION = 0X85,

        /// <SUMMARY>
        /// ACCEPTOR的MODEL/ID/VERSION发送要求
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        VERSION_INFORMATION = 0X88,

        /// <SUMMARY>
        /// ACCEPTOR的BOOT VERSION 发送要求
        /// 响应：SETTING STATUS(ACCEPTOR->CONTROLLER)
        /// </SUMMARY>
        BOOT_VERSION_INFORMATION = 0X89,

        /// <SUMMARY>
        /// [ESCROW DATA]的内容 (DENOMINATION DATA)的发送要求(PLUG&PLAY)功能
        /// 响应：DENOMINATION DATA(ACCEPTOR->CONTROLLER)
        /// 从[ESCROW DATA]61H依次作为连续的数据发送。
        /// </SUMMARY>
        DENOMINATION_DATA = 0X8A
    }

    /// <summary>
    /// 托管状态
    /// </summary>
    public enum Escrow
    {
        M01 = 0x61,
        M02,
        YUAN5,
        YUAN10,
        YUAN20,
        YUAN50,
        YUAN100,
        YUAN200,
        YUAN500
    }

    /// <summary>
    /// 拒绝
    /// </summary>
    public enum Rejecting
    {
        INSERT_ERROR = 0X71,

        STRIP_ERROR = 0X72,

        RESIDUAL_ERROR_IN_HEAD = 0X73,

        RATE_ERROR = 0X74,

        CARRY_ERROR = 0X75,

        DECIDE_ERROR = 0X76,

        LIGHT_PICTURE_ERROR = 0X77,

        OPTICAL_FLAT_ERROR = 0X78,

        INHIBIT_ERROR = 0X79,

        UNKNOW_ERROR = 0X7A,

        ACTION_ERROR = 0X7B,
        RESIDUAL_ERROR_IN_TRANSPORT = 0X7C,

        LENGTH_ERROR = 0X7D,

        LIGHT_PICTURE_ERROR2 = 0X7E,

        MONEY_FEATURES_ERROR = 0X7F
    }

    /// <summary>
    /// 异常
    /// </summary>
    public enum Failure
    {
        STACK_MOTOR_FAILURE = 0XA2,

        TRANSPORT_MOTOR_SPEED_FAILURE = 0XA5,

        TRANSPORT_MOTOR_FAILURE = 0XA6,

        SOLENOID_FAILURE = 0XA8,

        PB_UNIT_FAILURE = 0XA9,

        CASH_BOX_NOT_READY = 0XAB,

        VALIDATOR_HEAD_REMOVE = 0XAF,

        BOOT_ROM_FAILURE = 0XB0,

        EXTERNAL_ROM_FAILURE = 0XB1,

        RAM_FAILURE = 0XB2,

        EXTERNAL_ROM_WRITING_FAILURE = 0XB3
    }

    /// <summary>
    /// 差错控制
    /// </summary>
    public sealed class CrcCcitt
    {
        private static ushort[] CCITT_TABLE = 
    {
        0x0000, 0x1189, 0x2312, 0x329B, 0x4624, 0x57AD, 0x6536, 0x74BF,
        0x8C48, 0x9DC1, 0xAF5A, 0xBED3, 0xCA6C, 0xDBE5, 0xE97E, 0xF8F7,
        0x1081, 0x0108, 0x3393, 0x221A, 0x56A5, 0x472C, 0x75B7, 0x643E,
        0x9CC9, 0x8D40, 0xBFDB, 0xAE52, 0xDAED, 0xCB64, 0xF9FF, 0xE876,
        0x2102, 0x308B, 0x0210, 0x1399, 0x6726, 0x76AF, 0x4434, 0x55BD,
        0xAD4A, 0xBCC3, 0x8E58, 0x9FD1, 0xEB6E, 0xFAE7, 0xC87C, 0xD9F5,
        0x3183, 0x200A, 0x1291, 0x0318, 0x77A7, 0x662E, 0x54B5, 0x453C,
        0xBDCB, 0xAC42, 0x9ED9, 0x8F50, 0xFBEF, 0xEA66, 0xD8FD, 0xC974,
        0x4204, 0x538D, 0x6116, 0x709F, 0x0420, 0x15A9, 0x2732, 0x36BB,
        0xCE4C, 0xDFC5, 0xED5E, 0xFCD7, 0x8868, 0x99E1, 0xAB7A, 0xBAF3,
        0x5285, 0x430C, 0x7197, 0x601E, 0x14A1, 0x0528, 0x37B3, 0x263A,
        0xDECD, 0xCF44, 0xFDDF, 0xEC56, 0x98E9, 0x8960, 0xBBFB, 0xAA72,
        0x6306, 0x728F, 0x4014, 0x519D, 0x2522, 0x34AB, 0x0630, 0x17B9,
        0xEF4E, 0xFEC7, 0xCC5C, 0xDDD5, 0xA96A, 0xB8E3, 0x8A78, 0x9BF1,
        0x7387, 0x620E, 0x5095, 0x411C, 0x35A3, 0x242A, 0x16B1, 0x0738,
        0xFFCF, 0xEE46, 0xDCDD, 0xCD54, 0xB9EB, 0xA862, 0x9AF9, 0x8B70,
        0x8408, 0x9581, 0xA71A, 0xB693, 0xC22C, 0xD3A5, 0xE13E, 0xF0B7,
        0x0840, 0x19C9, 0x2B52, 0x3ADB, 0x4E64, 0x5FED, 0x6D76, 0x7CFF,
        0x9489, 0x8500, 0xB79B, 0xA612, 0xD2AD, 0xC324, 0xF1BF, 0xE036,
        0x18C1, 0x0948, 0x3BD3, 0x2A5A, 0x5EE5, 0x4F6C, 0x7DF7, 0x6C7E,
        0xA50A, 0xB483, 0x8618, 0x9791, 0xE32E, 0xF2A7, 0xC03C, 0xD1B5,
        0x2942, 0x38CB, 0x0A50, 0x1BD9, 0x6F66, 0x7EEF, 0x4C74, 0x5DFD,
        0xB58B, 0xA402, 0x9699, 0x8710, 0xF3AF, 0xE226, 0xD0BD, 0xC134,
        0x39C3, 0x284A, 0x1AD1, 0x0B58, 0x7FE7, 0x6E6E, 0x5CF5, 0x4D7C,
        0xC60C, 0xD785, 0xE51E, 0xF497, 0x8028, 0x91A1, 0xA33A, 0xB2B3,
        0x4A44, 0x5BCD, 0x6956, 0x78DF, 0x0C60, 0x1DE9, 0x2F72, 0x3EFB,
        0xD68D, 0xC704, 0xF59F, 0xE416, 0x90A9, 0x8120, 0xB3BB, 0xA232,
        0x5AC5, 0x4B4C, 0x79D7, 0x685E, 0x1CE1, 0x0D68, 0x3FF3, 0x2E7A,
        0xE70E, 0xF687, 0xC41C, 0xD595, 0xA12A, 0xB0A3, 0x8238, 0x93B1,
        0x6B46, 0x7ACF, 0x4854, 0x59DD, 0x2D62, 0x3CEB, 0x0E70, 0x1FF9,
        0xF78F, 0xE606, 0xD49D, 0xC514, 0xB1AB, 0xA022, 0x92B9, 0x8330,
        0x7BC7, 0x6A4E, 0x58D5, 0x495C, 0x3DE3, 0x2C6A, 0x1EF1, 0x0F78
    };
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="aBytes"></param>
        /// <returns></returns>
        public static byte[] Crc(byte[] aBytes)
        {
            ushort uUshort = aBytes.Aggregate<byte, ushort>(0, (current, vByte) => (ushort)(CCITT_TABLE[(current ^ vByte) & 0xff] ^ (current >> 8)));
            byte[] send2 = BitConverter.GetBytes(uUshort);
            var send3 = new byte[aBytes.Length + send2.Length];
            aBytes.CopyTo(send3, 0);
            send2.CopyTo(send3, aBytes.Length);
            return send3;
        }
    }
}
