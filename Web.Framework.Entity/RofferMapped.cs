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
    public class RofferMapped : TTCommonEntity
    {
        public RofferMapped()
        {
            this.TableName = "Roffermapped";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("Roffermapped", FieldName = "roffermapid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "roffermapid")]
        public System.Guid roffermapid { get; set; }

        [Key]
        [TTAttributs("Roffermapped", FieldName = "rofferid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "rofferid")]
        public System.Guid rofferid { get; set; }

        [Key]
        [TTAttributs("Roffermapped", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }

        [TTAttributs("Roffermapped", FieldName = "creditlimit", ParamaterDataType = SqlDbType.Decimal)]
        [Display(Name = "creditlimit")]
        public decimal creditlimit { get; set; }

        [TTAttributs("Roffermapped", FieldName = "expirydate", ParamaterDataType = SqlDbType.Date)]
        [Display(Name = "expirydate")]
        public DateTime expirydate { get; set; }

        [TTAttributs("Roffermapped", FieldName = "Status", ParamaterDataType = SqlDbType.Int, isUpdateField = false)]
        [Display(Name = "Status")]
        public MyEnumration.MyStatus Status { get; set; }

        [TTAttributs("Roffermapped", FieldName = "rstatus", ParamaterDataType = SqlDbType.Int, isUpdateField = false)]
        [Display(Name = "rstatus")]
        public MyEnumration.ROfferMyStatus rstatus { get; set; }


        [TTAttributs("Roffermapped", FieldName = "title", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "title")]
        [MaxLength(100)]
        public string title { get; set; }


        [TTAttributs("Roffermapped", FieldName = "up_username", ParamaterDataType = SqlDbType.NVarChar, isInsertField = false, isSelectField = true, isTableField = false, isMemoField = false)]
        [Display(Name = "up_username")]
        [MaxLength(100)]
        public string up_username { get; set; }
    }
    public class EntityRoffers
    {
        public int apitype { get; set; }
        public string operators { get; set; }
        public string mobile { get; set; }
        public string offer { get; set; }

        public string Cricle { get; set; }
    }
}
