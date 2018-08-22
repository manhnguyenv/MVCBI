using System.Collections.Generic;
using System.Web.Mvc;

namespace VASJ.BI.ModuleConnectors
{
    public interface IModuleConnector
    {
        List<SelectListItem> GetSelectItemList();

        //string GetContent(HtmlHelper htmlHelper, FrontEndCmsPage model, string id);

        bool IsFileUsed(string filePath);
    }
}