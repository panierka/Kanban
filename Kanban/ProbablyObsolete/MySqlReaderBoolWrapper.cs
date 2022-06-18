using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderBoolWrapper
    {
        public static bool Read(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<bool>.Read(reader, column);
        }

        public static bool? ReadNullable(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<bool>.ReadNullable(reader, column);
        }
    }
}
