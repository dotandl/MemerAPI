using System;
using MemerAPI.Models;

namespace MemerAPI
{
  /// <summary>
  /// Interface implemented by wrappers of the external services; containing
  /// methods getting the memes from the services
  /// </summary>
  public interface IWrapper
  {
    /// <summary>
    /// Method getting the random image from the service
    /// </summary>
    /// <returns>Info about the meme</returns>
    public static MemeInfo Random() => throw new NotImplementedException();
  }
}
