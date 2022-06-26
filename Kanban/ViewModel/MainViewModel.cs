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
        private readonly TablesManager tablesManager;
        private readonly JobsManager jobsManager;

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                int? lastId = CurrentProject?.Id;
                _projects = value;
                NotifyPropertyChanged(nameof(Projects));
                CurrentProject = Projects.Where(
                    x => x.Id is not null && 
                    x.Id == lastId).FirstOrDefault();
            }
        }

        public ObservableCollection<Table> CurrentProjectTables
        {
            get
            {
                if (CurrentProject is null)
                {
                    return new();
                }

                return new(CurrentProject.Tables);
            }
        }

        public ICommand CreateNewProject => _createNewProject ??= new RelayCommand
            (
                _ => 
                {
                    var newProject = projectsManager.CreateProject();
                    RefreshProjects();
                    CurrentProject = newProject;
                    SelectedTabIndex = 2;
                }
            );

        public ICommand DeleteCurrentProject => _deleteCurrentProject ??= new RelayCommand
            (
                _ =>
                {
                    SelectedTabIndex = 1;
                    projectsManager.DeleteProject(CurrentProject!);
                    CurrentProject = null;
                    RefreshProjects();
                },
                _ => CurrentProject is { }
            );

        public ICommand CreateTable => _createTable ??= new RelayCommand
            (
                _ =>
                {
                    tablesManager.CreateTable(CurrentProject!.Id!.Value);
                    CurrentProject.RefreshTables();
                    NotifyPropertyChanged(nameof(CurrentProjectTables));
                },
                _ => CurrentProject is { } && CurrentProject.Id is { }
            );

        public Project? CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                NotifyPropertyChanged(
                    nameof(CurrentProject), 
                    nameof(CurrentProjectName),
                    nameof(CurrentProjectDescription),
                    nameof(IsCurrentProjectSelected),
                    nameof(CurrentProjectTables));
            }
        }

        public bool IsCurrentProjectSelected => CurrentProject is { };
        public bool IsCurrentProjectEditable => projectsManager.CanUpdateProject(CurrentProject!);
        public string CurrentProjectName
        {
            get => CurrentProject?.Name ?? string.Empty;
            set
            {
                CurrentProject!.Name = value;
                projectsManager.UpdateProject(CurrentProject!);
                NotifyPropertyChanged(nameof(CurrentProjectName));
                RefreshProjects();
            }
        }
            
        public string CurrentProjectDescription
        {
            get => CurrentProject?.Description ?? string.Empty;
            set
            {
                CurrentProject!.Description = value;
                projectsManager.UpdateProject(CurrentProject!);
                NotifyPropertyChanged(nameof(CurrentProjectDescription));
                RefreshProjects();
            }
        }
        
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                NotifyPropertyChanged(nameof(SelectedTabIndex));
            }
        }

        private void RefreshProjects()
        {
            Projects = new(projectsManager.GetProjects());
            NotifyPropertyChanged(nameof(Projects));
        }

        public ICommand CreateNewJob => _createNewJob ??= new RelayCommand
            (
                _ =>
                {
                    jobsManager.CreateJob(CurrentProject!.Id!.Value);
                    CurrentProject.RefreshTables();
                    NotifyPropertyChanged(nameof(CurrentProjectTables));
                },
                _ => CurrentProject is { } && CurrentProject.Id is { }
            );

        public MainViewModel()
        {
            projectsManager = new();
            tablesManager = new();
            jobsManager = new();
            RefreshProjects();
        }

        #region Backing fields
        private ObservableCollection<Project> _projects = new();
        private ICommand? _createNewProject;
        private ICommand? _deleteCurrentProject;
        private Project? _currentProject;
        private int _selectedTabIndex;
        private ICommand? _createTable;
        private ICommand? _deleteTable;
        private ICommand? _createNewJob;
        #endregion
    }
}
