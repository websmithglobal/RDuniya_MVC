using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{
    public class SlabCommission
    {
        #region Declare Common Object
        List<ENT.SlabCommission> lstEntity = new List<ENT.SlabCommission>();
        ENT.SlabCommission objEntity = new ENT.SlabCommission();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public SlabCommission()
        {
            parFields.Clear();
        }

        public List<ENT.SlabCommission> GetList(Guid slabid)
        {
            try
            {
                parFields.Clear();


                //Add Condition Parameters to dictionery
                //parFields.Add(COM.HelperMethod.PropertyName<ENT.UserProfile>(x => x.usr_id), userid, COM.Enumration.Operators.WHERE);

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select null as rechargeslabid,o.operatorname,o.operatorcode,0 as md,0 as sd,0 as r,0 as charge from Operators as o where o.operatorcode not in (select s.operatorcode from SlabCommision as s where s.slabid = '" + slabid + "')";
                QueryDisctionery.SelectPart += " UNION";
                QueryDisctionery.SelectPart += " select s.rechargeslabid,o.operatorname,o.operatorcode,isnull(s.md,0) as md,isnull(s.sd,0) as sd,isnull(s.r,0) as r,isnull(s.charge,0) as charge from SlabCommision  as s inner join Operators as o on o.operatorcode = s.operatorcode where s.slabid = '" + slabid + "'";
                QueryDisctionery.OrderPart = "order by o.operatorcode desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.SlabCommission>(dr);
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
