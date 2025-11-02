using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class MotherBoardRepository : Repository, IRepository<MotherBoard>
    {
        public MotherBoardRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(MotherBoard item)
        {
            string sql = @"INSERT INTO [MotherBoard] 
                           (MotherBoardName, MotherBoardPrice, MotherBoardCompanyId)
                           VALUES (@MotherBoardName, @MotherBoardPrice, @MotherBoardCompanyId)";

            this.dbContext.AddParameter("@MotherBoardName", item.MotherBoardName);
            this.dbContext.AddParameter("@MotherBoardPrice", item.MotherBoardPrice.ToString());
            this.dbContext.AddParameter("@MotherBoardCompanyId", item.MotherBoardCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [MotherBoard] WHERE MotherBoardId = @MotherBoardId";
            this.dbContext.AddParameter("@MotherBoardId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<MotherBoard> GetAll()
        {
            List<MotherBoard> list = new List<MotherBoard>();
            string sql = @"SELECT * FROM [MotherBoard]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    MotherBoard motherBoard = new MotherBoard
                    {
                        MotherBoardId = Convert.ToInt32(reader["MotherBoardId"]),
                        MotherBoardName = reader["MotherBoardName"].ToString(),
                        MotherBoardPrice = Convert.ToInt32(reader["MotherBoardPrice"]),
                        MotherBoardCompanyId = Convert.ToInt32(reader["MotherBoardCompanyId"])
                    };
                    list.Add(motherBoard);
                }
            }
            return list;
        }

        public MotherBoard GetById(int id)
        {
            string sql = @"SELECT * FROM [MotherBoard] WHERE MotherBoardId = @MotherBoardId";
            this.dbContext.AddParameter("@MotherBoardId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new MotherBoard
                {
                    MotherBoardId = Convert.ToInt32(reader["MotherBoardId"]),
                    MotherBoardName = reader["MotherBoardName"].ToString(),
                    MotherBoardPrice = Convert.ToInt32(reader["MotherBoardPrice"]),
                    MotherBoardCompanyId = Convert.ToInt32(reader["MotherBoardCompanyId"])
                };
            }
        }

        public bool Update(MotherBoard item)
        {
            string sql = @"UPDATE [MotherBoard] 
                           SET MotherBoardName = @MotherBoardName, 
                               MotherBoardPrice = @MotherBoardPrice, 
                               MotherBoardCompanyId = @MotherBoardCompanyId
                           WHERE MotherBoardId = @MotherBoardId";

            this.dbContext.AddParameter("@MotherBoardName", item.MotherBoardName);
            this.dbContext.AddParameter("@MotherBoardPrice", item.MotherBoardPrice.ToString());
            this.dbContext.AddParameter("@MotherBoardCompanyId", item.MotherBoardCompanyId.ToString());
            this.dbContext.AddParameter("@MotherBoardId", item.MotherBoardId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
