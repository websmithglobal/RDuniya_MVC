using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class CountryMaster
    {

        #region Declare Common Object
        List<ENT.CountryMaster> lstEntity = new List<ENT.CountryMaster>();
        ENT.CountryMaster objEntity = new ENT.CountryMaster();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public CountryMaster()
        {
            parFields.Clear();
        }

        public List<ENT.CountryMaster> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select countryid,isocode,name,nicename,iso3,numcode,phonecode,CreatedDateTime,Status";
                QueryDisctionery.TablePart = @"from CountryMaster ";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where  nicename like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by CreatedDateTime desc, name";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.CountryMaster>(dr);
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

        public List<ENT.CountryMaster> GetListByStatus(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select countryid,isocode,name,nicename,iso3,numcode,phonecode,CreatedDateTime,Status";
                QueryDisctionery.TablePart = @"from CountryMaster ";
                QueryDisctionery.ParameterPart = "where Status=1";
                QueryDisctionery.OrderPart = "order by CreatedDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.CountryMaster>(dr);
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
        public List<ENT.CountryMaster> CheckDuplicate(List<Guid> ParentID, string strname, string strName2)
        {
            try
            {
                QueryDisctionery.SelectPart = "SELECT TOP 1 countryid";
                QueryDisctionery.TablePart = @"FROM  CountryMaster ";
                QueryDisctionery.ParameterPart += " WHERE nicename ='" + strname + "' ";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.CountryMaster>(dr);
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
