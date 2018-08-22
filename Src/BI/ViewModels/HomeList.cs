using System.Collections.Generic;
using System.Globalization;
using VASJ.BI.Models.DTO;

namespace VASJ.BI.ViewModels
{
    public class HomeList
    {
        public string Message { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool LoggedIn { get; set; }
    }
}