using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
  
    public class OperatorMapping
    {
        #region Declare Common Object
        List<ENT.OperatorMapping> lstEntity = new List<ENT.OperatorMapping>();
        ENT.OperatorMapping objEntity = new ENT.OperatorMapping();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public OperatorMapping()
        {
            parFields.Clear();
        }

        public List<ENT.OperatorMapping> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "SELECT om.operatormapid,a.apititle,o.operatorname,om.operatornomal,om.operatorspecial,om.SystemDateTime";
                QueryDisctionery.TablePart = @"from OperatorMapping as om inner join Apis as a on(om.apiid=a.apiid) inner join operators as o on(om.operatorid=o.operatorid) ";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where  a.apititle like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by om.SystemDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.OperatorMapping>(dr);
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
