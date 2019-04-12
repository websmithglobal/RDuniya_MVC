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
    public class UserDeviceToken : TTCommonEntity
    {
        public UserDeviceToken()
        {
            this.TableName = "UserDeviceToken";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("UserDeviceToken", FieldName = "udt_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "udt_id")]
        public System.Guid udt_id { get; set; }

        [TTAttributs("UserDeviceToken", FieldName = "udt_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "udt_userid")]
        public System.Guid udt_userid { get; set; }

        [TTAttributs("UserDeviceToken", FieldName = "udt_imei", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "udt_imei")]
        [MaxLength(50)]
        public string udt_imei { get; set; }

        [TTAttributs("UserDeviceToken", FieldName = "udt_devicetoken", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "udt_devicetoken")]
        [MaxLength(-1)]
        public string udt_devicetoken { get; set; }
    }
}
