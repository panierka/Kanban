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
        private readonly MySqlConnectionStringBuilder connectionStringBuilder;
        private static DatabaseConnection? instance;

        public static DatabaseConnection Instance => instance ??= new();

        public MySqlConnection Connection => new(connectionStringBuilder.ToString());

        private DatabaseConnection()
        {
            connectionStringBuilder = new()
            {
                UserID = Properties.Settings.Default.userID,
                Password = Properties.Settings.Default.password,
                Port = Properties.Settings.Default.port,
                Server = Properties.Settings.Default.server,
                Database = Properties.Settings.Default.database,
            };
        }
    }
}
