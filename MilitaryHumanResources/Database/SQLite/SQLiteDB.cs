using MilitaryHumanResources.Database.SQLite.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryHumanResources.Properties;
using MilitaryHumanResources.Common;

namespace MilitaryHumanResources.Database.SQLite
{
    public class SQLiteDB : BaseSqlCommands
    {
        private static SQLiteDB _instance;

        public static SQLiteDB Instance => _instance ?? (_instance = new SQLiteDB());

        public readonly string DEFAULT_DATABASE_NAME = "MilitaryLiteDB";

        private SQLiteDB()
        {
            if (_instance == null)
                Connect(Settings.Default.DBPath);
        }

        #region Implemented

        protected override string GetConnectionString(string databaseFilePath = null)
        {
            SQLiteConnectionStringBuilder conBuilder = new SQLiteConnectionStringBuilder();
            conBuilder.DataSource = databaseFilePath ?? Environment.CurrentDirectory + $@"\{DEFAULT_DATABASE_NAME}.db";
            conBuilder.DefaultTimeout = 5000;
            conBuilder.SyncMode = SynchronizationModes.Off;
            conBuilder.JournalMode = SQLiteJournalModeEnum.Memory;
            conBuilder.PageSize = 65536;
            conBuilder.CacheSize = 16777216;
            conBuilder.FailIfMissing = false;
            conBuilder.ReadOnly = false;

            // Create the database if not exists
            if (!File.Exists(conBuilder.DataSource))
                SQLiteConnection.CreateFile(conBuilder.DataSource);

            return conBuilder.ConnectionString;
        }

        /// <summary>
        /// Connect to SQLite database  
        /// </summary>
        /// <param name="databaseFilePath">Database file path.</param>
        public override void Connect(string databaseFilePath = null)
        {
            _connection?.Dispose();
            _connection = new SQLiteConnection(GetConnectionString(databaseFilePath));
            Settings.Default.SoftwareVersion = UpgradeDatabase(Settings.Default.SoftwareVersion);
            Settings.Default.Save();
        }

        public override DataTable Select(string query)
        {
            DataTable dt = new DataTable();
            OpenConnection();
            using (SQLiteCommand cmd = new SQLiteCommand(query, (SQLiteConnection)_connection))
            {
                dt.Load(cmd.ExecuteReader());
            }

            CloseConnection();
            return dt;
        }

        public override long Update(string query)
        {
            return ExecuteNonQuery(query);
        }

        public override long Insert(string query)
        {
            return ExecuteNonQuery(query);
        }

        protected override long ExecuteNonQuery(string query)
        {
            long rowsUpdated = 0;
            OpenConnection();
            using (var cmd = new SQLiteCommand(query, (SQLiteConnection)_connection))
            {                
                rowsUpdated = (long)cmd.ExecuteNonQuery();                
            }

            CloseConnection();
            return rowsUpdated;
        }

        #endregion

        public bool CreateTable(string name, List<SQLiteFieldType> fields)
        {
            var queryFields = new List<string>();
            foreach (var f in fields)
                queryFields.Add(f.ToString());
            var query = $"CREATE TABLE {name} ({string.Join(",", queryFields)})";

            try
            {
                OpenConnection();
                using (var command = new SQLiteCommand(query, (SQLiteConnection) _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog($"Can't create '{name}' table.");
                return false;
            }
            finally
            {
                CloseConnection();
            }

            return true;
        }

        /// <summary>
        /// Update the database structure
        /// </summary>
        /// <param name="versionNumber">The current software version</param>
        /// <returns>The new software version</returns>
        protected override string UpgradeDatabase(string versionNumber)
        {
            int version = ConvertVersionNumberToRealNumber(versionNumber);
            if (version <= 1000)
            {
                //Create Soldir table
                

                versionNumber = "1.0.0.0";
            }

            if (version < 1001)
            {

                versionNumber = "1.0.0.1";
            }
            return base.UpgradeDatabase(versionNumber);
        }
    }
}
