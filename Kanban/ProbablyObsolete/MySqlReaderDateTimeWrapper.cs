using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderDateTimeWrapper
    {
        public static DateTime Read(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<DateTime>.Read(reader, column);
        }

        public static DateTime? ReadNullable(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<DateTime>.ReadNullable(reader, column);
        }
    }
}
