using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Wrappers
{
  [TestClass]
  public class DemotMakerWrapper_Test
  {
    private DemotMakerWrapper _wrapper = new DemotMakerWrapper();

    [TestMethod]
    public async Task Generate_ResultMemeInfo()
    {
      const string title = "foo";

      MemeInfo meme = await _wrapper.GenerateAsync(
        title,
        "bar",
        File.ReadAllBytes("../../../Files/random-image.jpg"),
        "baz.jpg");

      Assert.AreEqual(meme.URI, meme.ViewURI);
      Assert.AreEqual(meme.Name, meme.Alt);

      Assert.AreEqual(title, meme.Name);

      Assert.IsTrue(new Regex(@"^http:\/\/demotmaker.com.pl\/obrazki\/.+\.jpg$")
        .IsMatch(meme.URI));
    }
  }
}
