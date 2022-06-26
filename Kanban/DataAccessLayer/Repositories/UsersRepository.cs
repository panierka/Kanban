using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Wrappers;
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
        public static User? GetUserFromLoginAndPassword(string login, string password)
        {
            var condition = $"where login = '{login}' and password = '{password}'";
            User? user = MySqlQueriesWrapper.GetRecord(condition, "users", x => new User(x));
            return user;
        }
    }
}
