using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities.Contracts
{
    internal interface IMySqlInsertable
    {
        public string ToInsert();
    }
}
