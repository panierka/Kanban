using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    internal class JobsManager
    {
        private User? user;

        public void SetUser(User? user)
        {
            this.user = user;
        }

        public Job CreateJob(int tableId)
        {
            Job job = new("Nowe zadanie", tableId, 1)
            {
                StartDate = DateTime.Now
            };

            JobsRepository.InsertJob(job, out _);
            return job;
        }

        internal void UpdateJob(Job targetJob)
        {
            JobsRepository.UpdateJob(targetJob, out _);
        }

        internal void DeleteJob(Job job)
        {
            JobsRepository.RemoveJob(job, out _);
        }
    }
}
