using System;

namespace JXCMS.Core.Exception
{
    public class CMSException : System.Exception
    {
        public CMSException() : base() { }

        public CMSException(string message) : base(message) { }
    }
}