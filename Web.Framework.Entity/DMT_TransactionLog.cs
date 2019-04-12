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
    public class DMT_TransactionLog : TTCommonEntity
    {
        public DMT_TransactionLog()
        {
            this.TableName = "DMT_Transaction_Log";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("DMT_Transaction_Log", FieldName = "dmt_transactionid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "dmt_transactionid")]
        public System.Guid dmt_transactionid { get; set; }

        [TTAttributs("DMT_Transaction_Log", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("DMT_Transaction_Log", FieldName = "dmt_title", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "dmt_title")]
        [MaxLength(100)]
        public string dmt_title { get; set; }

        [TTAttributs("DMT_Transaction_Log", FieldName = "dmt_mobile", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "dmt_mobile")]
        [MaxLength(50)]
        public string dmt_mobile { get; set; }

        [TTAttributs("DMT_Transaction_Log", FieldName = "dmt_requestno", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "dmt_requestno")]
        [MaxLength(200)]
        public string dmt_requestno { get; set; }

        [TTAttributs("DMT_Transaction_Log", FieldName = "dmt_response", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "dmt_response")]
        [MaxLength(-1)]
        public string dmt_response { get; set; }

        [TTAttributs("DMT_Transaction_Log", FieldName = "Status", ParamaterDataType = SqlDbType.TinyInt)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }


    }
}
