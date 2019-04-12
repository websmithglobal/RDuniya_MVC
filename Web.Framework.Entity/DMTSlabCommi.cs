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
    public class DMTSlabCommi : TTCommonEntity
    {
        public DMTSlabCommi()
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

        [TTAttributs("DMT_Slab", FieldName = "dmtslabname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dmtslabname")]
        [MaxLength(100)]
        public string dmtslabname { get; set; }

        
        [TTAttributs("DMT_Slab", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }
    }
}
