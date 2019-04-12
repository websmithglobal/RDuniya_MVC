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
    public class RechargeReport : IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.RechargeReport clsDAL = new DAL.RechargeReport();
        public ENT.RechargeReport Entity = new ENT.RechargeReport();
        List<ENT.RechargeReport> lstEntity;

        public List<ENT.RechargeReport> GetAllSearch(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid,int status,int operatorcode)
        {
            lstEntity = new List<ENT.RechargeReport>();
            lstEntity = clsDAL.GetListSearchList(ddrange, fromdate, todate, ddData, search, userlevel, userid, status, operatorcode);
            return lstEntity;
        }

        public List<ENT.RechargeReportApiView> GetAllSearchApi(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid, int status, int operatorcode)
        {
            List<ENT.RechargeReportApiView>  lstEntity = new List<ENT.RechargeReportApiView>();
            lstEntity = clsDAL.GetAllSearchApi(ddrange, fromdate, todate, ddData, search, userlevel, userid, status, operatorcode);
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
