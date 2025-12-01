using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;

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
        public List<CartComputer> GetAllOrders()
        {
            string sql = @"
        SELECT
            CartComputer.CartId,
            CartComputer.ComputerId,
            CartComputer.Quantity,
            CartComputer.Price AS ComputerPrice,
            Computer.ComputerName,
            Computer.ComputerPicture
        FROM
            (Cart
            INNER JOIN CartComputer ON Cart.CartId = CartComputer.CartId)
            INNER JOIN Computer ON Computer.ComputerId = CartComputer.ComputerId
        WHERE
            Cart.IsPayed = True
    ";

            List<CartComputer> orders = new List<CartComputer>();

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    CartComputer orderItem = new CartComputer();

                    orderItem.ComputerId = Convert.ToInt16(reader["ComputerId"]);
                    orderItem.ComputerName = reader["ComputerName"].ToString();
                    orderItem.ComputerPrice = Convert.ToInt16(reader["ComputerPrice"]);
                    orderItem.ComputerQuantity = Convert.ToInt16(reader["Quantity"]);
                    orderItem.ComputerPicture = reader["ComputerPicture"].ToString();

                    orders.Add(orderItem);
                }
            }

            return orders;
        }

        public List<CartComputer> GetOrdersByUserId(int userId)
        {
            // SQL query selects all necessary columns and aliases them to match model
            string sql = @"
        SELECT
            CartComputer.ComputerId,
            Computer.ComputerName,
            CartComputer.Price AS ComputerPrice,
            CartComputer.Quantity AS ComputerQuantity,
            Computer.ComputerPicture,
            Cart.UserId,
            Cart.IsPayed
        FROM
            (CartComputer
            INNER JOIN Computer ON Computer.ComputerId = CartComputer.ComputerId)
            INNER JOIN Cart ON Cart.CartId = CartComputer.CartId
        WHERE
            (@UserId = 0 OR Cart.UserId = @UserId)
            AND Cart.IsPayed = True
    ";

            // Add userId parameter (0 means all users)
            this.dbContext.AddParameter("@UserId", userId.ToString());

            List<CartComputer> orders = new List<CartComputer>();

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    // Create CartComputer using model factory
                    CartComputer orderItem = this.modelsFactory.CartComputerCreator.CreateModel(reader);

                    // Map aliased columns manually to ensure correct property assignment
                    orderItem.ComputerPrice = Convert.ToInt16(reader["ComputerPrice"]);
                    orderItem.ComputerQuantity = Convert.ToInt16(reader["ComputerQuantity"]);

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

        // This satisfies the interface
        public bool Update(Cart cart)
        {
            string sql = @"UPDATE Cart
                   SET UserId = @UserId,
                       [Date] = @Date,
                       IsPayed = @IsPayed
                   WHERE CartId = @CartId";

            this.dbContext.AddParameter("@UserId", cart.UserId.ToString());
            this.dbContext.AddParameter("@Date", cart.Date);
            this.dbContext.AddParameter("@IsPayed", cart.IsPayed.ToString());
            this.dbContext.AddParameter("@CartId", cart.CartId.ToString());

            return this.dbContext.Update(sql) > 0;
        }

        public bool UpdateCartStatus(int cartId, bool isPayed)
        {
            string sql = @"UPDATE Cart
                   SET IsPayed = @IsPayed
                   WHERE CartId = @CartId";

            // Convert to string because your dbContext.AddParameter expects string
            this.dbContext.AddParameter("@IsPayed", (isPayed ? -1 : 0).ToString());
            this.dbContext.AddParameter("@CartId", cartId.ToString());

            return this.dbContext.Update(sql) > 0;
        }



        public decimal GetTotalRevenue(string fromDate, string toDate)
        {
            string sql = @"
        SELECT SUM(CartComputer.Price * CartComputer.Quantity) AS TotalRevenue
        FROM Cart
        INNER JOIN CartComputer ON Cart.CartId = CartComputer.CartId
        WHERE CDate(Cart.[Date]) BETWEEN CDate(@FromDate) AND CDate(@ToDate)
          AND Cart.IsPayed = True;
    ";

            this.dbContext.AddParameter("@FromDate", fromDate);
            this.dbContext.AddParameter("@ToDate", toDate);

            decimal totalRevenue = 0;

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                if (reader.Read() && reader["TotalRevenue"] != DBNull.Value)
                {
                    totalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                }
            }

            return totalRevenue;
        }
        public CartComputer GetMostSoldComputer()
        {
            CartComputer mostSold = null;

            try
            {
                string sql = @"
        SELECT TOP 1
            Computer.ComputerId,
            Computer.ComputerName,
            Computer.ComputerPicture,
            Computer.Price,
            SUM(CartComputer.Quantity) AS TotalSold
        FROM (Computer 
            INNER JOIN CartComputer ON Computer.ComputerId = CartComputer.ComputerId)
            INNER JOIN Cart ON Cart.CartId = CartComputer.CartId
        WHERE Cart.IsPayed = True
        GROUP BY Computer.ComputerId, Computer.ComputerName, Computer.ComputerPicture, Computer.Price
        ORDER BY SUM(CartComputer.Quantity) DESC;
        ";

                using (IDataReader reader = this.dbContext.Select(sql))
                {
                    if (reader.Read())
                    {
                        mostSold = new CartComputer
                        {
                            ComputerId = Convert.ToInt16(reader["ComputerId"]),
                            ComputerName = reader["ComputerName"].ToString(),
                            ComputerPicture = reader["ComputerPicture"].ToString(),
                            computerPrice = Convert.ToInt16(reader["Price"]),
                            computerQuantity = Convert.ToInt16(reader["TotalSold"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching most sold computer: {ex.Message}");
            }

            return mostSold;
        }






    }
}
