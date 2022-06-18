using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.DataAccessLayer.Exceptions
{
    internal class InvalidDatabaseEnum : Exception
    {
        public InvalidDatabaseEnum(Type enumType, int invalidValue) : 
            base(GenerateMessage(enumType, invalidValue))
        { }

        private static string GenerateMessage(Type enumType, int invalidValue)
        {
            var definedEnums = enumType.GetEnumValues();
            var definedEnumMessage = string.Join(", ", definedEnums);

            return $"Provided value for enum cast was equal to {invalidValue}," +
                $"but the {enumType.Name} defines only {definedEnumMessage}";
        }
    }
}
