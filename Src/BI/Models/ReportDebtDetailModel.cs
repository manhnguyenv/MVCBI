using BI.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BI.Models
{
  public class ReportDebtDetailModel
  {
    public ReportDebtDetailModel()
    {
    }

    public ReportDebtDetailViewModel GetAll(
        string userId,
        string customerId,
        string maDonVi,
        string startDate,
        string endDate,
        string chiTietTheoHH,
        bool? mauBaoCao,
        bool? ngoaiTe
        )
    {
      List<ReportDebtDetail> listDebtDetail = new List<ReportDebtDetail>();
      List<ReportDebtDetailSummary> listDebtDetailSummary = new List<ReportDebtDetailSummary>();

      var dBg = DataHelper.GetDateFromNgayThangNam(startDate);
      var dTo = DataHelper.GetDateFromNgayThangNam(endDate);

      // SET DATEFORMAT DMY;
      // EXEC sp_arso1 '131%','%','01/01/2018','30/06/2018','04'

      var ds = new DataSet();

      var connectionString = DataHelper.BuildDynamicConnectionString(ConfigurationManager.ConnectionStrings["SM17ConnectionString"].ConnectionString, BackEndSessions.CurrentUser.Tenant);

      using (SqlConnection dbConnection = new SqlConnection(connectionString))
      {
        using (SqlCommand dbCommand = new SqlCommand("sp_arso1", dbConnection))
        {
          dbCommand.CommandType = CommandType.StoredProcedure;

          dbCommand.Parameters.AddWithValue("@cAcc", userId);
          dbCommand.Parameters.AddWithValue("@cCust", customerId);
          dbCommand.Parameters.AddWithValue("@dBg", dBg.ToShortDateString());
          dbCommand.Parameters.AddWithValue("@dTo", dTo.ToShortDateString());
          dbCommand.Parameters.AddWithValue("@cUnit", maDonVi);

          dbConnection.Open();

          SqlDataReader reader = dbCommand.ExecuteReader();

          while (reader.Read())
          {
            //Return object of sp_Arso1
            listDebtDetail.Add(new ReportDebtDetail()
            {
              Stt_rec = SqlDataReaderExt.CheckNull<string>(reader["Stt_rec"]),
              Ma_ct0 = SqlDataReaderExt.CheckNull<string>(reader["Ma_ct0"]),
              Stt_ct_nkc = SqlDataReaderExt.CheckNull<decimal>(reader["Stt_ct_nkc"]),
              Ngay_ct = SqlDataReaderExt.CheckNull<DateTime>(reader["Ngay_ct"]),
              So_ct = SqlDataReaderExt.CheckNull<string>(reader["So_ct"]),
              Ma_vv = SqlDataReaderExt.CheckNull<string>(reader["Ma_vv"]),
              tk = SqlDataReaderExt.CheckNull<string>(reader["Tk"]),
              Tk_du = SqlDataReaderExt.CheckNull<string>(reader["Tk_du"]),
              Ps_no = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_no"]),
              Ps_co = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_co"]),
              Ma_nt = SqlDataReaderExt.CheckNull<string>(reader["Ma_nt"]),
              Ty_gia = SqlDataReaderExt.CheckNull<decimal>(reader["Ty_gia"]),
              Ps_no_nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_no_nt"]),
              Ps_co_nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_co_nt"]),
              Dien_giai = SqlDataReaderExt.CheckNull<string>(reader["Dien_giai"]),
              Ong_ba = SqlDataReaderExt.CheckNull<string>(reader["Ong_ba"]),
              Ma_ct = SqlDataReaderExt.CheckNull<string>(reader["Ma_ct"]),
              So_luong = SqlDataReaderExt.CheckNull<decimal>(reader["So_luong"]),
              Gia = SqlDataReaderExt.CheckNull<decimal>(reader["Gia"]),
              Gia_nt = SqlDataReaderExt.CheckNull<decimal>(reader["Gia_nt"]),
              Tien = SqlDataReaderExt.CheckNull<decimal>(reader["Tien"]),
              Tien_nt = SqlDataReaderExt.CheckNull<decimal>(reader["Tien_nt"])
            });
          }

          reader.NextResult();

          while (reader.Read())
          {
            //Return object of
            listDebtDetailSummary.Add(new ReportDebtDetailSummary()
            {
              Tk = SqlDataReaderExt.CheckNull<string>(reader["Tk"]),
              Ten_Tk = SqlDataReaderExt.CheckNull<string>(reader["Ten_Tk"]),
              Du_No1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No1"]),
              Du_Co1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co1"]),
              Ps_No = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_No"]),
              Ps_Co = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_Co"]),
              Du_No2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No2"]),
              Du_Co2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co2"]),
              Du_No_Nt1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No_Nt1"]),
              Du_Co_Nt1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co_Nt1"]),
              Ps_No_Nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_No_Nt"]),
              Ps_Co_Nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_Co_Nt"]),
              Du_No_Nt2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No_Nt2"]),
              Du_Co_Nt2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co_Nt2"])
            });
          }

          dbConnection.Close();
        }
      }

      return new ReportDebtDetailViewModel()
      {
        ListDebtDetail = listDebtDetail,
        ListDebtDetailSummary = listDebtDetailSummary
      };
    }
  }
}
