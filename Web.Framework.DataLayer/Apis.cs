﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class Apis
    {

        #region Declare Common Object
        List<ENT.Apis> lstEntity = new List<ENT.Apis>();
        ENT.Apis objEntity = new ENT.Apis();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public Apis()
        {
            parFields.Clear();
        }

        public List<ENT.Apis> GetList(string search)
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select apiid,apititle,api,CreatedDateTime,Status ";
                QueryDisctionery.TablePart = @"from Apis ";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "where  apititle like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = "order by CreatedDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Apis>(dr);
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

        public List<ENT.Apis> GetActiveApi()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select apiid,apititle,api";
                QueryDisctionery.TablePart = @"from Apis ";

                QueryDisctionery.ParameterPart = "Where Status = 1";

                QueryDisctionery.OrderPart = "order by CreatedDateTime desc";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Apis>(dr);
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
