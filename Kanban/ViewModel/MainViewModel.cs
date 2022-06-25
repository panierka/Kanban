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
                    projectsManager.CreateProject(new($"student {DateTime.Now.Minute}"));
                    Projects = new(projectsManager.GetProjects());
                }
            );

        public MainViewModel()
        {
            projectsManager = new();
            Projects = new(projectsManager.GetProjects());
        }

        #region Backing fields
        private ObservableCollection<Project> _projects = new();
        private ICommand? _createNewProject;
        #endregion
    }
}
