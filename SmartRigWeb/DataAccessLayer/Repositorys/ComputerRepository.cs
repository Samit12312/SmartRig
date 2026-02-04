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
                           (ComputerName, ComputerTypeId, CompanyId, StorageId, RamId, CpuId, GpuId, Price, 
                            OperatingSystemId, CaseId, PowerSupplyId, CpuFanId, MotherBoardId, ComputerPicture)
                           VALUES (@ComputerName, @ComputerTypeId, @CompanyId, @StorageId, @RamId, @CpuId, 
                                   @GpuId, @Price, @OperatingSystemId, @CaseId, @PowerSupplyId, @CpuFanId, 
                                   @MotherBoardId, @ComputerPicture)";

            this.dbContext.AddParameter("@ComputerName", item.ComputerName);
            this.dbContext.AddParameter("@ComputerTypeId", item.ComputerTypeId.ToString());
            this.dbContext.AddParameter("@CompanyId", item.CompanyId.ToString());
            this.dbContext.AddParameter("@StorageId", item.StorageId.ToString());
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
            string sql = @"SELECT * FROM [Computer]";
            return GetComputers(sql);
        }
        public List<Computer> GetComputerByType(int TypeId)
        {
            string sql = $@"SELECT Computer.ComputerId, Computer.ComputerName, Computer.ComputerTypeId, Computer.CompanyId, Computer.StorageId, Computer.RamId, Computer.CpuId, Computer.GpuId, Computer.Price, Computer.OperatingSystemId, Computer.CaseId, Computer.PowerSupplyId, Computer.CpuFanId, Computer.MotherBoardId, Computer.ComputerPicture
                    FROM Type INNER JOIN Computer ON (Type.TypeId = Computer.ComputerTypeId) AND (Type.TypeId = Computer.ComputerTypeId)
                    WHERE Computer.ComputerTypeId=@TypeId;";
            this.dbContext.AddParameter("@TypeId", TypeId.ToString()); // Add parameter for TypeId
            return GetComputers(sql);
        }
        public List<Computer> GetComputersByCompanyId(int CompanyId)
        {
            string sql = @"SELECT ComputerId, ComputerName, ComputerTypeId, CompanyId,
                          StorageId, RamId, CpuId, GpuId, Price, OperatingSystemId,
                          CaseId, PowerSupplyId, CpuFanId, MotherBoardId, ComputerPicture
                   FROM Computer
                   WHERE CompanyId = @CompanyId;";

            this.dbContext.AddParameter("@CompanyId", CompanyId.ToString());
            return GetComputers(sql);
        }

        public List<Computer> GetComputersByOperatingSystemId(int OperatingSystemId)
        {
            string sql = @"SELECT Computer.ComputerId, Computer.ComputerName, Computer.ComputerTypeId, Computer.CompanyId, Computer.StorageId, Computer.RamId, Computer.CpuId, Computer.GpuId, Computer.Price, Computer.OperatingSystemId, Computer.CaseId, Computer.PowerSupplyId, Computer.CpuFanId, Computer.MotherBoardId, Computer.ComputerPicture
                   FROM OperatingSystem INNER JOIN Computer 
                   ON OperatingSystem.OperatingSystemId = Computer.OperatingSystemId
                   WHERE Computer.OperatingSystemId = @OperatingSystemId;";

            this.dbContext.AddParameter("@OperatingSystemId", OperatingSystemId.ToString()); // Add parameter for OperatingSystemId
            return GetComputers(sql);
        }
        public List<Computer> GetComputersByCartId(int cartId)
        {
            string sql = @"
        SELECT
            Computer.ComputerId,
            Computer.ComputerName,
            Computer.ComputerTypeId,
            Computer.CompanyId,
            Computer.StorageId,
            Computer.RamId,
            Computer.CpuId,
            Computer.GpuId,
            Computer.Price,
            Computer.OperatingSystemId,
            Computer.CaseId,
            Computer.PowerSupplyId,
            Computer.CpuFanId,
            Computer.MotherBoardId,
            Computer.ComputerPicture
        FROM Computer
        INNER JOIN CartComputer
            ON Computer.ComputerId = CartComputer.ComputerId
        WHERE CartComputer.CartId = @CartId;
    ";

            this.dbContext.AddParameter("@CartId", cartId.ToString());
            return GetComputers(sql);
        }


        public List<Computer> GetComputers(string sql)
        {
            List<Computer> list = new List<Computer>();
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelsFactory.ComputerCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Computer> GetByPriceRange(int minPrice, int maxPrice)
        {
            string sql = @"SELECT * FROM [Computer]
                   WHERE Price >= @MinPrice AND Price <= @MaxPrice";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());

            return GetComputers(sql);
        }
        public List<Computer> GetByPriceAscending() // from lowest to hightest
        {
            string sql = @"SELECT * FROM [Computer] ORDER BY Price ASC";
            return GetComputers(sql);
        }
        public List<Computer> GetByPriceDescending()// from highest to lowest
        {
            string sql = @"SELECT * FROM [Computer] ORDER BY Price DESC";
            return GetComputers(sql);
        }


        public Computer GetById(int id)
        {
            string sql = @"SELECT * FROM [Computer] WHERE ComputerId = @ComputerId";
            this.dbContext.AddParameter("@ComputerId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.modelsFactory.ComputerCreator.CreateModel(reader);
            }
        }
        public List<Computer> GetComputersByCompanyIdAndTypeId(int companyId, int typeId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE CompanyId = @CompanyId
                   AND ComputerTypeId = @TypeId";

            this.dbContext.AddParameter("@CompanyId", companyId.ToString());
            this.dbContext.AddParameter("@TypeId", typeId.ToString());

            return GetComputers(sql);
        }

        public List<Computer> GetComputersByCompanyIdAndOperatingSystemId(int companyId, int operatingSystemId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE CompanyId = @CompanyId
                   AND OperatingSystemId = @OperatingSystemId";

            this.dbContext.AddParameter("@CompanyId", companyId.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", operatingSystemId.ToString());

            return GetComputers(sql);
        }
        public List<Computer> GetByPriceRangeAndTypeId(int minPrice, int maxPrice, int typeId)
        {
            string sql = @"SELECT * FROM [Computer]
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND ComputerTypeId = @TypeId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@TypeId", typeId.ToString());

            return GetComputers(sql);
        }
        public List<Computer> GetByPriceRangeAndOperatingSystemId(int minPrice, int maxPrice, int operatingSystemId)
        {
            string sql = @"SELECT * FROM [Computer]
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND OperatingSystemId = @OperatingSystemId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", operatingSystemId.ToString());

            return GetComputers(sql);
        }
        public List<Computer> GetByPriceRangeAndCompanyId(int minPrice, int maxPrice, int companyId)
        {
            string sql = @"SELECT * FROM [Computer]
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND CompanyId = @CompanyId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@CompanyId", companyId.ToString());

            return GetComputers(sql);
        }

        public List<Computer> GetComputersByOperatingSystemIdAndTypeId(int operatingSystemId, int typeId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE OperatingSystemId = @OperatingSystemId
                   AND ComputerTypeId = @TypeId";

            this.dbContext.AddParameter("@OperatingSystemId", operatingSystemId.ToString());
            this.dbContext.AddParameter("@TypeId", typeId.ToString());

            return GetComputers(sql);
        }

        public List<Computer> GetByPriceRangeAndCompanyIdAndOperatingSystemId(int minPrice, int maxPrice, int companyId, int operatingSystemId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND CompanyId = @CompanyId
                   AND OperatingSystemId = @OperatingSystemId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@CompanyId", companyId.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", operatingSystemId.ToString());

            return GetComputers(sql);
        }

        public List<Computer> GetByPriceRangeAndCompanyIdAndTypeId(int minPrice, int maxPrice, int companyId, int typeId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND CompanyId = @CompanyId
                   AND ComputerTypeId = @TypeId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@CompanyId", companyId.ToString());
            this.dbContext.AddParameter("@TypeId", typeId.ToString());

            return GetComputers(sql);
        }

        public List<Computer> GetByPriceRangeAndOperatingSystemIdAndTypeId(int minPrice, int maxPrice, int operatingSystemId, int typeId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND OperatingSystemId = @OperatingSystemId
                   AND ComputerTypeId = @TypeId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", operatingSystemId.ToString());
            this.dbContext.AddParameter("@TypeId", typeId.ToString());

            return GetComputers(sql);
        }

        public List<Computer> GetByPriceRangeAndCompanyIdAndOperatingSystemIdAndTypeId(
        int minPrice, int maxPrice, int companyId, int operatingSystemId, int typeId)
        {
            string sql = @"SELECT * FROM Computer
                   WHERE Price >= @MinPrice 
                   AND Price <= @MaxPrice
                   AND CompanyId = @CompanyId
                   AND OperatingSystemId = @OperatingSystemId
                   AND ComputerTypeId = @TypeId";

            this.dbContext.AddParameter("@MinPrice", minPrice.ToString());
            this.dbContext.AddParameter("@MaxPrice", maxPrice.ToString());
            this.dbContext.AddParameter("@CompanyId", companyId.ToString());
            this.dbContext.AddParameter("@OperatingSystemId", operatingSystemId.ToString());
            this.dbContext.AddParameter("@TypeId", typeId.ToString());

            return GetComputers(sql);
        }

        public bool Update(Computer item)
        {
            string sql = @"UPDATE [Computer]
                           SET ComputerName = @ComputerName,
                               ComputerTypeId = @ComputerTypeId,
                               CompanyId = @CompanyId,
                               StorageId = @StorageId,
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
            this.dbContext.AddParameter("@StorageId", item.StorageId.ToString());
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
