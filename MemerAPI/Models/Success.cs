namespace MemerAPI.Models
{
  /// <summary>
  /// Class defining the request success
  /// </summary>
  public class Success
  {
    private dynamic _result;

    /// <value>A result value of the request</value>
    public dynamic Result { set { _result = value; } }

    /// <summary>
    /// Method preparing the success object (result object + error code = 0)
    /// </summary>
    /// <returns>The success object</returns>
    public object Get() =>
      TypeMerger.TypeMerger.Merge(new { error = 0 }, _result);
  }
}
