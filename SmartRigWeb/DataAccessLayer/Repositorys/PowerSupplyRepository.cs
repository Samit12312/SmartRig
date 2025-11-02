using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class PowerSupplyRepository : Repository, IRepository<PowerSupply>
    {
        public PowerSupplyRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(PowerSupply item)
        {
            string sql = @"INSERT INTO [PowerSupply] 
                           (PowerSupplyName, PowerSupplyPrice, PowerSupplyWatt, PowerSupplyCompanyId)
                           VALUES (@PowerSupplyName, @PowerSupplyPrice, @PowerSupplyWatt, @PowerSupplyCompanyId)";

            this.dbContext.AddParameter("@PowerSupplyName", item.PowerSupplyName);
            this.dbContext.AddParameter("@PowerSupplyPrice", item.PowerSupplyPrice.ToString());
            this.dbContext.AddParameter("@PowerSupplyWatt", item.PowerSupplyWatt.ToString());
            this.dbContext.AddParameter("@PowerSupplyCompanyId", item.PowerSupplyCompanyId.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [PowerSupply] WHERE PowerSupplyId = @PowerSupplyId";
            this.dbContext.AddParameter("@PowerSupplyId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<PowerSupply> GetAll()
        {
            List<PowerSupply> list = new List<PowerSupply>();
            string sql = @"SELECT * FROM [PowerSupply]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelsFactory.PowerSupplyCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public PowerSupply GetById(int id)
        {
            string sql = @"SELECT * FROM [PowerSupply] WHERE PowerSupplyId = @PowerSupplyId";
            this.dbContext.AddParameter("@PowerSupplyId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.PowerSupplyCreator.CreateModel(reader);
            }
        }

        public bool Update(PowerSupply item)
        {
            string sql = @"UPDATE [PowerSupply] 
                           SET PowerSupplyName = @PowerSupplyName, 
                               PowerSupplyPrice = @PowerSupplyPrice, 
                               PowerSupplyWatt = @PowerSupplyWatt, 
                               PowerSupplyCompanyId = @PowerSupplyCompanyId
                           WHERE PowerSupplyId = @PowerSupplyId";

            this.dbContext.AddParameter("@PowerSupplyName", item.PowerSupplyName);
            this.dbContext.AddParameter("@PowerSupplyPrice", item.PowerSupplyPrice.ToString());
            this.dbContext.AddParameter("@PowerSupplyWatt", item.PowerSupplyWatt.ToString());
            this.dbContext.AddParameter("@PowerSupplyCompanyId", item.PowerSupplyCompanyId.ToString());
            this.dbContext.AddParameter("@PowerSupplyId", item.PowerSupplyId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
