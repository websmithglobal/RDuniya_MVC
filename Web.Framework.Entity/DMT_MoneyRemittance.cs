using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data;
using System.Text;
using Web.Framework.Common;


namespace Web.Framework.Entity
{
    public class DMT_MoneyRemittance : TTCommonEntity
    {
        public DMT_MoneyRemittance()
        {
            this.TableName = "DMT_MoneyRemittance";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "mt_id")]
        public System.Guid mt_id { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_customermobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_customermobile")]
        public string mt_customermobile { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_BeneficiryMobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_BeneficiryMobile")]
        public string mt_BeneficiryMobile { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_accountnumber", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_accountnumber")]
        public string mt_accountnumber { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_ifsc", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_ifsc")]
        public string mt_ifsc { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_beneficiarycode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_beneficiarycode")]
        public string mt_beneficiarycode { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_amount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_amount")]
        public decimal mt_amount { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_routingtype", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_routingtype")]
        public string mt_routingtype { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_BeneficiaryName", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_BeneficiaryName")]
        public string mt_BeneficiaryName { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_remarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_remarks")]
        [MaxLength(-1)]
        public string mt_remarks { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_mode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_mode")]
        public string mt_mode { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_entrydate", ParamaterDataType = SqlDbType.DateTime)]
        [Display(Name = "mt_entrydate")]
        public System.DateTime mt_entrydate { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_charge", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_charge")]
        public decimal mt_charge { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_beforebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_beforebal")]
        public decimal mt_beforebal { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_balance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_balance")]
        public decimal mt_balance { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_totalamount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_totalamount")]
        public decimal mt_totalamount { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_mdcommi", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_mdcommi")]
        public decimal mt_mdcommi { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_sdcommi", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_sdcommi")]
        public decimal mt_sdcommi { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_rcommi", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "mt_rcommi")]
        public decimal mt_rcommi { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_userlevel", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "mt_userlevel")]
        public int mt_userlevel { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_Ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_Ipaddress")]
        public string mt_Ipaddress { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_RequestNo", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_RequestNo")]
        public string mt_RequestNo { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_Response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_Response")]
        [MaxLength(-1)]
        public string mt_Response { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "mt_RemitterId", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "mt_RemitterId")]
        public string mt_RemitterId { get; set; }

        [TTAttributs("DMT_MoneyRemittance", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public int Status { get; set; }

        [TTAttributs("", FieldName = "customername", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "customername")]
        public string customername { get; set; }
    }
}