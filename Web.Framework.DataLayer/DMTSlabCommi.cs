﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class DMTSlabCommi
    {
        #region Declare Common Object
        List<ENT.DMTSlabCommi> lstEntity = new List<ENT.DMTSlabCommi>();
        ENT.DMTSlabCommi objEntity = new ENT.DMTSlabCommi();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public DMTSlabCommi()
        {
            parFields.Clear();
        }

        public List<ENT.DMTSlabCommi> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select dmtslabid,dmtslabname";
                QueryDisctionery.TablePart = @"from DMT_Slab ";
                QueryDisctionery.OrderPart = "order by CreatedDateTime desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMTSlabCommi>(dr);
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
