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

        public T ReadEnum<T>(string column) where T : struct, Enum
        {
            if (reader[column] == DBNull.Value)
            {
                throw new DatabaseIsNullException(column);
            }

            return InterpretEnum<T>(column);
        }

        public T? ReadEnumNullable<T>(string column) where T : struct, Enum
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }

            return InterpretEnum<T>(column);
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

        private T InterpretEnum<T>(string column) where T : Enum
        {
            return (T)(object)0;
            var s = reader[column].ToString()!;
            var difficultyRawIntValue = int.Parse(s);
            if (!Enum.IsDefined(typeof(T), difficultyRawIntValue))
            {
                throw new InvalidDatabaseEnum(typeof(T), difficultyRawIntValue);
            }

            return (T)(object)difficultyRawIntValue;
        }
    }
}
