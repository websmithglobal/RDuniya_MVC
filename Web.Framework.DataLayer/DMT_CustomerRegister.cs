using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{

    public class DMT_CustomerRegister
    {

        #region Declare Common Object
        List<ENT.DMT_CustomerRegister> lstEntity = new List<ENT.DMT_CustomerRegister>();
        ENT.DMT_CustomerRegister objEntity = new ENT.DMT_CustomerRegister();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public DMT_CustomerRegister()
        {
            parFields.Clear();
        }

        public List<ENT.DMT_CustomerRegister> GetAll()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_CustomerRegister ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_CustomerRegister>(dr);
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

        public List<ENT.DMT_CustomerRegisterAdminView> GetAllCustomer(Guid search)
        {
            List<ENT.DMT_CustomerRegisterAdminView> lstEntity = new List<ENT.DMT_CustomerRegisterAdminView>();

            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_CustomerRegister ";

                QueryDisctionery.ParameterPart = "Where dmt_userid='"+search+"'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_CustomerRegisterAdminView>(dr);
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

        public List<ENT.DMT_CustomerRegisterApiView> GetAllApiCustomer(Guid search)
        {
            List<ENT.DMT_CustomerRegisterApiView> lstEntity = new List<ENT.DMT_CustomerRegisterApiView>();

            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_CustomerRegister ";

                QueryDisctionery.ParameterPart = "Where dmt_userid='" + search + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_CustomerRegisterApiView>(dr);
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

        public ENT.DMT_CustomerRegisterAdminView GetCustomerById(Guid guid)
        {
            ENT.DMT_CustomerRegisterAdminView Entity = new ENT.DMT_CustomerRegisterAdminView();

            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_CustomerRegister ";

                QueryDisctionery.ParameterPart = "Where dmt_customerid='"+ guid + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.DMT_CustomerRegisterAdminView>(dr);
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
