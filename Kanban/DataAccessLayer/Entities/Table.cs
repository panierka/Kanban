﻿using Kanban.DataAccessLayer.Wrappers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities
{
    internal record Table
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }

        public Table(string name)
        {
            Name = name;
        }

        public Table(MySqlDataReader reader)
        {
            var interpreter = new MySqlReaderInterpreter(reader);

            Id = interpreter.ReadValue<int>("id");
            Name = interpreter.ReadString("name");
            Description = interpreter.ReadStringNullable("description");
            StartDateTime = interpreter.ReadValue<DateTime>("start_datetime");
        }
    }
}