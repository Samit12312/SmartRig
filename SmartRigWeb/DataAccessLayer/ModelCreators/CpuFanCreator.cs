using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class CpuFanCreator : IModelCreator<CpuFan>
    {
        public CpuFan CreateModel(IDataReader reader)
        {
            return new CpuFan
            {
                CpuFanId = Convert.ToInt32(reader["CpuFanId"]),
                CpuFanName = Convert.ToString(reader["CpuFanName"]),
                CpuFanPrice = Convert.ToInt32(reader["CpuFanPrice"]),
                CpuFanCompanyId = Convert.ToInt32(reader["CpuFanCompanyId"])
            };
        }
    }
}
