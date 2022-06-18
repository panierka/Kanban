using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer;
using System.Windows;

namespace DatabaseConnectionAndRepositoryTest
{
    [TestClass]
    public class RepositoryTestTest
    {
        [TestMethod]
        public void SelectAllTest()
        {
            var tests = RepositoryTest.GetAllTests();

            var result = string.Join(", ", tests);
            var expected = "[1] hubert: 46 pts, [2] kuba: 47 pts";

            Assert.AreEqual(expected, result);
        }

        public class Test
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public int Score { get; set; }

            public Test(MySqlDataReader reader)
            {
                Id = int.Parse(reader["id"].ToString()!);
                Name = reader["name"].ToString();
                Score = int.Parse(reader["score"].ToString() ?? "0");
            }

            public override string ToString()
            {
                return $"[{Id}] {Name}: {Score} pts";
            }
        }

        private static class RepositoryTest
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
}