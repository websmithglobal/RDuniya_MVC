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
    public class FundReport : TTCommonEntity
    {
        public FundReport()
        {
            this.TableName = "FundTransfer";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("FundTransfer", FieldName = "fundid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "fundid")]
        public System.Guid fundid { get; set; }


        [TTAttributs("FundTransfer", FieldName = "UserName", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserName")]
        [MaxLength(100)]
        public string UserName { get; set; }
        
        [TTAttributs("FundTransfer", FieldName = "Transby", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Transby")]
        [MaxLength(100)]
        public string Transby { get; set; }

        [TTAttributs("FundTransfer", FieldName = "credit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "credit")]
        public decimal credit { get; set; }

        [TTAttributs("FundTransfer", FieldName = "debit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "debit")]
        public decimal debit { get; set; }

        [TTAttributs("FundTransfer", FieldName = "tooldbal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "tooldbal")]
        public decimal tooldbal { get; set; }

        [TTAttributs("FundTransfer", FieldName = "toclosebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "toclosebal")]
        public decimal toclosebal { get; set; }

        
        [TTAttributs("FundTransfer", FieldName = "fromoldbal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "fromoldbal")]
        public decimal fromoldbal { get; set; }

        [TTAttributs("FundTransfer", FieldName = "fromclosebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "fromclosebal")]
        public decimal fromclosebal { get; set; }


        [TTAttributs("FundTransfer", FieldName = "reqvia", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "reqvia")]
        [MaxLength(50)]
        public string reqvia { get; set; }

        [TTAttributs("FundTransfer", FieldName = "UserLevel", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserLevel")]
        [MaxLength(100)]
        public string UserLevel { get; set; }

        [TTAttributs("FundTransfer", FieldName = "remarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "remarks")]
        [MaxLength(500)]
        public string remarks { get; set; }
    }

    public class FundReportApiView
    {
        [Key]
        [TTAttributs("FundTransfer", FieldName = "fundid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "fundid")]
        public System.Guid fundid { get; set; }

        [TTAttributs("FundTransfer", FieldName = "UserName", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserName")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [TTAttributs("FundTransfer", FieldName = "Transby", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Transby")]
        [MaxLength(100)]
        public string Transby { get; set; }

        [TTAttributs("FundTransfer", FieldName = "credit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "credit")]
        public decimal credit { get; set; }

        [TTAttributs("FundTransfer", FieldName = "debit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "debit")]
        public decimal debit { get; set; }

        [TTAttributs("FundTransfer", FieldName = "tooldbal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "tooldbal")]
        public decimal tooldbal { get; set; }

        [TTAttributs("FundTransfer", FieldName = "toclosebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "toclosebal")]
        public decimal toclosebal { get; set; }

        [TTAttributs("FundTransfer", FieldName = "fromoldbal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "fromoldbal")]
        public decimal fromoldbal { get; set; }

        [TTAttributs("FundTransfer", FieldName = "fromclosebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "fromclosebal")]
        public decimal fromclosebal { get; set; }


        [TTAttributs("FundTransfer", FieldName = "reqvia", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "reqvia")]
        [MaxLength(50)]
        public string reqvia { get; set; }

        [TTAttributs("FundTransfer", FieldName = "UserLevel", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserLevel")]
        [MaxLength(100)]
        public string UserLevel { get; set; }

        [TTAttributs("FundTransfer", FieldName = "remarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "remarks")]
        [MaxLength(500)]
        public string remarks { get; set; }

        [TTAttributs("CommonFields", FieldName = "SystemDateTime", ParamaterDataType = SqlDbType.DateTime, isUpdateField = false)]
        public DateTime SystemDateTime { get; set; }

        [TTAttributs("", FieldName = "transferdatetext", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "transferdatetext")]
        [MaxLength(50)]
        public string transferdatetext {
            get
            {
                return this.SystemDateTime.GetFormatedDateTime();
            }
        }
    }
}
