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
    public class RofferDetailReport : TTCommonEntity
    {
        public RofferDetailReport()
        {
            this.TableName = "RofferDetails";
            this.CreatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("RofferDetails", FieldName = "rofferdetailid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rofferdetailid")]
        public System.Guid rofferdetailid { get; set; }

        [TTAttributs("RofferDetails", FieldName = "title", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "title")]
        [MaxLength(100)]
        public string title { get; set; }

        [TTAttributs("RofferDetails", FieldName = "up_username", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "up_username")]
        [MaxLength(150)]
        public string up_username { get; set; }

        [TTAttributs("RofferDetails", FieldName = "callcount", ParamaterDataType = SqlDbType.Int)]
        [Display(Name = "callcount")]
        public int callcount { get; set; }

        [TTAttributs("RofferDetails", FieldName = "url", ParamaterDataType = SqlDbType.NVarChar,isMemoField =true)]
        [Display(Name = "url")]
        [MaxLength(-1)]
        public string url { get; set; }

        [TTAttributs("RofferDetails", FieldName = "apitypename", ParamaterDataType = SqlDbType.NVarChar)]
        [Display(Name = "apitypename")]
        [MaxLength(100)]
        public string apitypename { get; set; }

        [TTAttributs("RofferDetails", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }


        [TTAttributs("RofferDetails", FieldName = "up_userid", ParamaterDataType = SqlDbType.UniqueIdentifier,isTableField =false)]
        [Display(Name = "up_userid")]
        public System.Guid up_userid { get; set; }
        
    }
}
