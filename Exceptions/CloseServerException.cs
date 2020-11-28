using System;
using System.Runtime.Serialization;


namespace Exceptions
{
    public class CloseServerException : Exception
    {
        public CloseServerException() : base() { }
        public CloseServerException(string message) : base(message) { }
        public CloseServerException(string message, Exception inner) : base(message, inner) { }

        protected CloseServerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
