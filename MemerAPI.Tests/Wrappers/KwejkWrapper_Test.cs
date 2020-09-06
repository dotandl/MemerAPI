using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Wrappers
{
  [TestClass]
  public class KwejkWrapper_Test
  {
    private KwejkWrapper _wrapper = new KwejkWrapper();

    [TestMethod]
    public async Task Random_ResultMemeInfo()
    {
      MemeInfo meme = await _wrapper.RandomAsync();

      // Due to the Kwejk random image page working, image URI and view URI
      // are the same
      Assert.AreEqual(meme.URI, meme.ViewURI);

      Assert.IsTrue(new Regex(@"^https?:\/\/.+\.kwejk\.pl\/.+$").IsMatch(meme.URI));
    }
  }
}
