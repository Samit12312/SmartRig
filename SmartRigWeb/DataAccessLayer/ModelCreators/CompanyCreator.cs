using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class CompanyCreator : IModelCreator<Company>
    {
        public Company CreateModel(IDataReader reader)
        {
            return new Company
            {
                CompanyId = Convert.ToInt16(reader["CompanyId"]),
                CompanyName = Convert.ToString(reader["CompanyName"])
            };
        }
    }
}
