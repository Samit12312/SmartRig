using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CpuRepository : Repository, IRepository<Cpu>
    {
        public CpuRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Cpu item)
        {
            string sql = @"INSERT INTO [Cpu] 
                           (CpuName, CpuPrice, CpuCompanyId)
                           VALUES (@CpuName, @CpuPrice, @CpuCompanyId)";

            this.dbContext.AddParameter("@CpuName", item.CpuName);
            this.dbContext.AddParameter("@CpuPrice", item.CpuPrice.ToString());
            this.dbContext.AddParameter("@CpuCompanyId", item.CpuCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Cpu] WHERE CpuId = @CpuId";
            this.dbContext.AddParameter("@CpuId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Cpu> GetAll()
        {
            List<Cpu> list = new List<Cpu>();
            string sql = @"SELECT * FROM [Cpu]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Cpu cpu = new Cpu
                    {
                        CpuId = Convert.ToInt32(reader["CpuId"]),
                        CpuName = reader["CpuName"].ToString(),
                        CpuPrice = Convert.ToInt32(reader["CpuPrice"]),
                        CpuCompanyId = Convert.ToInt32(reader["CpuCompanyId"])
                    };
                    list.Add(cpu);
                }
            }
            return list;
        }

        public Cpu GetById(int id)
        {
            string sql = @"SELECT * FROM [Cpu] WHERE CpuId = @CpuId";
            this.dbContext.AddParameter("@CpuId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new Cpu
                {
                    CpuId = Convert.ToInt32(reader["CpuId"]),
                    CpuName = reader["CpuName"].ToString(),
                    CpuPrice = Convert.ToInt32(reader["CpuPrice"]),
                    CpuCompanyId = Convert.ToInt32(reader["CpuCompanyId"])
                };
            }
        }

        public bool Update(Cpu item)
        {
            string sql = @"UPDATE [Cpu] 
                           SET CpuName = @CpuName, 
                               CpuPrice = @CpuPrice, 
                               CpuCompanyId = @CpuCompanyId
                           WHERE CpuId = @CpuId";

            this.dbContext.AddParameter("@CpuName", item.CpuName);
            this.dbContext.AddParameter("@CpuPrice", item.CpuPrice.ToString());
            this.dbContext.AddParameter("@CpuCompanyId", item.CpuCompanyId.ToString());
            this.dbContext.AddParameter("@CpuId", item.CpuId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
