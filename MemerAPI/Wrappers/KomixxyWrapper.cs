using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MemerAPI.Exceptions;
using MemerAPI.Extensions;
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
      WebClientPlus wc;
      string html;

      try
      {
        wc = new WebClientPlus();
        html = await wc.DownloadStringTaskAsync(_uris[UriType.Random]);
      }
      catch (WebException ex)
      {
        throw new ServiceOrConnectionException("Could not load the page", ex);
      }

      IConfiguration config = Configuration.Default;
      IBrowsingContext context = BrowsingContext.New(config);
      IDocument document = await context.OpenAsync(req => req.Content(html).Address(_baseUrl));

      IElement picDiv = document.DocumentElement.QuerySelector("#main_container .pic");

      IHtmlImageElement img =
        (IHtmlImageElement)picDiv.QuerySelector(".pic_image img");

      IHtmlHeadingElement h =
        (IHtmlHeadingElement)picDiv.QuerySelector("h1.picture");

      if (img == null || h == null)
        throw new NotFoundException("Either \"img\" or \"h1\" tag could not be found");

      MemeInfo meme = new MemeInfo
      {
        ViewURI = wc.ResponseURI.ToString(),
        URI = img.Source,
        Alt = img.AlternativeText,
        Name = h.TextContent
      };

      return meme;
    }
  }
}
