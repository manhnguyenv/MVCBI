using System.Collections.Generic;
using VASJ.BI.Helpers;
using VASJ.BI.Models;

namespace VASJ.BI.ViewModels
{
    public class BackEndTransactionLogList
    {
        #region Filter

        [DataAnnotationsDisplay("TransationDate_TransSearch")]
        [DataAnnotationsStringLengthMax(12)]
        public string TransationDate { get; set; }

        [DataAnnotationsDisplay("TransationType_TransSearch")]
        public int? TransationType { get; set; }

        [DataAnnotationsDisplay("UserId_TransSearch")]
        [DataAnnotationsStringLengthMax(255)]
        public string UserId { get; set; }

        [DataAnnotationsDisplay("Nick_TransSearch")]
        [DataAnnotationsStringLengthMax(255)]
        public string Nick { get; set; }

        [DataAnnotationsDisplay("Reason_TransSearch")]
        [DataAnnotationsStringLengthMax(255)]
        public string Reason { get; set; }

        #endregion Filter

        public List<TransactionLog> TransactionLogs { get; set; }
    }
}