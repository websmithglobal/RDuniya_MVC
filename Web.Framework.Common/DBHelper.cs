using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;

namespace Web.Framework.Common
{
    public class DBHelper
    {
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand sqlCMD = new SqlCommand();

        public DBHelper()
        {

        }

        private void GetConnection()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon = MySqlConnection.GetConnection.GetDBConnection();
                if (MySqlConnection.GetConnection.isConnectionOpen)
                {
                    sqlCMD.Connection = sqlCon;
                }
            }
        }

        ~DBHelper()
        {
            MySqlConnection.GetConnection.CloseConnection(sqlCon);
            sqlCMD.Dispose();
        }

        public void Disposed()
        {
            sqlCMD.Dispose();
            sqlCon.Close();
        }

        public object GetEnityByPrimaryKey(object objEntity)
        {
            object objResult = null;
            try
            {
                GetConnection();
                string strPrimaryKey = null;
                if (sqlCMD.Connection.State == ConnectionState.Open)
                {
                    if (sqlCMD.Connection.State == ConnectionState.Closed)
                        MySqlConnection.GetConnection.OpenConnection(sqlCon);
                    CommonMSSQL.ClearParameter(sqlCMD);

                    #region Get Primarykey Information from object
                    PropertyInfo[] properties = objEntity.GetType().GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                        if (attribute != null) // This property has a KeyAttribute
                        {
                            // Do something, to read from the property:
                            //object val = property.GetValue(objPrimaryKey);
                            strPrimaryKey = ((TTAttributs)objEntity.GetType().GetProperty(property.Name).GetCustomAttribute(typeof(TTAttributs), false)).FieldName;
                            break;
                        }
                    }

                    #endregion

                    CommonMSSQL.AddParameter(sqlCMD, objEntity, strPrimaryKey);
                    sqlCMD.CommandType = CommandType.Text;
                    sqlCMD.CommandText = CommonMSSQL.PrepairSelectPrimaryKey(HelperMethod.GetTableName(objEntity).ToString(), objEntity);
                    using (SqlDataReader dr = sqlCMD.ExecuteReader())
                    {
                        objResult = FillEntityFromReader(dr, objEntity);
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                objResult = null;
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
            return objResult;
        }

        public object GetEnityByPerameters(object objEntity, Dictionary<string, Enumration.Operators> parFields)
        {
            object objResult = null;
            try
            {
                GetConnection();
                if (sqlCMD.Connection.State == ConnectionState.Open)
                {
                    if (sqlCMD.Connection.State == ConnectionState.Closed)
                        MySqlConnection.GetConnection.OpenConnection(sqlCon);
                    CommonMSSQL.ClearParameter(sqlCMD);

                    for (int i = 0; i < parFields.Count; i++)
                    {
                        var el = parFields.ElementAt(i);
                        CommonMSSQL.AddParameter(sqlCMD, objEntity, el.Key);
                    }

                    sqlCMD.CommandType = CommandType.Text;
                    sqlCMD.CommandText = CommonMSSQL.PrepairSelectPerameters(parFields, HelperMethod.GetTableName(objEntity), objEntity);
                    using (SqlDataReader dr = sqlCMD.ExecuteReader())
                    {
                        objResult = FillEntityFromReader(dr, objEntity);
                        dr.Close();
                    }

                }
            }
            catch (Exception)
            {
                objResult = null;
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
            return objResult;
        }

        public SqlDataReader ExecuteReaderQuery(TTDictionaryQuery SQLDictionery, object objEntity)
        {
            SqlDataReader drResult = null;
            try
            {
                GetConnection();
                if (!MySqlConnection.GetConnection.isConnectionOpen)
                    sqlCon.Open();
                sqlCMD.Parameters.Clear();
                StringBuilder SQLQuery = new StringBuilder();

                SQLQuery.Append(SQLDictionery.SelectPart).AppendLine();
                SQLQuery.Append(SQLDictionery.TablePart).AppendLine();
                if (SQLDictionery.GroupPart.Trim() != "")
                    SQLQuery.Append(SQLDictionery.GroupPart).AppendLine();
                if (SQLDictionery.HavingPart.Trim() != "")
                    SQLQuery.Append(SQLDictionery.HavingPart).AppendLine();

                if (TTPagination.isPageing)
                {
                    SQLQuery.Append(string.Format("{0} {1}", SQLDictionery.OrderPart, HelperMethod.GetPageingString(SQLDictionery.OrderPart, objEntity))).AppendLine();
                }
                else
                {
                    SQLQuery.Append(SQLDictionery.OrderPart).AppendLine();
                }

                sqlCMD.CommandType = CommandType.Text;
                sqlCMD.CommandText = SQLQuery.ToString();
                SqlDataReader dr = sqlCMD.ExecuteReader();
                if (dr.HasRows)
                {
                    drResult = dr;
                    TTPagination.RecordCount = PagerRecordCount(SQLDictionery, null, objEntity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
            return drResult;
        }

        public SqlDataReader ExecuteReaderQuery(TTDictionaryQuery SQLDictionery, TTDictionary parDictionery, object objEntity)
        {
            SqlDataReader drResult = null;
            try
            {
                GetConnection();
                MySqlConnection.GetConnection.OpenConnection(sqlCon);
                sqlCMD.Parameters.Clear();
                if (objEntity != null)
                {
                    CommonMSSQL.AddParameter(sqlCMD, parDictionery, objEntity);
                }

                StringBuilder SQLQuery = new StringBuilder();
                SQLQuery.Append(SQLDictionery.SelectPart).AppendLine();
                SQLQuery.Append(SQLDictionery.TablePart).AppendLine();
                if (parDictionery.Count > 0)
                    SQLQuery.Append(HelperMethod.GetParameter(parDictionery, SQLQuery)).AppendLine();
                if (!string.IsNullOrEmpty(SQLDictionery.ParameterPart))
                {
                    if (SQLDictionery.ParameterPart.Trim() != "")
                    {
                        SQLQuery.Append(SQLDictionery.ParameterPart).AppendLine();
                    }
                }
                if (SQLDictionery.GroupPart != null)
                    SQLQuery.Append(SQLDictionery.GroupPart).AppendLine();
                if (SQLDictionery.HavingPart != null)
                    SQLQuery.Append(SQLDictionery.HavingPart).AppendLine();

                if (TTPagination.isPageing)
                {
                    if (TTPagination.PageSize != -1)
                    {
                        SQLQuery.Append(string.Format("{0} {1}", SQLDictionery.OrderPart, HelperMethod.GetPageingString(SQLDictionery.OrderPart, objEntity))).AppendLine();
                    }
                }
                else
                {
                    if (SQLDictionery.OrderPart != null)
                        SQLQuery.Append(SQLDictionery.OrderPart).AppendLine();
                }

                sqlCMD.CommandType = CommandType.Text;
                sqlCMD.CommandText = SQLQuery.ToString();
                SqlDataReader dr = sqlCMD.ExecuteReader();
                TTPagination.RecordCount = PagerRecordCount(SQLDictionery, parDictionery, objEntity);
                drResult = dr;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                TTPagination.isPageing = false;
            }
            return drResult;
        }

        private long PagerRecordCount(TTDictionaryQuery SQLDictionery, TTDictionary parDictionery, object objEntity)
        {
            long lngResult = 0;
            StringBuilder sbCount = new StringBuilder();
            SqlCommand sqlCMDCount = new SqlCommand();
            sqlCMDCount.Connection = MySqlConnection.GetConnection.GetDBConnection();
            sbCount.Append("select total_count = COUNT(*) ").AppendLine();
            sbCount.Append(SQLDictionery.TablePart).AppendLine();
            if (parDictionery != null)
            {
                if (parDictionery.Count > 0)
                {
                    sqlCMDCount.Parameters.Clear();
                    CommonMSSQL.AddParameter(sqlCMDCount, parDictionery, objEntity);
                    sbCount.Append(HelperMethod.GetParameter(parDictionery, sbCount)).AppendLine();
                    if (SQLDictionery.ParameterPart != null)
                    {
                        sbCount.Append(SQLDictionery.ParameterPart.Replace("WHERE", "AND"));
                    }
                }
                else { sbCount.Append(SQLDictionery.ParameterPart); }
            }
            else { sbCount.Append(SQLDictionery.ParameterPart); }

            if (SQLDictionery.GroupPart != null)
                sbCount.Append(SQLDictionery.GroupPart).AppendLine();
            if (SQLDictionery.HavingPart != null)
                sbCount.Append(SQLDictionery.HavingPart).AppendLine();
            sqlCMDCount.CommandText = sbCount.ToString();
            object objCount = sqlCMDCount.ExecuteScalar();
            if (objCount != null)
            {
                lngResult = objCount == DBNull.Value ? 0 : Convert.ToInt64(objCount);
                TTPagination.PageCount = (long)Math.Ceiling((double)lngResult / TTPagination.PageSize);
            }
            sqlCMDCount.Connection.Close();
            sqlCMDCount.Dispose();
            return lngResult;
        }
        
        #region create list from datareader
        private object FillEntityFromReader(SqlDataReader dr, object objEntity)
        {
            object objResult = null;
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        foreach (var prop in objEntity.GetType().GetProperties())
                        {
                            TTAttributs attr = HelperMethod.GetTTAttributes(objEntity, prop.Name);
                            if (attr.isTableField)
                            {
                                int ordinal = dr.GetOrdinal(attr.FieldName);
                                object objValue = dr.GetValue(ordinal);
                                if (objValue != System.DBNull.Value)
                                {
                                    prop.SetValue(objEntity, objValue, null);
                                }
                            }
                        }
                    }
                    objResult = objEntity;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objResult;
        }

        //usage below method
        //List<ItemDetails> itemDetails = SQLHelper.CopyDataReaderToEntity<ItemDetails>(reader);
        public static TEntity CopyDataReaderToSingleEntity<TEntity>(IDataReader dataReader) where TEntity : class
        {
            TEntity entities = null;
            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            while (dataReader.Read())
            {
                TEntity tempEntity = Activator.CreateInstance<TEntity>();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        TTAttributs MyAttributs = HelperMethod.GetTTAttributes(tempEntity, property.Name);
                        if (MyAttributs.isSelectField)
                            SetValue<TEntity>(property, tempEntity, dataReader[property.Name]);
                    }
                    catch (Exception ex) { }
                }
                entities = tempEntity;
            }
            return entities;
        }

        public static List<TEntity> CopyDataReaderToEntity<TEntity>(IDataReader dataReader) where TEntity : class
        {
            List<TEntity> entities = new List<TEntity>();
            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            while (dataReader.Read())
            {
                TEntity tempEntity = Activator.CreateInstance<TEntity>();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        TTAttributs MyAttributs = HelperMethod.GetTTAttributes(tempEntity, property.Name);
                        if (MyAttributs.isSelectField)
                            SetValue<TEntity>(property, tempEntity, dataReader[property.Name]);
                    }
                    catch (Exception ex) { }
                }
                entities.Add(tempEntity);
            }
            return entities;
        }

        private static TEntity SetValue<TEntity>(PropertyInfo property, TEntity entity, object propertyValue) where TEntity : class
        {
            if (property.CanRead)
            {
                //if (property.PropertyType.Name != "String" &&
                //    property.PropertyType.Name != "Single" &&
                //    property.PropertyType.Name != "Int32" &&
                //    property.PropertyType.Name != "Int64" &&
                //    property.PropertyType.Name != "Guid" &&
                //    property.PropertyType.Name != "DateTime" &&
                //    property.PropertyType.Name != "Decimal" &&
                //    property.PropertyType.Name != "Boolean" &&
                //    property.PropertyType.Name != "LocationType" &&
                //    property.PropertyType.Name != "AlertCategory" &&
                //    property.PropertyType.Name != "AlertType" &&
                //    property.PropertyType.Name != "AppLogType" &&
                //    property.PropertyType.Name != "RegisterVia" &&
                //    property.PropertyType.Name != "CoinContentType" &&
                //    property.PropertyType.Name != "CoinPatternType" &&
                //    property.PropertyType.Name != "MasterStatus" &&
                //    property.PropertyType.Name != "MockQuestionType")
                //    return entity;
                if (propertyValue == null)
                {
                    if (property.PropertyType.Name == "String")
                        propertyValue = "";
                    else
                        propertyValue = 0;
                }
                if (property.CanWrite)
                {
                    if (propertyValue != DBNull.Value)
                    {
                        if (property.PropertyType.Name == "Single")
                            property.SetValue(entity, Convert.ToSingle(propertyValue), null);
                        else if (property.PropertyType.Name == "Int32")
                            property.SetValue(entity, Convert.ToInt32(propertyValue), null);
                        else if (property.PropertyType.Name == "Int64")
                            property.SetValue(entity, Convert.ToInt64(propertyValue), null);
                        else { property.SetValue(entity, propertyValue, null); }
                        //else if (property.PropertyType.Name == "String")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "Boolean")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "DateTime")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "Guid")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "Decimal")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "LocationType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "AlertCategory")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "AlertType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "AppLogType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "RegisterVia")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "CoinContentType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "CoinPatternType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "MockQuestionType")
                        //    property.SetValue(entity, propertyValue, null);
                    }
                }
            }
            return entity;
        }

        #endregion

        public static List<T> Query<T>(string query) where T : new()
        {
            List<T> res = new List<T>();
            SqlCommand q = new SqlCommand();
            SqlDataReader r = q.ExecuteReader();
            while (r.Read())
            {
                T t = new T();

                for (int inc = 0; inc < r.FieldCount; inc++)
                {
                    Type type = t.GetType();
                    PropertyInfo prop = type.GetProperty(r.GetName(inc));
                    prop.SetValue(t, r.GetValue(inc), null);
                }

                res.Add(t);
            }
            r.Close();

            return res;

        }

        /// <summary>
        /// Josheph SP COde
        /// </summary>
        public struct SQLReturnValue
        {
            /// <summary>
            /// Integral value returned from SQL Procedure.
            /// </summary>
            public int ValueFromSQL;
            /// <summary>
            /// String message returned from SQL Procedure.
            /// </summary>
            public string MessageFromSQL;
            public string MessageFromSQL1;
        }

        /// <summary>
        /// This function is for only used in user portal
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="ParamValue"></param>
        /// <param name="AddOutputParameters"></param>
        /// <returns></returns>
        public SQLReturnValue ExecuteProcWithMessageKMT(string ProcedureName, string[,] ParamValue, bool AddOutputParameters)
        {
            GetConnection();
            if (!MySqlConnection.GetConnection.isConnectionOpen)
                sqlCon.Open();
            sqlCMD.Parameters.Clear();
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                COMMAND.Connection = sqlCon;
                COMMAND.CommandType = CommandType.StoredProcedure;

                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);

                if (AddOutputParameters == true)
                {
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE1", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                }

                if (sqlCon.State != ConnectionState.Open) { sqlCon.Open(); }
                COMMAND.ExecuteNonQuery();
                sqlCon.Close();

                SQLReturnValue M = new SQLReturnValue();
                M.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                M.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                M.MessageFromSQL1 = COMMAND.Parameters["@OUTMESSAGE1"].Value.ToString();
                return M;
            }
            catch
            {
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
        }

        /// <summary>
        /// This function is for only used in API Project
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="ParamValue"></param>
        /// <param name="AddOutputParameters"></param>
        /// <returns></returns>
        public SQLReturnValue ExecuteProcWithMessage(string ProcedureName, string[,] ParamValue, bool AddOutputParameters)
        {
            GetConnection();
            if (!MySqlConnection.GetConnection.isConnectionOpen)
                sqlCon.Open();
            sqlCMD.Parameters.Clear();
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                COMMAND.Connection = sqlCon;
                COMMAND.CommandType = CommandType.StoredProcedure;

                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);

                if (AddOutputParameters == true)
                {
                    COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                    COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    //COMMAND.Parameters.Add("@OUTMESSAGE1", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                }

                if (sqlCon.State != ConnectionState.Open) { sqlCon.Open(); }
                COMMAND.ExecuteNonQuery();
                sqlCon.Close();

                SQLReturnValue M = new SQLReturnValue();
                M.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                M.MessageFromSQL = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                //M.MessageFromSQL1 = COMMAND.Parameters["@OUTMESSAGE1"].Value.ToString();
                return M;
            }
            catch
            {
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
        }

        /// <summary>
        /// This function is for only used in API Project
        /// </summary>
        /// <param name="ProceduerName"></param>
        /// <param name="ParamValue"></param>
        /// <param name="ReturnTypeOutVal"></param>
        /// <returns></returns>
        public MEMBERS.SQLReturnMessageNValue ExecuteProceduerWithMessageNValue(string ProceduerName, object[,] ParamValue, int ReturnTypeOutVal)
        {
            GetConnection();
            if (!MySqlConnection.GetConnection.isConnectionOpen)
                sqlCon.Open();
            sqlCMD.Parameters.Clear();
            MEMBERS.SQLReturnMessageNValue res = new MEMBERS.SQLReturnMessageNValue();
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProceduerName;
                COMMAND.Connection = sqlCon;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), (ParamValue[i, 1] == null ? null : (ParamValue[i, 1].ToString() == "null" ? null : ParamValue[i, 1].ToString())));
                }
                COMMAND.Parameters.AddRange(param);
                COMMAND.Parameters.Add("OUTVAL", (ReturnTypeOutVal == 1 ? SqlDbType.UniqueIdentifier : SqlDbType.Int)).Direction = ParameterDirection.Output;
                COMMAND.Parameters.Add("OUTMESSAGE", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                //COMMAND.Parameters.Add("OUTMESSAGE1", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                if (sqlCon.State != ConnectionState.Open) { sqlCon.Open(); }
                COMMAND.ExecuteNonQuery();
                sqlCon.Close();
                SQLReturnValue M = new SQLReturnValue();
                res.Outval = int.Parse(COMMAND.Parameters["OUTVAL"].Value.ToString());
                res.Outmsg = COMMAND.Parameters["OUTMESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
            return res;
        }

        /// <summary>
        /// Executes the procedure and return output value and message from sql server.
        /// </summary>
        /// <param name="ProcedureName">Name of procedure</param>
        /// <param name="ParamValue">Collection of sql parameters as two dimentional array.</param>
        /// <returns>Returns Return values from sql procedure.</returns>
        public MEMBERS.SQLReturnMessageNValue ExecuteProcedureReturnMessageNValue(string ProcedureName, string[,] ParamValue)
        {
            try
            {
                GetConnection();
                if (!MySqlConnection.GetConnection.isConnectionOpen)
                    sqlCon.Open();
               
                MEMBERS.SQLReturnMessageNValue returnval = new MEMBERS.SQLReturnMessageNValue();
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                COMMAND.Connection = sqlCon;
                COMMAND.CommandTimeout = 0;
                COMMAND.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                ///Adds the output parameter
                COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;
                COMMAND.Parameters.Add("@OUTMESSAGE", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                if (sqlCon.State != ConnectionState.Open) { sqlCon.Open(); }
                COMMAND.ExecuteNonQuery();
                ///Retrive value from output parameters to return value structure.
                returnval.Outmsg = COMMAND.Parameters["@OUTMESSAGE"].Value.ToString();
                returnval.Outval = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                sqlCon.Close();
                return returnval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }


        }

        /// <summary>
        /// Executes the procedure and return output value and message from sql server.
        /// </summary>
        /// <param name="ProcedureName">Name of procedure</param>
        /// <param name="ParamValue">Collection of sql parameters as two dimentional array.</param>
        /// <returns>Returns Return values from sql procedure.</returns>
        public MEMBERS.SQlReturnInteger ExecuteProcedureReturnInteger(string ProcedureName, string[,] ParamValue)
        {
            try
            {
                GetConnection();
                if (!MySqlConnection.GetConnection.isConnectionOpen)
                    sqlCon.Open();

                MEMBERS.SQlReturnInteger returnval = new MEMBERS.SQlReturnInteger();
                SqlCommand COMMAND = new SqlCommand();
                COMMAND.CommandText = ProcedureName;
                COMMAND.Connection = sqlCon;
                COMMAND.CommandType = CommandType.StoredProcedure;

                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);
                ///Adds the output parameter
                COMMAND.Parameters.Add("@OUTVAL", SqlDbType.Int).Direction = ParameterDirection.Output;

                if (sqlCon.State != ConnectionState.Open) { sqlCon.Open(); }
                COMMAND.ExecuteNonQuery();
                ///Retrive value from output parameters to return value structure.
                returnval.ValueFromSQL = int.Parse(COMMAND.Parameters["@OUTVAL"].Value.ToString());
                sqlCon.Close();
                return returnval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
        }

        public DataTable ExecuteProcForDataTable(string ProcedureName, string[,] ParamValue)
        {
            GetConnection();
            if (!MySqlConnection.GetConnection.isConnectionOpen)
                sqlCon.Open();
            sqlCMD.Parameters.Clear();
            try
            {
                SqlCommand COMMAND = new SqlCommand();
                DataTable dt = new DataTable();

                COMMAND.CommandText = ProcedureName;
                COMMAND.Connection = sqlCon;
                COMMAND.CommandType = CommandType.StoredProcedure;

                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
                }
                COMMAND.Parameters.AddRange(param);

                if (sqlCon.State != ConnectionState.Open) { sqlCon.Open(); }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(COMMAND);
                dataAdapter.Fill(dt);

                sqlCon.Close();
               
                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                MySqlConnection.GetConnection.CloseConnection(sqlCon);
            }
        }
    }
}
