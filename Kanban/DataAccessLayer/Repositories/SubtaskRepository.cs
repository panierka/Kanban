using Kanban.DataAccessLayer.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class SubtaskRepository
    {
        public static List<Subtask> GetSubtasksFromTask(int taskId)
        {
            List<Subtask> subtasks = new();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                connection.Open();
                string query = $"select * from tasks " +
                    $"join subtasks s on tasks.id = s.master_task_id " +
                    $"where tasks.id = {taskId};";
                MySqlCommand command = new(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var test = new Subtask(reader);
                    subtasks.Add(test);
                }
            }

            return subtasks;
        }
    }
}
