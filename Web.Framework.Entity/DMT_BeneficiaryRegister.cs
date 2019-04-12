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
    public class DMT_BeneficiaryRegister : TTCommonEntity
    {
        public DMT_BeneficiaryRegister()
        {
            this.TableName = "DMT_BeneficiaryRegister";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiaryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_beneficiaryid")]
        public System.Guid dmt_beneficiaryid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiaryname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_beneficiaryname")]
        [MaxLength(50)]
        public string dmt_beneficiaryname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiarymobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_beneficiarymobile")]
        [MaxLength(50)]
        public string dmt_beneficiarymobile { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_customerid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_customerid")]
        public System.Guid dmt_customerid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_bankname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_bankname")]
        [MaxLength(50)]
        public string dmt_bankname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_ifsc", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_ifsc")]
        [MaxLength(50)]
        public string dmt_ifsc { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_accountnumber", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_accountnumber")]
        [MaxLength(50)]
        public string dmt_accountnumber { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_branchname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_branchname")]
        [MaxLength(50)]
        public string dmt_branchname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_address")]
        [MaxLength(-1)]
        public string dmt_address { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_addharcard", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_addharcard")]
        [MaxLength(50)]
        public string dmt_addharcard { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmt_status")]
        public int dmt_status { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_Ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_Ipaddress")]
        [MaxLength(50)]
        public string dmt_Ipaddress { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(100)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_pincode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_pincode")]
        [MaxLength(50)]
        public string dmt_pincode { get; set; }
    }

    public class DMT_BeneficiaryRegisterAdminView
    {
        [Key]
        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiaryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_beneficiaryid")]
        public System.Guid dmt_beneficiaryid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiaryname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_beneficiaryname")]
        [MaxLength(50)]
        public string dmt_beneficiaryname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiarymobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_beneficiarymobile")]
        [MaxLength(50)]
        public string dmt_beneficiarymobile { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_bankname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_bankname")]
        [MaxLength(50)]
        public string dmt_bankname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_ifsc", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_ifsc")]
        [MaxLength(50)]
        public string dmt_ifsc { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_accountnumber", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_accountnumber")]
        [MaxLength(50)]
        public string dmt_accountnumber { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_branchname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_branchname")]
        [MaxLength(50)]
        public string dmt_branchname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_address")]
        [MaxLength(-1)]
        public string dmt_address { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_addharcard", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_addharcard")]
        [MaxLength(50)]
        public string dmt_addharcard { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmt_status")]
        public int dmt_status { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_Ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_Ipaddress")]
        [MaxLength(50)]
        public string dmt_Ipaddress { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(100)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_pincode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_pincode")]
        [MaxLength(50)]
        public string dmt_pincode { get; set; }

        [TTAttributs("", FieldName = "dmt_remitterId", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_remitterId")]
        public string dmt_remitterId { get; set; }

    }

    public class DMT_BeneficiaryRegisterApiView
    {
        [Key]
        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiaryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_beneficiaryid")]
        public System.Guid dmt_beneficiaryid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiaryname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_beneficiaryname")]
        [MaxLength(50)]
        public string dmt_beneficiaryname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_beneficiarymobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_beneficiarymobile")]
        [MaxLength(50)]
        public string dmt_beneficiarymobile { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_bankname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_bankname")]
        [MaxLength(50)]
        public string dmt_bankname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_ifsc", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_ifsc")]
        [MaxLength(50)]
        public string dmt_ifsc { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_accountnumber", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_accountnumber")]
        [MaxLength(50)]
        public string dmt_accountnumber { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_branchname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_branchname")]
        [MaxLength(50)]
        public string dmt_branchname { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_address")]
        [MaxLength(-1)]
        public string dmt_address { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_addharcard", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_addharcard")]
        [MaxLength(50)]
        public string dmt_addharcard { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmt_status")]
        public int dmt_status { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_Ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_Ipaddress")]
        [MaxLength(50)]
        public string dmt_Ipaddress { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(100)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }

        [TTAttributs("DMT_BeneficiaryRegister", FieldName = "dmt_pincode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_pincode")]
        [MaxLength(50)]
        public string dmt_pincode { get; set; }

        [TTAttributs("", FieldName = "dmt_remitterId", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_remitterId")]
        public string dmt_remitterId { get; set; }

    }
}
