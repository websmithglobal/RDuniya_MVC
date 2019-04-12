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
    public class SMSMaster : TTCommonEntity
    {
        public SMSMaster()
        {
            this.TableName = "SMSMaster";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("SMSMaster", FieldName = "smsid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "smsid")]
        public System.Guid smsid { get; set; }

        [TTAttributs("SMSMaster", FieldName = "smstitle", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "smstitle")]
        [MaxLength(100)]
        public string smstitle { get; set; }

        [TTAttributs("SMSMaster", FieldName = "smsapi", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "smsapi")]
        [MaxLength(-1)]
        public string smsapi { get; set; }

        [TTAttributs("SMSMaster", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

    }
}
