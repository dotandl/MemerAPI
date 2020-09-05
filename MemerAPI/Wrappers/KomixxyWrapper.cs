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
  public class KomixxyWrapper : Wrapper
  {
    protected override string _baseUrl { get; }
    protected override IDictionary<UriType, string> _uris { get; }

    public KomixxyWrapper()
    {
      _baseUrl = "https://komixxy.pl";

      _uris = new Dictionary<UriType, string>
      {
        { UriType.Random, $"{_baseUrl}/losuj" }
      };
    }

    public override async Task<MemeInfo> RandomAsync()
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

      return new MemeInfo
      {
        ViewURI = wc.ResponseURI.ToString(),
        URI = img.Source,
        Alt = img.AlternativeText,
        Name = h.TextContent
      };
    }
  }
}
