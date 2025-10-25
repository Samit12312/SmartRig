using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class CartCreator : IModelCreator<Cart>
    {
        public Cart CreateModel(IDataReader reader)
        {
            return new Cart
            {
                CartId = Convert.ToInt16(reader["CartId"]),
                UserId = Convert.ToInt16(reader["UserId"]),
                Date = Convert.ToString(reader["Date"]),
                IsPayed = Convert.ToBoolean(reader["IsPayed"])
            };
        }
    }
}
