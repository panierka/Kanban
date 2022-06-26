using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Wrappers
{
    internal static class MySqlVariableFormatter
    {
        public readonly static string DATE_FORMAT = "yyyy-MM-dd hh:mm:ss";

        public static string Format<T>(T variable)
        {
            if (variable is null)
            {
                return "NULL";
            }

            if (variable is int i)
            {
                return i.ToString();
            }

            return $"'{variable}'";
        }
    }
}
