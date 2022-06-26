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
    public class Table : IMySqlCompleteRecord
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }

        public List<Job> Jobs { get; set; } = new();
        public int? ProjectId { get; set; }

        public Table(string name, int projectId)
        {
            Name = name;
            ProjectId = projectId;
        }

        public Table(MySqlDataReader reader, int projectId)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Name = interpreter.ReadString("name");
            Description = interpreter.ReadStringNullable("description");
            StartDateTime = interpreter.ReadValue<DateTime>("start_datetime");

            ProjectId = projectId;
            RefreshJobs();
        }

        public string ToInsert()
        {
            return MySqlInsertBuilder.JoinAttributes(
               Name,
               Description,
               StartDateTime.ToString(MySqlVariableFormatter.DATE_FORMAT),
               ProjectId);
        }
        public void RefreshJobs()
        {
            Jobs = JobsRepository.GetJobsFromTable(Id!.Value);
        }
    }
}
