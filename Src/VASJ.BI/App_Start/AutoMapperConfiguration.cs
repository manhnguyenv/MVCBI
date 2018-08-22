using AutoMapper;
using VASJ.BI.Models;
using VASJ.BI.Models.DTO;

namespace VASJ.BI
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
