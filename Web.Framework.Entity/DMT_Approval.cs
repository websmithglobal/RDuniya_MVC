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
    public class DMT_Approval : TTCommonEntity
    {
        public DMT_Approval()
        {
            this.TableName = "DMT_Approval";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_Approval", FieldName = "approval_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "approval_id")]
        public System.Guid approval_id { get; set; }

        [TTAttributs("DMT_Approval", FieldName = "approval_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "approval_userid")]
        public System.Guid approval_userid { get; set; }

        [TTAttributs("DMT_Approval", FieldName = "approval_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "approval_status")]
        public byte approval_status { get; set; }
    }
}