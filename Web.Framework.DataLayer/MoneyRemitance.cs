using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using System.Data;

namespace Web.Framework.DataLayer
{
    public class MoneyRemitance
    {
        COM.DBHelper sqlhelper = new COM.DBHelper();


        #region DMT Beneficiary Class
        public struct DMT_BeneficiaryClass
        {
            public Int64 UserId { get; set; }
            public string BeneficiaryName { get; set; }
            public string BeneficiaryMobile { get; set; }
            public string CustomerMobile { get; set; }
            public Int64 CustomerIDF { get; set; }
            public string BankName { get; set; }
            public string IfscCode { get; set; }
            public string AccountNumber { get; set; }
            public string BranchName { get; set; }
            public string Address { get; set; }
            public string AddharCard { get; set; }
            public string Pincode { get; set; }
            public int Status { get; set; }
            public string IpAddress { get; set; }
            public string RequestNo { get; set; }
            public string Response { get; set; }
            public string BeneficiaryCode { get; set; }
        }
        #endregion


        #region DMT Get Beneficiary Class
        public struct DMT_GetBeneClass
        {
            public int UserId { get; set; }
            public string CustomerMobile { get; set; }
        }

        public DataTable DMT_Get_CustomerBeneficiary(DMT_GetBeneClass obj)
        {
            //SqlParameter[] param = new SqlParameter[2];
            //param[0] = new SqlParameter("@UserId", obj.UserId.ToString());
            //param[1] = new SqlParameter("@CustomerMobile", obj.CustomerMobile);

            string[,] param = { { "UserId", obj.UserId.ToString() }, { "CustomerMobile", obj.CustomerMobile } };
            DataTable dtTemp = sqlhelper.ExecuteProcForDataTable("DMT_GET_CUSTOMRBENEFICIARY", param);
            return dtTemp;
        }
        #endregion

        #region DMT Sufficent Class
        public struct DMT_SufFicentClass
        {
            public string Amount { get; set; }
            public int UserId { get; set; }
        }

        public COM.DBHelper.SQLReturnValue DMT_CheckSufficentBalance(DMT_SufFicentClass obj)
        {
            string[,] param = { { "UserId", obj.UserId.ToString()},
                            { "Amount", obj.Amount.ToString()}};
            COM.DBHelper.SQLReturnValue Mval = sqlhelper.ExecuteProcWithMessage("DMT_CHECK_SUFFICENTBAL", param, true);
            return Mval;
        }
        #endregion

        #region DMT Update Beneficiary Class
        public struct DMT_UpdateBeneClass
        {
            public int UserId { get; set; }
            public string BeneficiryCode { get; set; }
            public string BeneficiryAccNo { get; set; }
            public string BeneficiryMobile { get; set; }
        }

        public COM.DBHelper.SQLReturnValue DMT_UpdateBeneficiaryCode(DMT_UpdateBeneClass obj)
        {
            string[,] param = { { "UserId", obj.UserId.ToString()},
                            { "BeneficiryCode", obj.BeneficiryCode.ToString()},
                            { "BeneficiryAccNo", obj.BeneficiryAccNo.ToString()},
                            { "BeneficiryMobile", obj.BeneficiryMobile.ToString()}};
            COM.DBHelper.SQLReturnValue Mval = sqlhelper.ExecuteProcWithMessage("DMT_UPDATE_BENEFICIARY", param, true);
            return Mval;
        }
        #endregion

        #region DMT Delete Beneficiary Class
        public struct DMT_DeleBeneClass
        {
            public System.Guid userid { get; set; }
            public string BeneficiryCode { get; set; }
            public string CustomerMobile { get; set; }
        }

        public COM.DBHelper.SQLReturnValue DMT_Delete_BeneficiaryStatus(DMT_DeleBeneClass obj)
        {
            string[,] param = { { "UserId", obj.userid.ToString()},
                            { "BeneficiryCode", obj.BeneficiryCode.ToString()},
                            { "CustomerMobile", obj.CustomerMobile.ToString()}};
            COM.DBHelper.SQLReturnValue Mval = sqlhelper.ExecuteProcWithMessage("DMT_DELETE_BENEFI_STATUS", param, true);
            return Mval;

        }
        #endregion

        #region DMT Remittance
        public struct DMT_RemittParam
        {
            public string BeneficiryCode { get; set; }
            public string BeneficiryMobile { get; set; }
            public string CustomerMobile { get; set; }
            public string IFSC { get; set; }
            public string AccountNo { get; set; }
            public string TranPwd { get; set; }
            public string RountingType { get; set; }
            public string BeneficiaryName { get; set; }
            public string Remakrs { get; set; }
            public string Amount { get; set; }
            public string Mode { get; set; }
            public string RemitterId { get; set; }
            public string IpAddress { get; set; }
        }

        public struct DMT_Remitt
        {
            
            public Guid UserId { get; set; }
            public string BeneficiryCode { get; set; }
            public string BeneficiryMobile { get; set; }
            public string CustomerMobile { get; set; }
            public string IFSC { get; set; }
            public string AccountNo { get; set; }
            public string TranPwd { get; set; }
            public string RountingType { get; set; }
            public string BeneficiaryName { get; set; }
            public string Remakrs { get; set; }
            public decimal Amount { get; set; }
            public string Mode { get; set; }
            public string RemitterId { get; set; }
            public string IpAddress { get; set; }
            public string RequestNo { get; set; }
            public string Response { get; set; }

        }

        public COM.DBHelper.SQLReturnValue DMT_Remittance(DMT_Remitt obj)
        {
            string[,] param = {{ "USERID", obj.UserId.ToString()},
                            { "BENEFICIARYCODE", obj.BeneficiryCode.ToString()},
                            { "BENEFICIARYMOBILE", obj.BeneficiryMobile.ToString()},
                            { "CUSTOMERMOBILE", obj.CustomerMobile.ToString()},
                            { "IFSCCODE", obj.IFSC.ToString()},
                            { "ACOOUNTNO", obj.AccountNo.ToString()},
                            { "ROUTINGTYPE", obj.RountingType.ToString()},
                            { "BENEFICIARYNAME", obj.BeneficiaryName.ToString()},
                            { "REMARKS", obj.Remakrs.ToString()},
                            { "AMOUNT", obj.Amount.ToString()},
                            { "TRANSACTIONMODE", obj.Mode.ToString()},
                            { "RemitterId", obj.RemitterId.ToString()},
                            { "IpAddress", COM.GeneralClass.IpAddress()},
                            { "RequestNo", obj.RequestNo.ToString()},
                            { "Response", obj.Response.ToString()}};
            COM.DBHelper.SQLReturnValue Mval = sqlhelper.ExecuteProcWithMessage("DMT_REMITTANCE", param, true);
            return Mval;

        }
        #endregion
    }
}
