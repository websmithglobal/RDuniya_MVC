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
    public class DMT_Documents : TTCommonEntity
    {
        public DMT_Documents()
        {
            this.TableName = "DMT_Documents";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_Documents", FieldName = "dd_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dd_id")]
        public System.Guid dd_id { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dd_userid")]
        public System.Guid dd_userid { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_page1", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dd_page1")]
        [MaxLength(150)]
        public string dd_page1 { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_page2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dd_page2")]
        [MaxLength(150)]
        public string dd_page2 { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_type", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dd_type")]
        public int dd_type { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dd_status")]
        public int dd_status { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_remarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dd_remarks")]
        [MaxLength(500)]
        public string dd_remarks { get; set; }
    }

    public class DMT_DocumentsView
    {
        [Key]
        [TTAttributs("DMT_Documents", FieldName = "dd_id", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dd_id")]
        public System.Guid dd_id { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dd_userid")]
        public System.Guid dd_userid { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_page1", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dd_page1")]
        [MaxLength(150)]
        public string dd_page1 { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_page2", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dd_page2")]
        [MaxLength(150)]
        public string dd_page2 { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_type", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dd_type")]
        public int dd_type { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "dd_status")]
        public byte dd_status { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "dd_remarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "dd_remarks")]
        [MaxLength(500)]
        public string dd_remarks { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "up_username", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_username")]
        [MaxLength(150)]
        public string up_username { get; set; }

        [TTAttributs("DMT_Documents", FieldName = "up_mobile", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_mobile")]
        [MaxLength(150)]
        public string up_mobile { get; set; }

        [TTAttributs("", FieldName = "up_userlevel", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "up_userlevel")]
        public int up_userlevel { get; set; }
    }
}