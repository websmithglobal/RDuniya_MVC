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
    public class CircleMaster : TTCommonEntity
    {
        public CircleMaster()
        {
            this.TableName = "CircleMaster";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("CircleMaster", FieldName = "circleid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "circleid")]
        public System.Guid circleid { get; set; }

        [TTAttributs("CircleMaster", FieldName = "countryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "countryid")]
        public System.Guid countryid { get; set; }

        [TTAttributs("CircleMaster", FieldName = "circle_name", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "circle_name")]
        [MaxLength(200)]
        public string circle_name { get; set; }

        [TTAttributs("CircleMaster", FieldName = "circle_code", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "circle_code")]
        [MaxLength(200)]
        public string circle_code { get; set; }


        [TTAttributs("CircleMaster", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

        [TTAttributs("CircleMaster", FieldName = "countryname", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isTableField = false, isSelectField = true)]
        [Display(Name = "countryname")]
        [MaxLength(200)]
        public string countryname { get; set; }
    }
    public class Regions
    {
        /// <summary>
        /// this is country code
        /// </summary>
        [Required]
        public string country_code { get; set; }
    }
}
