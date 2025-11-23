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
                ComputerId = Convert.ToInt16(reader["ComputerId"]),
                ComputerName = Convert.ToString(reader["ComputerName"]),
                ComputerTypeId = Convert.ToInt16(reader["ComputerTypeId"]),
                CompanyId = Convert.ToInt16(reader["CompanyId"]),
                StorageId = Convert.ToInt16(reader["StorageId"]),
                RamId = Convert.ToInt16(reader["RamId"]),
                CpuId = Convert.ToInt16(reader["CpuId"]),
                GpuId = Convert.ToInt16(reader["GpuId"]),
                Price = Convert.ToInt16(reader["Price"]),
                OperatingSystemId = Convert.ToInt16(reader["OperatingSystemId"]),
                CaseId = Convert.ToInt16(reader["CaseId"]),
                PowerSupplyId = Convert.ToInt16(reader["PowerSupplyId"]),
                CpuFanId = Convert.ToInt16(reader["CpuFanId"]),
                MotherBoardId = Convert.ToInt16(reader["MotherBoardId"]),
                ComputerPicture = Convert.ToString(reader["ComputerPicture"])
            };
        }
    }
}
