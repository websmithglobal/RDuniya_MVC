using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class WalletRequest
    {

        #region Declare Common Object
        List<ENT.WalletRequest> lstEntity = new List<ENT.WalletRequest>();
        ENT.WalletRequest objEntity = new ENT.WalletRequest();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public WalletRequest()
        {
            parFields.Clear();
        }

        public List<ENT.WalletRequest> GetApprovedList(int ddrange, DateTime fromdate, DateTime todate, int ddData, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            try
            {
                parFields.Clear();

                QueryDisctionery.SelectPart += "SELECT w.wr_id,u.up_username as UserName,W.wr_status,(case W.wr_status when 0 then 'Pending' else 'Approved' end) as WalletStatus,w.wr_bankname,w.wr_accountno,w.wr_amount,(case w.wr_mode when 1 then 'NEFT' when 2 then 'IMPS' when 3 then 'CASH' else 'NONE' end) as Mode,w.wr_refrenceid,w.wr_remakrs,(CASE w.wr_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END) AS UserLevel,w.SystemDateTime";
                QueryDisctionery.TablePart += " FROM WalletRequest as w inner join UserProfile as u on(w.wr_userid=u.up_userid)";
                QueryDisctionery.ParameterPart += " where w.wr_upperid='" + userid + "'";
                if (ddrange == 0)
                {
                    QueryDisctionery.ParameterPart += " AND w.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  w.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                }
                else
                {
                    if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                    {
                        QueryDisctionery.ParameterPart += " AND (w.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                    }

                }
                QueryDisctionery.OrderPart += " order by w.SystemDateTime desc";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.WalletRequest>(dr);
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

        public List<ENT.WalletRequestApiView> GetAllSearchApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            List<ENT.WalletRequestApiView> lstEntity = new List<ENT.WalletRequestApiView>();
            try
            {
                parFields.Clear();

                QueryDisctionery.SelectPart += "SELECT w.wr_id,u.up_username as UserName,W.wr_status,(case W.wr_status when 0 then 'Pending' else 'Approved' end) as WalletStatus,w.wr_bankname,w.wr_accountno,w.wr_amount,(case w.wr_mode when 1 then 'NEFT' when 2 then 'IMPS' when 3 then 'CASH' else 'NONE' end) as Mode,w.wr_refrenceid,w.wr_remakrs,(CASE w.wr_userlevel WHEN 2 THEN 'SD' WHEN 1 THEN 'MD' WHEN 3 THEN 'R' WHEN 0 THEN 'ADMIN' WHEN 4 THEN 'APIUSER' END) AS UserLevel,w.SystemDateTime";
                QueryDisctionery.TablePart += " FROM WalletRequest as w inner join UserProfile as u on(w.wr_userid=u.up_userid)";
                QueryDisctionery.ParameterPart += " where w.wr_upperid='" + userid + "' AND w.wr_status = 0";
                if (ddrange == 0)
                {
                    QueryDisctionery.ParameterPart += " AND w.SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  w.SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                }
                else
                {
                    if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                    {
                        QueryDisctionery.ParameterPart += " AND (w.SystemDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                    }

                }
                QueryDisctionery.OrderPart += " order by w.SystemDateTime desc";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.WalletRequestApiView>(dr);
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

        public List<ENT.WalletRequest> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from WalletRequest ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.WalletRequest>(dr);
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


        public ENT.WalletRequest GetRequestById(Guid wr_id)
        {
            ENT.WalletRequest Entity = new ENT.WalletRequest();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from WalletRequest 
                inner join UserDeviceToken on WalletRequest.wr_userid = UserDeviceToken.udt_userid";

                QueryDisctionery.ParameterPart = "where wr_id='"+ wr_id + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.WalletRequest>(dr);
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
            return Entity;
        }

    }
}
