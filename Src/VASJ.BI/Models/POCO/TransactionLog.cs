using System;

namespace VASJ.BI.Models.POCO
{
    public class TransactionLog
    {
        public long Id { get; set; }
        public DateTime? TransationDate { get; set; }
        public string ReceiveAccount { get; set; }
        public int? TransationType { get; set; }
        public decimal? Amount { get; set; }
        public string UserId { get; set; }
        public string Nick { get; set; }
        public string Reason { get; set; }
        public bool? Status { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}