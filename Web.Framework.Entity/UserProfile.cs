using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Framework.Common;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Web.Framework.Entity
{
    public class UserProfile : TTCommonEntity
    {
        public UserProfile()
        {
            this.TableName = "UserProfile";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("UserProfile", FieldName = "up_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "up_id")]
        public System.Guid up_id { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "up_userid")]
        public System.Guid up_userid { get; set; }

        [TTAttributs("UserProfile", FieldName = "slabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "slabid")]
        public System.Guid slabid { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_upperid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "up_upperid")]
        public System.Guid up_upperid { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_userlevel", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "up_userlevel")]
        public MyEnumration.Userlevel up_userlevel { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_username", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_username")]
        [MaxLength(100)]
        public string up_username { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_email", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_email")]
        [MaxLength(100)]
        public string up_email { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_mobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_mobile")]
        [MaxLength(12)]
        public string up_mobile { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_address", ParamaterDataType = SqlDbType.NVarChar, isMemoField = true)]
        [Display(Name = "up_address")]
        [MaxLength(500)]
        public string up_address { get; set; }

        [TTAttributs("UserProfile", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_balance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "up_balance")]
        public decimal up_balance { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_imei", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_imei")]
        [MaxLength(200)]
        public string up_imei { get; set; }

        [TTAttributs("UserProfile", FieldName = "UserLevelName", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "UserLevelName")]
        [MaxLength(200)]
        public string UserLevelName { get; set; }

        [TTAttributs("UserProfile", FieldName = "ResponseMessage", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "ResponseMessage")]
        [MaxLength(-1)]
        public string ResponseMessage { get; set; }

        [TTAttributs("UserProfile", FieldName = "slabname", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "slabname")]
        [MaxLength(200)]
        public string slabname { get; set; }

        [TTAttributs("UserProfile", FieldName = "TwoFactorEnabled", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "TwoFactorEnabled")]
        public Boolean TwoFactorEnabled { get; set; }

    }

    public class ApiUserProfile
    {
        [TTAttributs("UserProfile", FieldName = "up_id", ParamaterDataType = SqlDbType.UniqueIdentifier, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_id")]
        public System.Guid up_id { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_userid", ParamaterDataType = SqlDbType.UniqueIdentifier, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_userid")]
        public System.Guid up_userid { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_userlevel", ParamaterDataType = SqlDbType.Int, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_userlevel")]
        public MyEnumration.Userlevel up_userlevel { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_username", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_username")]
        [MaxLength(100)]
        public string up_username { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_email", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_email")]
        [MaxLength(100)]
        public string up_email { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_mobile", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_mobile")]
        [MaxLength(12)]
        public string up_mobile { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_address", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_address")]
        [MaxLength(500)]
        public string up_address { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_balance", ParamaterDataType = SqlDbType.Decimal, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_balance")]
        public decimal up_balance { get; set; }

        [TTAttributs("UserProfile", FieldName = "UserLevelName", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "UserLevelName")]
        [MaxLength(200)]
        public string UserLevelName { get; set; }

        [TTAttributs("UserProfile", FieldName = "ResponseMessage", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isUpdateField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "ResponseMessage")]
        [MaxLength(-1)]
        public string ResponseMessage { get; set; }
    }

    public class UserProfileApiView
    {
        [Key]
        [TTAttributs("UserProfile", FieldName = "up_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "up_id")]
        public System.Guid up_id { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "up_userid")]
        public System.Guid up_userid { get; set; }

        [TTAttributs("UserProfile", FieldName = "slabid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "slabid")]
        public System.Guid slabid { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_upperid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "up_upperid")]
        public System.Guid up_upperid { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_userlevel", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "up_userlevel")]
        public MyEnumration.Userlevel up_userlevel { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_username", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_username")]
        [MaxLength(100)]
        public string up_username { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_email", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_email")]
        [MaxLength(100)]
        public string up_email { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_mobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_mobile")]
        [MaxLength(12)]
        public string up_mobile { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_address", ParamaterDataType = SqlDbType.NVarChar, isMemoField = true)]
        [Display(Name = "up_address")]
        [MaxLength(500)]
        public string up_address { get; set; }

        [TTAttributs("UserProfile", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_balance", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "up_balance")]
        public decimal up_balance { get; set; }

        [TTAttributs("UserProfile", FieldName = "up_imei", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_imei")]
        [MaxLength(200)]
        public string up_imei { get; set; }
    }
}
