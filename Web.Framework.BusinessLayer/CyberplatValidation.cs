using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using System.Data;
using System.Text.RegularExpressions;

namespace Web.Framework.BusinessLayer
{
    public class CyberplatValidation
    {
        COM.DBHelper sqlhelper = new COM.DBHelper();
        COM.MEMBERS.SQLReturnMessageNValue M;
        public COM.MEMBERS.SQLReturnMessageNValue CheckValidation(int operator_code, string NumToRch, string Amount, string AccountNo)
        {
            string[,] param = { { "operator_code", operator_code.ToString() } };
            DataTable dtTemp = sqlhelper.ExecuteProcForDataTable("CYBERPLAT_VALIDATION", param);
            if (dtTemp.Rows.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(NumToRch))
                {
                    Regex regex = new Regex(dtTemp.Rows[0]["auth_regex"].ToString());
                    Match match = regex.Match(NumToRch);
                    if (!match.Success)
                    {
                        M.Outval = 0; M.Outmsg = dtTemp.Rows[0]["auth_msg"].ToString(); return M;
                    }
                }
                else
                {
                    M.Outval = 0; M.Outmsg = "number is required!"; return M;
                }
                if (dtTemp.Rows[0]["auth_name2"].ToString() != "N")
                {
                    if (!string.IsNullOrWhiteSpace(AccountNo))
                    {
                        Regex regex = new Regex(dtTemp.Rows[0]["auth_regex2"].ToString());
                        Match match = regex.Match(AccountNo);
                        if (!match.Success)
                        {
                            M.Outval = 0; M.Outmsg = dtTemp.Rows[0]["auth_msg2"].ToString(); return M;
                        }
                    }
                    else
                    {
                        M.Outval = 0; M.Outmsg = "parameters 2 Required!"; return M;
                    }

                }
                M.Outval = 1; M.Outmsg = "Validated"; return M;
            }
            return M;
        }
    }
}
