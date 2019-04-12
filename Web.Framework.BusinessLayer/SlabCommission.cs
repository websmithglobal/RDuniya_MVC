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
    public class SlabCommission : IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.SlabCommission clsDAL = new DAL.SlabCommission();
        public ENT.SlabCommission Entity = new ENT.SlabCommission();
        List<ENT.SlabCommission> lstEntity;
        List<string> strvalidationResult = new List<string>();

        public List<ENT.SlabCommission> GetAll(Guid slabid)
        {
            lstEntity = new List<ENT.SlabCommission>();
            lstEntity = clsDAL.GetList(slabid);
            return lstEntity;
        }

        public bool DeleteByID(Guid slabid)
        {
            bool blnResult = false;
            try
            {
                int outval = objDAL.ExecuteQuery("delete from SlabCommision where slabid='"+ slabid + "'");
            }
            catch (Exception)
            {
                throw;
            }
            return blnResult;
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
        public int BulkInsert(List<ENT.SlabCommission> Entity)
        {
            int objResult = 0;
            try
            {
                DAL.CRUDOperation tt = new DAL.CRUDOperation();

                String query = string.Empty;

                foreach (ENT.SlabCommission p in Entity)
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
