using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using DAL = Web.Framework.DataLayer;

namespace Web.Framework.BusinessLayer
{
    
    public class MasterDistributor : IDisposable
    {
        COM.DBHelper SqlHelper = new COM.DBHelper();
        public COM.MEMBERS.SQLReturnMessageNValue UpdateSlab(Guid slabid,Guid userid)
        {
            return SqlHelper.ExecuteProceduerWithMessageNValue("UPDATE_SLAB", new object[,] {
              {"slabid",slabid},{"userid",userid}}, 3);
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
        // ~Recharge() {
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
