using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    public class LoginScreenExceptions : Exception
    {
        public LoginScreenExceptions() : base() { }
        public LoginScreenExceptions(string message) : base(message) { }
        public LoginScreenExceptions(string message, Exception inner) : base(message, inner) { }

        protected LoginScreenExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
