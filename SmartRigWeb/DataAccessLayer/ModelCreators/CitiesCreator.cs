using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class CitiesCreator : IModelCreator<Cities>
    {
        public Cities CreateModel(IDataReader reader)
        {
            return new Cities
            {
                CityId = Convert.ToInt16(reader["CityId"]),
                CityName = Convert.ToString(reader["CityName"])
            };
        }
    }
}
