using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{

    public class UserDeviceToken
    {

        #region Declare Common Object
        List<ENT.UserDeviceToken> lstEntity = new List<ENT.UserDeviceToken>();
        ENT.UserDeviceToken objEntity = new ENT.UserDeviceToken();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public UserDeviceToken()
        {
            parFields.Clear();
        }

        public List<ENT.UserDeviceToken> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from UserDeviceToken ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserDeviceToken>(dr);
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


        public ENT.UserDeviceToken GetUserDeviceToken(Guid userid)
        {
            ENT.UserDeviceToken Entity = new ENT.UserDeviceToken();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select top 1 * ";
                QueryDisctionery.TablePart = @"from UserDeviceToken ";

                QueryDisctionery.ParameterPart = "Where udt_userid='"+ userid + "'";

                QueryDisctionery.OrderPart = "Order by CreatedDateTime desc";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.UserDeviceToken>(dr);
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

        public ENT.UserDeviceToken GetUserDeviceTokenFromwalletRequest(Guid wr_id)
        {
            ENT.UserDeviceToken Entity = new ENT.UserDeviceToken();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from UserDeviceToken 
                inner join WalletRequest on WalletRequest.wr_userid = UserDeviceToken.udt_userid";

                QueryDisctionery.ParameterPart = "Where WalletRequest.wr_id ='" + wr_id + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.UserDeviceToken>(dr);
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
