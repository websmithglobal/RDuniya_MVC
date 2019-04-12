using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class IsEnabledApiCall
    {

        #region Declare Common Object
        List<ENT.IsEnabledApiCall> lstEntity = new List<ENT.IsEnabledApiCall>();
        ENT.IsEnabledApiCall objEntity = new ENT.IsEnabledApiCall();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public IsEnabledApiCall()
        {
            parFields.Clear();
        }

        public List<ENT.IsEnabledApiCall> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from IsEnabledApiCall ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.IsEnabledApiCall>(dr);
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

        public ENT.IsEnabledApiCall GetIsEnable(Guid id)
        {
            ENT.IsEnabledApiCall apicall = new Entity.IsEnabledApiCall();
            try
            {
                parFields.Clear();
                QueryDisctionery.SelectPart = "select top 1 Isenabled ";
                QueryDisctionery.TablePart = @"from IsEnabledApiCall ";
                QueryDisctionery.ParameterPart = "WHERE userid='" + id + "'";
                QueryDisctionery.OrderPart = "order by SystemDateTime desc";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    apicall = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.IsEnabledApiCall>(dr);
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
            return apicall;
        }
    }
}
