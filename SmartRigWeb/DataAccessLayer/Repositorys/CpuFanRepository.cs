using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CpuFanRepository : Repository, IRepository<CpuFan>
    {
        public CpuFanRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(CpuFan item)
        {
            string sql = @"INSERT INTO [CpuFan] 
                           (CpuFanName, CpuFanPrice, CpuFanCompanyId)
                           VALUES (@CpuFanName, @CpuFanPrice, @CpuFanCompanyId)";

            this.dbContext.AddParameter("@CpuFanName", item.CpuFanName);
            this.dbContext.AddParameter("@CpuFanPrice", item.CpuFanPrice.ToString());
            this.dbContext.AddParameter("@CpuFanCompanyId", item.CpuFanCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [CpuFan] WHERE CpuFanId = @CpuFanId";
            this.dbContext.AddParameter("@CpuFanId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<CpuFan> GetAll()
        {
            List<CpuFan> list = new List<CpuFan>();
            string sql = @"SELECT * FROM [CpuFan]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    CpuFan cpuFan = new CpuFan
                    {
                        CpuFanId = Convert.ToInt32(reader["CpuFanId"]),
                        CpuFanName = reader["CpuFanName"].ToString(),
                        CpuFanPrice = Convert.ToInt32(reader["CpuFanPrice"]),
                        CpuFanCompanyId = Convert.ToInt32(reader["CpuFanCompanyId"])
                    };
                    list.Add(cpuFan);
                }
            }
            return list;
        }

        public CpuFan GetById(int id)
        {
            string sql = @"SELECT * FROM [CpuFan] WHERE CpuFanId = @CpuFanId";
            this.dbContext.AddParameter("@CpuFanId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new CpuFan
                {
                    CpuFanId = Convert.ToInt32(reader["CpuFanId"]),
                    CpuFanName = reader["CpuFanName"].ToString(),
                    CpuFanPrice = Convert.ToInt32(reader["CpuFanPrice"]),
                    CpuFanCompanyId = Convert.ToInt32(reader["CpuFanCompanyId"])
                };
            }
        }

        public bool Update(CpuFan item)
        {
            string sql = @"UPDATE [CpuFan] 
                           SET CpuFanName = @CpuFanName, 
                               CpuFanPrice = @CpuFanPrice, 
                               CpuFanCompanyId = @CpuFanCompanyId
                           WHERE CpuFanId = @CpuFanId";

            this.dbContext.AddParameter("@CpuFanName", item.CpuFanName);
            this.dbContext.AddParameter("@CpuFanPrice", item.CpuFanPrice.ToString());
            this.dbContext.AddParameter("@CpuFanCompanyId", item.CpuFanCompanyId.ToString());
            this.dbContext.AddParameter("@CpuFanId", item.CpuFanId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
