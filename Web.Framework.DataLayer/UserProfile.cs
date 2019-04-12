using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ENT = Web.Framework.Entity;
using COM = Web.Framework.Common;

namespace Web.Framework.DataLayer
{

    public class UserProfile
    {

        #region Declare Common Object
        List<ENT.UserProfile> lstEntity = new List<ENT.UserProfile>();
        ENT.UserProfile objEntity = new ENT.UserProfile();
        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        #endregion


        public UserProfile()
        {
            parFields.Clear();
        }

        /// <summary>
        /// this method is used to get all MD and API users
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetApiUsers()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select up_userid,up_username,up_mobile";
                QueryDisctionery.TablePart = @"from UserProfile";
                QueryDisctionery.ParameterPart = "where up_userlevel=4 and status=1";
                QueryDisctionery.OrderPart = "order by up_username desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfile>(dr);
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

        /// <summary>
        /// this method is used to get all MD and API users
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetMdApiUsers()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select up_userid,up_username,up_mobile";
                QueryDisctionery.TablePart = @"from UserProfile";
                QueryDisctionery.ParameterPart = "where up_userlevel=4 OR up_userlevel=1 and status=1";
                QueryDisctionery.OrderPart = "order by up_username desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfile>(dr);
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


        /// <summary>
        /// this method is used to get all Distributors
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetDistributors()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select up_userid,up_username,up_mobile";
                QueryDisctionery.TablePart = @"from UserProfile";
                QueryDisctionery.ParameterPart = "where up_userlevel=2 and status=1";
                QueryDisctionery.OrderPart = "order by up_username desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfile>(dr);
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

