using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class StorageCreator : IModelCreator<Storage>
    {
        public Storage CreateModel(IDataReader reader)
        {
            return new Storage
            {
                StorageId = Convert.ToInt32(reader["StorageId"]),
                StorageName = Convert.ToString(reader["StorageName"]),
                StorageSize = Convert.ToString(reader["StorageSize"]),
                StorageSpeed = Convert.ToString(reader["StorageSpeed"]),
                StorageType = Convert.ToInt32(reader["StorageType"]),
                StoragePrice = Convert.ToInt32(reader["StoragePrice"]),
                StorageCompanyId = Convert.ToInt32(reader["StorageCompanyId"])
            };
        }
    }
}
