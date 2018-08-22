using AutoMapper;
using BI.Models;
using BI.Models.DTO;

namespace BI
{
  public class AutoMapperConfiguration
  {
    public static void Initialize()
    {
      Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<sp_Arso1, ReportDebtDetail>()
                  .ReverseMap();
        cfg.CreateMap<sp_Arcd1, ReportBalanceSheet>()
                  .ReverseMap();
        cfg.CreateMap<sp_Glcd2, ReportBalanceSheetAccount>()
              .ReverseMap();
      });
    }
  }
}
