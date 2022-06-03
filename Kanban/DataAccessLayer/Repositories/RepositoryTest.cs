using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.DataAccessLayer.Entities;
using MySql;
using MySql.Data.MySqlClient;

namespace Kanban.DataAccessLayer.Repositories
{
    public static class RepositoryTest
    {
        public static List<Test> GetAllTests()
        {
            List<Test> tests = new();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                connection.Open();
                string query = "select * from test";
                MySqlCommand command = new(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var test = new Test(reader);

                    tests.Add(test);
                }
            }

            return tests;
        }
    }
}
