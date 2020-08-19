namespace MemerAPI.Models
{
  /// <summary>
  /// Model containing info about a meme
  /// </summary>
  public class MemeInfo
  {
    /// <value>URI of the image</value>
    public string URI { get; set; }

    /// <value>Alternative text which can be displayed if image cannot be loaded</value>
    public string Alt { get; set; }

    /// <value>Name of the image (most often just the image's heading)</value>
    public string Name { get; set; }
  }
}
