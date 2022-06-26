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
    internal class TableViewModel : BaseViewModel
    {
        public Table TargetTable { get; set; }

        private readonly TablesManager tablesManager;

        public ICommand CreateNewJob => _createNewJob ??= new RelayCommand
            (
                _ =>
                {
                    MessageBox.Show("test dodawania");
                }
            );

        public bool IsCurrentTableEditable => tablesManager.CanUpdateTable(TargetTable);
        public string CurrentTableName
        {
            get => TargetTable.Name ?? string.Empty;
            set
            {
                TargetTable.Name = value;
                tablesManager.UpdateTable(TargetTable);
                NotifyPropertyChanged(nameof(CurrentTableName));
            }
        }

        public string CurrentTableDescription
        {
            get => TargetTable.Description ?? string.Empty;
            set
            {
                TargetTable.Description = value;
                tablesManager.UpdateTable(TargetTable);
                NotifyPropertyChanged(nameof(CurrentTableDescription));
            }
        }
        public ObservableCollection<JobViewModel> CurrentProjectTables
        {
            get
            {
                if (TargetTable is null)
                {
                    return new();
                }

                return new(TargetTable.Jobs.Select(x => new JobViewModel(x, tablesManager)));
            }
        }

        public TableViewModel(Table targetTable, TablesManager tablesManager)
        {
            TargetTable = targetTable;
            this.tablesManager = tablesManager;
        }

        private ICommand? _createNewJob;
    }
}
