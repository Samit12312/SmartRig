using System.Data;
using System.Data.OleDb;
namespace SmartRigWeb
{
    public interface IDbContext
    {
        void OpenConnection(); //פתיחת קשר עם מסד נתונים
        void CloseConnection();
        void BeginTransaction(); // פותח חבילה של פעולות
        void Commit();
        void RollBack();
        int Delete(string sql);
        int Insert(string sql);
        int Update(string sql);
        IDataReader Select(string sql); //object that can save Record set

    }
}
