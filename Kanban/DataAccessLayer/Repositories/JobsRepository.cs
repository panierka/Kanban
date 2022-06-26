using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class JobsRepository
    {
        private const string JOB_NAME = "jobs";
        public static List<Job> GetAllJobs()
        {
            return MySqlQueriesWrapper.SelectAll("tasks", x => new Job(x));
        }

        public static void InsertJob(Job job, out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("name", "description",
                 "state", "difficulty", "estimated_time", "start_datetime", "deadline_date","author_id", "master_table_id");
            MySqlQueriesWrapper.Insert(job, attributes, JOB_NAME, out successful);
        }
    }
}
