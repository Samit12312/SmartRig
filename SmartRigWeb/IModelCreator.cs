using System.Data;
namespace SmartRigWeb
{
    public interface IModelCreator<T>
    {
        T CreateModel(IDataReader reader); //ליצור מודל מהמקור (מידע) של הRecordSet

    }
}
