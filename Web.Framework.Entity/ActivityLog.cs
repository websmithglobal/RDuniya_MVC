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
    public class ActivityLog : TTCommonEntity
    {
        public ActivityLog()
        {
            this.TableName = "ActivityLog";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("ActivityLog", FieldName = "activityid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "activityid")]
        public System.Guid activityid { get; set; }

        [TTAttributs("ActivityLog", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("ActivityLog", FieldName = "ipaddress", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "ipaddress")]
        public string ipaddress { get; set; }

        [TTAttributs("ActivityLog", FieldName = "browser", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "browser")]
        public string browser { get; set; }

        [TTAttributs("ActivityLog", FieldName = "useragent", ParamaterDataType = SqlDbType.Text)]
        [Display(Name = "useragent")]
        public string useragent { get; set; }

        [TTAttributs("ActivityLog", FieldName = "macaddress", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "macaddress")]
        public string macaddress { get; set; }

    }
}
