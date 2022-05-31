using System;

namespace Swissbot
{

    [Serializable]
    public class NoAPIKeyException : Exception
    {
        public NoAPIKeyException() { }
        public NoAPIKeyException(string message) : base(message) { }
        public NoAPIKeyException(string message, Exception inner) : base(message, inner) { }
        protected NoAPIKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class NoCommandsException : Exception
    {
        public NoCommandsException() { }
        public NoCommandsException(string message) : base(message) { }
        public NoCommandsException(string message, Exception inner) : base(message, inner) { }
        protected NoCommandsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}