using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class UserProjectPermissionsRepository
    {
        private const string TABLE_NAME = "users_projects";

        public static List<UserProjectPermissions> GetAllUserPermissions(int userId)
        {
            return MySqlQueriesWrapper.SelectAllOnCondition(TABLE_NAME,
                $"{TABLE_NAME}.user_id = {userId}", x => new UserProjectPermissions(x));
        }

        public static List<UserProjectPermissions> GetAllProjectPermissions(int projectId)
        {
            return MySqlQueriesWrapper.SelectAllOnCondition(TABLE_NAME,
                $"{TABLE_NAME}.project_id = {projectId}", x => new UserProjectPermissions(x));
        }

        public static void InsertUserProjectPermissions(UserProjectPermissions perm, 
            out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("user_id", "project_id",
                "assigned_since", "permissions");
            MySqlQueriesWrapper.Insert(perm, attributes, TABLE_NAME, out successful);
        }

        public static void RemoveUserProjectPermissions(UserProjectPermissions perm, 
            out bool successful)
        {
            var condition = $"where id = {perm.Id}";
            MySqlQueriesWrapper.Remove(condition, TABLE_NAME, out successful);
        }
    }
}
