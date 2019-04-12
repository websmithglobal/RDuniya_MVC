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
    public class APIDocument : TTCommonEntity
    {
        public APIDocument()
        {
            this.TableName = "IpAddress";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("IpAddress", FieldName = "ipid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "ipid")]
        public System.Guid ipid { get; set; }
        

        [TTAttributs("IpAddress", FieldName = "address", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "address")]
        [MaxLength(100)]
        public string address { get; set; }

        [TTAttributs("IpAddress", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("IpAddress", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }
    }
}
