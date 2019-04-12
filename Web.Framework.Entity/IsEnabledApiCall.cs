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
    public class IsEnabledApiCall : TTCommonEntity
    {
        public IsEnabledApiCall()
        {
            this.TableName = "IsEnabledApiCall";
            this.CreatedDateTime = DateTime.Now;
            this.UpdatedDateTime = DateTime.Now;
            this.SystemDateTime = DateTime.Now;
        }
        [Key]
        [TTAttributs("IsEnabledApiCall", FieldName = "isenableid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "isenableid")]
        public System.Guid isenableid { get; set; }

        [TTAttributs("IsEnabledApiCall", FieldName = "enabletext", ParamaterDataType = SqlDbType.Text)]
        [Display(Name = "enabletext")]
        public string enabletext { get; set; }

        [TTAttributs("IsEnabledApiCall", FieldName = "Isenabled", ParamaterDataType = SqlDbType.Bit)]
        [Display(Name = "Isenabled")]
        public bool Isenabled { get; set; }

        [TTAttributs("IsEnabledApiCall", FieldName = "userid", ParamaterDataType = SqlDbType.UniqueIdentifier)]
        [Display(Name = "userid")]
        public System.Guid userid { get; set; }


        [TTAttributs("IsEnabledApiCall", FieldName = "ipaddress", ParamaterDataType = SqlDbType.VarChar)]
        [Display(Name = "ipaddress")]
        public string ipaddress { get; set; }
    }
}
