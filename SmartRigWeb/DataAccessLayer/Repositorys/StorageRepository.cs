using Models;
using System.Data;
using System.Collections.Generic;

namespace SmartRigWeb
{
    public class StorageRepository : Repository, IRepository<Models.Storage>
    {
        public StorageRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Models.Storage item)
        {
            string sql = @"INSERT INTO [Storage] 
                           (StorageName, StorageSize, StorageSpeed, StorageType, StoragePrice, StorageCompanyId)
                           VALUES (@StorageName, @StorageSize, @StorageSpeed, @StorageType, @StoragePrice, @StorageCompanyId)";

            this.dbContext.AddParameter("@StorageName", item.StorageName);
            this.dbContext.AddParameter("@StorageSize", item.StorageSize);
            this.dbContext.AddParameter("@StorageSpeed", item.StorageSpeed);
            this.dbContext.AddParameter("@StorageType", item.StorageType.ToString());
            this.dbContext.AddParameter("@StoragePrice", item.StoragePrice.ToString());
            this.dbContext.AddParameter("@StorageCompanyId", item.StorageCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Storage] WHERE StorageId = @StorageId";
            this.dbContext.AddParameter("@StorageId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Models.Storage> GetAll()
        {
            List<Models.Storage> storages = new List<Models.Storage>();
            string sql = @"SELECT * FROM [Storage]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    storages.Add(this.modelsFactory.StorageCreator.CreateModel(reader));
                }
            }
            return storages;
        }

        public Models.Storage GetById(int id)
        {
            string sql = @"SELECT * FROM [Storage] WHERE StorageId = @StorageId";
            this.dbContext.AddParameter("@StorageId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.StorageCreator.CreateModel(reader);
            }
        }

        public bool Update(Models.Storage item)
        {
            string sql = @"UPDATE [Storage] 
                           SET StorageName = @StorageName, 
                               StorageSize = @StorageSize, 
                               StorageSpeed = @StorageSpeed, 
                               StorageType = @StorageType, 
                               StoragePrice = @StoragePrice, 
                               StorageCompanyId = @StorageCompanyId
                           WHERE StorageId = @StorageId";

            this.dbContext.AddParameter("@StorageName", item.StorageName);
            this.dbContext.AddParameter("@StorageSize", item.StorageSize);
            this.dbContext.AddParameter("@StorageSpeed", item.StorageSpeed);
            this.dbContext.AddParameter("@StorageType", item.StorageType.ToString());
            this.dbContext.AddParameter("@StoragePrice", item.StoragePrice.ToString());
            this.dbContext.AddParameter("@StorageCompanyId", item.StorageCompanyId.ToString());
            this.dbContext.AddParameter("@StorageId", item.StorageId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
