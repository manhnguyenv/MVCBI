using System;

namespace BI.Models.POCO
{
  public class LogSystem
  {
    public int id { get; set; }
    public int? user_id { get; set; }
    public string username { get; set; }
    public int? type { get; set; }
    public string description { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public DateTime? deleted_at { get; set; }
    public string record_id { get; set; }
    public string table_name { get; set; }
    public DateTime? created_on { get; set; }
  }
}
