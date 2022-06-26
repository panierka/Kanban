using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.DataAccessLayer.Repositories;
using Kanban.DataAccessLayer.Entities;

namespace Kanban.Model
{
    internal class ProjectsManager
    {
        // private User user;

        public List<Project> GetProjects()
        {
            // filtrowanie względem uprawnień użytkownika
            return ProjectsRepository.GetAllProjects();
        }

        public void CreateProject()
        {
            Project project = new("Nowy projekt")
            {
                StartDateTime = DateTime.Now,                
            };
            ProjectsRepository.InsertProject(project, out _);
        }

        public bool CanUpdateProject(Project project)
        {
            //
            return true;
        }

        public void UpdateProject(Project project)
        {
            ProjectsRepository.UpdateProject(project, out _);
        }

        public void DeleteProject(Project project)
        {
            ProjectsRepository.RemoveProject(project, out _);
        }
    }
}