        /// <summary>
        /// this method is used to get all Distributors
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfileApiView> GetMasterDownline(Guid userid)
        {
            List<ENT.UserProfileApiView> lstEntity = new List<ENT.UserProfileApiView>();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from UserProfile";
                QueryDisctionery.ParameterPart = "where up_userlevel=2 and status=1 and UserProfile.up_upperid='" + userid + "'";
                QueryDisctionery.OrderPart = "order by up_username desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfileApiView>(dr);
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


        /// <summary>
        /// this method is used to get all Retailers
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfile> GetRetailers()
        {
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select up_userid,up_username,up_mobile";
                QueryDisctionery.TablePart = @"from UserProfile";
                QueryDisctionery.ParameterPart = "where up_userlevel=3 and status=1";
                QueryDisctionery.OrderPart = "order by up_username desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfile>(dr);
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

        /// <summary>
        /// this method is used to get all Retailers
        /// </summary>
        /// <returns></returns>
        public List<ENT.UserProfileApiView> GetDistributotDownline(Guid userid)
        {
            List<ENT.UserProfileApiView> lstEntity = new List<ENT.UserProfileApiView>();
            try
            {
                parFields.Clear();

                //Add Query in to string builder object
                QueryDisctionery.SelectPart = "select * ";
                QueryDisctionery.TablePart = @"from UserProfile";
                QueryDisctionery.ParameterPart = "where up_userlevel=3 and status=1 and UserProfile.up_upperid='" + userid + "'";
                QueryDisctionery.OrderPart = "order by up_username desc";
                //Execute Query and get SQLDataReader
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfileApiView>(dr);
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

        public ENT.UserProfile GetUserBalance(Guid id)
        {
            ENT.UserProfile lstEntity = new Entity.UserProfile();
            try
            {

                parFields.Clear();

                QueryDisctionery.SelectPart = "SELECT top 1 up_balance,up_userid,up_username";
                QueryDisctionery.TablePart = @"from UserProfile where up_userid='" + id + "'";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.UserProfile>(dr);
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

        public ENT.UserProfile GetUserSlabName(Guid upperid)
        {
            ENT.UserProfile lstEntity = new Entity.UserProfile();
            try
            {
                parFields.Clear();

                QueryDisctionery.SelectPart = "SELECT top 1 slabid";
                QueryDisctionery.TablePart = @"from UserProfile where up_userid='" + upperid + "'";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.UserProfile>(dr);
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

        public List<ENT.UserProfile> GetList(string search, COM.MyEnumration.Userlevel userlevel)
        {
            try
            {
                parFields.Clear();
                if (userlevel == COM.MyEnumration.Userlevel.APIUSER)
                {
                    QueryDisctionery.SelectPart = "select U.up_id,U.CreatedDateTime,U.Status,u.up_address,u.up_email,u.up_mobile,u.up_username,u.up_userid,RO.Name as UserLevelName,ISNULL(S.slabname,'NA') AS slabname,A.TwoFactorEnabled";
                    QueryDisctionery.TablePart = @"from AspNetUsers as A inner join UserProfile as U on (A.id=U.up_userid) inner join AspNetUserRoles as R on(A.id=R.userid) inner join AspNetRoles as RO on(RO.Id=R.roleid)  left join Slab as S on(U.slabid=S.slabid)";
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        QueryDisctionery.ParameterPart = "where  RO.Name = 'apiuser' AND U.up_username like '%" + search + "%' OR U.up_mobile like '%" + search + "%'";
                    }
                    else
                    {
                        QueryDisctionery.ParameterPart = " where  RO.Name = 'apiuser'";
                    }
                    QueryDisctionery.OrderPart = "order by U.CreatedDateTime desc";
                    //Execute Query and get SQLDataReader

                }
                else if (userlevel == COM.MyEnumration.Userlevel.MD)
                {
                    QueryDisctionery.SelectPart = "select U.up_id,U.CreatedDateTime,U.Status,u.up_address,u.up_email,u.up_mobile,u.up_username,u.up_userid,RO.Name as UserLevelName,ISNULL(S.slabname,'NA') AS slabname ";
                    QueryDisctionery.TablePart = @"from AspNetUsers as A inner join UserProfile as U on (A.id=U.up_userid) inner join AspNetUserRoles as R on(A.id=R.userid) inner join AspNetRoles as RO on(RO.Id=R.roleid)  left join Slab as S on(U.slabid=S.slabid)";
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        QueryDisctionery.ParameterPart = "where  RO.Name = 'masterdistributor' AND U.up_username like '%" + search + "%' OR U.up_mobile like '%" + search + "%'";
                    }
                    else
                    {
                        QueryDisctionery.ParameterPart = " where  RO.Name = 'masterdistributor'";
                    }
                    QueryDisctionery.OrderPart = "order by U.CreatedDateTime desc";
                }
                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfile>(dr);
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

        public List<ENT.UserProfile> GetUnderList(string search, Guid userid, COM.MyEnumration.Userlevel userlevel)
        {
            try
            {
                parFields.Clear();
                if (userlevel == COM.MyEnumration.Userlevel.MD)
                {
                    QueryDisctionery.SelectPart = "select U.up_id,U.CreatedDateTime,U.Status,u.up_address,u.up_email,u.up_mobile,u.up_username,u.up_userid,RO.Name as UserLevelName,ISNULL(S.slabname,'NA') AS slabname ";
                    QueryDisctionery.TablePart = @"from AspNetUsers as A inner join UserProfile as U on (A.id=U.up_userid) inner join AspNetUserRoles as R on(A.id=R.userid) inner join AspNetRoles as RO on(RO.Id=R.roleid)  left join Slab as S on(U.slabid=S.slabid)";

                    QueryDisctionery.ParameterPart = "where U.up_upperid='" + userid + "'";

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        QueryDisctionery.ParameterPart = QueryDisctionery.ParameterPart + "and RO.Name = 'distributor' AND U.up_username like '%" + search + "%' OR U.up_mobile like '%" + search + "%'";
                    }
                    else
                    {
                        QueryDisctionery.ParameterPart = QueryDisctionery.ParameterPart + " and RO.Name = 'distributor'";
                    }
                }
                else if (userlevel == COM.MyEnumration.Userlevel.SD)
                {
                    QueryDisctionery.SelectPart = "select U.up_id,U.CreatedDateTime,U.Status,u.up_address,u.up_email,u.up_mobile,u.up_username,u.up_userid,RO.Name as UserLevelName,ISNULL(S.slabname,'NA') AS slabname ";
                    QueryDisctionery.TablePart = @"from AspNetUsers as A inner join UserProfile as U on (A.id=U.up_userid) inner join AspNetUserRoles as R on(A.id=R.userid) inner join AspNetRoles as RO on(RO.Id=R.roleid)  left join Slab as S on(U.slabid=S.slabid)";

                    QueryDisctionery.ParameterPart = "where U.up_upperid='" + userid + "'";

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        QueryDisctionery.ParameterPart = QueryDisctionery.ParameterPart +  "and  RO.Name = 'masterdistributor' AND U.up_username like '%" + search + "%' OR U.up_mobile like '%" + search + "%'";
                    }
                    else
                    {
                        QueryDisctionery.ParameterPart = QueryDisctionery.ParameterPart + " and  RO.Name = 'masterdistributor'";
                    }
                } else if (userlevel == COM.MyEnumration.Userlevel.R)
                {
                    QueryDisctionery.SelectPart = "select U.up_id,U.CreatedDateTime,U.Status,u.up_address,u.up_email,u.up_mobile,u.up_username,u.up_userid,RO.Name as UserLevelName,ISNULL(S.slabname,'NA') AS slabname ";
                    QueryDisctionery.TablePart = @"from AspNetUsers as A inner join UserProfile as U on (A.id=U.up_userid) inner join AspNetUserRoles as R on(A.id=R.userid) inner join AspNetRoles as RO on(RO.Id=R.roleid)  left join Slab as S on(U.slabid=S.slabid)";

                    QueryDisctionery.ParameterPart = "where U.up_upperid='" + userid + "'";

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        QueryDisctionery.ParameterPart = QueryDisctionery.ParameterPart + "and  RO.Name = 'retailer' AND U.up_username like '%" + search + "%' OR U.up_mobile like '%" + search + "%'";
                    }
                    else
                    {
                        QueryDisctionery.ParameterPart = QueryDisctionery.ParameterPart + " and  RO.Name = 'retailer'";
                    }
                }

                QueryDisctionery.OrderPart = "order by U.CreatedDateTime desc";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToEntity<ENT.UserProfile>(dr);
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

        public ENT.UserProfile SearchCustomerDetails(string id)
        {
            ENT.UserProfile lstEntity = new Entity.UserProfile();
            try
            {
                parFields.Clear();

                QueryDisctionery.SelectPart = "SELECT top 1 up_id,up_mobile,up_userid,up_userlevel,(case when up_userlevel=1 then 'Master Distributor' when up_userlevel=2 then 'Distributor' when up_userlevel=3 then 'Retailer' when up_userlevel=4 then 'ApiUser' else 'None' end) as UserLevelName,up_username,up_balance";
                QueryDisctionery.TablePart = @"from UserProfile where up_userid='" + id + "'";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.UserProfile>(dr);
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

        public ENT.UserProfile GetUserProfile(string id)
        {
            ENT.UserProfile lstEntity = new Entity.UserProfile();
            try
            {

                parFields.Clear();

                QueryDisctionery.SelectPart = "SELECT top 1 up_id,up_mobile,up_email,up_userid,up_address,up_userlevel,(case when up_userlevel=1 then 'Master Distributor' when up_userlevel=2 then 'Distributor' when up_userlevel=3 then 'Retailer' when up_userlevel=4 then 'ApiUser' else 'None' end) as UserLevelName,up_username,up_balance";
                QueryDisctionery.TablePart = @"from UserProfile where up_userid='" + id + "'";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.UserProfile>(dr);
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

        public ENT.ApiUserProfile GetApiUserProfile(string id)
        {
            ENT.ApiUserProfile lstEntity = new Entity.ApiUserProfile();
            try
            {

                parFields.Clear();

                QueryDisctionery.SelectPart = "SELECT top 1 up_id,up_mobile,up_email,up_userid,up_address,up_userlevel,(case when up_userlevel=1 then 'Master Distributor' when up_userlevel=2 then 'Distributor' when up_userlevel=3 then 'Retailer' when up_userlevel=4 then 'ApiUser' else 'None' end) as UserLevelName,up_username,up_balance";
                QueryDisctionery.TablePart = @"from UserProfile where up_userid='" + id + "'";

                using (SqlDataReader dr = objDBHelper.ExecuteReaderQuery(QueryDisctionery, parFields, objEntity))
                {
                    lstEntity = COM.DBHelper.CopyDataReaderToSingleEntity<ENT.ApiUserProfile>(dr);
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
