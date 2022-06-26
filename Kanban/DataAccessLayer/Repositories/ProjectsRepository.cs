using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class ProjectsRepository
    {
        private const string TABLE_NAME = "projects";

        public static List<Project> GetAllProjects()
        {
            return MySqlQueriesWrapper.SelectAll(TABLE_NAME, x => new Project(x));
        }
        
        public static void InsertProject(Project project, out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("name", "description",
                "start_datetime", "deadline_datetime");
            MySqlQueriesWrapper.Insert(project, attributes, TABLE_NAME, out successful);
        }

        public static void UpdateProject(Project project, out bool successful)
        {
            string dateFormat = MySqlVariableFormatter.DATE_FORMAT;
            string attributeUpdates = $"name = {MySqlVariableFormatter.Format(project.Name)}, " +
                $"description = {MySqlVariableFormatter.Format(project.Description)}, " +
                $"start_datetime = {MySqlVariableFormatter.Format(project.StartDateTime.ToString(dateFormat))}, " +
                $"deadline_datetime = {MySqlVariableFormatter.Format(project.DeadlineDateTime?.ToString(dateFormat))}";
            MySqlQueriesWrapper.Update(project, attributeUpdates, TABLE_NAME, out successful);
        }

        public static void RemoveProject(Project project, out bool successful)
        {
            var condition = $"where id = {project.Id}";
            MySqlQueriesWrapper.Remove(condition, TABLE_NAME, out successful);
        }
    }
}
