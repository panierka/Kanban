using Kanban.DataAccessLayer.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderEnumWrapper<T> where T : struct, Enum
    {
        public static T Read(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                throw new DatabaseIsNullException(column);
            }

            return InterpretEnum(reader, column);
        }

        public static T? ReadNullable(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }

            return InterpretEnum(reader, column);
        }

        private static T InterpretEnum(MySqlDataReader reader, string column)
        {
            var difficultyRawIntValue = MySqlReaderIntWrapper.Read(reader, column);
            if (!Enum.IsDefined(typeof(T), difficultyRawIntValue))
            {
                throw new InvalidDatabaseEnum(typeof(T), difficultyRawIntValue);
            }

            return (T)(object)difficultyRawIntValue;
        }
    }
}
