using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class OperatorSetup
    {
        #region Declare Common Object
        List<ENT.OperatorSetup> lstEntity = new List<ENT.OperatorSetup>();
        ENT.OperatorSetup objEntity = new ENT.OperatorSetup();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public OperatorSetup()
        {
            parFields.Clear();
        }

        public List<ENT.OperatorSetup> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select o.operatorid,o.Billerlogo,o.Onlinevalidation,o.reqtype_normal,reqtype_special,o.Paymentvalidation,o.operatorname,o.operatorcode,o.Status,o.CreatedDateTime,s.servicename,c.nicename,o.auth_name,o.auth_msg,o.auth_regex,o.auth_datatype,o.auth_name2,o.auth_msg2,o.auth_regex2,o.auth_datatype2";
                QueryDisctionery.TablePart = @"from Operators as o inner join ServiceMaster as s on(o.serviceid=s.serviceid) inner join CountryMaster as c on(o.countryid=c.countryid)";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where  o.operatorname like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by o.CreatedDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.OperatorSetup>(dr);
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
        public List<ENT.OperatorSetup> GetListActive(string country_code, string key)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select o.operatorname,o.operatorcode,o.reqtype_normal,o.reqtype_special,o.Onlinevalidation,o.Paymentvalidation,o.Billerlogo,o.auth_name,o.auth_regex,o.auth_msg,o.auth_datatype,o.auth_name2,o.auth_regex2,o.auth_msg2,o.auth_datatype2";
                QueryDisctionery.TablePart = @"from Operators as o inner join CountryMaster as c on(o.countryid=c.countryid) inner join ServiceMaster as s on(o.serviceid=s.serviceid)";
                QueryDisctionery.ParameterPart += "where c.isocode='" + country_code + "' and s.servicecode='" + key + "'";
                QueryDisctionery.OrderPart = "order by o.operatorname desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.OperatorSetup>(dr);
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

        public List<ENT.OperatorView> GetOperators(string country_code, string serviceId)
        {
            List<ENT.OperatorView> lstEntity = new List<ENT.OperatorView>();

            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select o.operatorname,o.operatorcode,o.reqtype_normal,o.reqtype_special,o.Onlinevalidation,o.Paymentvalidation,o.Billerlogo,o.auth_name,o.auth_regex,o.auth_msg,o.auth_datatype,o.auth_name2,o.auth_regex2,o.auth_msg2,o.auth_datatype2,ISNULL(o.roffer_code,'') as roffer_code,ISNULL(o.viewoffer_code,'') as viewoffer_code";
                QueryDisctionery.TablePart = @"from Operators as o inner join CountryMaster as c on(o.countryid=c.countryid) inner join ServiceMaster as s on(o.serviceid=s.serviceid)";
                QueryDisctionery.ParameterPart += "where c.isocode='" + country_code + "' and s.serviceid='" + serviceId + "'";
                QueryDisctionery.OrderPart = "order by o.operatorname desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.OperatorView>(dr);
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

        public List<ENT.OperatorSetup> CheckDuplicate(List<Guid> ParentID, string strname, int operatorcode)
        {
            try
            {

                QueryDisctionery.SelectPart = "SELECT TOP 1 operatorid";
                QueryDisctionery.TablePart = @"FROM  Operators ";
                QueryDisctionery.ParameterPart += " WHERE (operatorname ='" + strname + "' OR operatorcode=" + operatorcode + ") ";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.OperatorSetup>(dr);
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

        public List<ENT.OperatorSetup> GetOperator(string serviceid, string countryid)
        {
            try
            {
                parFields.Clear();
                // QueryDisctionery.SelectPart = "select operatorid,operatorname,operatorcode";
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from operators";
                QueryDisctionery.ParameterPart = "where serviceid='" + serviceid + "' and countryid='" + countryid + "'";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.OperatorSetup>(dr);
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
