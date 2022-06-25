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
    internal class MainViewModel : BaseViewModel
    {
        private readonly ProjectsManager projectsManager;
        public bool trying = false;
        public bool std = false;

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public ICommand CreateNewProject => _createNewProject ??= new RelayCommand
            (
                _ => 
                {
                    trying = true;
                    projectsManager.CreateProject(new($"student {DateTime.Now.Minute}"));
                    Projects = new(projectsManager.GetProjects());
                }
            );

        public ICommand EditCurrentProject => _editCurrentProject ??= new RelayCommand
            (
                _ => throw new NotImplementedException(),
                _ => CurrentProject is { }
            );

        public ICommand DeleteCurrentProject => _deleteCurrentProject ??= new RelayCommand
            (
                _ => throw new NotImplementedException(),
                _ => CurrentProject is { }
            );

        public Project? CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                std = true;
                Console.WriteLine("std");
                NotifyPropertyChanged(
                    nameof(CurrentProject), 
                    nameof(CurrentProjectName),
                    nameof(CurrentProjectDescription),
                    nameof(IsCurrentProjectSelected));
            }
        }

        public bool IsCurrentProjectSelected => CurrentProject is { };

        public bool Std
        {
            get => std;
            set
            {
                std = value;
                NotifyPropertyChanged(nameof(Std));
            }
        }
        public string CurrentProjectName => CurrentProject?.Name ?? string.Empty;
        public string CurrentProjectDescription => CurrentProject?.Description ?? string.Empty;

        public MainViewModel()
        {
            projectsManager = new();
            Projects = new(projectsManager.GetProjects());
        }

        #region Backing fields
        private ObservableCollection<Project> _projects = new();
        private ICommand? _createNewProject;
        private ICommand? _editCurrentProject;
        private ICommand? _deleteCurrentProject;
        private Project? _currentProject;
        #endregion
    }
}
