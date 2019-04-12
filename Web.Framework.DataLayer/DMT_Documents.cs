using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class DMT_Documents
    {

        #region Declare Common Object
        List<ENT.DMT_Documents> lstEntity = new List<ENT.DMT_Documents>();
        ENT.DMT_Documents objEntity = new ENT.DMT_Documents();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public DMT_Documents()
        {
            parFields.Clear();
        }

        public List<ENT.DMT_Documents> GetList()
        {
            try
            {
                parFields.Clear();

                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from DMT_Documents ";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_Documents>(dr);
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

        public List<ENT.DMT_DocumentsView> GetAllBySearch()
        {
            List<ENT.DMT_DocumentsView> lstEntity = new List<ENT.DMT_DocumentsView>();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select *,UserProfile.up_username,UserProfile.up_mobile";
                QueryDisctionery.TablePart = @"from DMT_Documents 
                inner join UserProfile on UserProfile.up_userid = DMT_Documents.dd_userid";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_DocumentsView>(dr);
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

        public ENT.DMT_Documents GetByUserId(Guid guid)
        {
            ENT.DMT_Documents Entity = new ENT.DMT_Documents();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select top 1 * ";
                QueryDisctionery.TablePart = @"from DMT_Documents ";

                QueryDisctionery.ParameterPart = "Where dd_userid='"+guid+"'";

                QueryDisctionery.OrderPart = "order by CreatedDateTime desc";

                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    Entity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.DMT_Documents>(dr);
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