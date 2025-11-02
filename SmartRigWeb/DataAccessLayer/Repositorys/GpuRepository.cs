using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class GpuRepository : Repository, IRepository<Gpu>
    {
        public GpuRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Gpu item)
        {
            string sql = @"INSERT INTO [Gpu] 
                           (GpuName, GpuSize, GpuSpeed, GpuPrice, GpuCompanyId)
                           VALUES (@GpuName, @GpuSize, @GpuSpeed, @GpuPrice, @GpuCompanyId)";

            this.dbContext.AddParameter("@GpuName", item.GpuName);
            this.dbContext.AddParameter("@GpuSize", item.GpuSize);
            this.dbContext.AddParameter("@GpuSpeed", item.GpuSpeed);
            this.dbContext.AddParameter("@GpuPrice", item.GpuPrice.ToString());
            this.dbContext.AddParameter("@GpuCompanyId", item.GpuCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Gpu] WHERE GpuId = @GpuId";
            this.dbContext.AddParameter("@GpuId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Gpu> GetAll()
        {
            List<Gpu> list = new List<Gpu>();
            string sql = @"SELECT * FROM [Gpu]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Gpu gpu = new Gpu
                    {
                        GpuId = Convert.ToInt32(reader["GpuId"]),
                        GpuName = reader["GpuName"].ToString(),
                        GpuSize = reader["GpuSize"].ToString(),
                        GpuSpeed = reader["GpuSpeed"].ToString(),
                        GpuPrice = Convert.ToInt32(reader["GpuPrice"]),
                        GpuCompanyId = Convert.ToInt32(reader["GpuCompanyId"])
                    };
                    list.Add(gpu);
                }
            }
            return list;
        }

        public Gpu GetById(int id)
        {
            string sql = @"SELECT * FROM [Gpu] WHERE GpuId = @GpuId";
            this.dbContext.AddParameter("@GpuId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new Gpu
                {
                    GpuId = Convert.ToInt32(reader["GpuId"]),
                    GpuName = reader["GpuName"].ToString(),
                    GpuSize = reader["GpuSize"].ToString(),
                    GpuSpeed = reader["GpuSpeed"].ToString(),
                    GpuPrice = Convert.ToInt32(reader["GpuPrice"]),
                    GpuCompanyId = Convert.ToInt32(reader["GpuCompanyId"])
                };
            }
        }

        public bool Update(Gpu item)
        {
            string sql = @"UPDATE [Gpu] 
                           SET GpuName = @GpuName, 
                               GpuSize = @GpuSize, 
                               GpuSpeed = @GpuSpeed, 
                               GpuPrice = @GpuPrice, 
                               GpuCompanyId = @GpuCompanyId
                           WHERE GpuId = @GpuId";

            this.dbContext.AddParameter("@GpuName", item.GpuName);
            this.dbContext.AddParameter("@GpuSize", item.GpuSize);
            this.dbContext.AddParameter("@GpuSpeed", item.GpuSpeed);
            this.dbContext.AddParameter("@GpuPrice", item.GpuPrice.ToString());
            this.dbContext.AddParameter("@GpuCompanyId", item.GpuCompanyId.ToString());
            this.dbContext.AddParameter("@GpuId", item.GpuId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
