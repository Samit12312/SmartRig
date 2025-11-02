using Models;
using System.Data;
using System.Collections.Generic;

namespace SmartRigWeb
{
    public class GpuRepository : Repository, IRepository<Gpu>
    {
        public GpuRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory) { }

        public bool Create(Gpu item)
        {
            string sql = @"INSERT INTO [Gpu] (GpuName, GpuCompanyId, GpuPrice) VALUES (@GpuName, @GpuCompanyId, @GpuPrice)";
            this.dbContext.AddParameter("@GpuName", item.GpuName);
            this.dbContext.AddParameter("@GpuCompanyId", item.GpuCompanyId.ToString());
            this.dbContext.AddParameter("@GpuPrice", item.GpuPrice.ToString());
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Gpu] WHERE GpuId=@GpuId";
            this.dbContext.AddParameter("@GpuId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Gpu> GetAll()
        {
            List<Gpu> list = new List<Gpu>();
            string sql = @"SELECT * FROM [Gpu]";
            using (IDataReader reader = this.dbContext.Select(sql))
                while (reader.Read())
                    list.Add(this.modelsFactory.GpuCreator.CreateModel(reader));
            return list;
        }

        public Gpu GetById(int id)
        {
            string sql = @"SELECT * FROM [Gpu] WHERE GpuId=@GpuId";
            this.dbContext.AddParameter("@GpuId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.GpuCreator.CreateModel(reader);
            }
        }

        public bool Update(Gpu item)
        {
            string sql = @"UPDATE [Gpu] SET GpuName=@GpuName, GpuCompanyId=@GpuCompanyId, GpuPrice=@GpuPrice WHERE GpuId=@GpuId";
            this.dbContext.AddParameter("@GpuName", item.GpuName);
            this.dbContext.AddParameter("@GpuCompanyId", item.GpuCompanyId.ToString());
            this.dbContext.AddParameter("@GpuPrice", item.GpuPrice.ToString());
            this.dbContext.AddParameter("@GpuId", item.GpuId.ToString());
            return this.dbContext.Update(sql) > 0;
        }
    }
}
