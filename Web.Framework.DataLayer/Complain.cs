using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class Complain
    {
        #region Declare Common Object
        List<ENT.Complain> lstEntity = new List<ENT.Complain>();
        ENT.Complain objEntity = new ENT.Complain();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public Complain()
        {
            parFields.Clear();
        }

        public List<ENT.Complain> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from Complain ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Complain>(dr);
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

        public List<ENT.ComplainView> GetAllSearch(int ddrange, DateTime fromdate, DateTime todate)
        {
            List<ENT.ComplainView> lstEntity = new List<ENT.ComplainView>();

            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select Complain.*,Recharge.numbertorecharge as rechargenumber , (UserProfile.up_username + ' - ' + UserProfile.up_mobile) as retailername";
                QueryDisctionery.TablePart = @"from Complain 
                inner join Recharge on Complain.rechargeid =  Recharge.rechargeid
                inner join UserProfile on Recharge.createdby =  UserProfile.up_userid";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.ComplainView>(dr);
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
    }
}