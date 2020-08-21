using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Exceptions;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Wrappers
{
  [TestClass]
  public class JejaWrapper_Test
  {
    [TestMethod]
    public async Task Random_ResultMemeInfo()
    {
      MemeInfo meme;

      // Try-catch handles the NotFoundException thrown when e.g. site displays
      // video instead of image
      try
      {
        meme = await JejaWrapper.Random();
      }
      catch (NotFoundException)
      {
        await Random_ResultMemeInfo();
        return;
      }

      Assert.IsTrue(new Regex(@"^https?:\/\/memy.jeja.pl\/.+$")
        .IsMatch(meme.ViewURI));

      Assert.IsTrue(new Regex(@"^https?:\/\/.+\.jeja.pl/.+$")
        .IsMatch(meme.URI));
    }
  }
}
