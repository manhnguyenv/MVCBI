using BI.Helpers;

namespace BI.ViewModels
{
  public class BackEndImpersonate
  {
    [DataAnnotationsDisplay("Username")]
    [DataAnnotationsRequired]
    public string Username { get; set; }
  }
}
