using BI.Helpers;
using BI.Models.POCO;
using System.Collections.Generic;

namespace BI.ViewModels
{
  public class BackEndTenantAddEdit
  {
    public int Id { get; set; }

    [DataAnnotationsDisplay("UserName")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(255)]
    public string UserName { get; set; }

    [DataAnnotationsDisplay("Name")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsRequired]
    [DataAnnotationsStringLengthMax(255)]
    public string Name { get; set; }

    [DataAnnotationsDisplay("NameEn")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(255)]
    public string NameEn { get; set; }

    [DataAnnotationsDisplay("MST")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsRequired]
    [DataAnnotationsStringLengthMax(50)]
    public string MST { get; set; }

    [DataAnnotationsDisplay("Dvcs")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(50)]
    public string Dvcs { get; set; }

    [DataAnnotationsDisplay("Email")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(50)]
    public string Email { get; set; }

    [DataAnnotationsDisplay("Phone")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(50)]
    public string Phone { get; set; }

    [DataAnnotationsDisplay("Representative")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(50)]
    public string Representative { get; set; }

    [DataAnnotationsDisplay("DateIssue")]
    [DataAnnotationsStringLengthMax(10)]
    public string DateIssue { get; set; }

    [DataAnnotationsDisplay("DateActive")]
    [DataAnnotationsStringLengthMax(10)]
    public string DateActive { get; set; }

    [DataAnnotationsDisplay("Domain")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(255)]
    public string Domain { get; set; }

    [DataAnnotationsDisplay("Address")]
    [DataAnnotationsOnlyVietnameseCharacters]
    [DataAnnotationsStringLengthMax(255)]
    public string Address { get; set; }

    [DataAnnotationsDisplay("ServerName")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsRequired]
    public string ServerName { get; set; }

    [DataAnnotationsDisplay("DbName")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsRequired]
    public string DbName { get; set; }

    [DataAnnotationsDisplay("DbUserName")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsRequired]
    public string DbUserName { get; set; }

    [DataAnnotationsDisplay("DbPassword")]
    [DataAnnotationsStringLengthMax(50)]
    [DataAnnotationsRequired]
    public string DbPassword { get; set; }

    [DataAnnotationsDisplay("DbPort")]
    [DataAnnotationsStringLengthMax(50)]
    public string DbPort { get; set; }

    public bool ShowHideDbName { get; set; }

    public List<DmDvcs> DmDvcss { get; set; }

    public BackEndTenantAddEdit()
    {
      DmDvcss = new List<DmDvcs>();
      ShowHideDbName = false;
    }
  }
}
