using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class ActivityLog
    {
        #region Declare Common Object
        List<ENT.ActivityLog> lstEntity = new List<ENT.ActivityLog>();
        ENT.ActivityLog objEntity = new ENT.ActivityLog();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public ActivityLog()
        {
            parFields.Clear();
        }

        public List<ENT.ActivityLog> GetList(Guid userid)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select activityid,browser,ipaddress,macaddress,CreatedDatetime ";
                QueryDisctionery.TablePart = @" from ActivityLog ";
                QueryDisctionery.ParameterPart = "where userid='" + userid + "' and SystemDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  SystemDateTime<dateadd(d, datediff(d, 0, getdate())+1, 0)";
                QueryDisctionery.OrderPart = "Order by SystemDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.ActivityLog>(dr);
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
