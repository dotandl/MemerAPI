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
  public class JejaWrapper : Wrapper
  {
    protected override string _baseUrl { get; }
    protected override IDictionary<UriType, string> _uris { get; }

    public JejaWrapper()
    {
      _baseUrl = "https://memy.jeja.pl";

      _uris = new Dictionary<UriType, string>
      {
        { UriType.Random, $"{_baseUrl}/losowe" }
      };
    }

    public override async Task<MemeInfo> RandomAsync()
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
