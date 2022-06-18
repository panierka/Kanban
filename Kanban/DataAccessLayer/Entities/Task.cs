using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;
using Kanban.DataAccessLayer.Wrappers;

namespace Kanban.DataAccessLayer.Entities
{
    internal record Task
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }

        public Task(string name)
        {
            Name = name;
            Difficulty = DifficultyLevel.NOT_SPECIFIED;
            StartDate = DateTime.Now;
        }

        public Task(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Name = interpreter.ReadString("name");
            Description = interpreter.ReadStringNullable("name");
            Difficulty = interpreter.ReadEnum<DifficultyLevel>("difficulty");
            EstimatedTime = interpreter.ReadValueNullable<TimeSpan>("estimated_work_time");
            StartDate = interpreter.ReadValue<DateTime>("start_datetime");
            DeadlineDate = interpreter.ReadValueNullable<DateTime>("deadline_datetime");
        }

        internal enum DifficultyLevel : int
        {
            NOT_SPECIFIED,
            VERY_EASY,
            EASY,
            MEDIUM,
            HARD,
            VERY_HARD
        }
    }
}
