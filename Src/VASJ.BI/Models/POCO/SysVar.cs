namespace VASJ.BI.Models.POCO
{
    public class SysVar
    {
        public string Sys_Id { get; set; }
        public string Sys_Var { get; set; }
        public string Sys_Type { get; set; }
        public decimal? Sys_Value { get; set; }
        public string Sys_Name { get; set; }
        public bool? IsEditable { get; set; }
    }
}