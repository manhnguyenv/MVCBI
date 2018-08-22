using BI.Models.POCO;
using System.Collections.Generic;
using System.Linq;

namespace BI.Helpers
{
  public class RefDataHelper
  {
    public static List<DmDvcs> GetAllItems_DmDvcs()
    {
      return AdoHelper2.ExecCachedListProc<DmDvcs>("sp_DmDvcs_select", false, false);
    }

    public static List<DmKh> GetAllItems_DmKh(string Ma_Dvcs)
    {
      var dictParams = new Dictionary<string, object>
            {
                { "Ma_Dvcs", Ma_Dvcs }
            };
      return new StoredProcedureFactory().GetListBy<DmKh>(dictParams, "sp_DmKh_Select").ToList();
    }

    public static List<DmTk> GetAllItems_DmTk(string Ma_Dvcs)
    {
      var dictParams = new Dictionary<string, object>
            {
                { "Ma_Dvcs", Ma_Dvcs }
            };
      return new StoredProcedureFactory().GetListBy<DmTk>(dictParams, "sp_DmTk_Select").ToList();
    }
  }
}
