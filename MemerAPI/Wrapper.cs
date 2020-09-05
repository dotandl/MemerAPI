using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemerAPI.Models;

namespace MemerAPI
{
  /// <summary>
  /// Type of the URI address (e.g. address to the random image)
  /// </summary>
  public enum UriType
  {
    Random,
    Page
  }

  /// <summary>
  /// Interface implemented by wrappers of the external services; containing
  /// methods getting the memes from the services
  /// </summary>
  public abstract class Wrapper
  {
    /// <summary>
    /// Base URL of the external service
    /// </summary>
    protected abstract string _baseUrl { get; }

    /// <summary>
    /// Dictionary containing bindings between the services' URIs and their types
    /// </summary>
    protected abstract IDictionary<UriType, string> _uris { get; }

    /// <summary>
    /// Method getting the random image from the service
    /// </summary>
    /// <returns>Info about the meme</returns>
    public abstract Task<MemeInfo> RandomAsync();
  }
}
