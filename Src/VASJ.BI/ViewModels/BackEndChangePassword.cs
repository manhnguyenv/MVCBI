using System.ComponentModel.DataAnnotations;
using VASJ.BI.Helpers;

namespace VASJ.BI.ViewModels
{
    public class BackEndChangePassword
    {
        [DataAnnotationsDisplay("CurrentPassword")]
        [DataAnnotationsRequired]
        [DataAnnotationsStringLengthMax(255)]
        public string CurrentPassword { get; set; }

        [DataAnnotationsDisplay("NewPassword")]
        [DataAnnotationsRequired]
        [DataAnnotationsStringLengthRange(8, 255)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "NewPasswordAndConfirmationPasswordDoNotMatch")]
        [DataAnnotationsDisplay("ConfirmationPassword")]
        [DataAnnotationsRequired]
        public string ConfirmationPassword { get; set; }
    }
}