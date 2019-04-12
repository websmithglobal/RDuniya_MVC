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
    public class Routing: COM.DALInterface,IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.Routing clsDAL = new DAL.Routing();
        public ENT.Routing Entity = new ENT.Routing();
        List<string> strvalidationResult = new List<string>();
        List<ENT.Routing> lstEntity;


        private List<string> ValidationEntry(object obj)
        {
            strvalidationResult.Clear();
            Entity = (ENT.Routing)obj;
            if (string.IsNullOrWhiteSpace(Entity.routetitle)) { strvalidationResult.Add("route title Required!"); }
            if (Entity.operatorid.ToString() == "00000000-0000-0000-0000-000000000000") { strvalidationResult.Add("Please Select Operator."); }
            if (Entity.amountmethod.ToString() == "-1") { strvalidationResult.Add("Please Select amount method."); }
            if (Entity.rechargemethod.ToString() == "-1") { strvalidationResult.Add("Please Select recharge method."); }
            if (Entity.amountmethod.ToString() == "0") { Entity.amountrangefrom = 0; Entity.amountrangeto = 0; }
            if (Entity.amountmethod.ToString() == "1") { Entity.amountSpecific = "0"; }
            if (Entity.rechargemethod.ToString() == "0") { Entity.machineid = new Guid("00000000-0000-0000-0000-000000000000"); Entity.smartid = new Guid("00000000-0000-0000-0000-000000000000"); }
            if (Entity.rechargemethod.ToString() == "1") { Entity.apiid = new Guid("00000000-0000-0000-0000-000000000000"); Entity.smartid = new Guid("00000000-0000-0000-0000-000000000000"); }
            if (Entity.rechargemethod.ToString() == "2") { Entity.apiid = new Guid("00000000-0000-0000-0000-000000000000"); Entity.machineid = new Guid("00000000-0000-0000-0000-000000000000"); }
            return strvalidationResult;
        }
        
        public bool Insert(object obj)
        {

            bool blnResult = false;
            try
            {
                strvalidationResult = ValidationEntry(obj);
                if (strvalidationResult.Count() == 0)
                {
                    COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "routeid");
                    if (objDAL.Insert(obj))
                    {
                        blnResult = true;
                    }

                }
                else { throw new Exception(string.Join("<br />", strvalidationResult)); }
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
                strvalidationResult = ValidationEntry(obj);
                if (strvalidationResult.Count() == 0)
                {
                    if (objDAL.Update(obj))
                    {
                        blnResult = true;
                    }
                }
                else { throw new Exception(string.Join("<br />", strvalidationResult)); }
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
        public object GetByPrimaryKey(ENT.Routing Entity)
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
        public bool UpdateStatus(Guid PrimarKey, COM.MyEnumration.MyStatus Status)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Routing>(x => x.routeid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Routing>(x => x.Status), false);
                Entity.routeid = PrimarKey;
                Entity.Status = Status;
                if (objDAL.SaveChanges(dctFields, Entity))
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
        public List<ENT.Routing> GetAll(string search)
        {
            lstEntity = new List<ENT.Routing>();
            lstEntity = clsDAL.GetList(search);
            return lstEntity;
        }
        public List<ENT.Routing> CheckDuplicateCombination(List<Guid> ParentID, string strName, string strName2)
        {
            lstEntity = new List<ENT.Routing>();
            lstEntity = clsDAL.CheckDuplicate(ParentID, strName, strName2);
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
