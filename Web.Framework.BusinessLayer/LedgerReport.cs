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

    public class LedgerReport : IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.LedgerReport clsDAL = new DAL.LedgerReport();
        public ENT.LedgerReport Entity = new ENT.LedgerReport();
        List<ENT.LedgerReport> lstEntity;

        public List<ENT.LedgerReport> GetAllSearch(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            lstEntity = new List<ENT.LedgerReport>();
            lstEntity = clsDAL.GetListSearchList(ddrange, fromdate, todate, ddData, search, userlevel, userid);
            return lstEntity;
        }

        public List<ENT.LedgerReportApiView> GetAllSearchApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            List<ENT.LedgerReportApiView>  lstEntity = new List<ENT.LedgerReportApiView>();
            lstEntity = clsDAL.GetListSearchListApi(ddrange, fromdate, todate, ddData, search, userlevel, userid);
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
