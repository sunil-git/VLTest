﻿using System;
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
    public abstract class DBManagerBase
    {
        protected DatabaseHelper databaseHelper = null;
        protected DbDataReader dbDataReader = null;
        protected DataSet dataSet = null;
        protected DALEnums.ProviderType providerType;
        protected String connectionString = String.Empty;
        protected bool isOpen = false;

        public bool IsOpen
        {

            get
            {
                return isOpen;

            }

            set
            {

                isOpen = value;

            }

        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }

        }

        public DbConnection Connection
        {
            get
            {

                return databaseHelper.Connection;
            }
        }

        public DbCommand Command
        {
            get
            {

                return databaseHelper.Command;
            }

        }

        public DALEnums.ProviderType DBProvider
        {
            set
            {
                providerType = value;
            }

            get
            {
                return providerType;
            }

        }

        public DataSet DBSet
        {
            get
            {
                return dataSet;
            }

        }

        public DbDataReader DBReader
        {
            get
            {
                return dbDataReader;
            }

        }

        protected void Open(string connectionString)
        {
            databaseHelper = new DatabaseHelper(connectionString, DBProvider);
        }

        protected void Close()
        {
            if (dbDataReader != null)
                if (!dbDataReader.IsClosed)
                    dbDataReader.Close();
            databaseHelper.Dispose();
        }

        public void BeginTransaction()
        {
            databaseHelper.BeginTransaction();
        }

        public void CommitTransaction()
        {
            databaseHelper.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            databaseHelper.RollbackTransaction();
        }
    }
}
