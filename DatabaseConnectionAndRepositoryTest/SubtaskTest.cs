using Kanban.DataAccessLayer;
using Kanban.DataAccessLayer.Entities;
using MySql.Data.MySqlClient;

namespace DatabaseConnectionAndRepositoryTest
{
    [TestClass]
    public class SubtasksTest
    {
        [TestMethod]
        public void SelectAllTest()
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                connection.Open();
                string query = "select * from tasks";
                MySqlCommand command = new(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AssignedTask task = new(reader);
                    Console.WriteLine(string.Join("\n", task.Subtasks));
                }
            }
        }
    }
}