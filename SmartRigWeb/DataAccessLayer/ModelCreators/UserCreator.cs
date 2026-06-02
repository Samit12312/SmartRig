using System;
using System.Data;
using Models;
using Models.ViewModels;

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
                UserPhoneNumber = Convert.ToString(reader["UserPhoneNumber"]),
                UserSalt = reader["UserSalt"].ToString(),
                Manager = Convert.ToBoolean(reader["Manager"])
            };

        }
        public User CreateFromEditUser(EditUserViewModel data)
        {
            User user = new User();

            user.UserId = data.UserId;
            user.UserName = data.UserName;
            user.UserEmail = data.UserEmail;
            user.UserPassword = data.UserPassword;
            user.UserAddress = data.UserAddress;
            user.UserPhoneNumber = data.UserPhoneNumber;
            user.CityId = data.CityId;
            user.Manager = data.Manager;

            return user;
        }
    }
}
