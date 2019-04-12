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
    public class CountryMaster : TTCommonEntity
    {
        public CountryMaster()
        {
            this.TableName = "CountryMaster";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("CountryMaster", FieldName = "countryid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "countryid")]
        public System.Guid countryid { get; set; }

        [TTAttributs("CountryMaster", FieldName = "isocode", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "isocode")]
        [MaxLength(100)]
        public string isocode { get; set; }

        [TTAttributs("CountryMaster", FieldName = "name", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "name")]
        [MaxLength(100)]
        public string name { get; set; }

        [TTAttributs("CountryMaster", FieldName = "nicename", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "nicename")]
        [MaxLength(100)]
        public string nicename { get; set; }

        [TTAttributs("CountryMaster", FieldName = "iso3", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "iso3")]
        [MaxLength(100)]
        public string iso3 { get; set; }

        [TTAttributs("CountryMaster", FieldName = "numcode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "numcode")]
        public int numcode { get; set; }

        [TTAttributs("CountryMaster", FieldName = "phonecode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "phonecode")]
        public int phonecode { get; set; }

        [TTAttributs("CountryMaster", FieldName = "Status", ParamaterDataType = SqlDbType.Int, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }
        
    }
}
