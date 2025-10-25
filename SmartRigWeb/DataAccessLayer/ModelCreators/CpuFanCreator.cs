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
                CpuFanId = Convert.ToInt16(reader["CpuFanId"]),
                CpuFanName = Convert.ToString(reader["CpuFanName"]),
                CpuFanPrice = Convert.ToInt16(reader["CpuFanPrice"]),
                CpuFanCompanyId = Convert.ToInt16(reader["CpuFanCompanyId"])
            };
        }
    }
}
