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
    public class DMT_SlabCommission : TTCommonEntity
    {
        public DMT_SlabCommission()
        {
            this.TableName = "DMT_SlabCommision";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_SlabCommision", FieldName = "dmtslabcommid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmtslabcommid")]
        public System.Guid dmtslabcommid { get; set; }


        [TTAttributs("DMT_SlabCommision", FieldName = "dmtslabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmtslabid")]
        public System.Guid dmtslabid { get; set; }

        [TTAttributs("DMT_SlabCommision", FieldName = "dmtfromval", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "dmtfromval")]
        public decimal dmtfromval { get; set; }

        [TTAttributs("DMT_SlabCommision", FieldName = "dmttoval", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "dmttoval")]
        public decimal dmttoval { get; set; }

        [TTAttributs("DMT_SlabCommision", FieldName = "dmtmd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "dmtmd")]
        public decimal dmtmd { get; set; }

        [TTAttributs("DMT_SlabCommision", FieldName = "dmtsd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "dmtsd")]
        public decimal dmtsd { get; set; }


        [TTAttributs("DMT_SlabCommision", FieldName = "dmtrd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "dmtrd")]
        public decimal dmtrd { get; set; }

        [TTAttributs("DMT_SlabCommision", FieldName = "dmttype", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dmttype")]
        public int dmttype { get; set; }
    }
}
