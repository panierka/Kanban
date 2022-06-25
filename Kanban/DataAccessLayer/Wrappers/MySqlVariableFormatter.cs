using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Wrappers
{
    internal static class MySqlVariableFormatter
    {
        public static string Format<T>(T variable)
        {
            if (variable is null)
            {
                return "NULL";
            }

            return $"'{variable}'";
        }
    }
}
