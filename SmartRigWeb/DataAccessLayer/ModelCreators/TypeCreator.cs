using System;
using System.Data;
using Models;

namespace SmartRigWeb
{
    public class TypeCreator : IModelCreator<Models.Type>
    {
        public Models.Type CreateModel(IDataReader reader)
        {
            return new Models.Type
            {
                TypeId = Convert.ToInt16(reader["TypeId"]),
                TypeName = Convert.ToString(reader["TypeName"]),
                TypeCode = Convert.ToInt16(reader["TypeCode"])
            };
        }
    }
}
