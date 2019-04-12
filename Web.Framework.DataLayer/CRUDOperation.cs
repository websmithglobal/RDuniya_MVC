﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using COM = Web.Framework.Common;
using ENT = Web.Framework.Entity;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using Web.Framework.Common;
using System.ComponentModel.DataAnnotations;

namespace Web.Framework.DataLayer
{
    public class CRUDOperation : COM.DALInterface
    {
        private SqlConnection sqlCON;
        private SqlCommand sqlCMD = new SqlCommand();

        COM.TTDictionary parFields = new COM.TTDictionary();
        COM.DBHelper objDBHelper = new COM.DBHelper();
        COM.TTDictionaryQuery QueryDisctionery = new COM.TTDictionaryQuery();
        
        public CRUDOperation()
        {

        }

        private void GetConnection()
        {
            sqlCON = COM.MySqlConnection.GetConnection.GetDBConnection();
            if (COM.MySqlConnection.GetConnection.isConnectionOpen)
            {
                sqlCMD.Connection = sqlCON;
            }
        }
        public int ExecuteQuery(string Query)
        {
            GetConnection();
            COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
            int blnResult = 0;
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            try
            {
                if (sqlCON.State == ConnectionState.Open)
                {
                    sqlCMD.CommandText = Query;
                    blnResult = sqlCMD.ExecuteNonQuery();
                    if (blnResult >= 0) { trnsaction.Commit(); }
                    // COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception ex)
            {
                blnResult = 0;
                trnsaction.Rollback();
                throw;
            }
            finally
            {
                COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            }
            return blnResult;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            }
        }

        public void Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);

        }

        ~CRUDOperation()
        {
            COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
        }

        #region Insert,Update,Delete Methods
        public bool Insert(object Entity)
        {
            bool blnResult = false;
            GetConnection();
            COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            try
            {
                if (sqlCON.State == ConnectionState.Open)
                {
                    COM.CommonMSSQL.ClearParameter(sqlCMD);

                    #region Get Primarykey Information from object
                    PropertyInfo[] properties = Entity.GetType().GetProperties();
                    #endregion

                    foreach (var entSave in Entity.GetType().GetProperties())
                    {
                        COM.CommonMSSQL.AddParameter(sqlCMD, Entity, entSave.Name, true);
                    }
                    sqlCMD.CommandText = COM.CommonMSSQL.PrepairInsertQuery(sqlCMD, COM.HelperMethod.GetTableName(Entity));
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0) { blnResult = true; trnsaction.Commit(); }
                    COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                trnsaction.Rollback();
                throw;
            }
            finally
            {
                COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            }
            return blnResult;
        }
        public bool Update(object Entity)
        {
            GetConnection();
            COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            bool blnResult = false;
            try
            {
                if (sqlCON.State == ConnectionState.Open)
                {
                    COM.CommonMSSQL.ClearParameter(sqlCMD);
                    foreach (var entSave in Entity.GetType().GetProperties())
                    {
                        COM.CommonMSSQL.AddParameter(sqlCMD, Entity, entSave.Name, false);
                    }
                    sqlCMD.CommandText = COM.CommonMSSQL.PrepairUpdateQuery(sqlCMD, COM.HelperMethod.GetTableName(Entity), Entity);
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0) { blnResult = true; trnsaction.Commit(); }
                    COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                trnsaction.Rollback();
                throw;
            }
            finally
            {
                COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            }
            return blnResult;
        }

