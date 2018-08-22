using VASJ.BI.Helpers;

namespace VASJ.BI.ViewModels
{
    public class BackEndLogSystemAddEdit
    {
        public int id { get; set; }

        [DataAnnotationsDisplay("user_id")]
        public int? user_id { get; set; }

        [DataAnnotationsDisplay("AccountName")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string username { get; set; }

        [DataAnnotationsDisplay("record_id")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string record_id { get; set; }

        [DataAnnotationsDisplay("table_name")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string table_name { get; set; }

        [DataAnnotationsDisplay("type")]
        public int type { get; set; }

        [DataAnnotationsDisplay("description")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string description { get; set; }

        [DataAnnotationsDisplay("created_at")]
        public string created_at { get; set; }

        [DataAnnotationsDisplay("updated_at")]
        public string updated_at { get; set; }

        [DataAnnotationsDisplay("deleted_at")]
        public string deleted_at { get; set; }
    }
}