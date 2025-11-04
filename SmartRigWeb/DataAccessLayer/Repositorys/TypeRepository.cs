using Models;
using System.Data;
using System.Collections.Generic;

namespace SmartRigWeb
{
    public class TypeRepository : Repository, IRepository<Models.Type>
    {
        public TypeRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Models.Type item)
        {
            string sql = @"INSERT INTO [Type] (TypeName, TypeCode)
                           VALUES (@TypeName, @TypeCode)";

            this.dbContext.AddParameter("@TypeName", item.TypeName);
            this.dbContext.AddParameter("@TypeCode", item.TypeCode.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Type] WHERE TypeId = @TypeId";
            this.dbContext.AddParameter("@TypeId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Models.Type> GetAll()
        {
            List<Models.Type> types = new List<Models.Type>();
            string sql = @"SELECT * FROM [Type]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    types.Add(this.modelsFactory.TypeCreator.CreateModel(reader));
                }
            }
            return types;
        }

        public List<Models.Type> GetAllByTypeCode(int TypeCode)
        {
            List<Models.Type> types = new List<Models.Type>();
            string sql = @"SELECT * FROM [Type] where TypeCode=@TypeCode";
            this.dbContext.AddParameter("@TypeCode", TypeCode.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    types.Add(this.modelsFactory.TypeCreator.CreateModel(reader));
                }
            }
            return types;
        }

        public Models.Type GetById(int id)
        {
            string sql = @"SELECT * FROM [Type] WHERE TypeId = @TypeId";
            this.dbContext.AddParameter("@TypeId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.TypeCreator.CreateModel(reader);
            }
        }

        public bool Update(Models.Type item)
        {
            string sql = @"UPDATE [Type] 
                           SET TypeName = @TypeName, TypeCode = @TypeCode
                           WHERE TypeId = @TypeId";

            this.dbContext.AddParameter("@TypeName", item.TypeName);
            this.dbContext.AddParameter("@TypeCode", item.TypeCode.ToString());
            this.dbContext.AddParameter("@TypeId", item.TypeId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
