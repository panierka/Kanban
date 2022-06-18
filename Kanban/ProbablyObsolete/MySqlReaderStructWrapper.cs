using Kanban.DataAccessLayer.Exceptions;
using MySql.Data.MySqlClient;
using System;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderStructWrapper<T> where T : struct
    {
        public static T Read(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                throw new DatabaseIsNullException(column);
            }

            return (T)reader[column];
        }

        public static T? ReadNullable(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }

            return (T)reader[column];
        }
    }
}
