﻿using System;
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
using System.Windows.Controls;

namespace Kanban.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly UserAccountController userAccountController;
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

        public ObservableCollection<TableViewModel> CurrentProjectTables
        {
            get
            {
                if (CurrentProject is null)
                {
                    return new();
                }

                List<TableViewModel> tableViewModels = new();
                foreach(var table in CurrentProject.Tables)
                {
                    TableViewModel tableViewModel = new(table, tablesManager, jobsManager);
                    tableViewModel.OnChangeStructure += () =>
                    {
                        CurrentProject?.RefreshTables();
                        NotifyPropertyChanged(nameof(CurrentProjectTables));
                    };
                    tableViewModels.Add(tableViewModel);
                }

                return new(tableViewModels);
            }
        }

        public TableViewModel CurrentlySelectedTableViewModel
        {
            set => CurrentTable = value?.TargetTable; 
        }

        public Table? CurrentTable
        {
            get => _currentTable;
            set
            {
                _currentTable = value;
                NotifyPropertyChanged(
                    nameof(CurrentTable),
                    nameof(IsCurrentTableSelected));
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

        public ICommand CreateNewTable => _createNewTable ??= new RelayCommand
            (
                _ =>
                {
                    tablesManager.CreateTable(CurrentProject!.Id!.Value);
                    CurrentProject.RefreshTables();
                    NotifyPropertyChanged(nameof(CurrentProjectTables));
                },
                _ => CurrentProject is { } && CurrentProject.Id is { }
            );

        public ICommand DeleteCurrentTable => _deleteCurrentTable ??= new RelayCommand
            (
                _ =>
                {
                    tablesManager.DeleteTable(CurrentTable!);
                    CurrentTable = null;
                    CurrentProject!.RefreshTables();
                    NotifyPropertyChanged(nameof(CurrentProjectTables));
                },
                _ => IsCurrentTableSelected
            );

        public bool IsCurrentTableSelected => CurrentTable is { };
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
                    nameof(CurrentProjectTables),
                    nameof(CanProjectSettingsBeDisplayed),
                    nameof(ProjectStartDate),
                    nameof(ProjectDeadlineDate),
                    nameof(ProjectPermissions),
                    nameof(IsCurrentProjectEditable));
            }
        }

        public bool CanProjectSettingsBeDisplayed => IsCurrentProjectSelected
            && projectsManager.CanDisplayProjectSettings();
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

        public string? UserLogin { get; set; }
        public string? UserName { get; set; }

        public ICommand TryLogIn => _tryLogIn ??= new RelayCommand
            (
                x =>
                {
                    userAccountController.TryLogin(UserLogin!, (x as PasswordBox)!.Password);
                    NotifyPropertyChanged(
                        nameof(UserAccountInformation),
                        nameof(IsAnyUserLogged));
                },
                x => !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty((x as PasswordBox)!.Password)
            );

        public string? NewUserLogin { get; set; }
        public string? NewUserName { get; set; }

        public ICommand Register => _register ??= new RelayCommand
            (
                x => userAccountController.Register(NewUserName!, NewUserLogin!, (x as PasswordBox)!.Password),
                x =>
                !string.IsNullOrEmpty(NewUserLogin) &&
                !string.IsNullOrEmpty(NewUserName) && !string.IsNullOrEmpty((x as PasswordBox)!.Password)
            );

        public string UserAccountInformation
        {
            get
            {
                if (userAccountController.CurrentlyLoggedUser is null)
                {
                    return "Aktualnie nie jesteś zalogowany.";
                }

                return $"Zalogowano jako {userAccountController.CurrentlyLoggedUser.Name} " +
                    $"({userAccountController.CurrentlyLoggedUser.Login})";
            }
        }

        public bool IsAnyUserLogged => userAccountController.CurrentlyLoggedUser is { };

        public string ProjectStartDate => CurrentProject?
            .StartDateTime.ToString("dd-MM-yyyy") ?? string.Empty;

        public string ProjectDeadlineDate
        {
            get => CurrentProject?.DeadlineDateTime?.ToString("dd-MM-yyyy") ?? string.Empty;
            set
            {
                if (CurrentProject is null || !DateTime.TryParse(value, out var date))
                {
                    return;
                }

                CurrentProject.DeadlineDateTime = date;
                projectsManager.UpdateProject(CurrentProject!);
            }
        }

        public ObservableCollection<UserProjectPermissions> ProjectPermissions
        {
            get
            {
                if (CurrentProject is null)
                {
                    return new();
                }

                return new(projectsManager.GetAllPermissions(CurrentProject));
            }
        }

        public string LoginToGrantPermissionsTo { get; set; } = string.Empty;

        public List<UserProjectPermissions.PermissionLevel> PossiblePermissionLevels 
        {
            get => typeof(UserProjectPermissions.PermissionLevel)
                .GetEnumValues()
                .Cast<UserProjectPermissions.PermissionLevel>()
                .ToList();
        }

        public UserProjectPermissions.PermissionLevel SelectedPermissionLevel { get; set; }

        public ICommand AddUserWithPermissions => _addUserWithPermissions ??= new RelayCommand
            (
                _ =>
                {
                    projectsManager.SetPermissionsToUser(
                        LoginToGrantPermissionsTo,
                        CurrentProject!,
                        SelectedPermissionLevel);
                    NotifyPropertyChanged(nameof(ProjectPermissions));
                },
                _ => IsCurrentProjectSelected && projectsManager.CanUpdatePermissions(CurrentProject!)
            );

        public ICommand DeleteUserWithPermissions => _deleteUserWithPermissions ??= new RelayCommand
            (
                _ => 
                {
                    projectsManager.RemovePermissionsFromUser(
                        LoginToGrantPermissionsTo,
                        CurrentProject!);
                    NotifyPropertyChanged(nameof(ProjectPermissions));
                },
                _ => IsCurrentProjectSelected && projectsManager.CanUpdatePermissions(CurrentProject!)
            );

        public string CurrentUserName { get; set; }

        public MainViewModel()
        {
            userAccountController = new(new SHA256Encryptor());
            projectsManager = new();
            tablesManager = new();
            jobsManager = new();

            userAccountController.OnUserChanged += UpdateUser;
            userAccountController.OnUserChanged += _ =>
            {
                CurrentProject = Projects.FirstOrDefault();
                NotifyPropertyChanged(
                    nameof(CanProjectSettingsBeDisplayed),
                    nameof(IsCurrentProjectEditable));

                if (userAccountController.CurrentlyLoggedUser is { })
                {
                    CurrentUserName = userAccountController.CurrentlyLoggedUser.Name;
                    NotifyPropertyChanged(nameof(CurrentUserName));
                }           
            };

            RefreshProjects();

            void UpdateUser(User? user)
            {
                projectsManager.SetUser(user);
                RefreshProjects();
                tablesManager.SetUser(user);
                jobsManager.SetUser(user);
            };
        }

        #region Backing fields
        private ObservableCollection<Project> _projects = new();
        private ICommand? _createNewProject;
        private ICommand? _deleteCurrentProject;
        private Project? _currentProject;
        private int _selectedTabIndex;
        private ICommand? _createNewTable;
        private ICommand? _deleteCurrentTable;
        private Table? _currentTable;
        private ICommand? _tryLogIn;
        private ICommand? _register;
        private ICommand? _addUserWithPermissions;
        private ICommand? _deleteUserWithPermissions;
        #endregion
    }
}
