using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.OpenApi.Any;
using Models;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace SmartRigWeb
{
    public class UserRepository : Repository, IRepository<User>
    {
        public UserRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        public bool Create(User item)
        {
            string sql = $@"INSERT INTO [User] 
                    (UserName, UserEmail, UserPassword, UserAdress, CityId, UserPhoneNumber, Manager, UserSalt)
                    VALUES (@UserName, @UserEmail, @UserPassword, @UserAdress, @CityId, @UserPhoneNumber, @Manager, @UserSalt)";

            this.dbContext.AddParameter("@UserName", item.UserName);
            this.dbContext.AddParameter("@UserEmail", item.UserEmail);
            this.dbContext.AddParameter("@UserAdress", item.UserAddress);
            this.dbContext.AddParameter("@CityId", item.CityId.ToString());
            this.dbContext.AddParameter("@UserPhoneNumber", item.UserPhoneNumber);
            this.dbContext.AddParameter("@Manager", item.Manager.ToString());
            string salt = GenerateSalt();
            this.dbContext.AddParameter("@UserPassword", CaculateHash(salt, item.UserPassword));
            this.dbContext.AddParameter("@UserSalt", salt);

            return this.dbContext.Insert(sql) > 0;
        }
        private string GenerateSalt() // generateing salt for the food :D
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        private string CaculateHash(string password, string salt)
        {
            string s = password + salt;
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(s);
            using (SHA256 sha256 =  SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(pass);
                return Convert.ToBase64String(bytes);
            }
        }

        public bool Delete(string Id)
        {
            string sql = $@"DELETE FROM [User] 
                            WHERE UserId = @UserId";
            this.dbContext.AddParameter("UserId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            string sql = $@"SELECT * FROM [User]";
            using(IDataReader reader = this.dbContext.Select(sql))
            {
                while(reader.Read())
                {
                    users.Add(this.modelsFactory.UserCreator.CreateModel(reader));
                }
            }
            return users;
        }
        public User GetById(int id)
        {
            string sql = $@"SELECT * FROM [User] WHERE UserId = @UserId";
            this.dbContext.AddParameter("@UserId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.UserCreator.CreateModel(reader);
            }
            return null;
        }


        public bool Update(User item)
        {
            string sql = @"
    UPDATE [User]
    SET 
        UserName = @UserName,
        UserEmail = @UserEmail,
        UserPassword = @UserPassword,
        UserAddress = @UserAddress,
        CityId = @CityId,
        UserPhoneNumber = @UserPhoneNumber,
        Manager = @Manager
    WHERE 
        UserId = @UserId";

            this.dbContext.AddParameter("@UserName", item.UserName);
            this.dbContext.AddParameter("@UserEmail", item.UserEmail);
            this.dbContext.AddParameter("@UserPassword", item.UserPassword);
            this.dbContext.AddParameter("@UserAddress", item.UserAddress);

            this.dbContext.AddParameter("@CityId", item.CityId.ToString());
            this.dbContext.AddParameter("@UserPhoneNumber", item.UserPhoneNumber);

            // IMPORTANT FIX HERE:
            this.dbContext.AddParameter("@Manager", item.Manager ? "-1" : "0");

            this.dbContext.AddParameter("@UserId", item.UserId.ToString());

            return this.dbContext.Update(sql) > 0;
        }

        public string Login(string userEmail, string userPassword)
        {
            string sql = @"SELECT UserId, UserPassword, UserSalt
                   FROM [User] 
                   WHERE UserEmail = @UserEmail";

            // Add parameters to the query to avoid SQL injection
            this.dbContext.AddParameter("@UserEmail", userEmail);
            string salt = string.Empty;
            string hash = string.Empty;
            string userId = string.Empty;
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                if(reader.Read() == true)
                {
                    hash = reader["UserPassword"].ToString();
                    salt = reader["UserSalt"].ToString();
                    userId = reader["userId"].ToString();
                }
                if (hash == CaculateHash(userPassword, salt))
                    return userId;
                return null;
            }

            return this.dbContext.GetValue(sql).ToString();

        }

    }
}
