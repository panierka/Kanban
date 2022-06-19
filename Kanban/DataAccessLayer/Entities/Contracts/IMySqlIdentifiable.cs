using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Entities.Contracts
{
    internal interface IMySqlIdentifiable
    {
        public int? Id { get; set; }
    }
}
