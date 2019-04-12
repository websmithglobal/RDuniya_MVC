using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsRecharge.Models
{
    public class ResponseModels
    {
    }

    #region INOUTAPI
    public class DoRechargeResponse
    {
        public string Code { get; set; }
        public string ReferenceID { get; set; }
        public string Message { get; set; }
    }

    public class INOUTRESPONSE
    {
        public DoRechargeResponse DoRechargeResponse { get; set; }
    }
    #endregion
}