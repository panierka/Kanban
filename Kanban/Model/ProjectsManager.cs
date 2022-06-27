using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.DataAccessLayer.Repositories;
using Kanban.DataAccessLayer.Entities;
using System.Windows;

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
                GetAllUserPermissions(user.Id!.Value); 

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

        public bool CanUpdateProject(Project? project)
        {
            if (project is null)
            {
                return false;
            }

            return GetMyPermissions(project)?.Level !=
                UserProjectPermissions.PermissionLevel.USER;
        }

        public void UpdateProject(Project project)
        {
            ProjectsRepository.UpdateProject(project, out _);
        }

        public void DeleteProject(Project project)
        {
            ProjectsRepository.RemoveProject(project, out _);
        }

        public List<UserProjectPermissions> GetAllPermissions(Project project)
        {
            return UserProjectPermissionsRepository.GetAllProjectPermissions(project.Id!.Value);
        }
        
        public UserProjectPermissions? GetMyPermissions(Project project)
        {
            if (user is null)
            {
                return null;
            }

            return GetAllPermissions(project).FirstOrDefault(x => x.UserId == user.Id);
        }

        public bool CanUpdatePermissions(Project? project)
        {
            if (project is null)
            {
                return false;
            }

            return GetMyPermissions(project)?.Level == 
                UserProjectPermissions.PermissionLevel.SUPER_ADMIN;
        }

        public void SetPermissionsToUser(string login, Project project,
            UserProjectPermissions.PermissionLevel level)
        {
            if (!CanUpdatePermissions(project) || project.Id is null)
            {
                return;
            }

            var otherUser = UsersRepository.GetUserFromLogin(login);

            if (otherUser is null)
            {
                MessageBox.Show($"Nie istnieje użytkownik z loginem {login}");
                return;
            }

            var permissions = UserProjectPermissionsRepository
                .GetAllUserPermissions(otherUser.Id!.Value)
                .FirstOrDefault(x => x.ProjectId == project.Id);

            if (permissions is { })
            {
                UserProjectPermissionsRepository.RemoveUserProjectPermissions(permissions, out _);
            }

            permissions = new UserProjectPermissions(otherUser.Id!.Value, project.Id!.Value)
            {
                Level = level
            };

            UserProjectPermissionsRepository.InsertUserProjectPermissions(permissions, out _);
        }

        public void RemovePermissionsFromUser(string login, Project project)
        {
            if (!CanUpdatePermissions(project) || project.Id is null)
            {
                return;
            }

            var otherUser = UsersRepository.GetUserFromLogin(login);

            if (otherUser is null)
            {
                MessageBox.Show($"Nie istnieje użytkownik z loginem {login}");
                return;
            }

            var permissions = UserProjectPermissionsRepository
                .GetAllUserPermissions(otherUser.Id!.Value)
                .FirstOrDefault(x => x.ProjectId == project.Id);

            if (permissions is { })
            {
                UserProjectPermissionsRepository.RemoveUserProjectPermissions(permissions, out _);
            }
        }
    }
}
