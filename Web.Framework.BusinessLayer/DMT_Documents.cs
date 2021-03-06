﻿using System;
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
    public class DMT_Documents : COM.DALInterface , IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.DMT_Documents clsDAL = new DAL.DMT_Documents();
        public ENT.DMT_Documents Entity = new ENT.DMT_Documents();
        List<ENT.DMT_Documents> lstEntity;

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "dd_id");
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

        public object GetByPrimaryKey(ENT.DMT_Documents Entity)
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
        public bool UpdatePartial(ENT.DMT_Documents objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_userid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_page1), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_page2), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_type), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_remarks), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.UpdatedDateTime), false);
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

        public bool UpdateApprove(ENT.DMT_Documents objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.dd_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_Documents>(x => x.UpdatedDateTime), false);
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

        public List<ENT.DMT_Documents> GetAll()
        {
            lstEntity = new List<ENT.DMT_Documents>();
            lstEntity = clsDAL.GetList();
            return lstEntity;
        }

        public List<ENT.DMT_DocumentsView> GetAllBySearch()
        {
            List<ENT.DMT_DocumentsView>  lstEntity = new List<ENT.DMT_DocumentsView>();
            lstEntity = clsDAL.GetAllBySearch();
            return lstEntity;
        }

        public ENT.DMT_Documents GetByUserId(Guid guid)
        {
            ENT.DMT_Documents Entity = new ENT.DMT_Documents();
            Entity = clsDAL.GetByUserId(guid);
            return Entity;
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
        // ~DMT_Documents() {
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