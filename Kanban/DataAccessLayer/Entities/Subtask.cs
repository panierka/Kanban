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
    public class Subtask : IMySqlCompleteRecord
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public int JobId { get; set; }

        public Subtask(string text, int jobId)
        {
            Text = text;
            JobId = jobId;
        }

        public Subtask(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Text = interpreter.ReadString("text");
            IsDone = interpreter.ReadValue<bool>("is_done");
            JobId = interpreter.ReadValue<int>("master_task_id");
        }

        public string ToInsert()
        {
            return MySqlInsertBuilder.JoinAttributes(Text, IsDone);
        }
    }
}
