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
    public class Routing
    {
        #region Declare Common Object
        List<ENT.Routing> lstEntity = new List<ENT.Routing>();
        ENT.Routing objEntity = new ENT.Routing();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion

        public Routing()
        {
            parFields.Clear();
        }
        public List<ENT.Routing> GetList(string search)
        {
            try
            {
                parFields.Clear();
                QueryDisctionery.SelectPart = "select r.routeid,r.routetitle,o.operatorname,r.amountmethod,r.amountSpecific,r.amountrangefrom,r.amountrangeto,r.rechargemethod,r.SystemDateTime,isnull(a.apititle,'N/A') as apititle,isnull(s.smartmobile,'N/A') as smartmobile ,isnull(m.name,'N/A') as machinename,r.status";
                QueryDisctionery.TablePart = @"from Routing as r left join Operators as o on(r.operatorid=o.operatorid) left join Apis as a on(r.apiid=a.apiid) left join SmartMobile as s on(r.smartid=s.mobileid) left join Machine as m on(r.machineid=m.machineid)";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    QueryDisctionery.ParameterPart = "Where  r.routetitle like '%" + search + "%'";
                }
                QueryDisctionery.OrderPart = " Order By r.routeid desc ";
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Routing>(dr);
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
        public List<ENT.Routing> CheckDuplicate(List<Guid> ParentID, string strname, string strName2)
        {
            try
            {


                QueryDisctionery.SelectPart = "SELECT TOP 1 routeid";
                QueryDisctionery.TablePart = @"FROM  Routing ";
                QueryDisctionery.ParameterPart += " WHERE routetitle ='" + strname + "' ";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.Routing>(dr);
                    objDBHelper.Disposed();
                }
            }
            catch
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
