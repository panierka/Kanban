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
    internal class TableViewModel:BaseViewModel
    {
        private ICommand? _createNewJob;
        public ICommand CreateNewJob => _createNewJob ??= new RelayCommand
            (
                _ =>
                {

                }
            );
    }
}
