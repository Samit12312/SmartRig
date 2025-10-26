using Models;
using System.Data;

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
            string sql = $@"INSERT INTO [User] (UserName,UserEmail,UserPassword,UserAdress,CityId,UserPhoneNumber)
                            VALUES ( @UserName, @UserEmail,@UserPassword,@UserAdress, @CityId,@UserPhoneNumber )";
            this.dbContext.AddParameter("@UserName", item.UserName);
            this.dbContext.AddParameter("@UserEmail", item.UserEmail);
            this.dbContext.AddParameter("@UserPassword", item.UserPassword);
            this.dbContext.AddParameter("@UserAddress", item.UserAddress);
            this.dbContext.AddParameter("@CityId", item.CityId.ToString());
            this.dbContext.AddParameter("@UserPhoneNumber", item.UserPhoneNumber);
            return this.dbContext.Insert(sql)>0;
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
            string sql = @"SELECT * FROM [User] WHERE UserId = @UserId";
            this.dbContext.AddParameter("@UserId", id.ToString());
            IDataReader reader = this.dbContext.Select(sql);
            reader.Read();
            return this.modelsFactory.UserCreator.CreateModel(reader);
        }


        public bool Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
