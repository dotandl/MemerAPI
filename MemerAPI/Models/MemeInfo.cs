namespace MemerAPI.Models
{
  public enum MediaType
  {
    Image,
    Gif,
    Video
  }

  /// <summary>
  /// Model containing info about a meme
  /// </summary>
  public class MemeInfo
  {
    /// <value>URI to the page with the meme</value>
    public string ViewURI { get; set; }

    /// <value>URI of the image</value>
    public string URI { get; set; }

    /// <value>Alternative text which can be displayed if image cannot be loaded</value>
    public string Alt { get; set; }

    /// <value>Name of the image (most often just the image's heading)</value>
    public string Name { get; set; }

    /// <value>Type of the element (image, git, video)</value>
    public MediaType Type { get; set; }

    /// <summary>
    /// Method preparing a meme object
    /// </summary>
    /// <returns>A meme object</returns>
    public object Get() => new
    {
      ViewURI,
      URI,
      Alt,
      Name,
      Type = Type.ToString().ToLower()
    };
  }
}

// TODO: remove all Get() methods from models
