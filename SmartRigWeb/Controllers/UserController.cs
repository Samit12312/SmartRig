using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models;

namespace SmartRigWeb
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        RepositoryFactory repositoryFactory;
        public UserController()
        {
            repositoryFactory = new RepositoryFactory();
        }
        [HttpGet]
        public CatalogViewModel GetCatalog(int? minPrice = null, int? maxPrice = null, int? companyId = null, int? operatingSystemId = null, int? typeId = null)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();

            try
            {
                this.repositoryFactory.ConnectDbContext();

                catalogViewModel.Types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);
                catalogViewModel.Companies = this.repositoryFactory.CompanyRepository.GetAll();
                catalogViewModel.OperatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();

                // Initialize computer list
                List<Computer> computers = new List<Computer>();

                // Filters
                if (minPrice.HasValue && maxPrice.HasValue &&
                    !companyId.HasValue && !operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRange(minPrice.Value, maxPrice.Value);
                }
                else if (!minPrice.HasValue && !maxPrice.HasValue &&
                    companyId.HasValue && !operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyId(companyId.Value);
                }
                else if (!minPrice.HasValue && !maxPrice.HasValue &&
                    !companyId.HasValue && operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemId(operatingSystemId.Value);
                }
                else if (!minPrice.HasValue && !maxPrice.HasValue &&
                    !companyId.HasValue && !operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetComputerByType(typeId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    companyId.HasValue && !operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyId(minPrice.Value, maxPrice.Value, companyId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    !companyId.HasValue && operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemId(minPrice.Value, maxPrice.Value, operatingSystemId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    !companyId.HasValue && !operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndTypeId(minPrice.Value, maxPrice.Value, typeId.Value);
                }
                else if (!minPrice.HasValue && !maxPrice.HasValue &&
                    companyId.HasValue && operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndOperatingSystemId(companyId.Value, operatingSystemId.Value);
                }
                else if (!minPrice.HasValue && !maxPrice.HasValue &&
                    companyId.HasValue && !operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndTypeId(companyId.Value, typeId.Value);
                }
                else if (!minPrice.HasValue && !maxPrice.HasValue &&
                    !companyId.HasValue && operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemIdAndTypeId(operatingSystemId.Value, typeId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    companyId.HasValue && operatingSystemId.HasValue &&
                    !typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemId(minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    companyId.HasValue && !operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndTypeId(minPrice.Value, maxPrice.Value, companyId.Value, typeId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    !companyId.HasValue && operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemIdAndTypeId(minPrice.Value, maxPrice.Value, operatingSystemId.Value, typeId.Value);
                }
                else if (minPrice.HasValue && maxPrice.HasValue &&
                    companyId.HasValue && operatingSystemId.HasValue &&
                    typeId.HasValue)
                {
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemIdAndTypeId(minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value, typeId.Value);
                }
                else
                {
                    computers = this.repositoryFactory.ComputerRepository.GetAll();
                }

                // Map computers to ComputerCatalogViewModel with real specs
                catalogViewModel.Computers = new List<ComputerCatalogViewModel>();

                foreach (Computer computer in computers)
                {
                    // Load full objects
                    Ram ram = this.repositoryFactory.RamRepository.GetById(computer.RamId);
                    PowerSupply powerSupply = this.repositoryFactory.PowerSupplyRepository.GetById(computer.PowerSupplyId);
                    Models.Type type = this.repositoryFactory.TypeRepository.GetById(computer.ComputerTypeId);
                    Storage storage = this.repositoryFactory.StorageRepository.GetById(computer.StorageId);
                    Gpu gpu = this.repositoryFactory.GpuRepository.GetById(computer.GpuId);
                    MotherBoard motherboard = this.repositoryFactory.MotherBoardRepository.GetById(computer.MotherBoardId);
                    Models.OperatingSystem os = this.repositoryFactory.OperatingSystemRepository.GetById(computer.OperatingSystemId);
                    CpuFan cpuFan = this.repositoryFactory.CpuFanRepository.GetById(computer.CpuFanId);
                    Company company = this.repositoryFactory.CompanyRepository.GetById(computer.CompanyId);
                    Case computerCase = this.repositoryFactory.CaseRepository.GetById(computer.CaseId);
                    Cpu cpu = this.repositoryFactory.CpuRepository.GetById(computer.CpuId);

                    // Map to view model
                    ComputerCatalogViewModel computerCatalogViewModel = new ComputerCatalogViewModel();
                    computerCatalogViewModel.Id = computer.ComputerId;
                    computerCatalogViewModel.ComputerName = computer.ComputerName;
                    computerCatalogViewModel.ComputerPicture = computer.ComputerPicture;
                    computerCatalogViewModel.Cpu = cpu.CpuName;
                    computerCatalogViewModel.Gpu = gpu.GpuName;
                    computerCatalogViewModel.Ram = ram.RamName;
                    computerCatalogViewModel.Storage = storage.StorageName;
                    computerCatalogViewModel.OperatingSystem = os.OperatingSystemName;
                    computerCatalogViewModel.Price = computer.Price;

                    catalogViewModel.Computers.Add(computerCatalogViewModel);
                }

                return catalogViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        //public CatalogViewModel GetCatalog(int? minPrice = null, int? maxPrice = null, int? companyId = null, int? operatingSystemId = null, int? typeId = null)
        //{
        //    CatalogViewModel catalogViewModel = new CatalogViewModel();

        //    try
        //    {
        //        this.repositoryFactory.ConnectDbContext();

        //        catalogViewModel.types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);
        //        catalogViewModel.Companys = this.repositoryFactory.CompanyRepository.GetAll();
        //        catalogViewModel.operatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();

        //        // Initialize immediately
        //        List<Computer> computers = new List<Computer>();


        //        // Price Range Filter
        //        if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRange(minPrice.Value, maxPrice.Value);

        //        // Company Filter
        //        else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyId(companyId.Value);

        //        // Operating System Filter
        //        else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemId(operatingSystemId.Value);

        //        // Type Filter
        //        else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetComputerByType(typeId.Value);

        //        // Price + Company Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyId(minPrice.Value, maxPrice.Value, companyId.Value);

        //        // Price + Operating System Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemId(minPrice.Value, maxPrice.Value, operatingSystemId.Value);

        //        // Price + Type Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndTypeId(minPrice.Value, maxPrice.Value, typeId.Value);

        //        // Company + Operating System Filter
        //        else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndOperatingSystemId(companyId.Value, operatingSystemId.Value);

        //        // Company + Type Filter
        //        else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndTypeId(companyId.Value, typeId.Value);

        //        // Operating System + Type Filter
        //        else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemIdAndTypeId(operatingSystemId.Value, typeId.Value);

        //        // Price + Company + Operating System Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == false)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemId(minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value);

        //        // Price + Company + Type Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == false &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndTypeId(minPrice.Value, maxPrice.Value, companyId.Value, typeId.Value);

        //        // Price + Operating System + Type Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == false && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemIdAndTypeId(minPrice.Value, maxPrice.Value, operatingSystemId.Value, typeId.Value);

        //        // Price + Company + Operating System + Type Filter
        //        else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
        //            companyId.HasValue == true && operatingSystemId.HasValue == true &&
        //            typeId.HasValue == true)
        //            computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemIdAndTypeId(minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value, typeId.Value);
        //        // If no filters are applied, get all computers
        //        else
        //        {
        //            computers = this.repositoryFactory.ComputerRepository.GetAll();
        //        }

        //        catalogViewModel.Computers = computers;
        //        return catalogViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        this.repositoryFactory.DisconnectDb();
        //    }
        //}
        [HttpGet]
        public ComputerDetailsViewModel ComputerDetailsViewModel(int id)
        {
            ComputerDetailsViewModel cDVM = new ComputerDetailsViewModel();
            try
            {
                this.repositoryFactory.ConnectDbContext();
                Computer computer = this.repositoryFactory.ComputerRepository.GetById(id);
                Ram ram = this.repositoryFactory.RamRepository.GetById(computer.RamId);
                PowerSupply PS = this.repositoryFactory.PowerSupplyRepository.GetById(computer.PowerSupplyId);
                Models.Type type = this.repositoryFactory.TypeRepository.GetById(computer.ComputerTypeId);
                Storage storage = this.repositoryFactory.StorageRepository.GetById(computer.StorageId);
                Gpu Gpu = this.repositoryFactory.GpuRepository.GetById(computer.GpuId);
                MotherBoard motherboard = this.repositoryFactory.MotherBoardRepository.GetById(computer.MotherBoardId);
                Models.OperatingSystem OS = this.repositoryFactory.OperatingSystemRepository.GetById(computer.OperatingSystemId);
                CpuFan cpuFan = this.repositoryFactory.CpuFanRepository.GetById(computer.CpuFanId);
                Company company = this.repositoryFactory.CompanyRepository.GetById(computer.CompanyId);
                Case computerCase = this.repositoryFactory.CaseRepository.GetById(computer.CaseId);
                Cpu cpu = this.repositoryFactory.CpuRepository.GetById(computer.CpuId);


                cDVM.computer = computer;
                cDVM.type = type;
                cDVM.cpuFan = cpuFan;
                cDVM.cpu = cpu;
                cDVM.company = company;
                cDVM.gpu = Gpu;
                cDVM.computerCase = computerCase;
                cDVM.motherBoard = motherboard;
                cDVM.powerSupply = PS;
                cDVM.ram = ram;
                return cDVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        [HttpGet]
        public List<CartComputer> GetCart(int userId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetCartById(userId); // unpaid cart
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CartComputer>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        [HttpGet]
        public List<CartComputer> GetOrdersHistory(int userId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetOrdersByUserId(userId); // IsPayed = true
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CartComputer>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }


        [HttpPost]
        public bool UpdateProfile(User user)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.UserRepository.Update(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
    }
}
