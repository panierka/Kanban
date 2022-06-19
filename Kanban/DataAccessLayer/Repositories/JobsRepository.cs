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
        public static List<Job> GetAllJobs()
        {
            return MySqlQueriesWrapper.SelectAll("tasks", x => new Job(x));
        }
    }
}
