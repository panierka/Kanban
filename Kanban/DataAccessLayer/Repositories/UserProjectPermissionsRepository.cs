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

        public static List<UserProjectPermissions> GetAllUserProjectPermissions(int userId)
        {
            return MySqlQueriesWrapper.SelectAllOnCondition(TABLE_NAME,
                $"{TABLE_NAME}.user_id = {userId}", x => new UserProjectPermissions(x));
        }

        public static void InsertUserProjectPermissions(UserProjectPermissions project, 
            out bool successful)
        {
            string attributes = MySqlInsertBuilder.JoinNames("user_id", "project_id",
                "assigned_since", "permissions");
            MySqlQueriesWrapper.Insert(project, attributes, TABLE_NAME, out successful);
        }
    }
}
