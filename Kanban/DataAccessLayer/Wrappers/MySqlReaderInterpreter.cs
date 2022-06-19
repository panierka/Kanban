using Kanban.DataAccessLayer.Exceptions;
using Kanban.Obsolete;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Wrappers
{
    internal class MySqlReaderInterpreter
    {
        private readonly MySqlDataReader reader;

        public MySqlReaderInterpreter(MySqlDataReader reader)
        {
            this.reader = reader;
        }

        public string ReadString(string column)
        {
            if (reader[column] == DBNull.Value)
            {
                throw new DatabaseIsNullException(column);
            }

            return reader[column].ToString()!;
        }

        public string? ReadStringNullable(string column)
        {
            return reader[column].ToString();
        }

        public T ReadValue<T>(string column) where T : struct
        {
            if (reader[column] == DBNull.Value)
            {
                throw new DatabaseIsNullException(column);
            }

            return (T)reader[column];
        }

        public T? ReadValueNullable<T>(string column) where T : struct
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }

            return (T)reader[column];
        }
    }
}
