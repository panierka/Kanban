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
        public Job CreateJob(int projectId)
        {
            Job job = new("Nowe zadanie")
            {
                StartDate = DateTime.Now
            };

            JobsRepository.InsertJob(job, out _);
            return job;
        }
    
    }
}
