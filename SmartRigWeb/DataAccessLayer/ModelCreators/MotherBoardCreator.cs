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
                MotherBoardId = Convert.ToInt32(reader["MotherBoardId"]),
                MotherBoardName = Convert.ToString(reader["MotherBoardName"]),
                MotherBoardPrice = Convert.ToInt32(reader["MotherBoardPrice"]),
                MotherBoardCompanyId = Convert.ToInt32(reader["MotherBoardCompanyId"])
            };
        }
    }
}
