using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class RofferDetailReport
    {
        #region Declare Common Object
        List<ENT.RofferDetailReport> lstEntity = new List<ENT.RofferDetailReport>();
        ENT.RofferDetailReport objEntity = new ENT.RofferDetailReport();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public RofferDetailReport()
        {
            parFields.Clear();
        }
        public List<ENT.RofferDetailReport> GetList(string search)
        {
            try
            {
                parFields.Clear();


                QueryDisctionery.SelectPart = "select u.up_username,u.up_userid";
                QueryDisctionery.TablePart = @"from RofferMapped as r inner join UserProfile as u on(r.userid=u.up_userid)";
                QueryDisctionery.GroupPart = "group by u.up_username,u.up_userid";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.RofferDetailReport>(dr);
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
        public List<ENT.RofferDetailReport> GetAll(int ddrange, DateTime fromdate, DateTime todate, int ddData, Guid ddUsername, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            try
            {
                parFields.Clear();
                if (userlevel == 0)
                {
                    QueryDisctionery.SelectPart += "select rd.rofferdetailid,ra.title,up.up_username,rd.callcount,rd.SystemDateTime,rd.url,rd.Status,(case when rd.apitype=1 then 'Roffer' when rd.apitype=2 then 'Simple' when rd.apitype=3 then 'Dth Plan' when rd.apitype=4 then 'Dth info'  end) as apitypename";
                    QueryDisctionery.SelectPart += " from RofferDetails as rd inner join RofferAPI as ra on(rd.rofferid=ra.rofferid) inner join UserProfile as up on(rd.userid=up.up_userid)";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.SelectPart += " AND rd.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  rd.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.SelectPart += " AND (rd.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += "order by rd.SystemDateTime desc";
                }
                else if (userlevel > 0)
                {
                    QueryDisctionery.SelectPart += "select rd.rofferdetailid,ra.title,up.up_username,rd.callcount,rd.SystemDateTime,rd.url,rd.Status,(case when rd.apitype=1 then 'Roffer' when rd.apitype=2 then 'Simple' when rd.apitype=3 then 'Dth Plan' when rd.apitype=4 then 'Dth info'  end) as apitypename";
                    QueryDisctionery.SelectPart += " from RofferDetails as rd inner join RofferAPI as ra on(rd.rofferid=ra.rofferid) inner join UserProfile as up on(rd.userid=up.up_userid)";
                    QueryDisctionery.SelectPart += " where rd.userid='" + userid + "'";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.SelectPart += " AND rd.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  rd.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.SelectPart += " AND (rd.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }

                    }
                    QueryDisctionery.OrderPart += "order by rd.SystemDateTime desc";
                }
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.RofferDetailReport>(dr);
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
