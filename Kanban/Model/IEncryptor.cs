using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    internal interface IEncryptor
    {
        public string Encrypt(string text);
    }
}
