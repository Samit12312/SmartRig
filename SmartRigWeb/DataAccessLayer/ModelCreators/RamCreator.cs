using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class RamCreator : IModelCreator<Ram>
    {
        public Ram CreateModel(IDataReader reader)
        {
            return new Ram
            {
                RamId = Convert.ToInt32(reader["RamId"]),
                RamName = Convert.ToString(reader["RamName"]),
                RamSize = Convert.ToString(reader["RamSize"]),
                RamTypeId = Convert.ToInt32(reader["RamTypeId"]),
                RamSpeed = Convert.ToString(reader["RamSpeed"]),
                RamPrice = Convert.ToInt32(reader["RamPrice"]),
                RamCompanyId = Convert.ToInt32(reader["RamCompanyId"])
            };
        }
    }
}
