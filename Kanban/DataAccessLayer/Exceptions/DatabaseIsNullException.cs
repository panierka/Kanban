using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Exceptions
{
    internal class DatabaseIsNullException : Exception
    {
        public DatabaseIsNullException(string columnName)
            : base($"Column {columnName} contains NULL value and that invalidates application logic") { }
    }
}
