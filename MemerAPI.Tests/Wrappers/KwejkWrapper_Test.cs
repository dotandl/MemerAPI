using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Exceptions;
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
      MemeInfo meme;

      // Try-catch handles the NotFoundException thrown when e.g. site displays
      // video instead of image
      try
      {
        meme = await _wrapper.RandomAsync();
      }
      catch (NotFoundException)
      {
        await Random_ResultMemeInfo();
        return;
      }

      // Due to the Kwejk random image page working, image URI and view URI
      // are the same
      Assert.AreEqual(meme.URI, meme.ViewURI);

      Assert.IsTrue(new Regex(@"^https?:\/\/.+\.kwejk\.pl\/.+$").IsMatch(meme.URI));
    }
  }
}
