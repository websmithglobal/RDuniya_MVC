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
    public class Complain : COM.DALInterface , IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.Complain clsDAL = new DAL.Complain();
        public ENT.Complain Entity = new ENT.Complain();
        List<ENT.Complain> lstEntity;

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "complainid");
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

        public object GetByPrimaryKey(ENT.Complain Entity)
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
        public bool UpdatePartial(ENT.Complain objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.complainid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.rechargeid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.complaintype), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.detail), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.complainstatus), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.adminremarks), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.UpdatedDateTime), false);
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

        public bool UpdateStatus(ENT.Complain objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.complainid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.complainstatus), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.adminremarks), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Complain>(x => x.UpdatedDateTime), false);
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

        public List<ENT.Complain> GetAll()
        {
            lstEntity = new List<ENT.Complain>();
            lstEntity = clsDAL.GetList();
            return lstEntity;
        }


        public List<ENT.ComplainView> GetAllSearch(int ddrange, DateTime fromdate , DateTime todate)
        {
            List<ENT.ComplainView>  lstEntity = new List<ENT.ComplainView>();
            lstEntity = clsDAL.GetAllSearch(ddrange,fromdate,todate);
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
        // ~Complain() {
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