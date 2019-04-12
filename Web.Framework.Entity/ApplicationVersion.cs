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
    public class ApplicationVersion : TTCommonEntity
    {
        public ApplicationVersion()
        {
            this.TableName = "ApplicationVersion";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("ApplicationVersion", FieldName = "appversionid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "appversionid")]
        public System.Guid appversionid { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "androidversion", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "androidversion")]
        public decimal androidversion { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "androidmessage", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "androidmessage")]
        public string androidmessage { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "androidupdateurl", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "androidupdateurl")]
        public string androidupdateurl { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "iosversion", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "iosversion")]
        public decimal iosversion { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "iosmessage", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "iosmessage")]
        public string iosmessage { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "iosupdateurl", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "iosupdateurl")]
        public string iosupdateurl { get; set; }
    }

    public class ApplicationVersionView
    {
        [Key]
        [TTAttributs("ApplicationVersion", FieldName = "appversionid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "appversionid")]
        public System.Guid appversionid { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "androidversion", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "androidversion")]
        public decimal androidversion { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "androidmessage", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "androidmessage")]
        public string androidmessage { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "androidupdateurl", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "androidupdateurl")]
        public string androidupdateurl { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "iosversion", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "iosversion")]
        public decimal iosversion { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "iosmessage", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "iosmessage")]
        public string iosmessage { get; set; }

        [TTAttributs("ApplicationVersion", FieldName = "iosupdateurl", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "iosupdateurl")]
        public string iosupdateurl { get; set; }
    }
}