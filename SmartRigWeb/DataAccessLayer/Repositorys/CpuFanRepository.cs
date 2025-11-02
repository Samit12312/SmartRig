using Models;
using System.Data;
using System.Collections.Generic;

namespace SmartRigWeb
{
    public class CpuFanRepository : Repository, IRepository<CpuFan>
    {
        public CpuFanRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory) { }

        public bool Create(CpuFan item)
        {
            string sql = @"INSERT INTO [CpuFan] (CpuFanName, CpuFanCompanyId, CpuFanPrice) VALUES (@CpuFanName, @CpuFanCompanyId, @CpuFanPrice)";
            this.dbContext.AddParameter("@CpuFanName", item.CpuFanName);
            this.dbContext.AddParameter("@CpuFanCompanyId", item.CpuFanCompanyId.ToString());
            this.dbContext.AddParameter("@CpuFanPrice", item.CpuFanPrice.ToString());
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [CpuFan] WHERE CpuFanId=@CpuFanId";
            this.dbContext.AddParameter("@CpuFanId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<CpuFan> GetAll()
        {
            List<CpuFan> list = new List<CpuFan>();
            string sql = @"SELECT * FROM [CpuFan]";
            using (IDataReader reader = this.dbContext.Select(sql))
                while (reader.Read())
                    list.Add(this.modelsFactory.CpuFanCreator.CreateModel(reader));
            return list;
        }

        public CpuFan GetById(int id)
        {
            string sql = @"SELECT * FROM [CpuFan] WHERE CpuFanId=@CpuFanId";
            this.dbContext.AddParameter("@CpuFanId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.CpuFanCreator.CreateModel(reader);
            }
        }

        public bool Update(CpuFan item)
        {
            string sql = @"UPDATE [CpuFan] SET CpuFanName=@CpuFanName, CpuFanCompanyId=@CpuFanCompanyId, CpuFanPrice=@CpuFanPrice WHERE CpuFanId=@CpuFanId";
            this.dbContext.AddParameter("@CpuFanName", item.CpuFanName);
            this.dbContext.AddParameter("@CpuFanCompanyId", item.CpuFanCompanyId.ToString());
            this.dbContext.AddParameter("@CpuFanPrice", item.CpuFanPrice.ToString());
            this.dbContext.AddParameter("@CpuFanId", item.CpuFanId.ToString());
            return this.dbContext.Update(sql) > 0;
        }
    }
}
