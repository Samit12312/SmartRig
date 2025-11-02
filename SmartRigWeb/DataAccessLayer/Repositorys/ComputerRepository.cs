using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartRigWeb
{
    public class ComputerRepository : Repository, IRepository<Computer>
    {
        public ComputerRepository(OleDbConext dbContext, ModelsFactory modelsFactory)
            : base(dbContext, modelsFactory)
        {
        }

        // method to create a new Computer entry in the database
        public bool Create(Computer item)
        {
            string sql = @"INSERT INTO [Computer] 
                           (ComputerName, ComputerTypeId, CompanyId, StroageId, RamId, CpuId, GpuId, Price, 
                            OperatingSystemId, CaseId, PowerSupplyId, CpuFanId, MotherBoardId, ComputerPicture)
                           VALUES (@ComputerName, @ComputerTypeId, @CompanyId, @StroageId, @RamId, @CpuId, 
                                   @GpuId, @Price, @OperatingSystemId, @CaseId, @PowerSupplyId, @CpuFanId, 
                                   @MotherBoardId, @ComputerPicture)";

            this.dbContext.AddParameter("@ComputerName", item.ComputerName);
            this.dbContext.AddParameter("@ComputerTypeId", item.ComputerTypeId.ToString());
            this.dbContext.AddParameter("@CompanyId", item.CompanyId.ToString());
            this.dbContext.AddParameter("@StroageId", item.StroageId.ToString());
            this.dbContext.AddParameter("@RamId", item.RamId.ToString());
            this.dbContext.AddParameter("@CpuId", item.CpuId.ToString());
            this.dbContext.AddParameter("@GpuId", item.GpuId.ToString());
            this.dbContext.AddParameter("@Price", item.Price.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", item.OperatingSystemId.ToString());
            this.dbContext.AddParameter("@CaseId", item.CaseId.ToString());
            this.dbContext.AddParameter("@PowerSupplyId", item.PowerSupplyId.ToString());
            this.dbContext.AddParameter("@CpuFanId", item.CpuFanId.ToString());
            this.dbContext.AddParameter("@MotherBoardId", item.MotherBoardId.ToString());
            this.dbContext.AddParameter("@ComputerPicture", item.ComputerPicture);

            return this.dbContext.Insert(sql) > 0;
        }

        // method to delete a Computer record based on its ID
        public bool Delete(string Id)
        {
            string sql = @"DELETE FROM [Computer] WHERE ComputerId = @ComputerId";
            this.dbContext.AddParameter("@ComputerId", Id);
            return this.dbContext.Insert(sql) > 0;
        }

        // method to get all computers from the database
        public List<Computer> GetAll()
        {
            List<Computer> list = new List<Computer>();
            string sql = @"SELECT * FROM [Computer]";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    Computer computer = new Computer
                    {
                        ComputerId = Convert.ToInt32(reader["ComputerId"]),
                        ComputerName = reader["ComputerName"].ToString(),
                        ComputerTypeId = Convert.ToInt32(reader["ComputerTypeId"]),
                        CompanyId = Convert.ToInt32(reader["CompanyId"]),
                        StroageId = Convert.ToInt32(reader["StroageId"]),
                        RamId = Convert.ToInt32(reader["RamId"]),
                        CpuId = Convert.ToInt32(reader["CpuId"]),
                        GpuId = Convert.ToInt32(reader["GpuId"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        OperatingSystemId = Convert.ToInt32(reader["OperatingSystemId"]),
                        CaseId = Convert.ToInt32(reader["CaseId"]),
                        PowerSupplyId = Convert.ToInt32(reader["PowerSupplyId"]),
                        CpuFanId = Convert.ToInt32(reader["CpuFanId"]),
                        MotherBoardId = Convert.ToInt32(reader["MotherBoardId"]),
                        ComputerPicture = reader["ComputerPicture"].ToString()
                    };
                    list.Add(computer);
                }
            }
            return list;
        }
        public Computer GetById(int id)
        {
            string sql = @"SELECT * FROM [Computer] WHERE ComputerId = @ComputerId";
            this.dbContext.AddParameter("@ComputerId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return new Computer
                {
                    ComputerId = Convert.ToInt32(reader["ComputerId"]),
                    ComputerName = reader["ComputerName"].ToString(),
                    ComputerTypeId = Convert.ToInt32(reader["ComputerTypeId"]),
                    CompanyId = Convert.ToInt32(reader["CompanyId"]),
                    StroageId = Convert.ToInt32(reader["StroageId"]),
                    RamId = Convert.ToInt32(reader["RamId"]),
                    CpuId = Convert.ToInt32(reader["CpuId"]),
                    GpuId = Convert.ToInt32(reader["GpuId"]),
                    Price = Convert.ToInt32(reader["Price"]),
                    OperatingSystemId = Convert.ToInt32(reader["OperatingSystemId"]),
                    CaseId = Convert.ToInt32(reader["CaseId"]),
                    PowerSupplyId = Convert.ToInt32(reader["PowerSupplyId"]),
                    CpuFanId = Convert.ToInt32(reader["CpuFanId"]),
                    MotherBoardId = Convert.ToInt32(reader["MotherBoardId"]),
                    ComputerPicture = reader["ComputerPicture"].ToString()
                };
            }
        }

        public bool Update(Computer item)
        {
            string sql = @"UPDATE [Computer]
                           SET ComputerName = @ComputerName,
                               ComputerTypeId = @ComputerTypeId,
                               CompanyId = @CompanyId,
                               StroageId = @StroageId,
                               RamId = @RamId,
                               CpuId = @CpuId,
                               GpuId = @GpuId,
                               Price = @Price,
                               OperatingSystemId = @OperatingSystemId,
                               CaseId = @CaseId,
                               PowerSupplyId = @PowerSupplyId,
                               CpuFanId = @CpuFanId,
                               MotherBoardId = @MotherBoardId,
                               ComputerPicture = @ComputerPicture
                           WHERE ComputerId = @ComputerId";

            this.dbContext.AddParameter("@ComputerName", item.ComputerName);
            this.dbContext.AddParameter("@ComputerTypeId", item.ComputerTypeId.ToString());
            this.dbContext.AddParameter("@CompanyId", item.CompanyId.ToString());
            this.dbContext.AddParameter("@StroageId", item.StroageId.ToString());
            this.dbContext.AddParameter("@RamId", item.RamId.ToString());
            this.dbContext.AddParameter("@CpuId", item.CpuId.ToString());
            this.dbContext.AddParameter("@GpuId", item.GpuId.ToString());
            this.dbContext.AddParameter("@Price", item.Price.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", item.OperatingSystemId.ToString());
            this.dbContext.AddParameter("@CaseId", item.CaseId.ToString());
            this.dbContext.AddParameter("@PowerSupplyId", item.PowerSupplyId.ToString());
            this.dbContext.AddParameter("@CpuFanId", item.CpuFanId.ToString());
            this.dbContext.AddParameter("@MotherBoardId", item.MotherBoardId.ToString());
            this.dbContext.AddParameter("@ComputerPicture", item.ComputerPicture);
            this.dbContext.AddParameter("@ComputerId", item.ComputerId.ToString());

            return this.dbContext.Update(sql) > 0;
        }
    }
}
