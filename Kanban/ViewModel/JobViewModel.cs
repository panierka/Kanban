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
    internal class JobViewModel:BaseViewModel
    {
        public Job TargetJob { get; init; }

        private JobsManager jobsManager;
        private SubtasksManager subtasksManager;
        private Subtask? _currentSubtask;
        private ICommand? _createNewSubtask;
        private ICommand? _deleteCurrentSubtask;
        public Subtask? CurrentSubtask
        {
            get => _currentSubtask;
            set
            {
                _currentSubtask = value;
                NotifyPropertyChanged(
                nameof(CurrentSubtask),
                nameof(IsCurrentSubtaskSelected));
            }
        }

        public bool IsCurrentSubtaskSelected => CurrentSubtask is { };

        public ICommand CreateNewSubtask => _createNewSubtask ??= new RelayCommand
            (
                _ =>
                {
                    var subtask = subtasksManager.CreateSubtask(TargetJob.Id!.Value);
                    CurrentSubtask = subtask;
                },
                _ => TargetJob.Id is { }
            );

        public ICommand DeleteCurrentSubtask => _deleteCurrentSubtask ??= new RelayCommand
            (
                _ =>
                {
                    subtasksManager.DeleteSubtask(CurrentSubtask!);
                },
                _ => IsCurrentSubtaskSelected
            );

        public JobViewModel(Job job, JobsManager jobsManager)
        {
            TargetJob = job;
            this.jobsManager = jobsManager;
        }
    }
}
