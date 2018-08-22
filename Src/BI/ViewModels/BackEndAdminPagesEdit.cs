using System.Collections.Generic;
using VASJ.BI.Helpers;
using VASJ.BI.Models;

namespace VASJ.BI.ViewModels
{
    public class BackEndAdminPagesEdit
    {
        public int? PageId { get; set; }

        [DataAnnotationsDisplay("ParentPage")]
        public int? PageParentId { get; set; }

        [DataAnnotationsDisplay("PageName")]
        [DataAnnotationsRequired]
        [DataAnnotationsStringLengthMax(255)]
        public string PageName { get; set; }

        [DataAnnotationsDisplay("Url")]
        [DataAnnotationsStringLengthMax(1000)]
        public string Url { get; set; }

        [DataAnnotationsDisplay("Target")]
        [DataAnnotationsRequired]
        [DataAnnotationsStringLengthMax(255)]
        public string Target { get; set; }

        [DataAnnotationsDisplay("ShowInMenu")]
        public bool ShowInMenu { get; set; }

        [DataAnnotationsDisplay("Active")]
        public bool IsActive { get; set; }

        [DataAnnotationsDisplay("CssClass")]
        [DataAnnotationsStringLengthMax(255)]
        public string CssClass { get; set; }

        public List<GroupPermission> GroupsPermissions { get; set; }
    }
}