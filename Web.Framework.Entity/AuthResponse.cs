using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Entity
{
    public class AuthResponse
    {
        public int Code { get; set; } = 1;
        public object Message { get; set; } = new object();

        public AuthResponse() { }

        public AuthResponse(int _code, object _message)
        {
            this.Code = _code;
            this.Message = _message;
        }
    }
}
