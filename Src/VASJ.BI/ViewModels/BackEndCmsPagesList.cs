﻿using System.Web;
using VASJ.BI.Helpers;

namespace VASJ.BI.ViewModels
{
    public class BackEndCmsPagesList
    {
        [DataAnnotationsDisplay("PageName")]
        [DataAnnotationsStringLengthMax(255)]
        public string PageName { get; set; }

        public HtmlString TreeTablePageList { get; set; }
    }
}