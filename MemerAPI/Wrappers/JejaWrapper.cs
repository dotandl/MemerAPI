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
  /// Wrapper for https://memy.jeja.pl
  /// </summary>
  public class JejaWrapper : IWrapper
  {
    private static readonly string _baseUrl = "https://memy.jeja.pl";
    private static readonly IDictionary<UriType, string> _uris =
      new Dictionary<UriType, string>
      {
        { UriType.Random, $"{_baseUrl}/losowe" }
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

      IElement picDiv = document.DocumentElement.QuerySelector("#wrapper-wrap .left");

      IHtmlImageElement img = (IHtmlImageElement)picDiv.QuerySelector(".left-wrap img");
      IHtmlAnchorElement a = (IHtmlAnchorElement)picDiv.QuerySelector("h2 a");

      if (img == null || a == null)
        throw new NotFoundException("Either \"img\" or \"a\" tag could not be found");

      return new MemeInfo
      {
        ViewURI = a.Href,
        URI = img.Source,
        Alt = img.AlternativeText,
        Name = a.TextContent
      };
    }
  }
}
