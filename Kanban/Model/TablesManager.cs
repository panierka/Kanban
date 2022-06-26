using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    internal class TablesManager
    {
        public Table CreateTable(int projectId)
        {
            Table table = new("Nowa tablica", projectId)
            {
                StartDateTime = DateTime.Now
            };

            TablesRepository.InsertTable(table, out _);
            return table;
        }
    }
}
