using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;
using System.Data.SqlClient;

namespace Web.Framework.DataLayer
{
    public class DMT_MoneyRemittance
    {
        #region Declare Common Object
        List<ENT.DMT_MoneyRemittance> lstEntity = new List<ENT.DMT_MoneyRemittance>();
        ENT.DMT_MoneyRemittance objEntity = new ENT.DMT_MoneyRemittance();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public DMT_MoneyRemittance()
        {
            parFields.Clear();
        }

        public List<ENT.DMT_MoneyRemittance> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_MoneyRemittance ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_MoneyRemittance>(dr);
                    objDBHelper.Disposed();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parFields.Clear();
            }
            return lstEntity;
        }

        public List<ENT.DMT_MoneyRemittance> GetBySearch(int ddrange, DateTime fromdate, DateTime todate, int ddData, string search, COM.MyEnumration.Userlevel userlevel, Guid userid)
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_MoneyRemittance as MR";

                if (userlevel == 0)
                {
                    QueryDisctionery.ParameterPart = "Where 1 = 1";
                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND (MR.CreatedDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  MR.CreatedDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (MR.CreatedDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }
                    }
                }
                else
                {
                    QueryDisctionery.ParameterPart = "Where MR.userid='" + userid + "'";

                    if (ddrange == 0)
                    {
                        QueryDisctionery.ParameterPart += " AND (MR.CreatedDateTime >= dateadd(d,datediff(d, 0, getdate()), 0) and  MR.CreatedDateTime < dateadd(d, datediff(d, 0, getdate())+1, 0)) ";
                    }
                    else
                    {
                        if (!(String.IsNullOrEmpty(fromdate.ToString()) && String.IsNullOrEmpty(todate.ToString())))
                        {
                            QueryDisctionery.ParameterPart += " AND (MR.CreatedDateTime BETWEEN '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd") + " 00:00:01" + "' AND '" + Convert.ToDateTime(todate).ToString("yyyy-MM-dd") + " 23:59:59" + "')";
                        }
                    }
                }

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_MoneyRemittance>(dr);
                    objDBHelper.Disposed();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parFields.Clear();
            }
            return lstEntity;
        }

        public ENT.DMT_MoneyRemittance GetById(Guid guid)
        {
            ENT.DMT_MoneyRemittance Entity = new ENT.DMT_MoneyRemittance();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select DMT_MoneyRemittance.* ,(DMT_CustomerRegister.dmt_firstname + ' ' + DMT_CustomerRegister.dmt_lastname)  as customername";
                QueryDisctionery.TablePart = @"from DMT_MoneyRemittance 
                inner join DMT_CustomerRegister on DMT_MoneyRemittance.mt_RemitterId = DMT_CustomerRegister.dmt_requestno";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.DMT_MoneyRemittance>(dr);
                    objDBHelper.Disposed();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parFields.Clear();
            }
            return Entity;
        }
    }
}
