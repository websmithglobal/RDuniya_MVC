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
    public class OperatorSetup : TTCommonEntity
    {
        public OperatorSetup()
        {
            this.TableName = "Operators";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Operators", FieldName = "operatorid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "operatorid")]
        public System.Guid operatorid { get; set; }

        [TTAttributs("Operators", FieldName = "operatorname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "operatorname")]
        [MaxLength(100)]
        public string operatorname { get; set; }

        [TTAttributs("Operators", FieldName = "serviceid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "serviceid")]
        public System.Guid serviceid { get; set; }

        [TTAttributs("Operators", FieldName = "countryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "countryid")]
        public System.Guid countryid { get; set; }

        [TTAttributs("Operators", FieldName = "operatorcode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "operatorcode")]
        public int operatorcode { get; set; }

        [TTAttributs("Operators", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }


        [TTAttributs("Operators", FieldName = "Billerlogo", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Billerlogo")]
        [MaxLength(500)]
        public string Billerlogo { get; set; }


        [TTAttributs("Operators", FieldName = "Onlinevalidation", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Onlinevalidation")]
        [MaxLength(50)]
        public string Onlinevalidation { get; set; }

        [TTAttributs("Operators", FieldName = "Paymentvalidation", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Paymentvalidation")]
        [MaxLength(50)]
        public string Paymentvalidation { get; set; }


        [TTAttributs("Operators", FieldName = "auth_name", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_name")]
        [MaxLength(50)]
        public string auth_name { get; set; }


        [TTAttributs("Operators", FieldName = "auth_regex", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_regex")]
        [MaxLength(50)]
        public string auth_regex { get; set; }

        [TTAttributs("Operators", FieldName = "auth_msg", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_msg")]
        [MaxLength(-1)]
        public string auth_msg { get; set; }


        [TTAttributs("Operators", FieldName = "auth_datatype", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_datatype")]
        [MaxLength(50)]
        public string auth_datatype { get; set; }

        [TTAttributs("Operators", FieldName = "reqtype_normal", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "reqtype_normal")]
        public int reqtype_normal { get; set; }

        [TTAttributs("Operators", FieldName = "reqtype_special", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "reqtype_special")]
        public int reqtype_special { get; set; }

        [TTAttributs("Operators", FieldName = "nicename", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isTableField = false, isSelectField = true)]
        [Display(Name = "nicename")]
        [MaxLength(100)]
        public string nicename { get; set; }


        [TTAttributs("Operators", FieldName = "servicename", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isTableField = false, isSelectField = true)]
        [Display(Name = "servicename")]
        [MaxLength(100)]
        public string servicename { get; set; }


        [TTAttributs("Operators", FieldName = "auth_name2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_name2")]
        [MaxLength(50)]
        public string auth_name2 { get; set; }


        [TTAttributs("Operators", FieldName = "auth_regex2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_regex2")]
        [MaxLength(50)]
        public string auth_regex2 { get; set; }

        [TTAttributs("Operators", FieldName = "auth_msg2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_msg2")]
        [MaxLength(-1)]
        public string auth_msg2 { get; set; }


        [TTAttributs("Operators", FieldName = "auth_datatype2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_datatype2")]
        [MaxLength(50)]
        public string auth_datatype2 { get; set; }
    }

    public class OperatorView
    {
        [Key]
        [TTAttributs("Operators", FieldName = "operatorid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "operatorid")]
        public System.Guid operatorid { get; set; }

        [TTAttributs("Operators", FieldName = "operatorname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "operatorname")]
        [MaxLength(100)]
        public string operatorname { get; set; }

        [TTAttributs("Operators", FieldName = "serviceid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "serviceid")]
        public System.Guid serviceid { get; set; }

        [TTAttributs("Operators", FieldName = "countryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "countryid")]
        public System.Guid countryid { get; set; }

        [TTAttributs("Operators", FieldName = "operatorcode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "operatorcode")]
        public int operatorcode { get; set; }

        [TTAttributs("Operators", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }


        [TTAttributs("Operators", FieldName = "Billerlogo", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Billerlogo")]
        [MaxLength(500)]
        public string Billerlogo { get; set; }


        [TTAttributs("Operators", FieldName = "Onlinevalidation", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Onlinevalidation")]
        [MaxLength(50)]
        public string Onlinevalidation { get; set; }

        [TTAttributs("Operators", FieldName = "Paymentvalidation", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Paymentvalidation")]
        [MaxLength(50)]
        public string Paymentvalidation { get; set; }


        [TTAttributs("Operators", FieldName = "auth_name", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_name")]
        [MaxLength(50)]
        public string auth_name { get; set; }


        [TTAttributs("Operators", FieldName = "auth_regex", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_regex")]
        [MaxLength(50)]
        public string auth_regex { get; set; }

        [TTAttributs("Operators", FieldName = "auth_msg", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_msg")]
        [MaxLength(-1)]
        public string auth_msg { get; set; }


        [TTAttributs("Operators", FieldName = "auth_datatype", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_datatype")]
        [MaxLength(50)]
        public string auth_datatype { get; set; }

        [TTAttributs("Operators", FieldName = "reqtype_normal", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "reqtype_normal")]
        public int reqtype_normal { get; set; }

        [TTAttributs("Operators", FieldName = "reqtype_special", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "reqtype_special")]
        public int reqtype_special { get; set; }

        [TTAttributs("Operators", FieldName = "nicename", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isTableField = false, isSelectField = true)]
        [Display(Name = "nicename")]
        [MaxLength(100)]
        public string nicename { get; set; }

        [TTAttributs("Operators", FieldName = "servicename", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isTableField = false, isSelectField = true)]
        [Display(Name = "servicename")]
        [MaxLength(100)]
        public string servicename { get; set; }

        [TTAttributs("Operators", FieldName = "auth_name2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_name2")]
        [MaxLength(50)]
        public string auth_name2 { get; set; }

        [TTAttributs("Operators", FieldName = "auth_regex2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_regex2")]
        [MaxLength(50)]
        public string auth_regex2 { get; set; }

        [TTAttributs("Operators", FieldName = "auth_msg2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_msg2")]
        [MaxLength(-1)]
        public string auth_msg2 { get; set; }


        [TTAttributs("Operators", FieldName = "auth_datatype2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "auth_datatype2")]
        [MaxLength(50)]
        public string auth_datatype2 { get; set; }


        [TTAttributs("Operators", FieldName = "auth_maxlength", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "auth_maxlength")]
        public int auth_maxlength { get; set; }

        [TTAttributs("Operators", FieldName = "auth_maxlength2", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "auth_maxlength2")]
        public int auth_maxlength2 { get; set; }

        [TTAttributs("Operators", FieldName = "roffer_code", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "roffer_code")]
        [MaxLength(50)]
        public string roffer_code { get; set; }

        [TTAttributs("Operators", FieldName = "viewoffer_code", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "viewoffer_code")]
        [MaxLength(50)]
        public string viewoffer_code { get; set; }
    }

    public class Operators
    {
        /// <summary>
        /// this is account ref
        /// </summary>
        [Required]
        public string country_code { get; set; }

        /// <summary>
        /// this is account ref
        /// </summary>
        [Required]
        public string key { get; set; }
    }
}
