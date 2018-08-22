using BI.Helpers;
using BI.Models;
using System.Collections.Generic;

namespace BI.ViewModels
{
  public class BackEndGroupsList
  {
    [DataAnnotationsDisplay("GroupName")]
    [DataAnnotationsStringLengthMax(255)]
    public string GroupName { get; set; }

    public List<Group> GroupList { get; set; }
  }
}
