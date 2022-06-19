using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Repositories
{
    internal static class TablesRepository
    {
        public static List<Table> GetAllTables()
        {
            return MySqlQueriesWrapper.SelectAll("tables", x => new Table(x));
        }
    }
}

