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
    public class OperatorMapping : TTCommonEntity
    {
        public OperatorMapping()
        {
            this.TableName = "OperatorMapping";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("OperatorMapping", FieldName = "operatormapid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "operatormapid")]
        public System.Guid operatormapid { get; set; }

        [TTAttributs("OperatorMapping", FieldName = "apiid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "apiid")]
        public System.Guid apiid { get; set; }

        [TTAttributs("OperatorMapping", FieldName = "operatorid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "operatorid")]
        public System.Guid operatorid { get; set; }

        [TTAttributs("OperatorMapping", FieldName = "operatornomal", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "operatornomal")]
        [MaxLength(100)]
        public string operatornomal { get; set; }

        [TTAttributs("OperatorMapping", FieldName = "operatorspecial", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "operatorspecial")]
        [MaxLength(100)]
        public string operatorspecial { get; set; }

        [TTAttributs("OperatorMapping", FieldName = "apititle", ParamaterDataType = SqlDbType.VarChar,isInsertField =false,isSelectField =true,isTableField =false,isUpdateField =false)]
        [Display(Name = "apititle")]
        [MaxLength(100)]
        public string apititle { get; set; }

        [TTAttributs("OperatorMapping", FieldName = "operatorname", ParamaterDataType = SqlDbType.VarChar, isInsertField = false, isSelectField = true, isTableField = false, isUpdateField = false)]
        [Display(Name = "operatorname")]
        [MaxLength(100)]
        public string operatorname { get; set; }
        
    }
}
