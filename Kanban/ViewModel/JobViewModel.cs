using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using Kanban.ViewModel.Base;
using System.Windows;
using System.Collections.ObjectModel;
using Kanban.DataAccessLayer.Entities;
using Kanban.Model;

namespace Kanban.ViewModel
{
    internal class JobViewModel
    {
        public Job TargetJob { get; init; }

        private JobsManager jobsManager;

        public JobViewModel(Job job, JobsManager jobsManager)
        {
            TargetJob = job;
            this.jobsManager = jobsManager;
        }
    }
}
