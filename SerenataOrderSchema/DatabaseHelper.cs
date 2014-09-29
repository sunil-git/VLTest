using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;


namespace SerenataOrderSchema
{
    public class DatabaseHelper : IDisposable
    {
        private string strConnectionString;
        private DbConnection objConnection;
        private DbCommand objCommand;
        private DbProviderFactory objFactory = null;

        public DatabaseHelper(string connectionstring, DALEnums.ProviderType provider)
        {

            this.strConnectionString = connectionstring;
            objFactory = DBFactory.GetProvider(provider);
            objConnection = objFactory.CreateConnection();
            objCommand = objFactory.CreateCommand();
            objConnection.ConnectionString = this.strConnectionString;
            objCommand.Connection = objConnection;

        }

        internal int AddParameter(string name, object value)
        {

            DbParameter dbParameter = objFactory.CreateParameter();
            dbParameter.ParameterName = name;
            dbParameter.Value = value;
            return objCommand.Parameters.Add(dbParameter);

        }

        internal int AddParameter(DbParameter parameter)
        {

            return objCommand.Parameters.Add(parameter);

        }

        internal int AddParameter(string name, DALEnums.StoredProcedureParameterDirection parameterDirection)
        {

            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = String.Empty;
            parameter.DbType = DbType.String;
            parameter.Size = 50;
            switch (parameterDirection)
            {

                case DALEnums.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DALEnums.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DALEnums.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DALEnums.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;

            }
            return objCommand.Parameters.Add(parameter);

        }

        internal int AddParameter(string name, object value, DALEnums.StoredProcedureParameterDirection parameterDirection)
        {

            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = DbType.String;
            parameter.Size = 50;
            switch (parameterDirection)
            {

                case DALEnums.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DALEnums.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DALEnums.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DALEnums.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;

            }

            return objCommand.Parameters.Add(parameter);

        }

