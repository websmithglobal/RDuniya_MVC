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
    public class WalletRequest : COM.DALInterface , IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.WalletRequest clsDAL = new DAL.WalletRequest();
        public ENT.WalletRequest Entity = new ENT.WalletRequest();
        List<ENT.WalletRequest> lstEntity;

        public List<ENT.WalletRequest> GetAllSearch(int ddrange, DateTime fromdate, DateTime todate, int ddData, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            lstEntity = new List<ENT.WalletRequest>();
            lstEntity = clsDAL.GetApprovedList(ddrange, fromdate, todate, ddData, userlevel, userid);
            return lstEntity;
        }

        public List<ENT.WalletRequestApiView> GetAllSearchApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            List<ENT.WalletRequestApiView> lstEntity = new List<ENT.WalletRequestApiView>();
            lstEntity = clsDAL.GetAllSearchApi(ddrange, fromdate, todate, ddData, userlevel, userid);
            return lstEntity;
        }

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "wr_id");
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

        public object GetByPrimaryKey(ENT.WalletRequest Entity)
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

        public ENT.WalletRequest GetRequestById(Guid wr_id)
        {
            ENT.WalletRequest Entity = new ENT.WalletRequest();
            Entity = clsDAL.GetRequestById(wr_id);
            return Entity;
        }

        // this function for just referance for partial update field user have to create seperate function learn from this function.
        public bool UpdatePartial(ENT.WalletRequest objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_bankname), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_accountno), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_amount), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_refrenceid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_remakrs), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.wr_status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.WalletRequest>(x => x.UpdatedDateTime), false);
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

        public List<ENT.WalletRequest> GetAll()
        {
            lstEntity = new List<ENT.WalletRequest>();
            lstEntity = clsDAL.GetList();
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
        // ~WalletRequest() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
