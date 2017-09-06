using System;

namespace MagicDuels
{
    [Serializable]
    public class BadCardDataException : Exception
    {
        public BadCardDataException() { }
        public BadCardDataException(string message) : base(message) { }
        public BadCardDataException(string message, Exception inner) : base(message, inner) { }
        protected BadCardDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class BadSteamProfileException : Exception
    {
        public BadSteamProfileException() { }
        public BadSteamProfileException(string message) : base(message) { }
        public BadSteamProfileException(string message, Exception inner) : base(message, inner) { }
        protected BadSteamProfileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
