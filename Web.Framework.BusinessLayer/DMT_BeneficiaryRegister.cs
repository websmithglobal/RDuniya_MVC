using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Web.Framework.Entity;
using DAL = Web.Framework.DataLayer;
using COM = Web.Framework.Common;
using System.Linq.Expressions;

namespace Web.Framework.BusinessLayer
{
    public class DMT_BeneficiaryRegister : COM.DALInterface , IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.DMT_BeneficiaryRegister clsDAL = new DAL.DMT_BeneficiaryRegister();
        public ENT.DMT_BeneficiaryRegister Entity = new ENT.DMT_BeneficiaryRegister();
        List<ENT.DMT_BeneficiaryRegister> lstEntity;

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "dmt_beneficiaryid");
                if (objDAL.Insert(obj))
                {
                    blnResult = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return blnResult;
        }

        public int BulkInsert(List<ENT.DMT_BeneficiaryRegister> Entity)
        {
            int objResult = 0;
            try
            {
                DAL.CRUDOperation tt = new DAL.CRUDOperation();

                String query = string.Empty;

                foreach (ENT.DMT_BeneficiaryRegister p in Entity)
                {
                    query = query + COM.CommonMSSQL.PrepairInsertQueryByEntity(p, p.TableName);
                }

                objResult = tt.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw;
            }
            return objResult;
        }

        public bool Update(object obj)
        {
            bool blnResult = false;
            try
            {
                if (objDAL.Update(obj))
                {
                    blnResult = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return blnResult;
        }

        public bool Delete(object obj)
        {
            bool blnResult = false;
            try
            {
                if (objDAL.Delete(obj))
                {
                    blnResult = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return blnResult;
        }

        public object GetByPrimaryKey(ENT.DMT_BeneficiaryRegister Entity)
        {
            object objResult = null;
            try
            {
                DAL.CRUDOperation tt = new DAL.CRUDOperation();
                objResult = tt.GetEntityByPrimartKey(Entity);
            }
            catch (Exception)
            {
                throw;
            }
            return objResult;
        }

        // this function for just referance for partial update field user have to create seperate function learn from this function.
        public bool UpdatePartial(ENT.DMT_BeneficiaryRegister objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_beneficiaryid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.userid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_beneficiaryname), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_beneficiarymobile), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_customerid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_bankname), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_ifsc), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_accountnumber), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_branchname), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_address), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_addharcard), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_Ipaddress), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_requestno), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_response), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_pincode), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.UpdatedDateTime), false);
                objEntity.FieldCollection = dctFields;
                if (objDAL.SaveChanges(objEntity.FieldCollection, objEntity))
                {
                    blnResult = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return blnResult;
        }

        public bool UpdateStatusWithId(ENT.DMT_BeneficiaryRegister objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_beneficiaryid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_requestno), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.dmt_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_BeneficiaryRegister>(x => x.UpdatedDateTime), false);

                objEntity.FieldCollection = dctFields;
                if (objDAL.SaveChanges(objEntity.FieldCollection, objEntity))
                {
                    blnResult = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return blnResult;
        }

        public List<ENT.DMT_BeneficiaryRegister> GetAll()
        {
            lstEntity = new List<ENT.DMT_BeneficiaryRegister>();
            lstEntity = clsDAL.GetList();
            return lstEntity;
        }

        public List<ENT.DMT_BeneficiaryRegisterAdminView> GetBenificarybyCustomer(Guid customerid)
        {
            List<ENT.DMT_BeneficiaryRegisterAdminView>  lstEntity = new List<ENT.DMT_BeneficiaryRegisterAdminView>();
            lstEntity = clsDAL.GetBenificarybyCustomer(customerid);
            return lstEntity;
        }

        public List<ENT.DMT_BeneficiaryRegisterApiView> GetBenificarybyCustomerForApi(Guid customerid)
        {
            List<ENT.DMT_BeneficiaryRegisterApiView> lstEntity = new List<ENT.DMT_BeneficiaryRegisterApiView>();
            lstEntity = clsDAL.GetBenificarybyCustomerForApi(customerid);
            return lstEntity;
        }

        public ENT.DMT_BeneficiaryRegisterAdminView GetBenificarybyId(Guid beneficaryid)
        {
            ENT.DMT_BeneficiaryRegisterAdminView lstEntity = new ENT.DMT_BeneficiaryRegisterAdminView();
            lstEntity = clsDAL.GetBenificarybyId(beneficaryid);
            return lstEntity;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DMT_BeneficiaryRegister() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
