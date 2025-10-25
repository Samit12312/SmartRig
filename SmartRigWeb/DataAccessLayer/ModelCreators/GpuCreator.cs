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
                GpuId = Convert.ToInt16(reader["GpuId"]),
                GpuName = Convert.ToString(reader["GpuName"]),
                GpuSize = Convert.ToString(reader["GpuSize"]),
                GpuSpeed = Convert.ToString(reader["GpuSpeed"]),
                GpuPrice = Convert.ToInt16(reader["GpuPrice"]),
                GpuCompanyId = Convert.ToInt16(reader["GpuCompanyId"])
            };
        }
    }
}
