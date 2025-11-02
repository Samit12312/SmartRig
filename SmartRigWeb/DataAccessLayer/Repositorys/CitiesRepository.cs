using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CitiesRepository : Repository, IRepository<Cities>
    {
        public CitiesRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Cities item)
        {
            string sql = @"INSERT INTO [Cities] 
                           (CityName) 
                           VALUES (@CityName)";

            this.dbContext.AddParameter("@CityName", item.CityName);

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Cities] WHERE CityId = @CityId";
            this.dbContext.AddParameter("@CityId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Cities> GetAll()
        {
            List<Cities> list = new List<Cities>();
            string sql = @"SELECT * FROM [Cities]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelsFactory.CitiesCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public Cities GetById(int id)
        {
            string sql = @"SELECT * FROM [Cities] WHERE CityId = @CityId";
            this.dbContext.AddParameter("@CityId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.CitiesCreator.CreateModel(reader);
            }
        }

        public bool Update(Cities item)
        {
            string sql = @"UPDATE [Cities] 
                           SET CityName = @CityName 
                           WHERE CityId = @CityId";

            this.dbContext.AddParameter("@CityName", item.CityName);
            this.dbContext.AddParameter("@CityId", item.CityId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
