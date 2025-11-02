using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class RamRepository : Repository, IRepository<Ram>
    {
        public RamRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Ram item)
        {
            string sql = @"INSERT INTO [Ram] 
                           (RamName, RamSize, RamSpeed, RamPrice, RamCompanyId) 
                           VALUES (@RamName, @RamSize, @RamSpeed, @RamPrice, @RamCompanyId)";

            this.dbContext.AddParameter("@RamName", item.RamName);
            this.dbContext.AddParameter("@RamSize", item.RamSize);
            this.dbContext.AddParameter("@RamSpeed", item.RamSpeed);
            this.dbContext.AddParameter("@RamPrice", item.RamPrice.ToString());
            this.dbContext.AddParameter("@RamCompanyId", item.RamCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Ram] WHERE RamId = @RamId";
            this.dbContext.AddParameter("@RamId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Ram> GetAll()
        {
            List<Ram> list = new List<Ram>();
            string sql = @"SELECT * FROM [Ram]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelsFactory.RamCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public Ram GetById(int id)
        {
            string sql = @"SELECT * FROM [Ram] WHERE RamId = @RamId";
            this.dbContext.AddParameter("@RamId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.RamCreator.CreateModel(reader);
            }
        }

        public bool Update(Ram item)
        {
            string sql = @"UPDATE [Ram] 
                           SET RamName = @RamName, 
                               RamSize = @RamSize, 
                               RamSpeed = @RamSpeed, 
                               RamPrice = @RamPrice, 
                               RamCompanyId = @RamCompanyId
                           WHERE RamId = @RamId";

            this.dbContext.AddParameter("@RamName", item.RamName);
            this.dbContext.AddParameter("@RamSize", item.RamSize);
            this.dbContext.AddParameter("@RamSpeed", item.RamSpeed);
            this.dbContext.AddParameter("@RamPrice", item.RamPrice.ToString());
            this.dbContext.AddParameter("@RamCompanyId", item.RamCompanyId.ToString());
            this.dbContext.AddParameter("@RamId", item.RamId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
