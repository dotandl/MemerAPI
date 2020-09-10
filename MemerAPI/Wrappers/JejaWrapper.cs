using System;
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

    private int RandomNthChild() => new Random().Next(0, 8);

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

      IElement picDiv = document.DocumentElement
        .QuerySelectorAll("#wrapper-wrap .left .ob-left-box-images")[RandomNthChild()];

      IHtmlImageElement img = (IHtmlImageElement)picDiv
        .QuerySelector(".left-wrap a img:last-child");
      IHtmlAnchorElement a = (IHtmlAnchorElement)picDiv
        .QuerySelector("h2 a");
      IHtmlInputElement input = (IHtmlInputElement)picDiv
        .QuerySelector(".left-wrap input[type=\"hidden\"]");
      IHtmlSourceElement src = (IHtmlSourceElement)picDiv
        .QuerySelector(".left-wrap video > source");

      if ((src == null && img == null) || a == null)
        throw new NotFoundException(
          "Either \"img\", \"source\" or \"a\" tag could not be found");

      MemeInfo meme;

      if (src != null)
      {
        meme = new MemeInfo
        {
          ViewURI = a.Href,
          URI = src.Source,
          Alt = string.Empty,
          Name = a.TextContent,
          Type = MediaType.Video
        };
      }
      else if (input != null)
      {
        meme = new MemeInfo
        {
          ViewURI = a.Href,
          URI = input.Value,
          Alt = img.AlternativeText,
          Name = a.TextContent,
          Type = MediaType.Gif
        };
      }
      else
      {
        meme = new MemeInfo
        {
          ViewURI = a.Href,
          URI = img.Source,
          Alt = img.AlternativeText,
          Name = a.TextContent,
          Type = MediaType.Image
        };
      }

      return meme;
    }
  }
}
