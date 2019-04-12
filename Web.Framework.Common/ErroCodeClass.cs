using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Common
{
    public class ErroCodeClass
    {
        public class ErrorMessage
        {
            public int Code { get; set; }
            public string Message { get; set; }

            public ErrorMessage() { }
            public ErrorMessage(int _Code, string _Message)
            {
                this.Code = _Code;
                this.Message = _Message;
            }
        }
    }
}
