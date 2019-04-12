using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class RechargeReport
    {
        #region Declare Common Object
        List<ENT.RechargeReport> lstEntity = new List<ENT.RechargeReport>();
        ENT.RechargeReport objEntity = new ENT.RechargeReport();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public RechargeReport()
        {
            parFields.Clear();
        }
        public List<ENT.RechargeReport> GetListSearchList(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid, int status, int operatorcode)
        {
            try
            {
                parFields.Clear();
                if (userlevel == 0)
                {
                    QueryDisctionery.SelectPart += "SELECT r.rechargeid,r.userid,o.operatorname,r.SystemDateTime,r.operatorcode,r.numbertorecharge,r.amount,r.txid,r.status,r.countrycode,r.beforebalance,r.balance,r.reqvia,r.proccessdate,r.webusername,r.accountref,r.requestid,r.ipaddress,u.up_mobile,u.up_userlevel,u.up_username,r.commimd,r.commisd,r.commir,r.charge,(case when r.rechargemethod=0 then (select top 1 apititle from Apis where apiid=r.routeid) when r.rechargemethod=3 then (select top 1 smartmobile from SmartMobile where mobileid=r.routeid) else (select top 1 name from machine where machineid=r.routeid) end) as ApiTitle,(CASE WHEN R.reqtype = 0 THEN 'NORMAL' ELSE 'SPECIAL'END) AS reqtype";
                    QueryDisctionery.TablePart += " FROM Recharge as r inner join Operators as o on(r.operatorcode = o.operatorcode) inner join UserProfile as u on(r.userid = u.up_userid)";
                    QueryDisctionery.ParameterPart += " WHERE ";
                    QueryDisctionery.ParameterPart += " (r.numbertorecharge LIKE(CASE WHEN '" + search + "' <> '' THEN  '%' + '" + search + "' + '%'  ELSE r.numbertorecharge END) OR U.up_username LIKE (CASE WHEN '" + search + "' <> '' THEN '%'+ '" + search + "' + '%' ELSE U.up_username END))";
                    QueryDisctionery.ParameterPart += " AND (R.STATUS = (CASE WHEN " + status + " <> '-1' THEN " + status + " ELSE R.STATUS END)) ";
                    QueryDisctionery.ParameterPart += " AND (R.operatorcode = (CASE WHEN " + operatorcode + " <> '-1' THEN " + operatorcode + " ELSE R.operatorcode END)) ";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND (r.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  r.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (r.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += "order by r.SystemDateTime desc";
                }
                else if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart += "SELECT r.rechargeid,r.userid,o.operatorname,r.SystemDateTime,r.operatorcode,r.numbertorecharge,r.amount,r.txid,r.countrycode,r.status,r.beforebalance,r.balance,r.reqvia,r.proccessdate,r.webusername,r.accountref,r.requestid,r.ipaddress,u.up_mobile,u.up_userlevel,u.up_username,r.commimd,r.commisd,r.commir,r.charge,(case when r.rechargemethod=0 then (select top 1 apititle from Apis where apiid=r.routeid) when r.rechargemethod=3 then (select top 1 smartmobile from SmartMobile where mobileid=r.routeid) else (select top 1 name from machine where machineid=r.routeid) end) as ApiTitle,(CASE WHEN R.reqtype = 0 THEN 'NORMAL' ELSE 'SPECIAL'END) AS reqtype";
                    QueryDisctionery.TablePart += " FROM Recharge as r inner join Operators as o on(r.operatorcode = o.operatorcode) inner join UserProfile as u on(r.userid = u.up_userid)";
                    QueryDisctionery.ParameterPart += " WHERE ";
                    QueryDisctionery.ParameterPart += " r.userid='" + userid + "' AND u.up_userlevel=" + (int)userlevel + " ";
                    QueryDisctionery.ParameterPart += " AND (r.numbertorecharge LIKE(CASE WHEN '" + search + "' <> '' THEN  '%' + '" + search + "' + '%'  ELSE r.numbertorecharge END)) ";
                    QueryDisctionery.ParameterPart += " AND (R.STATUS = (CASE WHEN " + status + " <> '-1' THEN " + status + " ELSE R.STATUS END)) ";
                    QueryDisctionery.ParameterPart += " AND (R.operatorcode = (CASE WHEN " + operatorcode + " <> '-1' THEN " + operatorcode + " ELSE R.operatorcode END)) ";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND (r.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  r.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (r.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += "order by r.SystemDateTime desc";

                }
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.RechargeReport>(dr);
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

        public List<ENT.RechargeReportApiView> GetAllSearchApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid, int status, int operatorcode)
        {
            List<ENT.RechargeReportApiView> lstEntity = new List<ENT.RechargeReportApiView>();
            try
            {
                parFields.Clear();
                if (userlevel == 0)
                {
                    QueryDisctionery.SelectPart += "SELECT r.rechargeid,r.userid,o.operatorname,r.SystemDateTime,r.operatorcode,r.numbertorecharge,r.amount,r.txid,r.status,r.countrycode,r.beforebalance,r.balance,r.reqvia,r.proccessdate,r.webusername,r.accountref,r.requestid,r.ipaddress,u.up_mobile,u.up_userlevel,u.up_username,r.commimd,r.commisd,r.commir,r.charge,(case when r.rechargemethod=0 then (select top 1 apititle from Apis where apiid=r.routeid) when r.rechargemethod=3 then (select top 1 smartmobile from SmartMobile where mobileid=r.routeid) else (select top 1 name from machine where machineid=r.routeid) end) as ApiTitle,(CASE WHEN R.reqtype = 0 THEN 'NORMAL' ELSE 'SPECIAL'END) AS reqtype";
                    QueryDisctionery.TablePart += " FROM Recharge as r inner join Operators as o on(r.operatorcode = o.operatorcode) inner join UserProfile as u on(r.userid = u.up_userid)";
                    QueryDisctionery.ParameterPart += " WHERE ";
                    QueryDisctionery.ParameterPart += " (r.numbertorecharge LIKE(CASE WHEN '" + search + "' <> '' THEN  '%' + '" + search + "' + '%'  ELSE r.numbertorecharge END) OR U.up_username LIKE (CASE WHEN '" + search + "' <> '' THEN '%'+ '" + search + "' + '%' ELSE U.up_username END))";
                    QueryDisctionery.ParameterPart += " AND (R.STATUS = (CASE WHEN " + status + " <> '-1' THEN " + status + " ELSE R.STATUS END)) ";
                    QueryDisctionery.ParameterPart += " AND (R.operatorcode = (CASE WHEN " + operatorcode + " <> '-1' THEN " + operatorcode + " ELSE R.operatorcode END)) ";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND (r.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  r.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (r.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += "order by r.SystemDateTime desc";
                }
                else if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart += "SELECT r.rechargeid,r.userid,o.operatorname,r.SystemDateTime,r.operatorcode,r.numbertorecharge,r.amount,r.txid,r.countrycode,r.status,r.beforebalance,r.balance,r.reqvia,r.proccessdate,r.webusername,r.accountref,r.requestid,r.ipaddress,u.up_mobile,u.up_userlevel,u.up_username,r.commimd,r.commisd,r.commir,r.charge,(case when r.rechargemethod=0 then (select top 1 apititle from Apis where apiid=r.routeid) when r.rechargemethod=3 then (select top 1 smartmobile from SmartMobile where mobileid=r.routeid) else (select top 1 name from machine where machineid=r.routeid) end) as ApiTitle,(CASE WHEN R.reqtype = 0 THEN 'NORMAL' ELSE 'SPECIAL'END) AS reqtype";
                    QueryDisctionery.TablePart += " FROM Recharge as r inner join Operators as o on(r.operatorcode = o.operatorcode) inner join UserProfile as u on(r.userid = u.up_userid)";
                    QueryDisctionery.ParameterPart += " WHERE ";
                    QueryDisctionery.ParameterPart += " r.userid='" + userid + "' AND u.up_userlevel=" + (int)userlevel + " ";
                    QueryDisctionery.ParameterPart += " AND (r.numbertorecharge LIKE(CASE WHEN '" + search + "' <> '' THEN  '%' + '" + search + "' + '%'  ELSE r.numbertorecharge END)) ";
                    QueryDisctionery.ParameterPart += " AND (R.STATUS = (CASE WHEN " + status + " <> '-1' THEN " + status + " ELSE R.STATUS END)) ";
                    QueryDisctionery.ParameterPart += " AND (R.operatorcode = (CASE WHEN " + operatorcode + " <> '-1' THEN " + operatorcode + " ELSE R.operatorcode END)) ";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND (r.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  r.SystemDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (r.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += "order by r.SystemDateTime desc";

                }
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.RechargeReportApiView>(dr);
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
