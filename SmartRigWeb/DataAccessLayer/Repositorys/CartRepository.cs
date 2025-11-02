using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CartRepository : Repository, IRepository<Cart>
    {
        public CartRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(Cart item)
        {
            string sql = @"INSERT INTO [Cart] (UserId, Date, IsPayed) 
                           VALUES (@UserId, @Date, @IsPayed)";

            this.dbContext.AddParameter("@UserId", item.UserId.ToString());
            this.dbContext.AddParameter("@Date", item.Date);
            this.dbContext.AddParameter("@IsPayed", item.IsPayed.ToString());

            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Cart] WHERE CartId = @CartId";
            this.dbContext.AddParameter("@CartId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Cart> GetAll()
        {
            List<Cart> list = new List<Cart>();
            string sql = @"SELECT * FROM [Cart]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Cart c = new Cart
                    {
                        CartId = Convert.ToInt32(reader["CartId"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Date = reader["Date"].ToString(),
                        IsPayed = Convert.ToBoolean(reader["IsPayed"])
                    };
                    list.Add(c);
                }
            }
            return list;
        }

        public Cart GetById(int id)
        {
            string sql = @"SELECT * FROM [Cart] WHERE CartId = @CartId";
            this.dbContext.AddParameter("@CartId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new Cart
                {
                    CartId = Convert.ToInt32(reader["CartId"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    Date = reader["Date"].ToString(),
                    IsPayed = Convert.ToBoolean(reader["IsPayed"])
                };
            }
        }

        public bool Update(Cart item)
        {
            string sql = @"UPDATE [Cart] 
                           SET UserId = @UserId, 
                               Date = @Date, 
                               IsPayed = @IsPayed 
                           WHERE CartId = @CartId";

            this.dbContext.AddParameter("@UserId", item.UserId.ToString());
            this.dbContext.AddParameter("@Date", item.Date);
            this.dbContext.AddParameter("@IsPayed", item.IsPayed.ToString());
            this.dbContext.AddParameter("@CartId", item.CartId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
