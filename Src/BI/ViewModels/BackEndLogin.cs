using BI.Helpers;
using BI.Models.POCO;
using System.Collections.Generic;

namespace BI.ViewModels
{
  public class BackEndLogin
  {
    public List<DmDvcs> DmDvcss { get; set; }

    [DataAnnotationsDisplay("Username")]
    [DataAnnotationsRequired]
    public string Username { get; set; }

    [DataAnnotationsDisplay("Password")]
    [DataAnnotationsRequired]
    public string Password { get; set; }

    //[DataAnnotationsDisplay("Ma_Dvcs")]
    //[DataAnnotationsRequired]
    //public string Ma_Dvcs { get; set; }

    public string ReturnUrl { get; set; }

    public BackEndLogin()
    {
      DmDvcss = new List<DmDvcs>();
    }
  }
}
