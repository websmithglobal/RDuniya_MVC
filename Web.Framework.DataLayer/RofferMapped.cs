using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class RofferMapped
    {
        #region Declare Common Object
        List<ENT.RofferMapped> lstEntity = new List<ENT.RofferMapped>();
        ENT.RofferMapped objEntity = new ENT.RofferMapped();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public RofferMapped()
        {
            parFields.Clear();
        }

        public List<ENT.RofferMapped> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select m.roffermapid,m.userid,m.rofferid,r.title,u.up_username,m.creditlimit,m.expirydate,m.Status,m.rstatus,m.SystemDateTime,m.CreatedDateTime";
                QueryDisctionery.TablePart = @"from RofferMapped as m inner join RofferAPI as r on(r.rofferid=m.rofferid) inner join UserProfile as u on(m.userid=u.up_userid)";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where u.up_username like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by r.SystemDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.RofferMapped>(dr);
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
