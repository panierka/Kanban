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

        public event Action? OnChangeStructure;

        private readonly TablesManager tablesManager;
        private readonly JobsManager jobsManager;

        public JobViewModel CurrentlySelectedJobViewModel
        {
            set
            {
                CurrentJob = value?.TargetJob;
            }
        }

        public ObservableCollection<JobViewModel> CurrentProjectJobs
        {
            get
            {
                if (TargetTable is null)
                {
                    return new();
                }
                return new(TargetTable.Jobs.Select(x => new JobViewModel(x, jobsManager)));
            }
        }

        public Job? CurrentJob
        {
            get => _currentJob;
            set
            {
                _currentJob = value;
                NotifyPropertyChanged(
                    nameof(CurrentJob),
                    nameof(IsCurrentJobSelected),
                    nameof(JobViewModel.CurrentJobName),
                    nameof(JobViewModel.CurrentJobDescription));
            }
        }

        public bool IsCurrentJobSelected => CurrentJob is { };

        public ICommand CreateNewJob => _createNewJob ??= new RelayCommand
            (
                _ =>
                {
                    var job = jobsManager.CreateJob(TargetTable.Id!.Value);
                    CurrentJob = job;
                    TargetTable.RefreshJobs();
                    NotifyPropertyChanged(nameof(CurrentProjectJobs));
                    OnChangeStructure?.Invoke();
                },
                _ => TargetTable.Id is { }
            );

        public ICommand DeleteCurrentJob => _deleteCurrentJob ??= new RelayCommand
            (
                _ =>
                {
                    jobsManager.DeleteJob(CurrentJob!);
                    CurrentJob = null;
                    TargetTable.RefreshJobs();
                    NotifyPropertyChanged(nameof(CurrentProjectJobs));
                    OnChangeStructure?.Invoke();
                },
                _ => IsCurrentJobSelected
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
        public ObservableCollection<JobViewModel> TargetTableJobs
        {
            get
            {
                return new(TargetTable.Jobs.Select(x => new JobViewModel(x, jobsManager)));
            }
        }

        public TableViewModel(Table targetTable, TablesManager tablesManager, JobsManager jobsManager)
        {
            TargetTable = targetTable;
            this.tablesManager = tablesManager;
            this.jobsManager = jobsManager;
        }


        private ICommand? _createNewJob;
        private ICommand? _deleteCurrentJob;
        private Job? _currentJob;
    }
}
