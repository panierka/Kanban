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
        private User? user;

        public void SetUser(User? user)
        {
            this.user = user;   
        }

        public List<Project> GetProjects()
        {
            if (user is null)
            {
                return new();
            }

            var permissions = UserProjectPermissionsRepository.
                GetAllUserProjectPermissions(user.Id!.Value); 

            return ProjectsRepository.GetAllProjects()
                .Where(x => permissions.Select(p => p.ProjectId)
                .ToList().Contains(x.Id!.Value)).ToList();
        }

        public Project CreateProject()
        {
            Project project = new("Nowy projekt")
            {
                StartDateTime = DateTime.Now,                
            };
            ProjectsRepository.InsertProject(project, out _);
            UserProjectPermissions perm = new(user!.Id!.Value, project!.Id!.Value)
            {
                AssignedSince = DateTime.Now,
                Level = UserProjectPermissions.PermissionLevel.SUPER_ADMIN
            };
            UserProjectPermissionsRepository.InsertUserProjectPermissions(perm, out _);

            return project;
        }

        public bool CanDisplayProjectSettings()
        {
            return user is { };
        }

        public bool CanUpdateProject(Project project)
        {
            if (user is null)
            {
                return false;
            }

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
