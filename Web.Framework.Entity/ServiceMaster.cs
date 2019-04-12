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
    public class ServiceMaster : TTCommonEntity
    {
        public ServiceMaster()
        {
            this.TableName = "ServiceMaster";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("ServiceMaster", FieldName = "serviceid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "serviceid")]
        public System.Guid serviceid { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "servicename", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "servicename")]
        [MaxLength(200)]
        public string servicename { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "servicecode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "servicecode")]
        [MaxLength(200)]
        public string servicecode { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "images", ParamaterDataType = SqlDbType.NVarChar, isMemoField = true)]
        [Display(Name = "images")]
        [MaxLength(-1)]
        public string images { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "icon", ParamaterDataType = SqlDbType.NVarChar, isMemoField = true)]
        [Display(Name = "icon")]
        [MaxLength(-1)]
        public string icon { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

    }

    public class Services
    {
        /// <summary>
        /// this is account ref
        /// </summary>
        [Required]
        public string country_code { get; set; }

    }

    public class ServiceMasterView
    {
        [Key]
        [TTAttributs("ServiceMaster", FieldName = "serviceid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "serviceid")]
        public System.Guid serviceid { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "servicename", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "servicename")]
        [MaxLength(200)]
        public string servicename { get; set; }

        [TTAttributs("ServiceMaster", FieldName = "servicecode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "servicecode")]
        [MaxLength(200)]
        public string servicecode { get; set; }

        [TTAttributs("", FieldName = "serviceimage", ParamaterDataType = SqlDbType.NVarChar , isSelectField = true)]
        [Display(Name = "serviceimage")]
        [MaxLength(500)]
        public string serviceimage { get; set; }
    }
}
