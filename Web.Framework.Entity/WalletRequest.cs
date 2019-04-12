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
    public class WalletRequest : TTCommonEntity
    {
        public WalletRequest()
        {
            this.TableName = "WalletRequest";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("WalletRequest", FieldName = "wr_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "wr_id")]
        public System.Guid wr_id { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "wr_userid")]
        public System.Guid wr_userid { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_bankname", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "wr_bankname")]
        [MaxLength(100)]
        public string wr_bankname { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_accountno", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "wr_accountno")]
        [MaxLength(100)]
        public string wr_accountno { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_amount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "wr_amount")]
        public decimal wr_amount { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_refrenceid", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "wr_refrenceid")]
        [MaxLength(100)]
        public string wr_refrenceid { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_remakrs", ParamaterDataType = SqlDbType.VarChar,isMemoField =true)]
        [Display(Name = "wr_remakrs")]
        [MaxLength(500)]
        public string wr_remakrs { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_mode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "wr_mode")]
        public int wr_mode { get; set; }

        [TTAttributs("WalletRequest", FieldName = "Mode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Mode")]
        public string Mode { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_status", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "wr_status")]
        public int wr_status { get; set; }

        [TTAttributs("WalletRequest", FieldName = "WalletStatus", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "WalletStatus")]
        public string WalletStatus { get; set; }

        [TTAttributs("WalletRequest", FieldName = "UserLevel", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserLevel")]
        public string UserLevel { get; set; }

        [TTAttributs("WalletRequest", FieldName = "UserName", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [TTAttributs("WalletRequest", FieldName = "udt_devicetoken", ParamaterDataType = SqlDbType.NVarChar,isInsertField =false)]
        [Display(Name = "udt_devicetoken")]
        public string udt_devicetoken { get; set; }
    }

    public class WalletRequestApiView
    {
        [Key]
        [TTAttributs("WalletRequest", FieldName = "wr_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "wr_id")]
        public System.Guid wr_id { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_bankname", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "wr_bankname")]
        [MaxLength(100)]
        public string wr_bankname { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_accountno", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "wr_accountno")]
        [MaxLength(100)]
        public string wr_accountno { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_amount", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "wr_amount")]
        public decimal wr_amount { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_refrenceid", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "wr_refrenceid")]
        [MaxLength(100)]
        public string wr_refrenceid { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_remakrs", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "wr_remakrs")]
        [MaxLength(500)]
        public string wr_remakrs { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_mode", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "wr_mode")]
        public int wr_mode { get; set; }

        [TTAttributs("WalletRequest", FieldName = "Mode", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "Mode")]
        public string Mode { get; set; }

        [TTAttributs("WalletRequest", FieldName = "wr_status", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "wr_status")]
        public int wr_status { get; set; }

        [TTAttributs("WalletRequest", FieldName = "WalletStatus", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "WalletStatus")]
        public string WalletStatus { get; set; }

        [TTAttributs("WalletRequest", FieldName = "UserLevel", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserLevel")]
        public string UserLevel { get; set; }

        [TTAttributs("WalletRequest", FieldName = "UserName", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [TTAttributs("WalletRequest", FieldName = "SystemDateTime", ParamaterDataType = SqlDbType.DateTime)]
        [Display(Name = "SystemDateTime")]
        public DateTime SystemDateTime { get; set; }

        [TTAttributs("WalletRequest", FieldName = "createddatetext", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "createddatetext")]
        public string createddatetext { get
            {
                return this.SystemDateTime.GetFormatedDateTime();
            }
        }

    }
}
