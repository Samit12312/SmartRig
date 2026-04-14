using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class GpuCreator : IModelCreator<Gpu>
    {
        public Gpu CreateModel(IDataReader reader)
        {
            return new Gpu
            {
                GpuId = Convert.ToInt32(reader["GpuId"]),
                GpuName = Convert.ToString(reader["GpuName"]),
                GpuSize = Convert.ToString(reader["GpuSize"]),
                GpuSpeed = Convert.ToString(reader["GpuSpeed"]),
                GpuPrice = Convert.ToInt32(reader["GpuPrice"]),
                GpuCompanyId = Convert.ToInt32(reader["GpuCompanyId"])
            };
        }
    }
}
