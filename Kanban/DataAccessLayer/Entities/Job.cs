using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;
using Kanban.DataAccessLayer.Wrappers;
using Kanban.DataAccessLayer.Repositories;
using Kanban.DataAccessLayer.Entities.Contracts;

namespace Kanban.DataAccessLayer.Entities
{
    public class Job: IMySqlCompleteRecord
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.NOT_SPECIFIED;
        public StateLevel State { get; set; } = StateLevel.NOT_SPECIFIED;
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? DeadlineDate { get; set; }

        public int AuthorId { get; set; }
        public int TableId { get; set; }
        public List<Subtask> Subtasks { get; set; } = new();

        public Job(string name, int tableId, int authorId)
        {
            Name = name;
            TableId = tableId;
            AuthorId = authorId;
        }

        public Job(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Name = interpreter.ReadString("name");
            Description = interpreter.ReadStringNullable("description");
            Difficulty = InterpretDifficultyLevel(interpreter.ReadString("difficulty"));
            State = InterpretStateLevel(interpreter.ReadString("state"));
            EstimatedTime = interpreter.ReadValueNullable<TimeSpan>("estimated_work_time");
            StartDate = interpreter.ReadValue<DateTime>("start_datetime");
            DeadlineDate = interpreter.ReadValueNullable<DateTime>("deadline_datetime");
            AuthorId = interpreter.ReadValue<int>("author_id");
            TableId = interpreter.ReadValue<int>("master_table_id");

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

        private string ReinterpretDifficultyLevel(DifficultyLevel value)
        {
            return value switch
            {
                DifficultyLevel.VERY_EASY => "very easy",
                DifficultyLevel.EASY => "easy",
                DifficultyLevel.MEDIUM => "medium",
                DifficultyLevel.HARD => "hard",
                DifficultyLevel.VERY_HARD => "very hard",
                DifficultyLevel.NOT_SPECIFIED => "not specified",
                _ => "very easy"
            };
        }

        private StateLevel InterpretStateLevel(string raw)
        {
            return raw switch
            {
                "awaiting" => StateLevel.AWAITING,
                "worked on" => StateLevel.WORKED_ON,
                "put off" => StateLevel.PUT_OFF,
                "waiting for review" => StateLevel.WAITING_FOR_REVIEW,
                "completed" => StateLevel.COMPLETED,
                _ => StateLevel.NOT_SPECIFIED
            };
        }

        private string ReinterpretStateLevel(StateLevel value)
        {
            return value switch
            {
                StateLevel.AWAITING => "awaiting",
                StateLevel.WORKED_ON => "worked on",
                StateLevel.PUT_OFF => "put off",
                StateLevel.WAITING_FOR_REVIEW => "waiting for review",
                StateLevel.COMPLETED => "completed",
                StateLevel.NOT_SPECIFIED => "not specified"
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

        public enum StateLevel: int
        {
            NOT_SPECIFIED,
            AWAITING,
            WORKED_ON,
            PUT_OFF,
            WAITING_FOR_REVIEW,
            COMPLETED
        }
        public string ToInsert()
        {
            return MySqlInsertBuilder.JoinAttributes(
               Name,
               Description,
               ReinterpretStateLevel(State),
               ReinterpretDifficultyLevel(Difficulty),
               EstimatedTime,
               StartDate.ToString(MySqlVariableFormatter.DATE_FORMAT),
               DeadlineDate,
               AuthorId,
               TableId
               );
        }

    }
}
