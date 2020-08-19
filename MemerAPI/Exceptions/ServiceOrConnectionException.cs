using System;
using System.Runtime.Serialization;

namespace MemerAPI.Exceptions
{
  [Serializable]
  public class ServiceOrConnectionException : Exception
  {
    public ServiceOrConnectionException() { }
    public ServiceOrConnectionException(string message) : base(message) { }
    public ServiceOrConnectionException(string message, Exception inner) : base(message, inner) { }
    protected ServiceOrConnectionException(
        SerializationInfo info,
        StreamingContext context) : base(info, context) { }
  }
}
