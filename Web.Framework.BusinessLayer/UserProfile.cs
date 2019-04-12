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
    public class UserProfile : COM.DALInterface,IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.UserProfile clsDAL = new DAL.UserProfile();
        public ENT.UserProfile Entity = new ENT.UserProfile();
        List<ENT.UserProfile> lstEntity;

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "up_id");
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
        public ENT.UserProfile GetBalance(Guid id)
        {
            ENT.UserProfile lstEntity = new ENT.UserProfile();
            lstEntity = clsDAL.GetUserBalance(id);
            return lstEntity;
        }
        public ENT.UserProfile GetSlabName(Guid upperid)
        {
            ENT.UserProfile lstEntity = new ENT.UserProfile();
            lstEntity = clsDAL.GetUserSlabName(upperid);
            return lstEntity;
        }
        public bool UpdateStatus(Guid PrimarKey, COM.MyEnumration.MyStatus Status)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.Status), false);
                Entity.up_id = PrimarKey;
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

        public object GetByPrimaryKey(ENT.UserProfile Entity)
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
        public bool UpdatePartial(ENT.UserProfile objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_username), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_email), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.slabid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_address), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.UpdatedDateTime), false);
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

        public bool UpdatePartialOther(ENT.UserProfile objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_username), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_email), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.up_address), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.UpdatedDateTime), false);
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

        public List<ENT.UserProfile> GetAll(string search, COM.MyEnumration.Userlevel userlevel)
        {
            lstEntity = new List<ENT.UserProfile>();
            lstEntity = clsDAL.GetList(search, userlevel);
            return lstEntity;
        }
        public List<ENT.UserProfile> GetAllUnderUserList(string search, Guid userid, COM.MyEnumration.Userlevel userlevel)
        {
            lstEntity = new List<ENT.UserProfile>();
            lstEntity = clsDAL.GetUnderList(search, userid, userlevel);
            return lstEntity;
        }

        /// <summary>
        /// this method is used to get API users
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetApiUsers()
        {
            lstEntity = new List<ENT.UserProfile>();
            lstEntity = clsDAL.GetApiUsers();
            return lstEntity;
        }

        /// <summary>
        /// this method is used to get all MD and API users
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetMdApiUsers()
        {
            lstEntity = new List<ENT.UserProfile>();
            lstEntity = clsDAL.GetMdApiUsers();
            return lstEntity;
        }

        /// <summary>
        /// this method is used to get all Distributors
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetDistributors()
        {
            lstEntity = new List<ENT.UserProfile>();
            lstEntity = clsDAL.GetDistributors();
            return lstEntity;
        }

        /// <summary>
        /// this method is used to get all Distributors of Master Distributor with all detail
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfileApiView> GetMasterDownline(Guid userid)
        {
            List<ENT.UserProfileApiView>  lstEntity = new List<ENT.UserProfileApiView>();
            lstEntity = clsDAL.GetMasterDownline(userid);
            return lstEntity;
        }

        /// <summary>
        /// this method is used to get all Retailers
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetRetailers()
        {
            lstEntity = new List<ENT.UserProfile>();
            lstEntity = clsDAL.GetRetailers();
            return lstEntity;
        }


        /// <summary>
        /// this method is used to get all Retailer of Distributor with all detail
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfileApiView> GetDistributorDownline(Guid userid)
        {
            List<ENT.UserProfileApiView> lstEntity = new List<ENT.UserProfileApiView>();
            lstEntity = clsDAL.GetDistributotDownline(userid);
            return lstEntity;
        }

        public ENT.UserProfile SearchData(string id)
        {
            ENT.UserProfile lstEntity = new ENT.UserProfile();
            lstEntity = clsDAL.SearchCustomerDetails(id);
            return lstEntity;
        }

        public ENT.UserProfile GetUserProfile(string id)
        {
            ENT.UserProfile lstEntity = new ENT.UserProfile();
            lstEntity = clsDAL.GetUserProfile(id);
            return lstEntity;
        }

        public ENT.ApiUserProfile GetApiUserProfile(string id)
        {
            ENT.ApiUserProfile lstEntity = new ENT.ApiUserProfile();
            lstEntity = clsDAL.GetApiUserProfile(id);
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
