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
    public class DMT_CustomerRegister : COM.DALInterface, IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.DMT_CustomerRegister clsDAL = new DAL.DMT_CustomerRegister();
        public ENT.DMT_CustomerRegister Entity = new ENT.DMT_CustomerRegister();
        List<ENT.DMT_CustomerRegister> lstEntity;

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                // COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "dmt_customerid");
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

        public bool UpdateStatus(ENT.DMT_CustomerRegister objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_customerid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.UpdatedDateTime), false);
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

        public bool UpdateRemitter(ENT.DMT_CustomerRegister objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_customerid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_requestno), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.UpdatedDateTime), false);
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

        public bool UpdateStatusWithRemitter(ENT.DMT_CustomerRegister objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_customerid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.dmt_requestno), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_CustomerRegister>(x => x.UpdatedDateTime), false);
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

        public object GetByPrimaryKey(ENT.DMT_CustomerRegister Entity)
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

        public List<ENT.DMT_CustomerRegister> GetAll()
        {
            lstEntity = new List<ENT.DMT_CustomerRegister>();
            lstEntity = clsDAL.GetAll();
            return lstEntity;
        }

        public List<ENT.DMT_CustomerRegisterAdminView> GetAllCustomer(Guid loginid)
        {
            List<ENT.DMT_CustomerRegisterAdminView> lstEntity = new List<ENT.DMT_CustomerRegisterAdminView>();

            lstEntity = new List<ENT.DMT_CustomerRegisterAdminView>();
            lstEntity = clsDAL.GetAllCustomer(loginid);
            return lstEntity;
        }

        public List<ENT.DMT_CustomerRegisterApiView> GetAllApiCustomer(Guid loginid)
        {
            List<ENT.DMT_CustomerRegisterApiView> lstEntity = new List<ENT.DMT_CustomerRegisterApiView>();

            lstEntity = new List<ENT.DMT_CustomerRegisterApiView>();
            lstEntity = clsDAL.GetAllApiCustomer(loginid);
            return lstEntity;
        }

        public ENT.DMT_CustomerRegisterAdminView GetCustomerById(Guid guid)
        {
            ENT.DMT_CustomerRegisterAdminView Entity = new ENT.DMT_CustomerRegisterAdminView();
            Entity = clsDAL.GetCustomerById(guid);
            return Entity;
        }

        // this function for just referance for partial update field user have to create seperate function learn from this function.

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
                objDAL = null;
                clsDAL = null;
                Entity = null;
                lstEntity = null;
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~VisaApplicationDetail() {
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
