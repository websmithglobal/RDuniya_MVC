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
    public class DMT_MoneyRemittance : COM.DALInterface, IDisposable
    {
        DAL.CRUDOperation objDAL = new DAL.CRUDOperation();
        DAL.DMT_MoneyRemittance clsDAL = new DAL.DMT_MoneyRemittance();
        public ENT.DMT_MoneyRemittance Entity = new ENT.DMT_MoneyRemittance();
        List<ENT.DMT_MoneyRemittance> lstEntity;

        COM.DBHelper SqlHelper = new COM.DBHelper();

        public COM.MEMBERS.SQLReturnMessageNValue DMT_GetCharge(String Amount, Guid mainid, int userlevel)
        {
            try
            {
                string[,] param = { { "AMOUNT", Amount }, { "mainid", mainid.ToString() }, { "userlevel", userlevel.ToString() } };
                COM.MEMBERS.SQLReturnMessageNValue Mval = SqlHelper.ExecuteProcedureReturnMessageNValue("DMT_GET_CHARGE", param);
                return Mval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public COM.MEMBERS.SQLReturnMessageNValue DMT_VALIDATE_Transaction(String UserId, String Amount)
        {
            try
            {
                string[,] param = {
                                { "USERID", UserId},
                                { "AMOUNT", Amount}
                };
                COM.MEMBERS.SQLReturnMessageNValue Mval = SqlHelper.ExecuteProcedureReturnMessageNValue("DMT_REMITTANCE_VALID", param);
                return Mval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public COM.MEMBERS.SQLReturnMessageNValue DMT_REMITTANCE(ENT.DMT_MoneyRemittance obj)
        {
            try
            {
                string[,] param = { 
                            { "USERID", obj.userid.ToString()},
                            { "BENEFICIARYCODE", obj.mt_beneficiarycode.ToString()},
                            { "BENEFICIARYMOBILE", obj.mt_BeneficiryMobile.ToString()},
                            { "CUSTOMERMOBILE", obj.mt_customermobile.ToString()},
                            { "IFSCCODE", obj.mt_ifsc.ToString()},
                            { "ACOOUNTNO", obj.mt_accountnumber.ToString()},
                            { "ROUTINGTYPE", obj.mt_routingtype.ToString()},
                            { "BENEFICIARYNAME", obj.mt_BeneficiaryName.ToString()},
                            { "REMARKS", obj.mt_remarks.ToString()},
                            { "AMOUNT", obj.mt_amount.ToString()},
                            { "TRANSACTIONMODE", obj.mt_mode.ToString()},
                            { "RemitterId", obj.mt_RemitterId.ToString()},
                            { "IpAddress", obj.mt_Ipaddress.ToString()},
                            { "RequestNo", obj.mt_RequestNo.ToString()},
                            { "Response", obj.mt_Response.ToString()}
                };

                COM.MEMBERS.SQLReturnMessageNValue Mval = SqlHelper.ExecuteProcedureReturnMessageNValue("DMT_REMITTANCE", param);

                return Mval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*------------- FRAMEWORK CODE --------------*/

        #region framework code
        public bool Insert(object obj)
        {
            bool blnResult = false;
            try
            {
                COM.HelperMethod.SetValueInObject(obj, Guid.NewGuid(), "mt_id");
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

        public object GetByPrimaryKey(ENT.DMT_MoneyRemittance Entity)
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
        public bool UpdatePartial(ENT.DMT_MoneyRemittance objEntity)
        {
            bool blnResult = false;
            try
            {
                //Create Fields List in dictionary
                Dictionary<string, bool> dctFields = new Dictionary<string, bool>();
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_id), true);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.userid), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_customermobile), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_BeneficiryMobile), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_accountnumber), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_ifsc), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_beneficiarycode), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_amount), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_routingtype), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_BeneficiaryName), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_remarks), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_mode), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_entrydate), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_charge), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_beforebal), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_balance), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_totalamount), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_Ipaddress), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_RequestNo), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_Response), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.mt_RemitterId), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.Status), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.SystemDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.CreatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.CreatedDateTime), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.UpdatedBy), false);
                dctFields.Add(COM.HelperMethod.PropertyName<ENT.DMT_MoneyRemittance>(x => x.UpdatedDateTime), false);
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

        public List<ENT.DMT_MoneyRemittance> GetAll()
        {
            lstEntity = new List<ENT.DMT_MoneyRemittance>();
            lstEntity = clsDAL.GetList();
            return lstEntity;
        }

        public List<ENT.DMT_MoneyRemittance> GetBySearch(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            lstEntity = new List<ENT.DMT_MoneyRemittance>();
            lstEntity = clsDAL.GetBySearch(ddrange,fromdate,todate,ddData,search,userlevel,userid);
            return lstEntity;
        }


        public ENT.DMT_MoneyRemittance GetById(Guid guid)
        {
            ENT.DMT_MoneyRemittance Entity = new ENT.DMT_MoneyRemittance();
            Entity = clsDAL.GetById(guid);
            return Entity;
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
        // ~DMT_MoneyRemittance() {
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
        #endregion
    }
}
