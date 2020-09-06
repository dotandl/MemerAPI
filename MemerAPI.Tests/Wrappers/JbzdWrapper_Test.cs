using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Wrappers
{
  [TestClass]
  public class JbzdWrapper_Test
  {
    private JbzdWrapper _wrapper = new JbzdWrapper();

    [TestMethod]
    public async Task Random_ResultMemeInfo()
    {
      MemeInfo meme = await _wrapper.RandomAsync();

      Assert.IsTrue(new Regex(@"^https?:\/\/jbzd.com.pl\/.+$")
        .IsMatch(meme.ViewURI));

      Assert.IsTrue(new Regex(@"^https?:\/\/.+\.jbzd.com.pl/.+$")
        .IsMatch(meme.URI));
    }
  }
}
