using MemerAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests
{
  [TestClass]
  public class Success_Test
  {
    private readonly Success _success;

    public Success_Test() =>
      _success = new Success
      {
        Result = new
        {
          FirstProp = 1,
          SecondProp = "asd"
        }
      };

    [TestMethod]
    public void Get_ResultMergedObject()
    {
      object obj = new
      {
        Error = 0,
        FirstProp = 1,
        SecondProp = "asd"
      };

      AssertJson.AreEqual(obj, _success.Get());
    }
  }
}
