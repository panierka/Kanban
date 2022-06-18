using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderStringWrapper
    {
        public static string Read(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                throw new DatabaseIsNullException(column);
            }

            return reader[column].ToString()!;
        }

        public static string? ReadNullable(MySqlDataReader reader, string column)
        {
            return reader[column].ToString();
        }
    }
}
