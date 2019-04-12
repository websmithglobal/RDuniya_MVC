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
    public class Routing : TTCommonEntity
    {
        public Routing()
        {
            this.TableName = "Routing";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Routing", FieldName = "routeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "routeid")]
        public System.Guid routeid { get; set; }


        [TTAttributs("Routing", FieldName = "routetitle", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "routetitle")]
        [MaxLength(100)]
        public string routetitle { get; set; }

        [TTAttributs("Routing", FieldName = "operatorid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "operatorid")]
        public System.Guid operatorid { get; set; }

        [TTAttributs("Routing", FieldName = "amountmethod", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "amountmethod")]
        public int amountmethod { get; set; }

        [TTAttributs("Routing", FieldName = "amountSpecific", ParamaterDataType = SqlDbType.NVarChar, isMemoField = true)]
        [Display(Name = "amountSpecific")]
        [MaxLength(500)]
        public string amountSpecific { get; set; }

        [TTAttributs("Routing", FieldName = "amountrangefrom", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "amountrangefrom")]
        public Int64 amountrangefrom { get; set; }

        [TTAttributs("Routing", FieldName = "amountrangeto", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "amountrangeto")]
        public Int64 amountrangeto { get; set; }

        [TTAttributs("Routing", FieldName = "rechargemethod", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "rechargemethod")]
        public int rechargemethod { get; set; }

        [TTAttributs("Routing", FieldName = "apiid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "apiid")]
        public System.Guid apiid { get; set; }

        [TTAttributs("Routing", FieldName = "machineid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "machineid")]
        public System.Guid machineid { get; set; }

        [TTAttributs("Routing", FieldName = "smartid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "smartid")]
        public System.Guid smartid { get; set; }

        [TTAttributs("Routing", FieldName = "Status", ParamaterDataType = SqlDbType.Int, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

        [TTAttributs("Routing", FieldName = "operatorname", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false)]
        [Display(Name = "operatorname")]
        public string operatorname { get; set; }

        [TTAttributs("Routing", FieldName = "apititle", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false)]
        [Display(Name = "apititle")]
        public string apititle { get; set; }

        [TTAttributs("Routing", FieldName = "smartmobile", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false)]
        [Display(Name = "smartmobile")]
        public string smartmobile { get; set; }

        [TTAttributs("Routing", FieldName = "machinename", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false)]
        [Display(Name = "machinename")]
        public string machinename { get; set; }
    }
}
