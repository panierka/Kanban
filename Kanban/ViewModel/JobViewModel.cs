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
        private JobsManager jobsManager;
        private Job x;
        private TablesManager tablesManager;

        public JobViewModel(Job x, TablesManager tablesManager)
        {
            this.x = x;
            this.tablesManager = tablesManager;
        }
    }
}
