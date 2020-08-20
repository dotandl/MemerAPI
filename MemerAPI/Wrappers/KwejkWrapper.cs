using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MemerAPI.Exceptions;
using MemerAPI.Models;

namespace MemerAPI.Wrappers
{
  /// <summary>
  /// Wrapper for https://kwejk.pl
  /// </summary>
  public class KwejkWrapper : IWrapper
  {
    private static readonly string _baseUrl = "https://kwejk.pl";
    private static readonly IDictionary<UriType, string> _uris =
      new Dictionary<UriType, string>
      {
        { UriType.Random, $"{_baseUrl}/losowy" }
      };

    public static async Task<MemeInfo> Random()
    {
      WebClient wc;
      string html;

      try
      {
        wc = new WebClient();
        html = await wc.DownloadStringTaskAsync(_uris[UriType.Random]);
      }
      catch (WebException ex)
      {
        throw new ServiceOrConnectionException("Could not load the page", ex);
      }

      IConfiguration config = Configuration.Default;
      IBrowsingContext context = BrowsingContext.New(config);
      IDocument document = await context.OpenAsync(req => req.Content(html).Address(_baseUrl));

      IElement picDiv = document.DocumentElement.QuerySelector(".media-element-wrapper");

      IHtmlImageElement img = (IHtmlImageElement)picDiv.QuerySelector(".figure-holder figure img");
      IHtmlHeadingElement h = (IHtmlHeadingElement)picDiv.QuerySelector(".content h1");

      if (img == null || h == null)
        throw new NotFoundException("Either \"img\" or \"h\" tag could not be found");

      return new MemeInfo
      {
        // Unfortunately on Kwejk random image page there's no link to the
        // original image page, so ViewURI = URI
        ViewURI = img.Source,
        URI = img.Source,
        Alt = img.AlternativeText,
        Name = h.TextContent.Trim()
      };
    }
  }
}
