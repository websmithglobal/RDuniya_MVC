using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using System.Data;

namespace Web.Framework.DataLayer
{
    public class Dashboard
    {
        public ENT.Dashboard GetDashboardData(Guid userid, COM.MyEnumration.Userlevel userlevel)
        {
            ENT.Dashboard objDashBoardModels = new ENT.Dashboard();
            SqlConnection sqlCon = new SqlConnection();
            sqlCon = COM.MySqlConnection.GetConnection.GetDBConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("GET_DASHBOARD_COUNT", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userlevel", SqlDbType.Int).Value = userlevel;
                cmd.Parameters.Add("@userid", SqlDbType.UniqueIdentifier).Value = userid;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objDashBoardModels.TotalCustomers = string.Format("{0:n0}", Convert.ToInt32(dr[0]));
                        objDashBoardModels.TotalBalance = string.Format("{0:n0}", Convert.ToInt32(dr[1]));
                        objDashBoardModels.TotalTodayRequests = string.Format("{0:n0}", Convert.ToInt32(dr[2]));
                        objDashBoardModels.TotalSuccess = string.Format("{0:n0}", Convert.ToInt32(dr[3]));
                        objDashBoardModels.TotalFailed = string.Format("{0:n0}", Convert.ToInt32(dr[4]));
                        objDashBoardModels.DMTtotalSuccess = string.Format("{0:n0}", Convert.ToInt32(dr[5]));

                        objDashBoardModels.DMTtotalFailed = string.Format("{0:n0}", Convert.ToInt32(dr[6]));
                        objDashBoardModels.DMTinproccess = string.Format("{0:n0}", Convert.ToInt32(dr[7]));
                        objDashBoardModels.WalletRequests = string.Format("{0:n0}", Convert.ToInt32(dr[8]));
                        objDashBoardModels.LoginActivity = string.Format("{0:n0}", Convert.ToInt32(dr[9]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sqlCon.Close();
            }
            return objDashBoardModels;
        }
    }
}
