using System;

namespace MemerAPI.Models
{
  /// <summary>
  /// Class defining a request error and allowing to generate basic and full
  /// info about the error
  /// </summary>
  public class Error
  {
    private byte _code;
    private string _message;
    private Exception _exception;

    /// <value>API's internal error code (not the HTTP status)</value>
    public byte Code { set => _code = value; }

    /// <value>Short message describing the error</value>
    public string Message { set => _message = value; }

    /// <value>Exception caused the error</value>
    public Exception Exception { set => _exception = value; }

    /// <summary>
    /// Method preparing the object with the basic error info
    /// </summary>
    /// <returns>The error object</returns>
    private object GetInfo() => new
    {
      Error = _code,
      Message = _message
    };

    /// <summary>
    /// Method preparing the object with the full error info
    /// </summary>
    /// <returns>The error object</returns>
    private object GetDevInfo() => new
    {
      Error = _code,
      Message = _message,
      Exception = new
      {
        Type = _exception.GetType().ToString(),
        Message = _exception.Message,
        StackTrace = _exception.StackTrace
      }
    };

    /// <summary>
    /// Method preparing the error info (full for development; otherwise basic)
    /// </summary>
    /// <param name="dev">Development environment</param>
    /// <returns>The error object</returns>
    public object Get(bool dev) => dev ? GetDevInfo() : GetInfo();
  }
}
