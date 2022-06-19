using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Exceptions
{
    internal class DatabaseNoRecordException : Exception
    {
        public DatabaseNoRecordException(string condition, string table) : 
            base($"No record in table '{table}' fullfils the condition {condition}") { }
    }
}
