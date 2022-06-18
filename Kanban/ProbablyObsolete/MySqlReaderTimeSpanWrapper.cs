using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderTimeSpanWrapper
    {
        public static TimeSpan Read(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<TimeSpan>.Read(reader, column);
        }

        public static TimeSpan? ReadNullable(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<TimeSpan>.ReadNullable(reader, column);
        }
    }
}
