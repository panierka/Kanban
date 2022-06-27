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
    internal static class JobsRepository
    {
        private const string JOB_NAME = "tasks";
        public static List<Job> GetAllJobs()
        {
            return MySqlQueriesWrapper.SelectAll("tasks", x => new Job(x));
        }

        public static void InsertJob(Job job, out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("name", "description",
                 "state", "difficulty", "estimated_work_time", "start_datetime", "deadline_datetime","author_id", "master_table_id");
            MySqlQueriesWrapper.Insert(job, attributes, JOB_NAME, out successful);
        }

        public static List<Job> GetJobsFromTable(int tableId)
        {
            List<Job> jobs = new();

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                connection.Open();
                string query = $"select * from {JOB_NAME} " +
                    $"join tables t on t.id = {JOB_NAME}.master_table_id " +
                    $"where t.id = {tableId};";
                MySqlCommand command = new(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var job = new Job(reader);
                    jobs.Add(job);
                }
            }

            return jobs;
        }

        public static void RemoveJob(Job job, out bool successful)
        {
            var condition = $"where id = {job.Id}";
            MySqlQueriesWrapper.Remove(condition, JOB_NAME, out successful);
        }

        public static void UpdateJob(Job job, out bool successful)
        {
            string dateFormat = MySqlVariableFormatter.DATE_FORMAT;
            string attributeUpdates = $"name = {MySqlVariableFormatter.Format(job.Name)}, " +
                $"description = {MySqlVariableFormatter.Format(job.Description)}, " +
                $"state = {MySqlVariableFormatter.Format(job.StateAsString)}, " +
                $"difficulty = {MySqlVariableFormatter.Format(job.DifficultyAsString)}, " +
                $"estimated_work_time = {MySqlVariableFormatter.Format(job.EstimatedTime?.Ticks)}, " +
                $"start_datetime = {MySqlVariableFormatter.Format(job.StartDate.ToString(dateFormat))}, " +
                $"deadline_datetime = {MySqlVariableFormatter.Format(job.DeadlineDate?.ToString(dateFormat))}, " +
                $"master_table_id = {MySqlVariableFormatter.Format(job.TableId)}";
            MySqlQueriesWrapper.Update(job, attributeUpdates, JOB_NAME, out successful);
        }
    }
}
