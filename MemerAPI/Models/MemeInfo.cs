namespace MemerAPI.Models
{
  /// <summary>
  /// Model containing info about a meme
  /// </summary>
  public class MemeInfo
  {
    /// <value>URI of the image</value>
    public string URI { get; set; }

    /// <value>Name of the image (most often just the image's heading)</value>
    public string Name { get; set; }
  }
}
