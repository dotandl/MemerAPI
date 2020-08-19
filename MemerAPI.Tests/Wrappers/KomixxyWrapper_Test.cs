using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Wrappers
{
  [TestClass]
  public class KomixxyWrapper_Test
  {
    [TestMethod]
    public async Task Random_ResultMemeInfo()
    {
      MemeInfo meme = await KomixxyWrapper.Random();

      Assert.IsTrue(new Regex(@"^https?:\/\/komixxy.pl\/.+$")
        .IsMatch(meme.ViewURI));

      Assert.IsTrue(new Regex(@"^https?:\/\/komixxy.pl\/.+$")
        .IsMatch(meme.URI));
    }
  }
}
