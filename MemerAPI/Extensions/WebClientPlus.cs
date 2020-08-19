using System;
using System.Net;

namespace MemerAPI.Extensions
{
  /// <summary>
  /// WebClient extensions adding ability to check the redirect URI (pls don't
  /// kill me for the name)
  /// </summary>
  public class WebClientPlus : WebClient
  {
    private Uri _responseURI;

    /// <value>
    /// The response URI (useful when server redirects to the other URI and you want to know it)
    /// </value>
    public Uri ResponseURI { get { return _responseURI; } }

    protected override WebResponse GetWebResponse(WebRequest request)
    {
      WebResponse wr = base.GetWebResponse(request);
      _responseURI = wr.ResponseUri;
      return wr;
    }

    protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
    {
      WebResponse wr = base.GetWebResponse(request, result);
      _responseURI = wr.ResponseUri;
      return wr;
    }
  }
}
