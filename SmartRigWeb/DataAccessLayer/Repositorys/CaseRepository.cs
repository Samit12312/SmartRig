using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CaseRepository : Repository, IRepository<Case>
    {
        public CaseRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Case item)
        {
            string sql = @"INSERT INTO [Case] (CaseName, CasePrice, CaseCompanyId) 
                           VALUES (@CaseName, @CasePrice, @CaseCompanyId)";

            this.dbContext.AddParameter("@CaseName", item.CaseName);
            this.dbContext.AddParameter("@CasePrice", item.CasePrice);
            this.dbContext.AddParameter("@CaseCompanyId", item.CaseCompanyId);

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Case] WHERE CaseId = @CaseId";
            this.dbContext.AddParameter("@CaseId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Case> GetAll()
        {
            List<Case> list = new List<Case>();
            string sql = @"SELECT * FROM [Case]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Case c = new Case
                    {
                        CaseId = Convert.ToInt32(reader["CaseId"]),
                        CaseName = reader["CaseName"].ToString(),
                        CasePrice = Convert.ToInt32(reader["CasePrice"]),
                        CaseCompanyId = Convert.ToInt32(reader["CaseCompanyId"])
                    };
                    list.Add(c);
                }
            }
            return list;
        }

        public Case GetById(int id)
        {
            string sql = @"SELECT * FROM [Case] WHERE CaseId = @CaseId";
            this.dbContext.AddParameter("@CaseId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new Case
                {
                    CaseId = Convert.ToInt32(reader["CaseId"]),
                    CaseName = reader["CaseName"].ToString(),
                    CasePrice = Convert.ToInt32(reader["CasePrice"]),
                    CaseCompanyId = Convert.ToInt32(reader["CaseCompanyId"])
                };
            }
        }

        public bool Update(Case item)
        {
            string sql = @"UPDATE [Case] 
                           SET CaseName = @CaseName, 
                               CasePrice = @CasePrice, 
                               CaseCompanyId = @CaseCompanyId 
                           WHERE CaseId = @CaseId";

            this.dbContext.AddParameter("@CaseName", item.CaseName);
            this.dbContext.AddParameter("@CasePrice", item.CasePrice);
            this.dbContext.AddParameter("@CaseCompanyId", item.CaseCompanyId);
            this.dbContext.AddParameter("@CaseId", item.CaseId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
