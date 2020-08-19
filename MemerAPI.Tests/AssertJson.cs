using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MemerAPI.Tests
{
  public class AssertJson
  {
    public static void AreEqual(object expected, object actual)
    {
      string expectedJson = JsonConvert.SerializeObject(expected);
      string actualJson = JsonConvert.SerializeObject(actual);

      Assert.AreEqual(expectedJson, actualJson);
    }
  }
}
