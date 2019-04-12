using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Entity
{
    public class ROfferAPIResponse
    {
        public class Records
        {
            public string msg { get; set; }
        }

        public class RootObject
        {
            public object records { get; set; }
            public int status { get; set; }
        }
    }
}
