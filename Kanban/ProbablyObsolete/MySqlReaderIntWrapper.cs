using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Kanban.DataAccessLayer.Exceptions;

namespace Kanban.Obsolete
{
    internal static class MySqlReaderIntWrapper
    {
        public static int Read(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<int>.Read(reader, column);
        }

        public static int? ReadNullable(MySqlDataReader reader, string column)
        {
            return MySqlReaderStructWrapper<int>.ReadNullable(reader, column);
        }
    }
}
