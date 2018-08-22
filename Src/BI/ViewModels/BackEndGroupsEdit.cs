using VASJ.BI.Helpers;

namespace VASJ.BI.ViewModels
{
    public class BackEndGroupsEdit
    {
        [DataAnnotationsDisplay("GroupName")]
        [DataAnnotationsRequired]
        [DataAnnotationsStringLengthMax(255)]
        public string GroupName { get; set; }
    }
}