using VASJ.BI.Helpers;

namespace VASJ.BI.ViewModels
{
    public class BackEndImpersonate
    {
        [DataAnnotationsDisplay("Username")]
        [DataAnnotationsRequired]
        public string Username { get; set; }
    }
}