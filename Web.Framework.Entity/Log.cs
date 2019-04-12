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
    public class Log : TTCommonEntity
    {
        public Log()
        {
            this.TableName = "Log";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Log", FieldName = "logid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "logid")]
        public System.Guid logid { get; set; }

        [TTAttributs("Log", FieldName = "logdata", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "logdata")]
        [MaxLength(-1)]
        public string logdata { get; set; }

        [TTAttributs("Log", FieldName = "title", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "title")]
        [MaxLength(50)]
        public string title { get; set; }

        [TTAttributs("Log", FieldName = "type", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "type")]
        [MaxLength(50)]
        public Web.Framework.Common.MyEnumration.LOGTYPE type { get; set; }
    }
}