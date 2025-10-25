using Models;
using System;
using System.Data;

namespace SmartRigWeb
{
    public class MotherBoardCreator : IModelCreator<MotherBoard>
    {
        public MotherBoard CreateModel(IDataReader reader)
        {
            return new MotherBoard
            {
                MotherBoardId = Convert.ToInt16(reader["MotherBoardId"]),
                MotherBoardName = Convert.ToString(reader["MotherBoardName"]),
                MotherBoardPrice = Convert.ToInt16(reader["MotherBoardPrice"]),
                MotherBoardCompanyId = Convert.ToInt16(reader["MotherBoardCompanyId"])
            };
        }
    }
}
