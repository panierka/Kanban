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

        public void CreateProject(Project project)
        {
            ProjectsRepository.InsertProject(project, out bool _);
        }
    }
}
