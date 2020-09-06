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
  public class KwejkWrapper : Wrapper
  {
    protected override string _baseUrl { get; }
    protected override IDictionary<UriType, string> _uris { get; }

    public KwejkWrapper()
    {
      _baseUrl = "https://kwejk.pl";

      _uris = new Dictionary<UriType, string>
      {
        { UriType.Random, $"{_baseUrl}/losowy" }
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

      IElement picDiv = document.DocumentElement.QuerySelector(".media-element-wrapper");

      IHtmlImageElement img = (IHtmlImageElement)picDiv.QuerySelector(".figure-holder figure img");
      IHtmlHeadingElement h = (IHtmlHeadingElement)picDiv.QuerySelector(".content h1");
      IElement player = picDiv.QuerySelector(".figure-holder figure player");

      if ((player == null && img == null) || h == null)
        throw new NotFoundException(
          "Either \"img\", \"player\" or \"h\" tag could not be found");

      // Unfortunately on Kwejk random image page there's no link to the
      // original image page, so ViewURI = URI
      MemeInfo meme;

      if (player != null)
      {
        meme = new MemeInfo
        {
          ViewURI = player.GetAttribute("source"),
          URI = player.GetAttribute("source"),
          Alt = string.Empty,
          Name = h.TextContent.Trim(),
          Type = MediaType.Video
        };
      }
      else
      {
        meme = new MemeInfo
        {
          ViewURI = img.Source,
          URI = img.Source,
          Alt = img.AlternativeText,
          Name = h.TextContent.Trim(),
          Type = MediaType.Image
        };
      }

      return meme;
    }
  }
}
