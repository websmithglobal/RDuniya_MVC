using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Common
{
    public class MEMBERS
    {
        public struct SQLReturnMessageNValue
        {
            public int Outval { get; set; }
            public string Outmsg { get; set; }
        }

        public struct SQLReturnMessageNValueNDataTable
        {
            public object Outval { get; set; }
            public string Outmsg { get; set; }
            public DataTable dt { get; set; }
        }

        public struct SQLReturnValue
        {
            public object Outval { get; set; }
        }

        public struct SQLReturnMessage
        {
            public int Outmsg { get; set; }
        }

        /// <summary>
        /// Holds the procedures integeral output value.
        /// </summary>
        public struct SQlReturnInteger
        {
            /// <summary>
            /// Integral value returned from SQL Procedure.
            /// </summary>
            public int ValueFromSQL;
        }
    }
}
