using Kanban.DataAccessLayer.Entities.Contracts;
using Kanban.DataAccessLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities
{
    internal record User : IMySqlInsertable
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public User(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }

        public string ToInsert()
        {
            return MySqlInsertBuilder.JoinAttributes(Name, Login, Password);
        }
    }
}
