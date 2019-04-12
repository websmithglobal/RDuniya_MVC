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
    public class Recharge : IDisposable, COM.DALInterface
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.Recharge clsDAL = new DAL.Recharge();
        public ENT.Recharge Entity = new ENT.Recharge();
        List<ENT.Recharge> lstEntity;

        COM.DBHelper SqlHelper = new COM.DBHelper();

        public COM.MEMBERS.SQLReturnMessageNValue DoRecharge(ENT.Recharge obj, Guid userid, string ipaadress)
        {
            return SqlHelper.ExecuteProceduerWithMessageNValue("ADD_REQUEST_TO_RECHARGE", new object[,] {
              {"USERID",userid}
            , { "OPERATORCODE",obj.operatorcode.ToString()}
            , { "COUNTRYCODE", obj.countrycode}
            , { "AMTTORECH", obj.amount}
            , { "NUMTORECH", obj.numbertorecharge}
            , { "REQVIA", obj.reqvia}
            , { "RCTYPE", obj.reqtype}
            , { "ACCOUNTREF", obj.accountref}
            , { "IPADDRESS", ipaadress }
            , { "OPTIONALPARAM", obj.optionalparam == null ? string.Empty : obj.optionalparam  }
            , { "OPTIONALPARAM1", obj.optionalparam1 == null ? string.Empty : obj.optionalparam1 }}, 3);
        }

        public COM.MEMBERS.SQLReturnMessageNValue UpdateRecharge(string accountid, string transid, int status, string response)
        {
            return SqlHelper.ExecuteProceduerWithMessageNValue("UPDATE_REQUEST", new object[,] {
              {"accountid",accountid}
            , { "transid",transid}
            , { "response",response}
            , { "status", status}}, 3);
        }
        public class OurResponseClass
        {
            public string accountid { get; set; }
            public string transid { get; set; }
            public int status { get; set; }
            public string response { get; set; }
        }

        /*------------- FRAMEWORK CODE --------------*/

        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
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

        public object GetByPrimaryKey(ENT.Recharge Entity)
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
        public bool UpdatePartial(ENT.Recharge objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.rechargeid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.countrycode), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.operatorcode), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.numbertorecharge), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.amount), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.readstatus), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.txid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.commimd), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.commisd), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.commir), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.commia), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.charge), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.userid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.beforebalance), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.balance), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.reqtype), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.reqvia), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.proccessdate), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.rechargemethod), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.routeid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.webusername), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.requestid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.accountref), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.optionalparam), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.optionalparam1), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.ipaddress), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.Recharge>(x => x.UpdatedDateTime), false);
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

        public List<ENT.Recharge> GetAll()
        {
            lstEntity = new List<ENT.Recharge>();
            lstEntity = clsDAL.GetList();
            return lstEntity;
        }

        public List<ENT.Recharge> GetPending(Guid userid,String search)
        {
            lstEntity = new List<ENT.Recharge>();
            lstEntity = clsDAL.GetPendingList(userid, search);
            return lstEntity;
        }

        public List<ENT.Recharge> GetPendingListAdmin(String search)
        {
            lstEntity = new List<ENT.Recharge>();
            lstEntity = clsDAL.GetPendingListAdmin(search);
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
