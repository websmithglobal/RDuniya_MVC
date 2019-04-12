using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COM = Web.Framework.Common;
using ENT = Web.Framework.Entity;
namespace Web.Framework.DataLayer
{
    public class DMT_SlabCommission
    {
        #region Declare Common Object
        List<ENT.DMT_SlabCommission> lstEntity = new List<ENT.DMT_SlabCommission>();
        ENT.DMT_SlabCommission objEntity = new ENT.DMT_SlabCommission();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public DMT_SlabCommission()
        {
            parFields.Clear();
        }

        public List<ENT.DMT_SlabCommission> GetAllBYID(Guid id)
        {
            try
            {
                parFields.Clear();

                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"FROM DMT_SlabCommision";
                QueryDisctionery.ParameterPart = "where dmtslabid='" + id + "'";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.DMT_SlabCommission>(dr);
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
