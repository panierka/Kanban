using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Kanban.DataAccessLayer
{
    internal class DatabaseConnection
    {
        private readonly MySqlConnectionStringBuilder connectionStringBuilder = new();
        private static DatabaseConnection? instance;

        public static DatabaseConnection Instance => instance ??= new();

        public MySqlConnection Connection => new(connectionStringBuilder.ToString());

        private DatabaseConnection()
        {
            connectionStringBuilder.UserID = Properties.Settings.Default.userID;
            connectionStringBuilder.Server = Properties.Settings.Default.server;
            connectionStringBuilder.Database = Properties.Settings.Default.database;
        }
    }
}
