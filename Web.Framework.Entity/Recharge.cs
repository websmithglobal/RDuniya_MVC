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
    public class Recharge : TTCommonEntity
    {
        public Recharge()
        {
            this.TableName = "Recharge";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }

        [TTAttributs("Recharge", FieldName = "rechargeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeid")]
        public System.Guid rechargeid { get; set; }

        [TTAttributs("Recharge", FieldName = "countrycode", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "countrycode")]
        public string countrycode { get; set; }

        [TTAttributs("Recharge", FieldName = "operatorcode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "operatorcode")]
        public int operatorcode { get; set; }

        [TTAttributs("Recharge", FieldName = "numbertorecharge", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "numbertorecharge")]
        public string numbertorecharge { get; set; }

        [TTAttributs("Recharge", FieldName = "amount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "amount")]
        public decimal amount { get; set; }

        [TTAttributs("Recharge", FieldName = "readstatus", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "readstatus")]
        public int readstatus { get; set; }

        [TTAttributs("Recharge", FieldName = "txid", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "txid")]
        [MaxLength(-1)]
        public string txid { get; set; }

        [TTAttributs("Recharge", FieldName = "commimd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commimd")]
        public decimal commimd { get; set; }

        [TTAttributs("Recharge", FieldName = "commisd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commisd")]
        public decimal commisd { get; set; }

        [TTAttributs("Recharge", FieldName = "commir", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commir")]
        public decimal commir { get; set; }

        [TTAttributs("Recharge", FieldName = "commia", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commia")]
        public decimal commia { get; set; }

        [TTAttributs("Recharge", FieldName = "charge", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "charge")]
        public decimal charge { get; set; }

        [TTAttributs("Recharge", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("Recharge", FieldName = "beforebalance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "beforebalance")]
        public decimal beforebalance { get; set; }

        [TTAttributs("Recharge", FieldName = "balance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "balance")]
        public decimal balance { get; set; }

        [TTAttributs("Recharge", FieldName = "status", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "status")]
        public int status { get; set; }

        [TTAttributs("Recharge", FieldName = "reqtype", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "reqtype")]
        public int reqtype { get; set; }

        [TTAttributs("Recharge", FieldName = "reqvia", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "reqvia")]
        public string reqvia { get; set; }

        [TTAttributs("Recharge", FieldName = "proccessdate", ParamaterDataType = SqlDbType.DateTime)]
        [Display(Name = "proccessdate")]
        public System.DateTime proccessdate { get; set; }

        [TTAttributs("Recharge", FieldName = "rechargemethod", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "rechargemethod")]
        public int rechargemethod { get; set; }

        [TTAttributs("Recharge", FieldName = "routeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "routeid")]
        public System.Guid routeid { get; set; }

        [TTAttributs("Recharge", FieldName = "webusername", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "webusername")]
        public string webusername { get; set; }

        [TTAttributs("Recharge", FieldName = "requestid", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "requestid")]
        [MaxLength(32)]
        public string requestid { get; set; }

        [TTAttributs("Recharge", FieldName = "accountref", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "accountref")]
        [MaxLength(32)]
        public string accountref { get; set; }

        [TTAttributs("Recharge", FieldName = "optionalparam", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "optionalparam")]
        [MaxLength(100)]
        public string optionalparam { get; set; }

        [TTAttributs("Recharge", FieldName = "optionalparam1", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "optionalparam1")]
        [MaxLength(100)]
        public string optionalparam1 { get; set; }

        [TTAttributs("Recharge", FieldName = "ipaddress", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "ipaddress")]
        public string ipaddress { get; set; }

        [TTAttributs("", FieldName = "reqtypetext", ParamaterDataType = SqlDbType.VarChar,isTableField = false)]
        [Display(Name = "reqtypetext")]
        public string reqtypetext {
            get
            {
                if (this.reqtype == 0) { return "Normal"; }
                else if (this.reqtype == 1) { return "Special"; }
                else { return string.Empty; }
            }
        }

        [TTAttributs("", FieldName = "operatorname", ParamaterDataType = SqlDbType.VarChar, isTableField = false)]
        [Display(Name = "operatorname")]
        public string operatorname { get; set; }

        [TTAttributs("", FieldName = "requestby", ParamaterDataType = SqlDbType.VarChar, isTableField = false)]
        [Display(Name = "requestby")]
        public string requestby { get; set; }
    }
}