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
    internal static class SubtaskRepository
    {
        private const string SUBTASK_NAME = "subtasks";

        public static List<Subtask> GetAllSubtasks()
        {
            return MySqlQueriesWrapper.SelectAll("subtasks", x => new Subtask(x));
        }

        public static void InsertSubtask(Subtask subtask, out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("text", "is_done", "master_task_id");
            MySqlQueriesWrapper.Insert(subtask, attributes, SUBTASK_NAME, out successful);
        }
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
            public static void RemoveSubtask(Subtask subtask, out bool successful)
            {
                var condition = $"where id = {subtask.Id}";
                MySqlQueriesWrapper.Remove(condition, SUBTASK_NAME, out successful);
            }
    }
}
