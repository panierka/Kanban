﻿using Kanban.DataAccessLayer.Entities;
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
            Job job = new("Nowe zadanie")
            {
                StartDate = DateTime.Now
            };

            JobsRepository.InsertJob(job, out _);
            return job;
        }

        internal void DeleteJob(Job job)
        {
            JobsRepository.RemoveJob(job, out _);
        }
    }
}