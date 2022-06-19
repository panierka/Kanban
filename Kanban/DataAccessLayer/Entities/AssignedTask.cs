using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;
using Kanban.DataAccessLayer.Wrappers;
using Kanban.DataAccessLayer.Repositories;

namespace Kanban.DataAccessLayer.Entities
{
    public record AssignedTask
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.NOT_SPECIFIED;
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? DeadlineDate { get; set; }

        public List<Subtask> Subtasks { get; set; } = new();

        public AssignedTask(string name)
        {
            Name = name;
        }

        public AssignedTask(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Name = interpreter.ReadString("name");
            Description = interpreter.ReadStringNullable("name");
            Difficulty = InterpretDifficultyLevel(interpreter.ReadString("difficulty"));
            EstimatedTime = interpreter.ReadValueNullable<TimeSpan>("estimated_work_time");
            StartDate = interpreter.ReadValue<DateTime>("start_datetime");
            DeadlineDate = interpreter.ReadValueNullable<DateTime>("deadline_datetime");

            Subtasks = SubtaskRepository.GetSubtasksFromTask(Id.Value);
        }

        private DifficultyLevel InterpretDifficultyLevel(string raw)
        {
            return raw switch
            {
                "very easy" => DifficultyLevel.VERY_EASY,
                "easy" => DifficultyLevel.EASY,
                "medium" => DifficultyLevel.MEDIUM,
                "hard" => DifficultyLevel.HARD,
                "very hard" => DifficultyLevel.VERY_HARD,
                _ => DifficultyLevel.NOT_SPECIFIED
            };
        }

        public enum DifficultyLevel : int
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
