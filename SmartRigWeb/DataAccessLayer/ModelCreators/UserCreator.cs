using System;
using System.Data;
using Models;

namespace SmartRigWeb
{
    public class UserCreator : IModelCreator<User>
    {
        public User CreateModel(IDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt16(reader["UserId"]),
                UserName = Convert.ToString(reader["UserName"]),
                UserEmail = Convert.ToString(reader["UserEmail"]),
                UserPassword = Convert.ToString(reader["UserPassword"]),
                UserAddress = Convert.ToString(reader["UserAddress"]),
                CityId = Convert.ToInt16(reader["CityId"]),
                UserPhoneNumber = Convert.ToString(reader["UserPhoneNumber"])
            };
        }
    }
}
