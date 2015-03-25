using System;

namespace CommonLibrary.exception
{
    public class BusinessException : ApplicationException
    {
        public BusinessException()
            : base()
        {

        }

        public BusinessException(string msg)
            : base(msg)
        {

        }

        public BusinessException(string msg, Exception e)
            : base(msg, e)
        {

        }
    }
}
