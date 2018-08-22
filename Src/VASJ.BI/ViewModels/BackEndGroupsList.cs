using System.Collections.Generic;
using VASJ.BI.Helpers;
using VASJ.BI.Models;

namespace VASJ.BI.ViewModels
{
    public class BackEndGroupsList
    {
        [DataAnnotationsDisplay("GroupName")]
        [DataAnnotationsStringLengthMax(255)]
        public string GroupName { get; set; }

        public List<Group> GroupList { get; set; }
    }
}