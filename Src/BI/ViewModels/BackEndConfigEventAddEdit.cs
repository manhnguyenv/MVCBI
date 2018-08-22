using BI.Helpers;

namespace BI.ViewModels
{
  public class BackEndConfigEventAddEdit
  {
    public int Id { get; set; }

    [DataAnnotationsDisplay("EventName")]
    [DataAnnotationsRequired]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string EventName { get; set; }

    [DataAnnotationsDisplay("StartDate")]
    [DataAnnotationsRequired]
    public string StartDate { get; set; }

    [DataAnnotationsDisplay("EndDate")]
    [DataAnnotationsRequired]
    public string EndDate { get; set; }

    [DataAnnotationsDisplay("Status")]
    public int Status { get; set; } = 1;
  }
}
