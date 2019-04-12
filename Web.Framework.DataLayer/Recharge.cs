using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class Recharge
    {

        #region Declare Common Object
        List<ENT.Recharge> lstEntity = new List<ENT.Recharge>();
        ENT.Recharge objEntity = new ENT.Recharge();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public Recharge()
        {
            parFields.Clear();
        }

        public List<ENT.Recharge> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from Recharge ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Recharge>(dr);
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

        public List<ENT.Recharge> GetPendingList(Guid userid,string search)
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select  Recharge.*,Operators.operatorname,UserProfile.up_mobile as requestby";
                QueryDisctionery.TablePart = @"from Recharge
                inner join Operators on Operators.operatorcode = Recharge.operatorcode
                inner join UserProfile on UserProfile.up_userid = Recharge.CreatedBy";

                QueryDisctionery.ParameterPart = "WHERE Recharge.status = 0 and Recharge.CreatedBy='"+ userid + "'";

                if (!String.IsNullOrEmpty(search)) {
                    QueryDisctionery.ParameterPart = "AND Recharge.numbertorecharge = '"+ search + "'";
                }

                QueryDisctionery.OrderPart = "ORDER BY CreatedDateTime DESC";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Recharge>(dr);
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

        public List<ENT.Recharge> GetPendingListAdmin(string search)
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select  Recharge.*,Operators.operatorname,UserProfile.up_mobile as requestby";
                QueryDisctionery.TablePart = @"from Recharge
                inner join Operators on Operators.operatorcode = Recharge.operatorcode
                inner join UserProfile on UserProfile.up_userid = Recharge.CreatedBy";

                QueryDisctionery.ParameterPart = "WHERE Recharge.status = 0";

                if (!String.IsNullOrEmpty(search))
                {
                    QueryDisctionery.ParameterPart = "AND Recharge.numbertorecharge = '" + search + "'";
                }

                QueryDisctionery.OrderPart = "ORDER BY CreatedDateTime DESC";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Recharge>(dr);
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