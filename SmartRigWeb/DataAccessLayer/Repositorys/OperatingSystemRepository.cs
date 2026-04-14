using Models;
using System;
using System.Data;
using System.Collections.Generic;

namespace SmartRigWeb
{
    public class OperatingSystemRepository : Repository, IRepository<Models.OperatingSystem>
    {
        public OperatingSystemRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory) { }

        public bool Create(Models.OperatingSystem item)
        {
            string sql = @"INSERT INTO [OperatingSystem] 
                           (OperatingSystemName, OperatingSystemPrice, OperatingSystemCompany) 
                           VALUES (@OperatingSystemName, @OperatingSystemPrice, @OperatingSystemCompany)";

            this.dbContext.AddParameter("@OperatingSystemName", item.OperatingSystemName);
            this.dbContext.AddParameter("@OperatingSystemPrice", item.OperatingSystemPrice.ToString());
            this.dbContext.AddParameter("@OperatingSystemCompany", item.OperatingSystemCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Update(Models.OperatingSystem item)
        {
            string sql = @"UPDATE [OperatingSystem] 
                           SET OperatingSystemName = @OperatingSystemName, 
                               OperatingSystemPrice = @OperatingSystemPrice,
                               OperatingSystemCompany = @OperatingSystemCompany
                           WHERE OperatingSystemId = @OperatingSystemId";

            this.dbContext.AddParameter("@OperatingSystemName", item.OperatingSystemName);
            this.dbContext.AddParameter("@OperatingSystemPrice", item.OperatingSystemPrice.ToString());
            this.dbContext.AddParameter("@OperatingSystemCompany", item.OperatingSystemCompanyId.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", item.OperatingSystemId.ToString());

            return this.dbContext.Update(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [OperatingSystem] WHERE OperatingSystemId=@OperatingSystemId";
            this.dbContext.AddParameter("@OperatingSystemId", Id);
            return this.dbContext.Update(sql) > 0;
        }

        public List<Models.OperatingSystem> GetAll()
        {
            List<Models.OperatingSystem> list = new List<Models.OperatingSystem>();
            string sql = @"SELECT * FROM [OperatingSystem]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelsFactory.OperatingSystemCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public Models.OperatingSystem GetById(int id)
        {
            string sql = @"SELECT * FROM [OperatingSystem] WHERE OperatingSystemId=@OperatingSystemId";
            this.dbContext.AddParameter("@OperatingSystemId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.OperatingSystemCreator.CreateModel(reader);
            }
        }

        public List<Company> GetOperatingSystemCompanies()
        {
            List<Company> companies = new List<Company>();

            string sql = @"
                SELECT DISTINCT Company.CompanyId, Company.CompanyName
                FROM Company
                INNER JOIN OperatingSystem
                ON Company.CompanyId = OperatingSystem.OperatingSystemCompany";

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Company company = new Company();
                    company.CompanyId = Convert.ToInt32(reader["CompanyId"]);
                    company.CompanyName = reader["CompanyName"].ToString();
                    companies.Add(company);
                }
            }

            return companies;
        }
    }
}