using System;

namespace CommonLibrary.exception
{
    public class AnalysisXmlException : ApplicationException
    {
        public AnalysisXmlException()
            : base()
        {

        }

        public AnalysisXmlException(string msg)
            : base(msg)
        {

        }

        public AnalysisXmlException(string msg, Exception e)
            : base(msg, e)
        {

        }
    }
}
