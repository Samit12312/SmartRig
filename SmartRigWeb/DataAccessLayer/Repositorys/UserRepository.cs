using Models;
using System.Data;
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
            this.dbContext.AddParameter("@UserPassword", item.UserPassword);
            this.dbContext.AddParameter("@UserAdress", item.UserAddress);
            this.dbContext.AddParameter("@CityId", item.CityId.ToString());
            this.dbContext.AddParameter("@UserPhoneNumber", item.UserPhoneNumber);
            this.dbContext.AddParameter("@Manager", item.Manager.ToString());
            string salt = GenerateSalt();
            this.dbContext.AddParameter("@UserSalt", salt);

            return this.dbContext.Insert(sql) > 0;
        }
        private string GenerateSalt() // generateing salt for the food :D
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
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
            string sql = @"SELECT UserId, UserName, UserEmail, UserPassword 
                   FROM [User] 
                   WHERE UserEmail = @UserEmail AND UserPassword = @UserPassword";

            // Add parameters to the query to avoid SQL injection
            this.dbContext.AddParameter("@UserEmail", userEmail);
            this.dbContext.AddParameter("@UserPassword", userPassword);
            return this.dbContext.GetValue(sql).ToString();

        }

    }
}
