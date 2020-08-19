using System;
using MemerAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Models
{
  [TestClass]
  public class Error_Test
  {
    private readonly Error _err;

    public Error_Test() =>
      _err = new Error
      {
        Code = 2,
        Message = "An unexpected error occurred",
        Exception = new Exception("An error occurred")
      };

    [TestMethod]
    public void Get_InputDevTrue_ResultFullInfo()
    {
      object obj = new
      {
        Error = 2,
        Message = "An unexpected error occurred",
        Exception = new
        {
          Type = "System.Exception",
          Message = "An error occurred",
          StackTrace = (string)null
        }
      };

      AssertJson.AreEqual(obj, _err.Get(true));
    }

    [TestMethod]
    public void Get_InputDevFalse_ResultBasicInfo()
    {
      object obj = new
      {
        Error = 2,
        Message = "An unexpected error occurred"
      };

      AssertJson.AreEqual(obj, _err.Get(false));
    }
  }
}
