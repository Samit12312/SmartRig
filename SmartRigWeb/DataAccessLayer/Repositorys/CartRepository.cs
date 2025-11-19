using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class CartRepository : Repository, IRepository<Cart>
    {
        public CartRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory) { }

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
            return this.dbContext.Delete(sql) > 0;
        }
        public bool BuyCart(int cartId)
        {
            string sql = @"UPDATE Cart SET IsPayed = True WHERE CartId = @CartId";
            this.dbContext.AddParameter("@CartId", cartId.ToString());
            return this.dbContext.Update(sql) > 0;
        }

        public List<Cart> GetAll()
        {
            List<Cart> list = new List<Cart>();
            string sql = @"SELECT * FROM [Cart]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelsFactory.CartCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Cart> GetPaidCartByUserId(int userId)
        {
            List<Cart> carts = new List<Cart>();

            string sql = @"SELECT * FROM [Cart] 
                   WHERE UserId = @UserId 
                   AND IsPayed = True";

            this.dbContext.AddParameter("@UserId", userId.ToString());

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    carts.Add(this.modelsFactory.CartCreator.CreateModel(reader));
                }
            }

            return carts;
        }
        public bool AddComputer(int cartId, int computerId, int quantity)
        {
            string sql = @"INSERT INTO CartComputer (CartId, ComputerId, Quantity) 
                   VALUES (@CartId, @ComputerId, @Quantity)";
            this.dbContext.AddParameter("@CartId", cartId.ToString());
            this.dbContext.AddParameter("@ComputerId", computerId.ToString());
            this.dbContext.AddParameter("@Quantity", quantity.ToString());
            return this.dbContext.Insert(sql) > 0;
        }
        public bool RemoveComputer(int cartId, int computerId)
        {
            string sql = @"DELETE FROM CartComputer 
                   WHERE CartId = @CartId AND ComputerId = @ComputerId";
            this.dbContext.AddParameter("@CartId", cartId.ToString());
            this.dbContext.AddParameter("@ComputerId", computerId.ToString());
            return this.dbContext.Delete(sql) > 0;
        }

        public List<CartComputer> GetOrdersByUserId(int userId)
        {
            string sql = @"
        SELECT
            Cart.UserId,
            Computer.ComputerId,
            Computer.ComputerName,
            Computer.Price,
            CartComputer.Quantity,
            Computer.ComputerPicture,
            Cart.IsPayed
        FROM
            Computer
            INNER JOIN (Cart
                INNER JOIN CartComputer ON Cart.CartId = CartComputer.CartId)
                ON Computer.ComputerId = CartComputer.ComputerId
        WHERE
            Cart.UserId = @UserId
            AND Cart.IsPayed = True;
    ";

            this.dbContext.AddParameter("@UserId", userId.ToString());

            List<CartComputer> orders = new List<CartComputer>();

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    // Use model factory to create CartComputer
                    CartComputer orderItem = this.modelsFactory.CartComputerCreator.CreateModel(reader);

                    // Map quantity manually
                    orderItem.computerQuantity = Convert.ToInt16(reader["Quantity"]);

                    orders.Add(orderItem);
                }
            }

            return orders;
        }

        public List<CartComputer> GetCartById(int userId)
        {
            string sql = @"
        SELECT 
            Computer.ComputerId,
            Computer.ComputerName,
            Computer.Price,
            Computer.ComputerPicture,
            CartComputer.Quantity,
            Cart.UserId,
            Cart.IsPayed
        FROM 
            Computer
            INNER JOIN (Cart
                INNER JOIN CartComputer ON Cart.CartId = CartComputer.CartId)
                ON Computer.ComputerId = CartComputer.ComputerId
        WHERE 
            Cart.UserId = @UserId
            AND Cart.IsPayed = False;
    ";

            this.dbContext.AddParameter("@UserId", userId.ToString());

            List<CartComputer> cartItems = new List<CartComputer>();

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    // Create CartComputer using your model factory
                    CartComputer cartComputer = this.modelsFactory.CartComputerCreator.CreateModel(reader);

                    // Map quantity manually if needed
                    cartComputer.computerQuantity = Convert.ToInt16(reader["Quantity"]);

                    cartItems.Add(cartComputer);
                }
            }

            return cartItems;
        }



        public Cart GetById(int id)
        {
            string sql = @"SELECT * FROM [Cart] WHERE CartId = @CartId";
            this.dbContext.AddParameter("@CartId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.CartCreator.CreateModel(reader);
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
