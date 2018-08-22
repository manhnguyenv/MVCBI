using BI.Helpers;

namespace BI.ViewModels
{
  public class BackEndGroupsEdit
  {
    [DataAnnotationsDisplay("GroupName")]
    [DataAnnotationsRequired]
    [DataAnnotationsStringLengthMax(255)]
    public string GroupName { get; set; }
  }
}
