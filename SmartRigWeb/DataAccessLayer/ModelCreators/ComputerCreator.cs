using System;
using System.Data;
using Models;

namespace SmartRigWeb
{
    public class ComputerCreator : IModelCreator<Computer>
    {
        public Computer CreateModel(IDataReader reader)
        {
            return new Computer
            {
                ComputerId = Convert.ToInt32(reader["ComputerId"]),
                ComputerName = Convert.ToString(reader["ComputerName"]),
                ComputerTypeId = Convert.ToInt32(reader["ComputerTypeId"]),
                CompanyId = Convert.ToInt32(reader["CompanyId"]),
                StorageId = Convert.ToInt32(reader["StorageId"]),
                RamId = Convert.ToInt32(reader["RamId"]),
                CpuId = Convert.ToInt32(reader["CpuId"]),
                GpuId = Convert.ToInt32(reader["GpuId"]),
                Price = Convert.ToInt32(reader["Price"]),
                OperatingSystemId = Convert.ToInt32(reader["OperatingSystemId"]),
                CaseId = Convert.ToInt32(reader["CaseId"]),
                PowerSupplyId = Convert.ToInt32(reader["PowerSupplyId"]),
                CpuFanId = Convert.ToInt32(reader["CpuFanId"]),
                MotherBoardId = Convert.ToInt32(reader["MotherBoardId"]),
                ComputerPicture = Convert.ToString(reader["ComputerPicture"])
            };
        }
    }
}
