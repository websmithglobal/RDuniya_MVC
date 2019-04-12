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
    public class Complain : TTCommonEntity
    {
        public Complain()
        {
            this.TableName = "Complain";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Complain", FieldName = "complainid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "complainid")]
        public System.Guid complainid { get; set; }

        [TTAttributs("Complain", FieldName = "rechargeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeid")]
        public System.Guid rechargeid { get; set; }

        [TTAttributs("Complain", FieldName = "complaintype", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "complaintype")]
        public int complaintype { get; set; }

        [TTAttributs("Complain", FieldName = "detail", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "detail")]
        [MaxLength(-1)]
        public string detail { get; set; }

        [TTAttributs("Complain", FieldName = "complainstatus", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "complainstatus")]
        public int complainstatus { get; set; }

        [TTAttributs("Complain", FieldName = "adminremarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "adminremarks")]
        [MaxLength(-1)]
        public string adminremarks { get; set; }
    }




    public class ComplainView
    {
        [Key]
        [TTAttributs("Complain", FieldName = "complainid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "complainid")]
        public System.Guid complainid { get; set; }

        [TTAttributs("Complain", FieldName = "rechargeid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rechargeid")]
        public System.Guid rechargeid { get; set; }

        [TTAttributs("Complain", FieldName = "complaintype", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "complaintype")]
        public int complaintype { get; set; }

        [TTAttributs("Complain", FieldName = "detail", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "detail")]
        [MaxLength(-1)]
        public string detail { get; set; }

        [TTAttributs("Complain", FieldName = "complainstatus", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "complainstatus")]
        public int complainstatus { get; set; }

        [TTAttributs("Complain", FieldName = "adminremarks", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "adminremarks")]
        [MaxLength(-1)]
        public string adminremarks { get; set; }

        [TTAttributs("CommonFields", FieldName = "CreatedDateTime", ParamaterDataType = SqlDbType.DateTime, isUpdateField = false)]
        public DateTime CreatedDateTime { get; set; }

        [TTAttributs("Complain", FieldName = "complaintypetext", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "complaintypetext")]
        [MaxLength(50)]
        public string complaintypetext {
            get
            {
                if (this.complaintype == 1) { return ""; }
                else if (this.complaintype == 2) { return ""; }
                else if (this.complaintype == 3) { return ""; }
                else { return "NA"; }
            }
        }

        [TTAttributs("", FieldName = "retailername", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "retailername")]
        [MaxLength(50)]
        public string retailername { get; set; }

        [TTAttributs("", FieldName = "rechargenumber", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "rechargenumber")]
        [MaxLength(50)]
        public string rechargenumber { get; set; }
    }
}