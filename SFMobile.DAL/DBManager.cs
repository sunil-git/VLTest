using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data;
using System.Configuration;
using System.IO;

namespace SFMobile.DAL
{
    
    internal class DBFactory
    {
        
        private static DbProviderFactory objFactory = null;

        public static DbProviderFactory GetProvider(DALEnums.ProviderType provider)
        {

            switch (provider)
            {

                case DALEnums.ProviderType.SqlServer:

                    objFactory = SqlClientFactory.Instance;

                    break;

                case DALEnums.ProviderType.OleDb:

                    objFactory = OleDbFactory.Instance;

                    break;

                case DALEnums.ProviderType.ODBC:

                    objFactory = OdbcFactory.Instance;

                    break;

            }

            return objFactory;

        }
        public static DbDataAdapter GetDataAdapter(DALEnums.ProviderType providerType)
        {

            switch (providerType)
            {

                case DALEnums.ProviderType.SqlServer:

                    return new SqlDataAdapter();

                case DALEnums.ProviderType.OleDb:

                    return new OleDbDataAdapter();

                case DALEnums.ProviderType.ODBC:

                    return new OdbcDataAdapter();

                default:

                    return null;
            }

        }

    }

    public sealed class DBManager : DBManagerBase
    {
        public void OpenConnection()
        {

            connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;  
            base.Open(connectionString);

        }

        public void OpenConnection(String connectionString)
        {
            base.Open(connectionString);
            base.IsOpen = true;

        }

        public void CloseConnection()
        {
            //if (base.isOpen)
            base.Close();
            // base.IsOpen = false;

        }

        #region MobileConnection

        public void OpenMobileConnection()
        {

            connectionString = ConfigurationSettings.AppSettings["MobileConnectionString"].ToString();
            base.Open(connectionString);

        }

        public void OpenMobileConnection(String connectionString)
        {
            base.Open(connectionString);
            base.IsOpen = true;

        }

        public void CloseMobileConnection()
        {
            //if (base.isOpen)
            base.Close();
            // base.IsOpen = false;

        }
        #endregion

        public int AddParameter(string name, object value)
        {
            return databaseHelper.AddParameter(name, value);
        }

        public int AddParameter(string name, DALEnums.StoredProcedureParameterDirection parameterDirection)
        {
            return databaseHelper.AddParameter(name, parameterDirection);

        }

        public int AddParameter(string name, object value, DALEnums.StoredProcedureParameterDirection parameterDirection)
        {
            return databaseHelper.AddParameter(name, value, parameterDirection);
        }

        public int AddParameter(string name, DALEnums.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {
            return databaseHelper.AddParameter(name, parameterDirection, size, dbType);
        }

        public int AddParameter(string name, object value, DALEnums.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {
            return databaseHelper.AddParameter(name, value, parameterDirection, size, dbType);
        }

        public object GetParameter(string name)
        {
            return databaseHelper.GetParameter(name);
        }

        public DbDataReader ExecuteReader(string query)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query);
            return this.dbDataReader;
        }

        public DbDataReader ExecuteReader(string query, CommandType commandtype)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query, commandtype, DALEnums.DatabaseConnectionState.CloseOnExit);
            return this.dbDataReader;
        }

        public IDataReader ExecuteReader(string storedProcedureName, params object[] parameters)
        {
            this.dbDataReader = (DbDataReader)databaseHelper.ExecuteReader(storedProcedureName, parameters);
            return this.dbDataReader;
        }

        public DbDataReader ExecuteReader(string query, CommandType commandtype, DALEnums.DatabaseConnectionState connectionstate)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query, commandtype, connectionstate);
            return this.dbDataReader;
        }

        public DbDataReader ExecuteReader(string query, DALEnums.DatabaseConnectionState connectionstate)
        {
            this.dbDataReader = databaseHelper.ExecuteReader(query, connectionstate);
            return this.dbDataReader;
        }

        public object ExecuteScalar(string query)
        {
            return databaseHelper.ExecuteScalar(query);
        }

        public object ExecuteScalar(string query, CommandType commandtype)
        {
            return databaseHelper.ExecuteScalar(query, commandtype);
        }

        public object ExecuteScalar(string query, DALEnums.DatabaseConnectionState connectionstate)
        {
            return databaseHelper.ExecuteScalar(query, connectionstate);
        }

        public object ExecuteScalar(string query, CommandType commandtype, DALEnums.DatabaseConnectionState connectionstate)
        {
            return databaseHelper.ExecuteScalar(query, commandtype, connectionstate);
        }

        public DataSet ExecuteDataSet(string query)
        {
            this.dataSet = databaseHelper.ExecuteDataSet(query);
            return this.dataSet;
        }

        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            this.dataSet = databaseHelper.ExecuteDataSet(query, commandtype);
            return this.dataSet;
        }

        public int ExecuteNonQuery(string query, CommandType commandtype)
        {
            return databaseHelper.ExecuteNonQuery(query, commandtype);
        }

        public int ExecuteNonQuery(string query, CommandType commandtype, DALEnums.DatabaseConnectionState databaseConnectionState)
        {
            return databaseHelper.ExecuteNonQuery(query, commandtype, databaseConnectionState);
        }
        public void ClearParameters()
        {
            databaseHelper.ClearParameters();
        }
    }
}
