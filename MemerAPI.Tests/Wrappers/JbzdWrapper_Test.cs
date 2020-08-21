using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Exceptions;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Wrappers
{
  [TestClass]
  public class JbzdWrapper_Test
  {
    [TestMethod]
    public async Task Random_ResultMemeInfo()
    {
      MemeInfo meme;

      // Try-catch handles the NotFoundException thrown when e.g. site displays
      // video instead of image
      try
      {
        meme = await JbzdWrapper.Random();
      }
      catch (NotFoundException)
      {
        await Random_ResultMemeInfo();
        return;
      }

      Assert.IsTrue(new Regex(@"^https?:\/\/jbzd.com.pl\/.+$")
        .IsMatch(meme.ViewURI));

      Assert.IsTrue(new Regex(@"^https?:\/\/.+\.jbzd.com.pl/.+$")
        .IsMatch(meme.URI));
    }
  }
}
