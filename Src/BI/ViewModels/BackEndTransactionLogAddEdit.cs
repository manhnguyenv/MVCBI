using BI.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BI.ViewModels
{
  public class BackEndTransactionLogAddEdit
  {
    public long Id { get; set; }

    [DataAnnotationsDisplay("ID_Fighting")]
    [DataAnnotationsRequired]
    public Guid Id_Fighting { get; set; }

    [DataAnnotationsDisplay("TransationDate")]
    [DataAnnotationsRequired]
    public string TransationDate { get; set; }

    [DataAnnotationsDisplay("ReceiveAccount")]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string ReceiveAccount { get; set; }

    [DataAnnotationsDisplay("TransationType")]
    public int? TransationType { get; set; }

    [DataAnnotationsDisplay("Amount")]
    [DataAnnotationsRequired]
    [RegularExpression(@"^\d+(\.\d{0,3})?$", ErrorMessage = "Không thể chứa nhiều hơn 3 chữ số sau dấu chấm.")]
    [Range(0.001, Int32.MaxValue)]
    public decimal? Amount { get; set; }

    [DataAnnotationsDisplay("UserId")]
    [DataAnnotationsRequired]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string UserId { get; set; }

    [DataAnnotationsDisplay("Nick")]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string Nick { get; set; }

    [DataAnnotationsDisplay("Reason")]
    [DataAnnotationsRequired]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string Reason { get; set; }

    [DataAnnotationsDisplay("Status")]
    public bool Status { get; set; }

    [DataAnnotationsDisplay("ErrorMessage")]
    [DataAnnotationsOnlyVietnameseCharacters]
    public string ErrorMessage { get; set; }

    [DataAnnotationsDisplay("CreatedAt")]
    public string Created_At { get; set; }

    [DataAnnotationsDisplay("UpdatedAt")]
    public string Updated_At { get; set; }

    public BackEndTransactionLogAddEdit()
    {
      Id_Fighting = Guid.NewGuid();
    }
  }
}
