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
        public List<Job.DifficultyLevel> DifficultyLevels { get; set; } = Enum.GetValues(typeof(Job.DifficultyLevel)).Cast<Job.DifficultyLevel>().ToList();
        public List<Job.StateLevel> StateLevels { get; set; } = Enum.GetValues(typeof(Job.StateLevel)).Cast<Job.StateLevel>().ToList();
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

        public string CurrentJobName
        {
            get => TargetJob.Name ?? string.Empty;
            set
            {
                TargetJob.Name = value;
                jobsManager.UpdateJob(TargetJob);
                NotifyPropertyChanged(nameof(CurrentJobName));

            }
        }

        public string CurrentJobDescription
        {
            get => TargetJob.Description ?? string.Empty;
            set
            {
                TargetJob.Description = value;
                jobsManager.UpdateJob(TargetJob);
                NotifyPropertyChanged(nameof(CurrentJobDescription));
            }
        }

        public string CurrentJobStartDate => TargetJob?
            .StartDate.ToString("dd-MM-yyyy") ?? string.Empty;

        public string CurrentJobDeadlineDate
        {
            get => TargetJob?.DeadlineDate?.ToString("dd-MM-yyyy") ?? string.Empty;
            set
            {
                if (TargetJob is null || !DateTime.TryParse(value, out var date))
                {
                    return;
                }

                TargetJob.DeadlineDate = date;
                jobsManager.UpdateJob(TargetJob);
            }
        }


        public TimeSpan CurrentJobEstimatedTime
        {
            get => TargetJob.EstimatedTime ?? TimeSpan.Zero;
            set
            {
                TargetJob.EstimatedTime = value;
                jobsManager.UpdateJob(TargetJob);
                NotifyPropertyChanged(nameof(CurrentJobEstimatedTime));
            }
        }

        public Job.DifficultyLevel CurrentJobDifficulty
        {
            get => TargetJob.Difficulty;
            set
            {
                TargetJob.Difficulty = value;
                jobsManager.UpdateJob(TargetJob);
                NotifyPropertyChanged(nameof(CurrentJobDifficulty));
            }
        }

        public Job.StateLevel CurrentJobState
        {
            get => TargetJob.State;
            set
            {
                TargetJob.State = value;
                jobsManager.UpdateJob(TargetJob);
                NotifyPropertyChanged(nameof(CurrentJobState));
            }
        }

        public JobViewModel(Job job, JobsManager jobsManager)
        {
            TargetJob = job;
            this.jobsManager = jobsManager;
        }
    }
}
