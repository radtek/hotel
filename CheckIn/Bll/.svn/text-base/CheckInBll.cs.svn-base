using System;
using CheckIn.BackService;

namespace CheckIn.Bll
{
    class CheckInBll
    {
        public static QueryNoAndPj GetCheckInInfo(string validationCode, string phoneNumber, DateTime checkinTime, DateTime checkoutTime)
        {
            InterFace Inf = new InterFace();
            QueryNoAndPj q = Inf.QueryCheckNo(new QueryCheckNoInfo()
                {
                    CheckNo = validationCode,
                    PhoneNumber = phoneNumber,
                    CheckinTime = checkinTime,
                    CheckoutTime = checkoutTime,
                });
            return q;
        }
    }
}
