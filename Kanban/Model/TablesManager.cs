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
        private User? user;

        public void SetUser(User? user)
        {
            this.user = user;
        }

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
