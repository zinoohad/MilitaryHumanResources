using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryHumanResources.Database
{
    public abstract class BaseSqlCommands
    {

        protected DbConnection _connection;

        protected readonly string _stringConnection;

        #region Abstract Methods

        protected abstract string GetConnectionString(string databaseFilePath = null);

        public abstract void Connect(string databaseFilePath = null);

        public abstract DataTable Select(string query);

        public abstract long Update(string query);

        public abstract long Insert(string query);

        protected abstract long ExecuteNonQuery(string sqlQuery);

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Upgrade the database structure.
        /// This funcion will call when the object created
        /// </summary>
        /// <param name="versionNumber">Current version number</param>
        /// <returns>The new version number</returns>
        protected virtual string UpgradeDatabase(string versionNumber)
        {
            return versionNumber;
        }

        protected virtual bool CloseConnection()
        {
            if (_connection == null)
                return false;
            if (_connection.State == ConnectionState.Closed)
                return true;
            _connection.Close();
            return true;              
        }

        protected virtual bool OpenConnection()
        {
            if (_connection == null)
                return false;
            if (_connection.State == ConnectionState.Open)
                return true;
            _connection.Open();
            return true;
        }

        /// <summary>
        /// Convert the version number from string to numeric
        /// The version Type must to be from the format X.X.X.X
        /// </summary>
        /// <param name="version">The version number to convert.</param>
        /// <returns>Numeric represent the version number</returns>
        protected int ConvertVersionNumberToRealNumber(string version)
        {
            if (version == null)
                return 0;
            version = version.Replace(".", "");
            try
            {
                return int.Parse(version);
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        #endregion
    }
}