        public bool Delete(object Entity)
        {
            GetConnection();
            COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
            bool blnResult = false;
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            try
            {
                if (sqlCON.State == ConnectionState.Open)
                {
                    COM.CommonMSSQL.ClearParameter(sqlCMD);

                    PropertyInfo[] properties = Entity.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                        if (attribute != null) // This property has a KeyAttribute
                        {
                            COM.CommonMSSQL.AddParameter(sqlCMD, Entity, property.Name, true);
                            break;
                        }
                    }
                    sqlCMD.CommandText = COM.CommonMSSQL.PrepairDeleteQuery(COM.HelperMethod.GetTableName(Entity), Entity);
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0) { blnResult = true; trnsaction.Commit(); }
                    COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                trnsaction.Rollback();
                throw;
            }
            finally
            {
                COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            }
            return blnResult;
        }
        #endregion

        #region
        public bool DeleteByParameter(Dictionary<string, bool> objFieldsDictionary, object Entity)
        {
            GetConnection();
            bool blnResult = false;
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            try
            {
                COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    COM.CommonMSSQL.ClearParameter(sqlCMD);
                    foreach (KeyValuePair<string, bool> f in objFieldsDictionary)
                    {
                        COM.CommonMSSQL.AddParameter(sqlCMD, Entity, f.Key);
                    }
                    sqlCMD.CommandText = COM.CommonMSSQL.PrepairPartialDeleteQuery(objFieldsDictionary, COM.HelperMethod.GetTableName(Entity), Entity);
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0) { blnResult = true; trnsaction.Commit(); }
                    else{ trnsaction.Rollback(); }
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                trnsaction.Rollback();
                throw;
            }
            COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            return blnResult;
        }
        #endregion

        #region Partial Update Functions
        public bool SaveChanges(Dictionary<string, bool> objFieldsDictionary, object Entity)
        {
            GetConnection();
            bool blnResult = false;
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            try
            {
                COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    COM.CommonMSSQL.ClearParameter(sqlCMD);
                    foreach (KeyValuePair<string, bool> f in objFieldsDictionary)
                    {
                        COM.CommonMSSQL.AddParameter(sqlCMD, Entity, f.Key);
                    }

                    sqlCMD.CommandText = COM.CommonMSSQL.PrepairPartialUpdateQuery(objFieldsDictionary, COM.HelperMethod.GetTableName(Entity), Entity);
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0) { blnResult = true; trnsaction.Commit(); }
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                trnsaction.Rollback();
                throw;
            }
            COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            return blnResult;
        }
        public bool UpdateByQuery(Dictionary<string, bool> objFieldsDictionary, object Entity, string strQuery)
        {
            GetConnection();
            bool blnResult = false;
            SqlTransaction trnsaction = sqlCON.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlCMD.Transaction = trnsaction;
            try
            {
                COM.MySqlConnection.GetConnection.OpenConnection(sqlCON);
                if (sqlCON.State == ConnectionState.Open)
                {
                    COM.CommonMSSQL.ClearParameter(sqlCMD);
                    foreach (KeyValuePair<string, bool> f in objFieldsDictionary)
                    {
                        COM.CommonMSSQL.AddParameter(sqlCMD, Entity, f.Key, true);
                    }

                    sqlCMD.CommandText = strQuery;
                    int row = sqlCMD.ExecuteNonQuery();
                    if (row > 0) { blnResult = true; trnsaction.Commit(); }
                }
                else
                {
                    throw new Exception("Server not found...database connection error");
                }
            }
            catch (Exception)
            {
                blnResult = false;
                trnsaction.Rollback();
                throw;
            }
            COM.MySqlConnection.GetConnection.CloseConnection(sqlCON);
            return blnResult;
        }

        #endregion

        #region SingleEntity By PrimaryKey From DataReader
        public object GetEntityByPrimartKey(object Entity)
        {
            object objEntity = null;
            try
            {
                objEntity = new COM.DBHelper().GetEnityByPrimaryKey(Entity);

            }
            catch (Exception)
            {
                objEntity = null;
                throw;
            }
            return objEntity;
        }

        public object GetEntityByPerameters(object Entity, Dictionary<string, COM.Enumration.Operators> parFields)
        {
            object objEntity = null;
            try
            {
                objEntity = new COM.DBHelper().GetEnityByPerameters(Entity, parFields);

            }
            catch (Exception)
            {
                objEntity = null;
                throw;
            }
            return objEntity;
        }
        #endregion
    }
}
