﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WsRecharge.Models
{
    public class customerverificationmodel
    {
        public Guid dmt_customerid { get; set; }
        public string verificationotp { get; set; }
    }
}