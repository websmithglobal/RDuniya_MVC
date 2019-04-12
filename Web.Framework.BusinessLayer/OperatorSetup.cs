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
    public class OperatorSetup : COM.DALInterface,IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.OperatorSetup clsDAL = new DAL.OperatorSetup();
        public ENT.OperatorSetup Entity = new ENT.OperatorSetup();
        List<ENT.OperatorSetup> lstEntity;
        List<string> strvalidationResult = new List<string>();

        private List<string> ValidationEntry(object obj)
        {
            strvalidationResult.Clear();
            Entity = (ENT.OperatorSetup)obj;
            if (string.IsNullOrWhiteSpace(Entity.operatorname)) { strvalidationResult.Add("Operator Name Required!"); }
            if (string.IsNullOrWhiteSpace(Entity.operatorcode.ToString())) { strvalidationResult.Add("Operator Code Required!"); }
            if (Entity.serviceid.ToString() == "00000000-0000-0000-0000-000000000000") { strvalidationResult.Add("Please Select Service."); }
            if (Entity.countryid.ToString() == "00000000-0000-0000-0000-000000000000") { strvalidationResult.Add("Please Select Country."); }
            if (string.IsNullOrWhiteSpace(Entity.Onlinevalidation.ToString())) { strvalidationResult.Add("Online Validation Required!"); }
            if (string.IsNullOrWhiteSpace(Entity.Paymentvalidation.ToString())) { strvalidationResult.Add("Payment Validation Required!"); }

            if (string.IsNullOrWhiteSpace(Entity.auth_name.ToString())) { strvalidationResult.Add("Parameters Name Required!"); }

            if (string.IsNullOrWhiteSpace(Entity.auth_regex.ToString())) { strvalidationResult.Add("Parameters Regex Required!"); }
            if (string.IsNullOrWhiteSpace(Entity.auth_msg.ToString())) { strvalidationResult.Add("Parameters Message Required!"); }

            if (string.IsNullOrWhiteSpace(Entity.auth_datatype.ToString())) { strvalidationResult.Add("Parameters DataType Required!"); }
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
                    COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "operatorid");
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
        public bool UpdateStatus(Guid PrimarKey, COM.MyEnumration.MyStatus Status)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.operatorid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.Status), false);
                Entity.operatorid = PrimarKey;
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
        public object GetByPrimaryKey(ENT.OperatorSetup Entity)
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
        public bool UpdatePartial(ENT.OperatorSetup objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.operatorid), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.operatorname), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.Onlinevalidation), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.Paymentvalidation), false);

                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_name), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_msg), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_datatype), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_regex), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.reqtype_normal), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.reqtype_special), false);

                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.Billerlogo), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.operatorcode), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_name2), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_msg2), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_datatype2), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.OperatorSetup>(x => x.auth_regex2), false);
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

        public List<ENT.OperatorSetup> GetAll(string search)
        {
            lstEntity = new List<ENT.OperatorSetup>();
            lstEntity = clsDAL.GetList(search);
            return lstEntity;
        }
        public List<ENT.OperatorSetup> GetAllActived(string country_code,string key)
        {
            lstEntity = new List<ENT.OperatorSetup>();
            lstEntity = clsDAL.GetListActive(country_code,key);
            return lstEntity;
        }
        public List<ENT.OperatorSetup> GetOperatorByService(string serviceid,string countryid)
        {
            lstEntity = new List<ENT.OperatorSetup>();
            lstEntity = clsDAL.GetOperator(serviceid, countryid);
            return lstEntity;
        }
        public List<ENT.OperatorSetup> CheckDuplicateCombination(List<Guid> ParentID, string strName, int operatorcode)
        {
            lstEntity = new List<ENT.OperatorSetup>();
            lstEntity = clsDAL.CheckDuplicate(ParentID, strName, operatorcode);
            return lstEntity;
        }

        /// <summary>
        /// This method is used to get all operators vailable for particular service in india.
        /// </summary>
        /// <param name="country_code"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<ENT.OperatorView> GetOperators(string country_code, string key)
        {
            List<ENT.OperatorView>  lstEntity = new List<ENT.OperatorView>();
            lstEntity = clsDAL.GetOperators(country_code, key);
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
