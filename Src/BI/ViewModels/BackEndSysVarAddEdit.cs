using System;
using System.ComponentModel.DataAnnotations;
using VASJ.BI.Helpers;

namespace VASJ.BI.ViewModels
{
    public class BackEndSysVarAddEdit
    {
        public string Sys_Id { get; set; }

        [DataAnnotationsDisplay("Sys_Var")]
        [DataAnnotationsStringLengthMax(20, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMessageSysVarCannotBeMoreThanNCharacters")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string Sys_Var { get; set; }

        [DataAnnotationsDisplay("Sys_Type")]
        [DataAnnotationsStringLengthMax(1, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMessageSysTypeCannotBeMoreThanNCharacters")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string Sys_Type { get; set; }

        [DataAnnotationsDisplay("Sys_Value")]
        [DataAnnotationsRequired]
        [RegularExpression(@"^\d+(\.\d{0,3})?$", ErrorMessage = "Không thể chứa nhiều hơn 3 chữ số sau dấu chấm.")]
        [Range(0.001, Int32.MaxValue)]
        public decimal? Sys_Value { get; set; }

        [DataAnnotationsDisplay("Sys_Name")]
        [DataAnnotationsStringLengthMax(50, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMessageSysNameCannotBeMoreThanNCharacters")]
        [DataAnnotationsOnlyVietnameseCharacters]
        public string Sys_Name { get; set; }

        [DataAnnotationsDisplay("IsEditable")]
        public bool IsEditable { get; set; }

        public BackEndSysVarAddEdit()
        {
            IsEditable = true;
        }
    }
}