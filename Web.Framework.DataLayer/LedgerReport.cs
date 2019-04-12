using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class LedgerReport
    {
        #region Declare Common Object
        List<ENT.LedgerReport> lstEntity = new List<ENT.LedgerReport>();
        ENT.LedgerReport objEntity = new ENT.LedgerReport();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public LedgerReport()
        {
            parFields.Clear();
        }

        //public List<ENT.LedgerReport> GetListSearchList(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        //{
        //    try
        //    {
        //        parFields.Clear();
        //        if (userlevel == 0)
        //        {
        //            QueryDisctionery.SelectPart += "SELECT * FROM (SELECT LD.ledgerid,LD.SystemDateTime,LD.BeforeBal,LD.Amount AS 'CREDIT',0 AS 'DEBIT',LD.AfterBal ,LD.remakrs,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";
        //            QueryDisctionery.SelectPart += "FROM LedgerDetail AS LD INNER JOIN UserProfile AS U ON(LD.userid=U.up_userid)";
        //            QueryDisctionery.SelectPart += "WHERE ";
        //            QueryDisctionery.SelectPart += "(U.up_mobile LIKE(CASE WHEN '" + search + "' <> '' THEN  '%' + '" + search + "' + '%'  ELSE U.up_mobile END)) ";
        //            if (ddrange == 0)
        //            {
        //                QueryDisctionery.SelectPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
        //            }
        //            else
        //            {
        //                if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
        //                {
        //                    QueryDisctionery.SelectPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
        //                }

        //            }
        //            QueryDisctionery.SelectPart += "AND (LD.LedgerType ='RC' OR LD.LedgerType='FTC' OR LD.LedgerType = 'BTC')";
        //            QueryDisctionery.SelectPart += " UNION ";
        //            QueryDisctionery.SelectPart += "SELECT LD.ledgerid,LD.SystemDateTime,LD.BeforeBal,0 AS 'CREDIT',LD.Amount AS 'DEBIT',LD.AfterBal ,LD.remakrs,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";
        //            QueryDisctionery.SelectPart += " FROM LedgerDetail AS LD INNER JOIN UserProfile AS U ON(LD.userid=U.up_userid)";
        //            QueryDisctionery.SelectPart += " WHERE ";
        //            QueryDisctionery.SelectPart += "(U.up_mobile LIKE(CASE WHEN '" + search + "' <> '' THEN  '%' + '" + search + "' + '%'  ELSE U.up_mobile END)) ";

        //            if (ddrange == 0)
        //            {
        //                QueryDisctionery.SelectPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
        //            }
        //            else
        //            {
        //                if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
        //                {
        //                    QueryDisctionery.SelectPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
        //                }

        //            }
        //            QueryDisctionery.SelectPart += "AND (LD.LedgerType ='RD' OR LD.LedgerType='FTD' OR LD.LedgerType = 'BTD'))";
        //            QueryDisctionery.OrderPart += "AS TEMPLEDGER ORDER BY SystemDateTime DESC";
        //        }
        //        else if (userlevel > 0)
        //        {
        //            QueryDisctionery.SelectPart += "SELECT * FROM (SELECT LD.ledgerid,LD.SystemDateTime,LD.BeforeBal,LD.Amount AS 'CREDIT',0 AS 'DEBIT',LD.AfterBal ,LD.remakrs,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";
        //            QueryDisctionery.SelectPart += "FROM LedgerDetail AS LD INNER JOIN UserProfile AS U ON(LD.userid=U.up_userid)";
        //            QueryDisctionery.SelectPart += "WHERE ";
        //            QueryDisctionery.SelectPart += "LD.userid='" + userid + "' AND U.up_userlevel=" + (int)userlevel + " ";
        //            if (ddrange == 0)
        //            {
        //                QueryDisctionery.SelectPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
        //            }
        //            else
        //            {
        //                if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
        //                {
        //                    QueryDisctionery.SelectPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
        //                }

        //            }
        //            QueryDisctionery.SelectPart += "AND (LD.LedgerType ='RC' OR LD.LedgerType='FTC' OR LD.LedgerType = 'BTC')";
        //            QueryDisctionery.SelectPart += " UNION ";
        //            QueryDisctionery.SelectPart += "SELECT LD.ledgerid,LD.SystemDateTime,LD.BeforeBal,0 AS 'CREDIT',LD.Amount AS 'DEBIT',LD.AfterBal ,LD.remakrs,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";
        //            QueryDisctionery.SelectPart += " FROM LedgerDetail AS LD INNER JOIN UserProfile AS U ON(LD.userid=U.up_userid)";
        //            QueryDisctionery.SelectPart += " WHERE ";
        //            QueryDisctionery.SelectPart += "LD.userid='" + userid + "' AND U.up_userlevel=" + (int)userlevel + " ";
        //            if (ddrange == 0)
        //            {
        //                QueryDisctionery.SelectPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
        //            }
        //            else
        //            {
        //                if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
        //                {
        //                    QueryDisctionery.SelectPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
        //                }

        //            }
        //            QueryDisctionery.SelectPart += "AND (LD.LedgerType ='RD' OR LD.LedgerType='FTD' OR LD.LedgerType = 'BTD'))";
        //            QueryDisctionery.OrderPart += "AS TEMPLEDGER ORDER BY SystemDateTime DESC";

        //        }

        //        using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
        //        {
        //            lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.LedgerReport>(dr);
        //            objDBHelper.Disposed();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        parFields.Clear();
        //    }
        //    return lstEntity;
        //}

        public List<ENT.LedgerReport> GetListSearchList(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            try
            {
                parFields.Clear();
                if (userlevel == 0)
                {
                    QueryDisctionery.SelectPart = @"SELECT *,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";

                    QueryDisctionery.TablePart = @"FROM VW_LEDGERREPORT AS LD INNER JOIN UserProfile AS U ON (LD.userid=U.up_userid)";

                    QueryDisctionery.ParameterPart = "WHERE 1=1";

                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }
                    }

                    if (!String.IsNullOrEmpty(search))
                    {
                        QueryDisctionery.ParameterPart += "AND LD.remakrs like '%" + search + "%'";
                    }

                    QueryDisctionery.OrderPart += "ORDER BY LD.SystemDateTime DESC";
                }
                else if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart = @"SELECT *,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";

                    QueryDisctionery.TablePart = @"FROM VW_LEDGERREPORT AS LD INNER JOIN UserProfile AS U ON (LD.userid=U.up_userid)";

                    QueryDisctionery.ParameterPart = "WHERE LD.userid='" + userid + "' AND U.up_userlevel=" + (int)userlevel;

                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }
                    }

                    if (!String.IsNullOrEmpty(search))
                    {
                        QueryDisctionery.ParameterPart += " AND LD.remakrs like '%" + search + "%'";
                    }

                    QueryDisctionery.OrderPart += " ORDER BY LD.SystemDateTime DESC";
                }

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.LedgerReport>(dr);
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

        public List<ENT.LedgerReportApiView> GetListSearchListApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            List<ENT.LedgerReportApiView> lstEntity = new List<ENT.LedgerReportApiView>();

            try
            {
                parFields.Clear();

                if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart = @"SELECT *,(U.up_username+'('+(CASE U.up_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END)+')') AS UserName ";

                    QueryDisctionery.TablePart = @"FROM VW_LEDGERREPORT AS LD INNER JOIN UserProfile AS U ON (LD.userid=U.up_userid)";

                    QueryDisctionery.ParameterPart = "WHERE LD.userid='" + userid + "' AND U.up_userlevel=" + (int)userlevel;

                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += "AND (LD.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  LD.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += "AND (LD.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }
                    }

                    if (!String.IsNullOrEmpty(search))
                    {
                        QueryDisctionery.ParameterPart += " AND LD.remakrs like '%" + search + "%'";
                    }

                    QueryDisctionery.OrderPart += " ORDER BY LD.SystemDateTime DESC";
                }

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.LedgerReportApiView>(dr);
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
