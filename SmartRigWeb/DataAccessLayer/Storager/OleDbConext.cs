using System.Data;
using System.Data.OleDb;

namespace SmartRigWeb
{
    public class OleDbConext : IDbContext
    {
        OleDbConnection connection; // object rsponsable for the connection with database
        OleDbCommand command; // responsable to transfer the commands to database :):D:?:/ :\ :(:>:<:P:p:o:O:I:v:V:c:C:x:X:L:l.
        OleDbTransaction transaction;
        public OleDbConext()
        {
            this.connection = new OleDbConnection();
            this.connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetCurrentDirectory()}\App_Data\Computers.accdb";
            this.command = new OleDbCommand();
            this.command.Connection = this.connection;
        }
        public void BeginTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        public int Delete(string sql)
        {
            return ChangeDb(sql);
        }

        public int Insert(string sql)
        {
            return ChangeDb(sql);
        }

        public void OpenConnection()
        {
            this.connection.Open();
        }

        public void RollBack()
        {
            this.transaction.Rollback();
        }

        public IDataReader Select(string sql)
        {
            this.command.CommandText = sql;
            IDataReader dataReader = this.command.ExecuteReader();
            this.command.Parameters.Clear();
            return dataReader;
        }

        public int Update(string sql)
        {
            return ChangeDb(sql);
        }
        private int ChangeDb(string sql) 
        {
            this.command.CommandText = sql;
            int records = this.command.ExecuteNonQuery();
            this.command.Parameters.Clear();
            return records;
        }
        public void AddParameter(string name, string value)
        {
            this.command.Parameters.Add(new OleDbParameter(name, value));
        }
        public object GetValue(string sql)
        {
            this.command.CommandText = sql;
            object value = this.command.ExecuteScalar();
            this.command.Parameters.Clear();
            return value;
        }
    }
}
