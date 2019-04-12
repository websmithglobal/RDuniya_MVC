using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class FundReport
    {
        #region Declare Common Object
        List<ENT.FundReport> lstEntity = new List<ENT.FundReport>();
        ENT.FundReport objEntity = new ENT.FundReport();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public FundReport()
        {
            parFields.Clear();
        }
        public List<ENT.FundReport> GetListSearchList(int ddrange, DateTime fromdate, DateTime todate, int ddData, int ddUserType, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            try
            {
                parFields.Clear();


                if (userlevel == 0)
                {
                    QueryDisctionery.SelectPart += "SELECT F.fundid,(CASE z.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END) AS UserLevel,z.up_username+'['+z.up_mobile+']' as 'UserName',U.up_username as 'Transby',F.credit,F.debit,F.tooldbal,F.toclosebal,F.fromoldbal,F.FromCLOSEBAL,F.SystemDateTime,F.reqvia,F.remarks";
                    QueryDisctionery.TablePart += " FROM UserProfile U,fundTransfer F ,UserProfile Z";
                    QueryDisctionery.ParameterPart += " WHERE F.FundFrom=U.up_userid and z.up_userid=F.FundTo  AND U.up_userid IN(select URID from GETDOWNLINE('" + userid + "',1))";
                    QueryDisctionery.ParameterPart += " AND (Z.up_username LIKE (CASE WHEN '" + search + "'  <> '' THEN  '%'+ '" + search + "' + '%'  ELSE Z.up_username END) OR Z.up_mobile LIKE (CASE WHEN  '" + search + "' <> '' THEN '%'+ '" + search + "' + '%' ELSE Z.up_mobile END))";
                    QueryDisctionery.ParameterPart += " AND Z.up_userlevel = (CASE WHEN " + ddUserType + " <> -1 THEN " + ddUserType + " ELSE Z.up_userlevel  END)";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND F.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  F.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (F.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += " ORDER BY F.SystemDateTime DESC";
                }
                else if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart += "SELECT F.fundid,(SELECT TOP 1 Z.up_username FROM UserProfile z WHERE z.up_userid=F.FundFrom)AS 'Transby',(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END) AS UserLevel,U.up_username as UserName,F.credit,F.debit,F.tooldbal,F.toclosebal,0 as fromoldbal,0 as FromCLOSEBAL,F.SystemDateTime,F.reqvia,F.remarks";
                    QueryDisctionery.TablePart += " FROM UserProfile U,fundTransfer F";
                    QueryDisctionery.ParameterPart += " WHERE F.FundTo=U.up_userid ";
                    QueryDisctionery.ParameterPart += " AND U.up_userid IN(select URID from GETDOWNLINE('" + userid + "',1))";
                    QueryDisctionery.ParameterPart += " AND (U.up_username LIKE (CASE WHEN '" + search + "'  <> '' THEN  '%'+ '" + search + "' + '%'  ELSE U.up_username END) OR U.up_mobile LIKE (CASE WHEN  '" + search + "' <> '' THEN '%'+ '" + search + "' + '%' ELSE U.up_mobile END))";
                    QueryDisctionery.ParameterPart += " AND U.up_userlevel = (CASE WHEN " + ddUserType + " <> -1 THEN " + ddUserType + " ELSE U.up_userlevel  END)";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND F.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  F.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (F.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += " ORDER BY F.SystemDateTime DESC";
                }

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.FundReport>(dr);
                    objDBHelper.Disposed();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parFields.Clear();
            }
            return lstEntity;
        }

        public List<ENT.FundReportApiView> GetListSearchListApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            List<ENT.FundReportApiView> lstEntity = new List<ENT.FundReportApiView>();
            try
            {
                parFields.Clear();

                if (userlevel == 0)
                {
                    QueryDisctionery.SelectPart += "SELECT F.fundid,(CASE z.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END) AS UserLevel,z.up_username+'['+z.up_mobile+']' as 'UserName',U.up_username as 'Transby',F.credit,F.debit,F.tooldbal,F.toclosebal,F.fromoldbal,F.FromCLOSEBAL,F.SystemDateTime,F.reqvia,F.remarks";
                    QueryDisctionery.TablePart += " FROM UserProfile U,fundTransfer F ,UserProfile Z";
                    QueryDisctionery.ParameterPart += " WHERE F.FundFrom=U.up_userid and z.up_userid=F.FundTo  AND U.up_userid IN(select URID from GETDOWNLINE('" + userid + "',1))";
                    QueryDisctionery.ParameterPart += " AND (Z.up_username LIKE (CASE WHEN '" + search + "'  <> '' THEN  '%'+ '" + search + "' + '%'  ELSE Z.up_username END) OR Z.up_mobile LIKE (CASE WHEN  '" + search + "' <> '' THEN '%'+ '" + search + "' + '%' ELSE Z.up_mobile END))";

                    // QueryDisctionery.SelectPart += " AND Z.up_userlevel = (CASE WHEN " + ddUserType + " <> -1 THEN " + ddUserType + " ELSE Z.up_userlevel END)";

                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND F.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  F.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (F.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += " ORDER BY F.SystemDateTime DESC";
                }
                else if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart += "SELECT F.fundid,(SELECT TOP 1 Z.up_username FROM UserProfile z WHERE z.up_userid=F.FundFrom)AS 'Transby',(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END) AS UserLevel,U.up_username as UserName,F.credit,F.debit,F.tooldbal,F.toclosebal,0 as fromoldbal,0 as FromCLOSEBAL,F.SystemDateTime,F.reqvia,F.remarks";
                    QueryDisctionery.TablePart += " FROM UserProfile U,fundTransfer F";
                    QueryDisctionery.ParameterPart += " WHERE F.FundTo=U.up_userid ";
                    QueryDisctionery.ParameterPart += " AND U.up_userid IN(select URID from GETDOWNLINE('" + userid + "',1))";
                    QueryDisctionery.ParameterPart += " AND (U.up_username LIKE (CASE WHEN '" + search + "'  <> '' THEN  '%'+ '" + search + "' + '%'  ELSE U.up_username END) OR U.up_mobile LIKE (CASE WHEN  '" + search + "' <> '' THEN '%'+ '" + search + "' + '%' ELSE U.up_mobile END))";
                   
                    // QueryDisctionery.SelectPart += " AND U.up_userlevel = (CASE WHEN " + ddUserType + " <> -1 THEN " + ddUserType + " ELSE U.up_userlevel  END)";

                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND F.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  F.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (F.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += " ORDER BY F.SystemDateTime DESC";
                }

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.FundReportApiView>(dr);
                    objDBHelper.Disposed();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parFields.Clear();
            }
            return lstEntity;
        }
    }
}
