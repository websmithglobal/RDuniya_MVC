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
    public class RechargeSlab : TTCommonEntity
    {
        public RechargeSlab()
        {
            this.TableName = "Slab";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Slab", FieldName = "slabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "slabid")]
        public System.Guid slabid { get; set; }

        [TTAttributs("Slab", FieldName = "rechargeslabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeslabid")]
        public System.Guid rechargeslabid { get; set; }
        
        [TTAttributs("Slab", FieldName = "slabname", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "slabname")]
        [MaxLength(100)]
        public string slabname { get; set; }
        
        [TTAttributs("Slab", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }
    }
}
