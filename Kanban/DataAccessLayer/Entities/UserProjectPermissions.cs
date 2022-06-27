using Kanban.DataAccessLayer.Entities.Contracts;
using Kanban.DataAccessLayer.Wrappers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities
{
    internal class UserProjectPermissions : IMySqlCompleteRecord
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateTime AssignedSince { get; set; }
        public PermissionLevel Level { get; set; } = PermissionLevel.USER;

        public string LevelAsString => Level switch
        {
            PermissionLevel.USER => "user",
            PermissionLevel.ADMIN => "admin",
            PermissionLevel.SUPER_ADMIN => "super admin",
            _ => throw new NotImplementedException(),
        };

        public UserProjectPermissions(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public UserProjectPermissions(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            UserId = interpreter.ReadValue<int>("user_id");
            ProjectId = interpreter.ReadValue<int>("project_id");
            AssignedSince = interpreter.ReadValue<DateTime>("assigned_since");
            Level = InterpretPermissionLevel(interpreter.ReadString("permissions"));
        }

        private static PermissionLevel InterpretPermissionLevel(string raw)
        {
            return raw switch
            {
                "user" => PermissionLevel.USER,
                "admin" => PermissionLevel.ADMIN,
                "super admin" => PermissionLevel.SUPER_ADMIN,
                _ => PermissionLevel.USER
            };
        }

        public string ToInsert()
        {
            return MySqlInsertBuilder.JoinAttributes(
                    UserId,
                    ProjectId,
                    AssignedSince.ToString(MySqlVariableFormatter.DATE_FORMAT),
                    LevelAsString
                );
        }

        public enum PermissionLevel
        {
            USER,
            ADMIN,
            SUPER_ADMIN
        }
    }
}
