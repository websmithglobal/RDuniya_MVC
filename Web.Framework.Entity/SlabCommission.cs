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
    public class SlabCommission : TTCommonEntity
    {
        public SlabCommission()
        {
            this.TableName = "SlabCommision";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("SlabCommision", FieldName = "rechargeslabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeslabid")]
        public System.Guid rechargeslabid { get; set; }

        [TTAttributs("SlabCommision", FieldName = "slabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "slabid")]
        public System.Guid slabid { get; set; }

        [TTAttributs("SlabCommision", FieldName = "operatorname", ParamaterDataType = SqlDbType.NVarChar, isSelectField = true, isInsertField = false, isUpdateField = false, isTableField = false)]
        [Display(Name = "operatorname")]
        [MaxLength(500)]
        public string operatorname { get; set; }

        [TTAttributs("SlabCommision", FieldName = "operatorcode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "operatorcode")]
        public int operatorcode { get; set; }

        [TTAttributs("SlabCommision", FieldName = "md", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "md")]
        public decimal md { get; set; }

        [TTAttributs("SlabCommision", FieldName = "sd", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "sd")]
        public decimal sd { get; set; }

        [TTAttributs("SlabCommision", FieldName = "r", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "r")]
        public decimal r { get; set; }

        [TTAttributs("SlabCommision", FieldName = "charge", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "charge")]
        public decimal charge { get; set; }

    }
}
