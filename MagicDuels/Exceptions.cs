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
    public class SteamProfileNotFoundException : Exception
    {
        public SteamProfileNotFoundException() { }
        public SteamProfileNotFoundException(string message) : base(message) { }
        public SteamProfileNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected SteamProfileNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class BadSteamProfileDataException : Exception
    {
        public BadSteamProfileDataException() { }
        public BadSteamProfileDataException(string message) : base(message) { }
        public BadSteamProfileDataException(string message, Exception inner) : base(message, inner) { }
        protected BadSteamProfileDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
