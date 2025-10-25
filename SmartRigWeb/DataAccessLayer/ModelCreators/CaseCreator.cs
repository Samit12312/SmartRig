using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class CaseCreator : IModelCreator<Case>
    {
        public Case CreateModel(IDataReader reader)
        {
            return new Case
            {
                CaseId = Convert.ToInt16(reader["CaseId"]),
                CaseName = Convert.ToString(reader["CaseName"]),
                CasePrice = Convert.ToInt16(reader["CasePrice"]),
                CaseCompanyId = Convert.ToInt16(reader["CaseCompanyId"])
            };
        }
    }
}
