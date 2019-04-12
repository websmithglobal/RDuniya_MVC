using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
namespace Web.Framework.DataLayer
{
    public class CircleMaster
    {
        #region Declare Common Object
        List<ENT.CircleMaster> lstEntity = new List<ENT.CircleMaster>();
        ENT.CircleMaster objEntity = new ENT.CircleMaster();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public CircleMaster()
        {
            parFields.Clear();
        }

        public List<ENT.CircleMaster> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select c.circleid,c.countryid,c.circle_name,c.circle_code,c.CreatedDateTime,c.Status,cm.name as countryname";
                QueryDisctionery.TablePart = @"from CircleMaster as c inner join CountryMaster as cm on(c.countryid=cm.countryid)";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where   c.circle_name like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by c.CreatedDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.CircleMaster>(dr);
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
        public List<ENT.CircleMaster> GetListActived(string country_code)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select c.circle_name,c.circle_code";
                QueryDisctionery.TablePart = @"from CircleMaster as c inner join CountryMaster as cm on(c.countryid=cm.countryid)";
                QueryDisctionery.ParameterPart += " where c.status=1 and cm.isocode='" + country_code + "'";
                QueryDisctionery.OrderPart = "order by c.circle_name desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.CircleMaster>(dr);
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
        public List<ENT.CircleMaster> CheckDuplicate(List<Guid> ParentID, string strname, string strName2)
        {
            try
            {


                QueryDisctionery.SelectPart = "SELECT TOP 1 circleid";
                QueryDisctionery.TablePart = @"FROM  CircleMaster ";
                QueryDisctionery.ParameterPart += " WHERE circle_name ='" + strname + "' ";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.CircleMaster>(dr);
                    objDBHelper.Disposed();
                }
            }
            catch
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
