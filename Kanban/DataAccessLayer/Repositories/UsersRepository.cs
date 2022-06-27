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
        private const string TABLE_NAME = "users";

        public static User? GetUserFromLoginAndPassword(string login, string password)
        {
            var condition = $"where login = '{login}' and password = '{password}'";
            User? user = MySqlQueriesWrapper.GetRecord(condition, TABLE_NAME, x => new User(x));
            return user;
        }

        public static User? GetUserFromId(int id)
        {
            var condition = $"where id = {id}";
            User? user = MySqlQueriesWrapper.GetRecord(condition, TABLE_NAME, x => new User(x));
            return user;
        }

        public static User? GetUserFromLogin(string login)
        {
            var condition = $"where login = '{login}'";
            User? user = MySqlQueriesWrapper.GetRecord(condition, TABLE_NAME, x => new User(x));
            return user;
        }

        public static bool CheckIfLoginIsTaken(string login)
        {
            var condition = $"where login = '{login}'";
            return MySqlQueriesWrapper.CheckIfRecordExists(condition, TABLE_NAME);
        }

        internal static void AddUser(User user)
        {
            string attributes = MySqlInsertBuilder.JoinNames("name", "login", "password");
            MySqlQueriesWrapper.Insert(user, attributes, TABLE_NAME, out _);
        }
    }
}
