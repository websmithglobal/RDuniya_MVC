using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class ServiceMaster
    {
        #region Declare Common Object
        List<ENT.ServiceMaster> lstEntity = new List<ENT.ServiceMaster>();
        ENT.ServiceMaster objEntity = new ENT.ServiceMaster();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public ServiceMaster()
        {
            parFields.Clear();
        }

        public List<ENT.ServiceMaster> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select serviceid,servicename,servicecode,CreatedDateTime,Status ";
                QueryDisctionery.TablePart = @"from ServiceMaster ";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where  servicename like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by CreatedDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.ServiceMaster>(dr);
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

        public List<ENT.ServiceMaster> GetListActived(string country_code)
        {
            try
            {
                parFields.Clear();

         
                QueryDisctionery.SelectPart = "select s.servicename,s.servicecode,s.serviceid,s.images,s.icon";
                QueryDisctionery.TablePart = @"from Operators as o inner join ServiceMaster as s on(o.serviceid=s.serviceid) inner join CountryMaster as c on(o.countryid=c.countryid)";
                QueryDisctionery.ParameterPart += "where  c.isocode='" + country_code + "' and s.Status=1";
                QueryDisctionery.GroupPart += "group by s.servicename,s.servicecode,s.serviceid,s.images,s.icon";
                QueryDisctionery.OrderPart = "order by s.servicename desc";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.ServiceMaster>(dr);
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

        public List<ENT.ServiceMaster> GetServiceByCountryid(string countryid)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select s.servicename,s.serviceid ";
                QueryDisctionery.TablePart = @"from  Operators as o inner join ServiceMaster as s on(o.serviceid=s.serviceid)";
                QueryDisctionery.ParameterPart = "where o.countryid='" + countryid + "' and s.Status = 1";
                QueryDisctionery.GroupPart = "group by s.servicename,s.serviceid";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.ServiceMaster>(dr);
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

        public List<ENT.ServiceMasterView> GetServices(string countryid)
        {
            List<ENT.ServiceMasterView> lstEntity = new List<ENT.ServiceMasterView>();
            try
            {
                parFields.Clear();

                QueryDisctionery.SelectPart = "select s.servicename,s.serviceid,s.servicecode,s.images as serviceimage ";
                QueryDisctionery.TablePart = @"from  Operators as o inner join ServiceMaster as s on (o.serviceid=s.serviceid)";
                QueryDisctionery.ParameterPart = "where o.countryid='" + countryid + "' and s.Status = 1";
                QueryDisctionery.GroupPart = "group by s.servicename,s.serviceid,s.images,s.servicecode";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.ServiceMasterView>(dr);
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
