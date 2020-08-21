using System;
using System.Collections.Generic;
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
  public interface IWrapper
  {
    /// <summary>
    /// Base URL of the external service
    /// </summary>
    private static readonly string _baseUrl;

    /// <summary>
    /// Dictionary containing bindings between the services' URIs and their types
    /// </summary>
    private static readonly IDictionary<UriType, string> _uris;

    /// <summary>
    /// Method getting the random image from the service
    /// </summary>
    /// <returns>Info about the meme</returns>
    public static MemeInfo Random() => throw new NotImplementedException();
  }
}
