using Models;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace SmartRigWeb
{
    public class CartComputerCreator : IModelCreator<CartComputer>
    {
        public CartComputer CreateModel(IDataReader reader) 
        {
            return new CartComputer
            {
                ComputerId = Convert.ToInt32(reader["ComputerId"]),
                ComputerName = Convert.ToString(reader["ComputerName"]),
                ComputerPrice = Convert.ToInt16(reader["ComputerPrice"]),
                ComputerQuantity = Convert.ToInt16(reader["ComputerQuantity"]),
                ComputerPicture = Convert.ToString(reader["ComputerPicture"])
            };
        }
    }
}
