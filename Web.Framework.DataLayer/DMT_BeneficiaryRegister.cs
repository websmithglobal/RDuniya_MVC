using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using System.Data.SqlClient;

namespace Web.Framework.DataLayer
{
    public class DMT_BeneficiaryRegister
    {
        #region Declare Common Object
        List<ENT.DMT_BeneficiaryRegister> lstEntity = new List<ENT.DMT_BeneficiaryRegister>();
        ENT.DMT_BeneficiaryRegister objEntity = new ENT.DMT_BeneficiaryRegister();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public DMT_BeneficiaryRegister()
        {
            parFields.Clear();
        }

        public List<ENT.DMT_BeneficiaryRegister> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_BeneficiaryRegister ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_BeneficiaryRegister>(dr);
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

        public List<ENT.DMT_BeneficiaryRegisterAdminView> GetBenificarybyCustomer(Guid customerid)
        {
            List<ENT.DMT_BeneficiaryRegisterAdminView> lstEntity = new List<ENT.DMT_BeneficiaryRegisterAdminView>();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_BeneficiaryRegister ";

                QueryDisctionery.ParameterPart = "where dmt_customerid='"+ customerid + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_BeneficiaryRegisterAdminView>(dr);
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

        public List<ENT.DMT_BeneficiaryRegisterApiView> GetBenificarybyCustomerForApi(Guid customerid)
        {
            List<ENT.DMT_BeneficiaryRegisterApiView> lstEntity = new List<ENT.DMT_BeneficiaryRegisterApiView>();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_BeneficiaryRegister ";

                QueryDisctionery.ParameterPart = "where dmt_customerid='" + customerid + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_BeneficiaryRegisterApiView>(dr);
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

        public ENT.DMT_BeneficiaryRegisterAdminView GetBenificarybyId(Guid beneficaryid)
        {
            ENT.DMT_BeneficiaryRegisterAdminView Entity = new ENT.DMT_BeneficiaryRegisterAdminView();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select DMT_BeneficiaryRegister.*,DMT_CustomerRegister.dmt_requestno as dmt_remitterId ";
                QueryDisctionery.TablePart = @"from DMT_BeneficiaryRegister 
                INNER JOIN DMT_CustomerRegister on DMT_BeneficiaryRegister.dmt_customerid = DMT_CustomerRegister.dmt_customerid";

                QueryDisctionery.ParameterPart = "where dmt_beneficiaryid='" + beneficaryid + "'";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.DMT_BeneficiaryRegisterAdminView>(dr);
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
