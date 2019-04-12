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
    public class DMT_CustomerRegister : TTCommonEntity
    {
        public DMT_CustomerRegister()
        {
            this.TableName = "DMT_CustomerRegister";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_customerid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_customerid")]
        public System.Guid dmt_customerid { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_userid")]
        public System.Guid dmt_userid { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_mobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_mobile")]
        [MaxLength(50)]
        public String dmt_mobile { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_firstname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_firstname")]
        [MaxLength(50)]
        public string dmt_firstname { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_lastname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_lastname")]
        [MaxLength(50)]
        public string dmt_lastname { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_dateofbirth", ParamaterDataType = SqlDbType.Date)]
        [Display(Name = "dmt_dateofbirth")]
        public System.DateTime dmt_dateofbirth { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_address")]
        [MaxLength(-1)]
        public string dmt_address { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_pincode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_pincode")]
        [MaxLength(50)]
        public string dmt_pincode { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmt_status")]
        public int dmt_status { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_ipaddress")]
        [MaxLength(50)]
        public string dmt_ipaddress { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(100)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }
    }

    public class DMT_CustomerRegisterAdminView
    {
        [Key]
        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_customerid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_customerid")]
        public System.Guid dmt_customerid { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_userid")]
        public System.Guid dmt_userid { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_mobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_mobile")]
        [MaxLength(50)]
        public String dmt_mobile { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_firstname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_firstname")]
        [MaxLength(50)]
        public string dmt_firstname { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_lastname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_lastname")]
        [MaxLength(50)]
        public string dmt_lastname { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_dateofbirth", ParamaterDataType = SqlDbType.Date)]
        [Display(Name = "dmt_dateofbirth")]
        public System.DateTime dmt_dateofbirth { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_address")]
        [MaxLength(-1)]
        public string dmt_address { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_pincode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_pincode")]
        [MaxLength(50)]
        public string dmt_pincode { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmt_status")]
        public int dmt_status { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_ipaddress")]
        [MaxLength(50)]
        public string dmt_ipaddress { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(100)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }

        [Display(Name = "dmt_walletbalance")]
        public Decimal dmt_walletbalance { get; set; }

        [Display(Name = "dmt_transferlimit")]
        public Decimal dmt_transferlimit { get; set; }
    }

    public class DMT_CustomerRegisterApiView
    {
        [Key]
        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_customerid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_customerid")]
        public System.Guid dmt_customerid { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_userid")]
        public System.Guid dmt_userid { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_mobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_mobile")]
        [MaxLength(50)]
        public String dmt_mobile { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_firstname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_firstname")]
        [MaxLength(50)]
        public string dmt_firstname { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_lastname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_lastname")]
        [MaxLength(50)]
        public string dmt_lastname { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_dateofbirth", ParamaterDataType = SqlDbType.Date)]
        [Display(Name = "dmt_dateofbirth")]
        public System.DateTime dmt_dateofbirth { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_address")]
        [MaxLength(-1)]
        public string dmt_address { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_pincode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_pincode")]
        [MaxLength(50)]
        public string dmt_pincode { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmt_status")]
        public int dmt_status { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_ipaddress")]
        [MaxLength(50)]
        public string dmt_ipaddress { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(100)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_CustomerRegister", FieldName = "dmt_response", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }

        [Display(Name = "dmt_walletbalance")]
        public Decimal dmt_walletbalance { get; set; }

        [Display(Name = "dmt_transferlimit")]
        public Decimal dmt_transferlimit { get; set; }
    }

    public class EntityCustomerRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
    }
}
