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
    private KomixxyWrapper _wrapper = new KomixxyWrapper();

    [TestMethod]
    public async Task Random_ResultMemeInfo()
    {
      MemeInfo meme = await _wrapper.RandomAsync();

      Assert.IsTrue(new Regex(@"^https?:\/\/komixxy.pl\/.+$")
        .IsMatch(meme.ViewURI));

      Assert.IsTrue(new Regex(@"^https?:\/\/komixxy.pl\/.+$")
        .IsMatch(meme.URI));
    }
  }
}
