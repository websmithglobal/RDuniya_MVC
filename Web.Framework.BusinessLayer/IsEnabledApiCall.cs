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
    public class IsEnabledApiCall : COM.DALInterface, IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.IsEnabledApiCall clsDAL = new DAL.IsEnabledApiCall();
        public ENT.IsEnabledApiCall Entity = new ENT.IsEnabledApiCall();
        List<ENT.IsEnabledApiCall> lstEntity;

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "isenableid");
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

        public object GetByPrimaryKey(ENT.IsEnabledApiCall Entity)
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
        public bool UpdatePartial(ENT.IsEnabledApiCall objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.isenableid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.enabletext), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.Isenabled), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.ipaddress), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.IsEnabledApiCall>(x => x.UpdatedDateTime), false);
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

        public List<ENT.IsEnabledApiCall> GetAll()
        {
            lstEntity = new List<ENT.IsEnabledApiCall>();
            lstEntity = clsDAL.GetList();
            return lstEntity;
        }

        public ENT.IsEnabledApiCall GetIsEnabled(Guid id)
        {
            ENT.IsEnabledApiCall lstEntity = new ENT.IsEnabledApiCall();
            lstEntity = clsDAL.GetIsEnable(id);
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
        // ~ErrorLog() {
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
