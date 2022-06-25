using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class UsersRepository
    {
        public static bool GetUserFromLoginAndPassword(string login, string password)
        {
            using var connection = DatabaseConnection.Instance.Connection;

            var query = $"select * from users where " +
                $"users.login = '{login}' " +
                $"and users.password = '{password}'";
            MySqlCommand command = new(query, connection);
            connection.Open();
            int count = (int)command.ExecuteScalar();

            return count >= 1;
        }
    }
}
