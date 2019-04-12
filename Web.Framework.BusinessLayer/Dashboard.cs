using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Web.Framework.Entity;
using DAL = Web.Framework.DataLayer;
using COM = Web.Framework.Common;

namespace Web.Framework.BusinessLayer
{
    public class Dashboard:IDisposable
    {
        ENT.Dashboard objDashboard = new ENT.Dashboard();
        DAL.Dashboard objDalDashboard = new DAL.Dashboard();

        public ENT.Dashboard GetDashboardData(Guid userid,COM.MyEnumration.Userlevel userlevel)
        {
            try
            {
                objDashboard = new ENT.Dashboard();
                objDashboard = objDalDashboard.GetDashboardData(userid, userlevel);
                return objDashboard;
            }
            catch (Exception ex)
            {
                throw;
            }
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
                objDashboard = null;
                objDalDashboard = null;
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
