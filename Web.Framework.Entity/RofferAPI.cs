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
    public class RofferAPI : TTCommonEntity
    {
        public RofferAPI()
        {
            this.TableName = "RofferAPI";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("RofferAPI", FieldName = "rofferid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rofferid")]
        public System.Guid rofferid { get; set; }

        [TTAttributs("RofferAPI", FieldName = "title", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "title")]
        [MaxLength(50)]
        public string title { get; set; }

        [TTAttributs("RofferAPI", FieldName = "rofferurl", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "rofferurl")]
        [MaxLength(-1)]
        public string rofferurl { get; set; }

        [TTAttributs("RofferAPI", FieldName = "simpleurl", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "simpleurl")]
        [MaxLength(-1)]
        public string simpleurl { get; set; }

        [TTAttributs("RofferAPI", FieldName = "dthplanurl", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "dthplanurl")]
        [MaxLength(-1)]
        public string dthplanurl { get; set; }

        [TTAttributs("RofferAPI", FieldName = "dthinfourl", ParamaterDataType = SqlDbType.VarChar, isMemoField = true)]
        [Display(Name = "dthinfourl")]
        [MaxLength(-1)]
        public string dthinfourl { get; set; }

        [TTAttributs("RofferAPI", FieldName = "perdaylimit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "perdaylimit")]
        public decimal perdaylimit { get; set; }

        [TTAttributs("RofferAPI", FieldName = "Status", ParamaterDataType = SqlDbType.Int, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }
    }
}
