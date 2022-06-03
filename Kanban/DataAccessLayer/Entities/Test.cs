using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities
{
    public class Test
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Score { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Score} pts";
        }
    }
}
