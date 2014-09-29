/// <summary>
/// Author:Valuelabs
/// Date: 04/07/2011 1:25:14 PM
/// Class Name:DALEnums
/// Description:These DALEnums relate to the databases that we will be connecting to, depending our requirements.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMobile.DAL
{
    public partial class DALEnums
    {
        
        /// <summary>
        /// Provider Type enum contain the data provider types in it.
        /// </summary>
        public enum ProviderType
        {
            SqlServer,
            OleDb,
            Oracle,
            ODBC,
            ConfigDefined
        }

        
        /// <summary>
        ///DatabaseConnectionState enum keeps the database connection state open or close after a database operation is over.
        /// </summary>
        public enum DatabaseConnectionState
        {
            KeepOpen,
            CloseOnExit
        }
              
     
        /// <summary>
        /// StoredProcedureParameterDirection Enum sends data to the database or retrieve from the database.
        /// </summary>
        public enum StoredProcedureParameterDirection
        {
            Input,
            InputOutput,
            Output,
            ReturnValue
        }


    }
}
