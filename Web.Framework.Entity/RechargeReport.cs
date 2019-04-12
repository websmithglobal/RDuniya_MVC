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
    public class RechargeReport : TTCommonEntity
    {
        public RechargeReport()
        {
            this.TableName = "Recharge";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Recharge", FieldName = "rechargeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeid")]
        public System.Guid rechargeid { get; set; }

        [TTAttributs("Recharge", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }


        [TTAttributs("Recharge", FieldName = "operatorname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "operatorname")]
        public string operatorname { get; set; }

        [TTAttributs("Recharge", FieldName = "operatorcode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "operatorcode")]
        public int operatorcode { get; set; }

        [TTAttributs("Recharge", FieldName = "numbertorecharge", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "numbertorecharge")]
        public string numbertorecharge { get; set; }

        [TTAttributs("Recharge", FieldName = "amount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "amount")]
        public decimal amount { get; set; }

        [TTAttributs("Recharge", FieldName = "beforebalance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "beforebalance")]
        public decimal beforebalance { get; set; }

        [TTAttributs("Recharge", FieldName = "balance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "balance")]
        public decimal balance { get; set; }

        [TTAttributs("Recharge", FieldName = "txid", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "txid")]
        public string txid { get; set; }

        [TTAttributs("Recharge", FieldName = "reqvia", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "reqvia")]
        public string reqvia { get; set; }

        [TTAttributs("Recharge", FieldName = "proccessdate", ParamaterDataType = SqlDbType.DateTime)]
        [Display(Name = "proccessdate")]
        public DateTime proccessdate { get; set; }

        [TTAttributs("Recharge", FieldName = "webusername", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "webusername")]
        public string webusername { get; set; }

        [TTAttributs("Recharge", FieldName = "accountref", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "accountref")]
        public string accountref { get; set; }

        [TTAttributs("Recharge", FieldName = "requestid", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "requestid")]
        public string requestid { get; set; }

        [TTAttributs("Recharge", FieldName = "ipaddress", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "ipaddress")]
        public string ipaddress { get; set; }

        [TTAttributs("Recharge", FieldName = "ApiTitle", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "ApiTitle")]
        public string ApiTitle { get; set; }

        [TTAttributs("Recharge", FieldName = "reqtype", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "reqtype")]
        public string reqtype { get; set; }

        [TTAttributs("Recharge", FieldName = "countrycode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "countrycode")]
        [MaxLength(50)]
        public string countrycode { get; set; }

        [TTAttributs("Recharge", FieldName = "commimd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commimd")]
        public decimal commimd { get; set; }


        [TTAttributs("Recharge", FieldName = "commisd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commisd")]
        public decimal commisd { get; set; }


        [TTAttributs("Recharge", FieldName = "commir", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "commir")]
        public decimal commir { get; set; }


        [TTAttributs("Recharge", FieldName = "charge", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "charge")]
        public decimal charge { get; set; }

        [TTAttributs("Recharge", FieldName = "status", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "status")]
        public int status { get; set; }

    }


    public class RechargeReportApiView
    {
        [Key]
        [TTAttributs("Recharge", FieldName = "rechargeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeid")]
        public System.Guid rechargeid { get; set; }

        [TTAttributs("Recharge", FieldName = "operatorname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "operatorname")]
        public string operatorname { get; set; }

        [TTAttributs("Recharge", FieldName = "numbertorecharge", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "numbertorecharge")]
        public string numbertorecharge { get; set; }

        [TTAttributs("Recharge", FieldName = "amount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "amount")]
        public decimal amount { get; set; }

        [TTAttributs("Recharge", FieldName = "txid", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "txid")]
        public string txid { get; set; }

        [TTAttributs("Recharge", FieldName = "reqvia", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "reqvia")]
        public string reqvia { get; set; }

        [TTAttributs("Recharge", FieldName = "proccessdate", ParamaterDataType = SqlDbType.DateTime)]
        [Display(Name = "proccessdate")]
        public DateTime proccessdate { get; set; }

        [TTAttributs("Recharge", FieldName = "accountref", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "accountref")]
        public string accountref { get; set; }

        [TTAttributs("Recharge", FieldName = "requestid", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "requestid")]
        public string requestid { get; set; }

        [TTAttributs("Recharge", FieldName = "reqtype", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "reqtype")]
        public string reqtype { get; set; }

        [TTAttributs("Recharge", FieldName = "status", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "status")]
        public int status { get; set; }

        public String proccessdatetext { get {
                return this.proccessdate.GetFormatedDateTime();
            }
        }
    }
}
