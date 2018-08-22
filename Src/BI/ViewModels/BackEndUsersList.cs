using System.Collections.Generic;
using VASJ.BI.Helpers;
using VASJ.BI.Models;

namespace VASJ.BI.ViewModels
{
    public class BackEndUsersList
    {
        [DataAnnotationsDisplay("Username")]
        [DataAnnotationsStringLengthMax(255)]
        public string Username { get; set; }

        [DataAnnotationsDisplay("FullName")]
        [DataAnnotationsStringLengthMax(255)]
        public string FullName { get; set; }

        [DataAnnotationsDisplay("SubscriptionEmail")]
        [DataAnnotationsStringLengthMax(255)]
        public string Email { get; set; }

        [DataAnnotationsDisplay("Group")]
        public int? GroupId { get; set; }

        public List<User> UserList { get; set; }
    }
}