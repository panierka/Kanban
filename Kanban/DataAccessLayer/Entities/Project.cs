using Kanban.DataAccessLayer.Entities.Contracts;
using Kanban.DataAccessLayer.Repositories;
using Kanban.DataAccessLayer.Wrappers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities
{
    internal record Project : IMySqlCompleteRecord
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? DeadlineDateTime { get; set; }

        public List<Table> Tables { get; set; } = new();

        public Project(string name)
        {
            Name = name;    
        }

        public Project(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Name = interpreter.ReadString("name");
            Description = interpreter.ReadStringNullable("description");
            StartDateTime = interpreter.ReadValue<DateTime>("start_datetime");
            DeadlineDateTime = interpreter.ReadValueNullable<DateTime>("deadline_datetime");

            RefreshTables();
        }

        public void RefreshTables()
        {
            Tables = TablesRepository.GetTablesFromProject(Id!.Value);
        }

        public string ToInsert()
        {
            return MySqlInsertBuilder.JoinAttributes(Name, 
                Description, 
                StartDateTime.ToString(MySqlVariableFormatter.DATE_FORMAT), 
                DeadlineDateTime?.ToString(MySqlVariableFormatter.DATE_FORMAT));
        }
    }
}
