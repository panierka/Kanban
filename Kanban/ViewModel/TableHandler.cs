using Kanban.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.ViewModel
{
    internal class TableHandler
    {
        public TableViewModel ViewModel { get; set; }

        public Table Table { get; set; } 

        public TableHandler(Table table)
        {
            Table = table;
            ViewModel = new();
        }
    }
}
