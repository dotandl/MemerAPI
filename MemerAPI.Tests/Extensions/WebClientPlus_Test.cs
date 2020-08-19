using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemerAPI.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemerAPI.Tests.Extensions
{
  [TestClass]
  public class WebClientPlus_Test
  {
    private WebClientPlus _wc;

    [TestInitialize]
    public void BeforeEachTest() => _wc = new WebClientPlus();

    [TestMethod]
    public void ResponseURI_InputHttpBinGoOrg_ResultGoogleCom()
    {
      _wc.DownloadString("https://httpbingo.org/redirect-to?url=https://google.com/");

      Assert.IsTrue(new Regex(@"^https?:\/\/(?:www\.)?google\.com\/?$")
        .IsMatch(_wc.ResponseURI.ToString()));
    }

    [TestMethod]
    public async Task Async_ResponseURI_InputHttpBinGoOrg_ResultGoogleCom()
    {
      await _wc.DownloadStringTaskAsync("https://httpbingo.org/redirect-to?url=https://google.com/");

      Assert.IsTrue(new Regex(@"^https?:\/\/(?:www\.)?google\.com\/?$")
        .IsMatch(_wc.ResponseURI.ToString()));
    }
  }
}
