using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class OperatingSystemCreator : IModelCreator<Models.OperatingSystem>
    {
        public Models.OperatingSystem CreateModel(IDataReader reader)
        {
            return new Models.OperatingSystem
            {
                OperatingSystemId = Convert.ToInt32(reader["OperatingSystemId"]),
                OperatingSystemName = Convert.ToString(reader["OperatingSystemName"]),
                OperatingSystemPrice = Convert.ToInt32(reader["OperatingSystemPrice"]),
                OperatingSystemCompanyId = Convert.ToInt32(reader["OperatingSystemCompany"])
            };
        }
    }
}