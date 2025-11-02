using Models;
using System.Data;
using System.Collections.Generic;

namespace SmartRigWeb
{
    public class MotherBoardRepository : Repository, IRepository<MotherBoard>
    {
        public MotherBoardRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory) { }

        public bool Create(MotherBoard item)
        {
            string sql = @"INSERT INTO [MotherBoard] (MotherBoardName, MotherBoardCompanyId, MotherBoardPrice) 
                           VALUES (@MotherBoardName, @MotherBoardCompanyId, @MotherBoardPrice)";
            this.dbContext.AddParameter("@MotherBoardName", item.MotherBoardName);
            this.dbContext.AddParameter("@MotherBoardCompanyId", item.MotherBoardCompanyId.ToString());
            this.dbContext.AddParameter("@MotherBoardPrice", item.MotherBoardPrice.ToString());
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [MotherBoard] WHERE MotherBoardId=@MotherBoardId";
            this.dbContext.AddParameter("@MotherBoardId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<MotherBoard> GetAll()
        {
            List<MotherBoard> list = new List<MotherBoard>();
            string sql = @"SELECT * FROM [MotherBoard]";
            using (IDataReader reader = this.dbContext.Select(sql))
                while (reader.Read())
                    list.Add(this.modelsFactory.MotherBoardCreator.CreateModel(reader));
            return list;
        }

        public MotherBoard GetById(int id)
        {
            string sql = @"SELECT * FROM [MotherBoard] WHERE MotherBoardId=@MotherBoardId";
            this.dbContext.AddParameter("@MotherBoardId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.MotherBoardCreator.CreateModel(reader);
            }
        }

        public bool Update(MotherBoard item)
        {
            string sql = @"UPDATE [MotherBoard] SET MotherBoardName=@MotherBoardName, MotherBoardCompanyId=@MotherBoardCompanyId, MotherBoardPrice=@MotherBoardPrice WHERE MotherBoardId=@MotherBoardId";
            this.dbContext.AddParameter("@MotherBoardName", item.MotherBoardName);
            this.dbContext.AddParameter("@MotherBoardCompanyId", item.MotherBoardCompanyId.ToString());
            this.dbContext.AddParameter("@MotherBoardPrice", item.MotherBoardPrice.ToString());
            this.dbContext.AddParameter("@MotherBoardId", item.MotherBoardId.ToString());
            return this.dbContext.Update(sql) > 0;
        }
    }
}
