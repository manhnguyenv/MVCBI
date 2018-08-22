using System.Collections.Generic;
using System.Web.Mvc;

namespace BI.ModuleConnectors
{
  public interface IModuleConnector
  {
    List<SelectListItem> GetSelectItemList();

    //string GetContent(HtmlHelper htmlHelper, FrontEndCmsPage model, string id);

    bool IsFileUsed(string filePath);
  }
}
