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
    public class LedgerReport : TTCommonEntity
    {
        public LedgerReport()
        {
            this.TableName = "LedgerDetail";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("LedgerDetail", FieldName = "ledgerid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "ledgerid")]
        public System.Guid ledgerid { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "beforebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "beforebal")]
        public decimal beforebal { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "credit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "credit")]
        public decimal credit { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "debit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "debit")]
        public decimal debit { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "afterbal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "afterbal")]
        public decimal afterbal { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "remakrs", ParamaterDataType = SqlDbType.NVarChar,isMemoField =true)]
        [Display(Name = "remakrs")]
        public string remakrs { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "username", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "username")]
        public string username { get; set; }

    }

    public class LedgerReportApiView
    {
        [Key]
        [TTAttributs("LedgerDetail", FieldName = "ledgerid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "ledgerid")]
        public System.Guid ledgerid { get; set; }


        [TTAttributs("LedgerDetail", FieldName = "beforebal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "beforebal")]
        public decimal beforebal { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "credit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "credit")]
        public decimal credit { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "debit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "debit")]
        public decimal debit { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "afterbal", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "afterbal")]
        public decimal afterbal { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "remakrs", ParamaterDataType = SqlDbType.NVarChar, isMemoField = true)]
        [Display(Name = "remakrs")]
        public string remakrs { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "username", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "username")]
        public string username { get; set; }

        [TTAttributs("LedgerDetail", FieldName = "entrydatetext", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "entrydatetext")]
        public string entrydatetext { get {
                return this.SystemDateTime.GetFormatedDateTime();
            }
        }

        [TTAttributs("CommonFields", FieldName = "SystemDateTime", ParamaterDataType = SqlDbType.DateTime, isUpdateField = false)]
        public DateTime SystemDateTime { get; set; }

    }
}
