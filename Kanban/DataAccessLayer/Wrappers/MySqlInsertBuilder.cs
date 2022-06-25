using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Wrappers
{
    internal static class MySqlInsertBuilder
    {
        public static string JoinAttributes(params object?[] attributes)
        {
            return $"({string.Join(", ", attributes.Select(x => MySqlVariableFormatter.Format(x)))})";
        }

        public static string JoinNames(params object?[] names)
        {
            return $"({string.Join(", ", names)})";
        }
    }
}
