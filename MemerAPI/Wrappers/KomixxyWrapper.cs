using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MemerAPI.Models;

namespace MemerAPI.Wrappers
{
  /// <summary>
  /// Wrapper for https://komixxy.pl
  /// </summary>
  public class KomixxyWrapper : IWrapper
  {
    private static readonly string _baseUrl = "https://komixxy.pl";
    private static readonly IDictionary<UriType, string> _uris =
      new Dictionary<UriType, string>
      {
        { UriType.Random, $"{_baseUrl}/losuj" }
      };

    public static async Task<MemeInfo> Random()
    {
      string html;

      using (WebClient wc = new WebClient())
        html = await wc.DownloadStringTaskAsync(_uris[UriType.Random]);

      IConfiguration config = Configuration.Default.WithDefaultLoader();
      IBrowsingContext context = BrowsingContext.New(config);
      IDocument document = await context.OpenAsync(req => req.Content(html).Address(_baseUrl));

      IElement picDiv = document.DocumentElement.QuerySelector("#main_container .pic");

      IHtmlImageElement img =
        (IHtmlImageElement)picDiv.QuerySelector(".pic_image img");

      IHtmlHeadingElement h =
        (IHtmlHeadingElement)picDiv.QuerySelector("h1.picture");

      MemeInfo meme = new MemeInfo
      {
        URI = img.Source,
        Alt = img.AlternativeText,
        Name = h.TextContent
      };

      return meme;
    }
  }
}
