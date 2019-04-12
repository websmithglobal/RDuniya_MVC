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
    public class RofferAPI : COM.DALInterface, IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.RofferAPI clsDAL = new DAL.RofferAPI();
        public ENT.RofferAPI Entity = new ENT.RofferAPI();
        List<ENT.RofferAPI> lstEntity;
        List<string> strvalidationResult = new List<string>();

        private List<string> ValidationEntry(object obj)
        {
            strvalidationResult.Clear();
            Entity = (ENT.RofferAPI)obj;

            if (string.IsNullOrWhiteSpace(Entity.title)) { strvalidationResult.Add("Title Name is Required!"); }

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
                    COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "rofferid");
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

        public object GetByPrimaryKey(ENT.RofferAPI Entity)
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
        public bool UpdatePartial(ENT.RofferAPI objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.rofferid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.rofferurl), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.simpleurl), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.dthplanurl), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.dthinfourl), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.title), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.perdaylimit), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.UpdatedDateTime), false);
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

        public List<ENT.RofferAPI> GetAll(string search)
        {
            lstEntity = new List<ENT.RofferAPI>();
            lstEntity = clsDAL.GetList(search);
            return lstEntity;
        }

        public List<ENT.RofferAPI> GetAllActived()
        {
            lstEntity = new List<ENT.RofferAPI>();
            lstEntity = clsDAL.RofferActived();
            return lstEntity;
        }

        public List<ENT.RofferAPI> CheckDuplicateCombination(List<Guid> ParentID, string strName, string strName2)
        {
            lstEntity = new List<ENT.RofferAPI>();
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
        public bool UpdateStatus(Guid PrimarKey, COM.MyEnumration.MyStatus Status)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.rofferid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.RofferAPI>(x => x.Status), false);
                Entity.rofferid = PrimarKey;
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

    }
}
