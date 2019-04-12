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
    public class ServiceMaster : COM.DALInterface, IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.ServiceMaster clsDAL = new DAL.ServiceMaster();
        public ENT.ServiceMaster Entity = new ENT.ServiceMaster();
        List<ENT.ServiceMaster> lstEntity;
        List<string> strvalidationResult = new List<string>();

        private List<string> ValidationEntry(object obj)
        {
            strvalidationResult.Clear();
            Entity = (ENT.ServiceMaster)obj;
            if (string.IsNullOrWhiteSpace(Entity.servicename)) { strvalidationResult.Add("Service Name Required!"); }
            if (string.IsNullOrWhiteSpace(Entity.servicecode)) { strvalidationResult.Add("Service Code Required!"); }
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
                    COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "serviceid");
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
        public bool UpdateStatus(Guid PrimarKey, COM.MyEnumration.MyStatus Status)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.ServiceMaster>(x => x.serviceid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.ServiceMaster>(x => x.Status), false);
                Entity.serviceid = PrimarKey;
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

        public object GetByPrimaryKey(ENT.ServiceMaster Entity)
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
        public bool UpdatePartial(ENT.ServiceMaster objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.ServiceMaster>(x => x.serviceid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.ServiceMaster>(x => x.servicename), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.ServiceMaster>(x => x.servicecode), false);
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

        public List<ENT.ServiceMaster> GetAll(string search)
        {
            lstEntity = new List<ENT.ServiceMaster>();
            lstEntity = clsDAL.GetList(search);
            return lstEntity;
        }
        public List<ENT.ServiceMaster> GetAllActive(string country_code)
        {
            lstEntity = new List<ENT.ServiceMaster>();
            lstEntity = clsDAL.GetListActived(country_code);
            return lstEntity;
        }

        public List<ENT.ServiceMaster> GetService(string countryid)
        {
            lstEntity = new List<ENT.ServiceMaster>();
            lstEntity = clsDAL.GetServiceByCountryid(countryid);
            return lstEntity;
        }


        #region mobile api

        public List<ENT.ServiceMasterView> GetServices(string countryId)
        {
            List<ENT.ServiceMasterView> lstEntity = new List<ENT.ServiceMasterView>();
            lstEntity = clsDAL.GetServices(countryId);
            return lstEntity;
        }

        #endregion

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
