using MemerAPI.Models;

namespace MemerAPI.Wrappers
{
  /// <summary>
  /// Wrapper for https://komixxy.pl
  /// </summary>
  public class KomixxyWrapper : IWrapper
  {
    public static MemeInfo Random() => new MemeInfo // temporary just returning one of the images
    {
      URI = "https://komixxy.pl/1193844",
      Name = "Nutella"
    };
  }
}
