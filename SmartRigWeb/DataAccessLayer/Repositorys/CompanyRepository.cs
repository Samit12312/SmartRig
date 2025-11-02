using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CompanyRepository : Repository, IRepository<Company>
    {
        public CompanyRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Company item)
        {
            string sql = @"INSERT INTO [Company] (CompanyName) 
                           VALUES (@CompanyName)";

            this.dbContext.AddParameter("@CompanyName", item.CompanyName);

            return this.dbContext.Insert(sql) > 0;
        }
        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Company] WHERE CompanyId = @CompanyId";
            this.dbContext.AddParameter("@CompanyId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Company> GetAll()
        {
            List<Company> list = new List<Company>();
            string sql = @"SELECT * FROM [Company]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Company company = new Company
                    {
                        CompanyId = Convert.ToInt32(reader["CompanyId"]),
                        CompanyName = reader["CompanyName"].ToString()
                    };
                    list.Add(company);
                }
            }
            return list;
        }

        public Company GetById(int id)
        {
            string sql = @"SELECT * FROM [Company] WHERE CompanyId = @CompanyId";
            this.dbContext.AddParameter("@CompanyId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new Company
                {
                    CompanyId = Convert.ToInt32(reader["CompanyId"]),
                    CompanyName = reader["CompanyName"].ToString()
                };
            }
        }


        public bool Update(Company item)
        {
            string sql = @"UPDATE [Company] 
                           SET CompanyName = @CompanyName
                           WHERE CompanyId = @CompanyId";

            this.dbContext.AddParameter("@CompanyName", item.CompanyName);
            this.dbContext.AddParameter("@CompanyId", item.CompanyId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
