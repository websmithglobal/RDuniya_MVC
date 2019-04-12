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
    public class Apis : TTCommonEntity
    {
        public Apis()
        {
            this.TableName = "Apis";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Apis", FieldName = "apiid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "apiid")]
        public System.Guid apiid { get; set; }

        [TTAttributs("Apis", FieldName = "apititle", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "apititle")]
        [MaxLength(100)]
        public string apititle { get; set; }

        [TTAttributs("Apis", FieldName = "api", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "api")]
        [MaxLength(-1)]
        public string api { get; set; }

        [TTAttributs("Apis", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }
        
    }
}
