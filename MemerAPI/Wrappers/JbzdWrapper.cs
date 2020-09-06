using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MemerAPI.Exceptions;
using MemerAPI.Models;

/*
 * Note:
 * Jbzd's random image page needs the user to be logged in, so the wrapper
 * uses a workaround - it means it gets a subpage of the homepage
 * (e.g. https://jbzd.com.pl/str/4) and gets a one of the images on that page
 * (e.g. the second one). The workaround is called pseudo-random.
 */

namespace MemerAPI.Wrappers
{
  public class JbzdWrapper : Wrapper
  {
    /// <summary>
    /// The const that constraints the max subpage the wrapper can get a random
    /// image from (default 50)
    /// </summary>
    private const byte _maxSubpageNumber = 50;

    private class PseudoRandomImage
    {
      public int NthChild { get; set; }
      public string Site { get; set; }
    }

    protected override string _baseUrl { get; }
    protected override IDictionary<UriType, string> _uris { get; }

    public JbzdWrapper()
    {
      _baseUrl = "https://jbzd.com.pl";

      _uris = new Dictionary<UriType, string>
      {
        { UriType.Page, _baseUrl + "/str/{0}" }
      };
    }

    private PseudoRandomImage GetPseudoRandomImage()
    {
      Random rand = new Random();

      int subpage = rand.Next(1, _maxSubpageNumber),
          nthImage = rand.Next(0, 7); // There are max 8 images on the page

      return new PseudoRandomImage
      {
        NthChild = nthImage,
        Site = subpage == 1 ? _baseUrl : string.Format(_uris[UriType.Page], subpage)
      };
    }

    public override async Task<MemeInfo> RandomAsync()
    {
      PseudoRandomImage image = GetPseudoRandomImage();

      WebClient wc;
      string html;

      try
      {
        wc = new WebClient();
        html = await wc.DownloadStringTaskAsync(image.Site);
      }
      catch (WebException ex)
      {
        throw new ServiceOrConnectionException("Could not load the page", ex);
      }

      IConfiguration config = Configuration.Default;
      IBrowsingContext context = BrowsingContext.New(config);
      IDocument document = await context.OpenAsync(req => req.Content(html).Address(_baseUrl));

      IElement picDiv = document.DocumentElement.QuerySelectorAll(
        "#content-container article .article-content")[image.NthChild];

      IHtmlImageElement img = (IHtmlImageElement)picDiv
        .QuerySelector(".article-image img");
      IHtmlAnchorElement a = (IHtmlAnchorElement)picDiv
        .QuerySelector(".article-title a");
      IHtmlSourceElement src = (IHtmlSourceElement)picDiv
        .QuerySelector(".article-image video > source");

      if ((img == null && src == null) || a == null)
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
          Name = a.TextContent.Trim(),
          Type = MediaType.Video
        };
      }
      else
      {
        meme = new MemeInfo
        {
          ViewURI = a.Href,
          URI = img.Source,
          Alt = img.AlternativeText,
          Name = a.TextContent.Trim(),
          Type = MediaType.Image
        };
      }

      return meme;
    }
  }
}
