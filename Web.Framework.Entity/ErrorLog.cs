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
    public class ErrorLog : TTCommonEntity
    {
        public ErrorLog()
        {
            this.TableName = "ErrorLog";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }

        [Key]
        [TTAttributs("ErrorLog", FieldName = "errorid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "errorid")]
        public System.Guid errorid { get; set; }

        [TTAttributs("ErrorLog", FieldName = "errocode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "errocode")]
        [MaxLength(50)]
        public string errocode { get; set; }

        [TTAttributs("ErrorLog", FieldName = "errortext", ParamaterDataType = SqlDbType.Text,isMemoField = true)]
        [Display(Name = "errortext")]
        public string errortext { get; set; }

        [TTAttributs("ErrorLog", FieldName = "errorsource", ParamaterDataType = SqlDbType.Text, isMemoField = true)]
        [Display(Name = "errorsource")]
        public string errorsource { get; set; }

        [TTAttributs("ErrorLog", FieldName = "errorstacktrace", ParamaterDataType = SqlDbType.Text, isMemoField = true)]
        [Display(Name = "errorstacktrace")]
        public string errorstacktrace { get; set; }

        [TTAttributs("ErrorLog", FieldName = "errorfileurl", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "errorfileurl")]
        [MaxLength(250)]
        public string errorfileurl { get; set; }
    }
}