using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class PowerSupplyCreator : IModelCreator<PowerSupply>
    {
        public PowerSupply CreateModel(IDataReader reader)
        {
            return new PowerSupply
            {
                PowerSupplyId = Convert.ToInt16(reader["PowerSupplyId"]),
                PowerSupplyName = Convert.ToString(reader["PowerSupplyName"]),
                PowerSupplyPrice = Convert.ToInt16(reader["PowerSupplyPrice"]),
                PowerSupplyWatt = Convert.ToInt16(reader["PowerSupplyWatt"]),
                PowerSupplyCompanyId = Convert.ToInt16(reader["PowerSupplyCompanyId"])
            };
        }
    }
}
