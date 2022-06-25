using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.DataAccessLayer.Entities.Contracts;

namespace Kanban.DataAccessLayer.Exceptions
{
    internal class NoIdException : Exception
    {
        public NoIdException(IMySqlIdentifiable identifiable) : base($"{identifiable} has no Id!")
        {

        }
    }
}