        internal int AddParameter(string name, DALEnums.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {

            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = dbType;
            parameter.Size = size;
            switch (parameterDirection)
            {

                case DALEnums.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DALEnums.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DALEnums.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DALEnums.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;

            }

            return objCommand.Parameters.Add(parameter);

        }

        internal int AddParameter(string name, object value, DALEnums.StoredProcedureParameterDirection parameterDirection, int size, DbType dbType)
        {

            DbParameter parameter = objFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = dbType;
            parameter.Size = size;
            switch (parameterDirection)
            {

                case DALEnums.StoredProcedureParameterDirection.Input:
                    parameter.Direction = System.Data.ParameterDirection.Input;
                    break;
                case DALEnums.StoredProcedureParameterDirection.Output:
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    break;
                case DALEnums.StoredProcedureParameterDirection.InputOutput:
                    parameter.Direction = System.Data.ParameterDirection.InputOutput;
                    break;
                case DALEnums.StoredProcedureParameterDirection.ReturnValue:
                    parameter.Direction = System.Data.ParameterDirection.ReturnValue;
                    break;

            }

            return objCommand.Parameters.Add(parameter);

        }

        internal DbCommand Command
        {

            get
            {

                return objCommand;

            }

        }

        internal DbConnection Connection
        {

            get
            {

                return objConnection;

            }

        }

        internal void BeginTransaction()
        {

            if (objConnection.State == System.Data.ConnectionState.Closed)
            {

                objConnection.Open();

            }

            objCommand.Transaction = objConnection.BeginTransaction();

        }

        internal void CommitTransaction()
        {

            objCommand.Transaction.Commit();
            objConnection.Close();

        }

        internal void RollbackTransaction()
        {

            objCommand.Transaction.Rollback();
            objConnection.Close();

        }

        internal int ExecuteNonQuery(string query)
        {

            return ExecuteNonQuery(query, CommandType.Text, DALEnums.DatabaseConnectionState.CloseOnExit);

        }

        internal int ExecuteNonQuery(string query, CommandType commandtype)
        {

            return ExecuteNonQuery(query, commandtype, DALEnums.DatabaseConnectionState.CloseOnExit);

        }

        internal int ExecuteNonQuery(string query, DALEnums.DatabaseConnectionState connectionstate)
        {

            return ExecuteNonQuery(query, CommandType.Text, connectionstate);

        }

        internal int ExecuteNonQuery(string query, CommandType commandtype, DALEnums.DatabaseConnectionState connectionstate)
        {

            objCommand.CommandText = query;

            objCommand.CommandType = commandtype;

            int i = -1;

            try
            {

                if (objConnection.State == System.Data.ConnectionState.Closed)
                {

                    objConnection.Open();

                }

                i = objCommand.ExecuteNonQuery();

            }

            catch
            {

                throw;

            }

            finally
            {

                if (connectionstate == DALEnums.DatabaseConnectionState.CloseOnExit)
                {

                    objConnection.Close();

                }

            }

            return i;

        }

        internal object ExecuteScalar(string query)
        {

            return ExecuteScalar(query, CommandType.Text, DALEnums.DatabaseConnectionState.CloseOnExit);

        }

        internal object ExecuteScalar(string query, CommandType commandtype)
        {

            return ExecuteScalar(query, commandtype, DALEnums.DatabaseConnectionState.CloseOnExit);

        }

        internal object ExecuteScalar(string query, DALEnums.DatabaseConnectionState connectionstate)
        {

            return ExecuteScalar(query, CommandType.Text, connectionstate);

        }

        internal object ExecuteScalar(string query, CommandType commandtype, DALEnums.DatabaseConnectionState connectionstate)
        {

            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            object o = null;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = objCommand.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                // objCommand.Parameters.Clear();
                if (connectionstate == DALEnums.DatabaseConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }

            }

            return o;
        }
        internal DbDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, DALEnums.DatabaseConnectionState.CloseOnExit);
        }

        internal DbDataReader ExecuteReader(string query, CommandType commandtype)
        {
            return ExecuteReader(query, commandtype, DALEnums.DatabaseConnectionState.CloseOnExit);
        }

        internal DbDataReader ExecuteReader(string query, DALEnums.DatabaseConnectionState connectionstate)
        {
            return ExecuteReader(query, CommandType.Text, connectionstate);
        }

        internal DbDataReader ExecuteReader(string query, CommandType commandtype, DALEnums.DatabaseConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            DbDataReader reader = null;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                if (connectionstate == DALEnums.DatabaseConnectionState.CloseOnExit)
                {
                    reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }

                else
                {
                    reader = objCommand.ExecuteReader();
                }

            }

            catch
            {
            }

            finally
            {
                // objCommand.Parameters.Clear();
            }

            return reader;
        }
        internal DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text, DALEnums.DatabaseConnectionState.CloseOnExit);
        }

        internal DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            return ExecuteDataSet(query, commandtype, DALEnums.DatabaseConnectionState.CloseOnExit);
        }

        internal DataSet ExecuteDataSet(string query, DALEnums.DatabaseConnectionState connectionstate)
        {
            return ExecuteDataSet(query, CommandType.Text, connectionstate);
        }

        internal DataSet ExecuteDataSet(string query, CommandType commandtype, DALEnums.DatabaseConnectionState connectionstate)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds);
            }

            catch
            {
                throw;
            }

            finally
            {
                //objCommand.Parameters.Clear();
                if (connectionstate == DALEnums.DatabaseConnectionState.CloseOnExit)
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {

                        objConnection.Close();

                    }

                }

            }

            return ds;
        }
        public void Dispose()
        {
            if (objConnection.State == ConnectionState.Open)
            {

                objConnection.Close();
                objConnection.Dispose();

            }
            objCommand.Dispose();
        }

        internal IDataReader ExecuteReader(string storedProcedureName, params object[] parameters)
        {

            objCommand.CommandText = storedProcedureName;
            objCommand.CommandType = CommandType.StoredProcedure;
            DbDataReader reader = null;

            try
            {

                //RetrieveParameters(objCommand);
                SetParameterValues(objCommand, parameters);
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                reader = objCommand.ExecuteReader();

            }

            catch
            {
                throw;
            }

            finally
            {
                //objCommand.Parameters.Clear();
            }

            return reader;

        }
        internal void SetParameterValues(DbCommand objCommand, object[] parameters)
        {
            int index = 0;
            for (int i = 0; i < parameters.Length; i++)
            {

                DbParameter parameter = objCommand.Parameters[i + index];
                SetParameterValue(objCommand, parameter.ParameterName, parameters[i]);

            }

        }

        internal virtual void SetParameterValue(DbCommand dbCommand, string parameterName, object value)
        {

            dbCommand.Parameters[parameterName].Value = (value == null) ? DBNull.Value : value;

        }



        internal object GetParameter(string name)
        {
            return objCommand.Parameters[name].Value;
        }
        internal void ClearParameters()
        {
            objCommand.Parameters.Clear();
        }

    }

}








