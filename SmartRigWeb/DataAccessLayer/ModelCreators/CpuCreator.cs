using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class CpuCreator : IModelCreator<Cpu>
    {
        public Cpu CreateModel(IDataReader reader)
        {
            return new Cpu
            {
                CpuId = Convert.ToInt32(reader["CpuId"]),
                CpuName = Convert.ToString(reader["CpuName"]),
                CpuPrice = Convert.ToInt32(reader["CpuPrice"]),
                CpuCompanyId = Convert.ToInt32(reader["CpuCompanyId"])
            };
        }
    }
}
