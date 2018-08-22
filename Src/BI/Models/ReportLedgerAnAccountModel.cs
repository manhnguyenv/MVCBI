using BI.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BI.Models
{
  public class ReportLedgerAnAccountModel
  {
    public ReportLedgerAnAccountModel()
    {
    }

    public ReportLedgerAnAccountViewModel GetAll(
        string userId,
        string maDonVi,
        string startDate,
        string endDate,
        bool? mauBaoCao,
        bool? ngoaiTe
        )
    {
      List<ReportLedgerAnAccount> listLedgerAnAccount = new List<ReportLedgerAnAccount>();
      List<ReportLedgerAnAccountSummary> listLedgerAnAccountSummary = new List<ReportLedgerAnAccountSummary>();

      var dBg = DataHelper.GetDateFromNgayThangNam(startDate);
      var dTo = DataHelper.GetDateFromNgayThangNam(endDate);

      // SET DATEFORMAT DMY;
      // EXEC usp_Nkc_SoCaiMotTaiKhoanBak
      //   @_DocDate1 = '01/01/2018',
      //   @_DocDate2 = '10/01/2018',
      //   @_Account = '111',
      //   @_BranchCode = '04',
      //   @_IsCurrency = 1

      var ds = new DataSet();
      var connectionString = DataHelper.BuildDynamicConnectionString(ConfigurationManager.ConnectionStrings["SM17ConnectionString"].ConnectionString, BackEndSessions.CurrentUser.Tenant);

      using (SqlConnection dbConnection = new SqlConnection(connectionString))
      {
        using (SqlCommand dbCommand = new SqlCommand("usp_Nkc_SoCaiMotTaiKhoanBak", dbConnection))
        {
          dbCommand.CommandType = CommandType.StoredProcedure;

          dbCommand.Parameters.AddWithValue("@_Account", userId);
          dbCommand.Parameters.AddWithValue("@_DocDate1", dBg.ToShortDateString());
          dbCommand.Parameters.AddWithValue("@_DocDate2", dTo.ToShortDateString());
          dbCommand.Parameters.AddWithValue("@_BranchCode", maDonVi);

          dbConnection.Open();

          SqlDataReader reader = dbCommand.ExecuteReader();

          while (reader.Read())
          {
            //Return object of usp_Nkc_SoCaiMotTaiKhoanBak
            listLedgerAnAccount.Add(new ReportLedgerAnAccount()
            {
              Stt_rec = SqlDataReaderExt.CheckNull<string>(reader["Stt_rec"]),
              Ngay_ct = SqlDataReaderExt.CheckNullDate(reader["Ngay_ct"], CommonStrings.DateFormatDDMMYYYY),
              Ma_ct = SqlDataReaderExt.CheckNull<string>(reader["Ma_ct"]),
              So_ct = SqlDataReaderExt.CheckNull<string>(reader["So_ct"]),
              Ma_Kh = SqlDataReaderExt.CheckNull<string>(reader["Ma_Kh"]),
              Ten_Kh = SqlDataReaderExt.CheckNull<string>(reader["Ten_Kh"]),
              Dien_giai = SqlDataReaderExt.CheckNull<string>(reader["Dien_giai"]),
              Tk = SqlDataReaderExt.CheckNull<string>(reader["Tk"]),
              Tk_du = SqlDataReaderExt.CheckNull<string>(reader["Tk_du"]),
              Ps_no = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_no"]),
              Ps_co = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_co"]),
              Ps_no_nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_no_nt"]),
              Ps_co_nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_co_nt"]),
              Soft = SqlDataReaderExt.CheckNull<int>(reader["Soft"]),
              Ma_vv = SqlDataReaderExt.CheckNull<string>(reader["Ma_vv"]),
              Ma_nt = SqlDataReaderExt.CheckNull<string>(reader["Ma_nt"]),
              Ty_gia = SqlDataReaderExt.CheckNull<decimal>(reader["Ty_gia"]),
              Ma_Kh2 = SqlDataReaderExt.CheckNull<string>(reader["Ma_Kh2"]),
              Stt_ct_nkc = SqlDataReaderExt.CheckNull<decimal>(reader["Stt_ct_nkc"]),
              Nh_Dk = SqlDataReaderExt.CheckNull<string>(reader["Nh_Dk"])
            });
          }

          reader.NextResult();

          while (reader.Read())
          {
            //Return object of
            listLedgerAnAccountSummary.Add(new ReportLedgerAnAccountSummary()
            {
              Tk = SqlDataReaderExt.CheckNull<string>(reader["Tk"]),
              Ten_Du_Dk = SqlDataReaderExt.CheckNull<string>(reader["Ten_Du_Dk"]),
              Ten_Du_Ck = SqlDataReaderExt.CheckNull<string>(reader["Ten_Du_Ck"]),
              Du_No1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No1"]),
              Du_Co1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co1"]),
              Du_No_Nt1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No_Nt1"]),
              Du_Co_Nt1 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co_Nt1"]),
              Ps_No = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_No"]),
              Ps_Co = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_Co"]),
              Ps_No_Nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_No_Nt"]),
              Ps_Co_Nt = SqlDataReaderExt.CheckNull<decimal>(reader["Ps_Co_Nt"]),
              Du_No2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No2"]),
              Du_Co2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co2"]),
              Du_No_Nt2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_No_Nt2"]),
              Du_Co_Nt2 = SqlDataReaderExt.CheckNull<decimal>(reader["Du_Co_Nt2"])
            });
          }

          dbConnection.Close();
        }
      }

      return new ReportLedgerAnAccountViewModel()
      {
        ListLedgerAnAccount = listLedgerAnAccount,
        ListLedgerAnAccountSummary = listLedgerAnAccountSummary
      };
    }
  }
}
