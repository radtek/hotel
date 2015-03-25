using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.exception
{
    public class PMSResponseErrException : ApplicationException
    {
        public PMSResponseErrException()
            : base()
        {

        }

        public PMSResponseErrException(string msg)
            : base(msg)
        {

        }

        public PMSResponseErrException(string msg, Exception e)
            : base(msg, e)
        {

        }
    }
}
