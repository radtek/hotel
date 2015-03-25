using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class TradingInfoModel
    {/// <summary>
        /// 流水账号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 交易类型，1预授权，2预授权完成 
        /// </summary>
        public string TradingType { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNumber { get; set; }

        /// <summary>
        /// 入住订单号
        /// </summary>
        public string CheckOrderNumber { get; set; }

        /// <summary>
        /// 备用1
        /// </summary>
        public string Temp1 { get; set; }

        /// <summary>
        /// 备用2
        /// </summary>
        public string Temp2 { get; set; }

        /// <summary>
        /// 备用3
        /// </summary>
        public string Temp3 { get; set; }

        //------------------------------------------
        /// <summary>
        /// 返回码
        /// </summary>
        public string ReturnCode { get; set; }

        /// <summary>
        /// 银行行号
        /// </summary>
        public string BankNumber { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string Valid { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string LotNo { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string CertificateNo { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// 终端号
        /// </summary>
        public string TerminalNo { get; set; }

        /// <summary>
        /// 交易参考号
        /// </summary>
        public string TransactionReferenceNumber { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public string TradingDate { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public string TradingTime { get; set; }

        /// <summary>
        /// 授权号
        /// </summary>
        public string AuthorizationNumber { get; set; }
    }
}
