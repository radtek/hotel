using System;

namespace CommonLibrary.exception
{
    [Serializable]
    public class CreateXmlException : ApplicationException
    {
        public CreateXmlException()
            : base()
        {

        }

        public CreateXmlException(string msg)
            : base(msg)
        {

        }

        public CreateXmlException(string msg, Exception e)
            : base(msg, e)
        {

        }
    }
}
