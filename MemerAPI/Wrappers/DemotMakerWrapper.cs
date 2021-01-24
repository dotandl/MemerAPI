using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MemerAPI.Exceptions;
using MemerAPI.Models;

namespace MemerAPI.Wrappers
{
  /// <summary>
  /// Wrapper for http://demotmaker.com.pl
  /// /// </summary>
  public class DemotMakerWrapper : Wrapper
  {
    protected override string _baseUrl { get; }
    protected override IDictionary<UriType, string> _uris { get; }

    public DemotMakerWrapper()
    {
      _baseUrl = "http://demotmaker.com.pl";

      _uris = new Dictionary<UriType, string>
      {
        { UriType.Page, $"{_baseUrl}/?lang=pl" }
      };
    }

    public async Task<MemeInfo> GenerateAsync(
      string title,
      string description,
      byte[] image,
      string imageFilename)
    {
      HttpClient hc;
      string html;

      try
      {
        hc = new HttpClient();

        MultipartFormDataContent content = new MultipartFormDataContent();
        content.Add(new StringContent(title), "tekst");
        content.Add(new StringContent(description), "opis");
        content.Add(new ByteArrayContent(image), "plik", imageFilename);

        HttpResponseMessage response =
          await hc.PostAsync(_uris[UriType.Page], content);

        html = await response.Content.ReadAsStringAsync();
      }
      catch (WebException ex)
      {
        throw new ServiceOrConnectionException("Could not load page", ex);
      }

      IConfiguration config = Configuration.Default;
      IBrowsingContext context = BrowsingContext.New(config);
      IDocument document = await context.OpenAsync(req => req.Content(html));

      IHtmlInputElement demotDiv = (IHtmlInputElement)document.DocumentElement
        .QuerySelector("form > input[type=\"text\"][name=\"nazwa\"]");

      if (demotDiv == null)
        throw new NotFoundException("\"input\" tag could not be found");

      MemeInfo meme = new MemeInfo
      {
        ViewURI = demotDiv.Value,
        URI = demotDiv.Value,
        Alt = title,
        Name = title,
        Type = MediaType.Image
      };

      return meme;
    }
  }
}
