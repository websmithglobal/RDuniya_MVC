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
    public class DMT_Slab : TTCommonEntity
    {
        public DMT_Slab()
        {
            this.TableName = "DMT_Slab";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_Slab", FieldName = "dmtslabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmtslabid")]
        public System.Guid dmtslabid { get; set; }

        [TTAttributs("DMT_Slab", FieldName = "dmtslabname", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "dmtslabname")]
        public string dmtslabname { get; set; }

        [TTAttributs("DMT_Slab", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public byte Status { get; set; }
    }
}
