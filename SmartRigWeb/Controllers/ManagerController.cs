using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models;
using Models.ViewModels;
using System.Text.Json;

namespace SmartRigWeb
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ManagerController : ControllerBase
    {
        RepositoryFactory repositoryFactory;

        public ManagerController()
        {
            this.repositoryFactory = new RepositoryFactory();
        }
        [HttpPost]
        public bool AddComputer( )
        {
            string jsonData = Request.Form["data"];
            Computer data = JsonSerializer.Deserialize<Computer>(jsonData);
            IFormFile file = Request.Form.Files[0];    
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.OpenTransaction();
                this.repositoryFactory.ComputerRepository.Create(data);
                data.ComputerPicture = this.repositoryFactory.ComputerRepository.GetLastComputerId() + data.ComputerPicture;
                this.repositoryFactory.ComputerRepository.Update(data);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/computers", data.ComputerPicture);


                this.repositoryFactory.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.repositoryFactory.Rollback();
                return false;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        [HttpGet]
        public NewComputerViewModel GetNewComputerViewModel()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                NewComputerViewModel vm = new NewComputerViewModel();

                vm.Cpus = this.repositoryFactory.CpuRepository.GetAll();

                vm.Gpus = this.repositoryFactory.GpuRepository.GetAll();

                vm.Rams = this.repositoryFactory.RamRepository.GetAll();

                vm.Storages = this.repositoryFactory.StorageRepository.GetAll();

                vm.Motherboards = this.repositoryFactory.MotherBoardRepository.GetAll();

                vm.PowerSupplies = this.repositoryFactory.PowerSupplyRepository.GetAll();

                vm.Fans = this.repositoryFactory.CpuFanRepository.GetAll();

                vm.Cases = this.repositoryFactory.CaseRepository.GetAll();

                vm.Types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);

                vm.Companies = this.repositoryFactory.CompanyRepository.GetAll();

                vm.OS = this.repositoryFactory.OperatingSystemRepository.GetAll();

                return vm;
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
        [HttpPost]
        public bool EditComputer(Computer computer)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.ComputerRepository.Update(computer);
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
        [HttpGet]
        public bool RemoveComputer(string computerId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.ComputerRepository.Delete(computerId);
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
        [HttpPost]
        public bool ChangeCartStatus(int cartId, bool isPayed)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.UpdateCartStatus(cartId, isPayed);
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
        [HttpGet]
        public List<Computer> GetAllComputers()
        {
            //List< ComputerDetailsViewModel> computerDetailsViewModels = new List<ComputerDetailsViewModel>();
            //try
            //{
            //    this.repositoryFactory.ConnectDbContext();
            //    Computer computer = this.repositoryFactory.ComputerRepository.GetAll();
            //    Ram ram = this.repositoryFactory.RamRepository.GetById(computer.RamId);
            //    PowerSupply PS = this.repositoryFactory.PowerSupplyRepository.GetById(computer.PowerSupplyId);
            //    Models.Type type = this.repositoryFactory.TypeRepository.GetById(computer.ComputerTypeId);
            //    Storage storage = this.repositoryFactory.StorageRepository.GetById(computer.StorageId);
            //    Gpu Gpu = this.repositoryFactory.GpuRepository.GetById(computer.GpuId);
            //    MotherBoard motherboard = this.repositoryFactory.MotherBoardRepository.GetById(computer.MotherBoardId);
            //    Models.OperatingSystem OS = this.repositoryFactory.OperatingSystemRepository.GetById(computer.OperatingSystemId);
            //    CpuFan cpuFan = this.repositoryFactory.CpuFanRepository.GetById(computer.CpuFanId);
            //    Company company = this.repositoryFactory.CompanyRepository.GetById(computer.CompanyId);
            //    Case computerCase = this.repositoryFactory.CaseRepository.GetById(computer.CaseId);
            //    Cpu cpu = this.repositoryFactory.CpuRepository.GetById(computer.CpuId);

            //    computerDetailsViewModels.computer = computer;
            //    computerDetailsViewModels.type = type;
            //    computerDetailsViewModels.cpuFan = cpuFan;
            //    computerDetailsViewModels.operatingSystem = OS;
            //    computerDetailsViewModels.storage = storage;
            //    computerDetailsViewModels.cpu = cpu;
            //    computerDetailsViewModels.company = company;
            //    computerDetailsViewModels.gpu = Gpu;
            //    computerDetailsViewModels.computerCase = computerCase;
            //    computerDetailsViewModels.motherBoard = motherboard;
            //    computerDetailsViewModels.powerSupply = PS;
            //    computerDetailsViewModels.ram = ram;
            //    return computerDetailsViewModels;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return null;
            //}
            //finally
            //{
            //    this.repositoryFactory.DisconnectDb();
            //}
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.ComputerRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Computer>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        [HttpGet]
        public List<CartComputer> GetOrderById(int userId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetOrdersByUserId(userId); 
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
        //[HttpGet]
        //public List<CartComputer> GetAllOrders()
        //{
        //    try
        //    {
        //        this.repositoryFactory.ConnectDbContext();
        //        return this.repositoryFactory.CartRepository.GetAllOrders();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new List<CartComputer>();
        //    }
        //    finally
        //    {
        //        this.repositoryFactory.DisconnectDb();
        //    }
        //}
        [HttpGet]
        public CartComputer GetMostSoldComputer()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetMostSoldComputer();
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
        public decimal GetTotalRevenue(string fromDate, string toDate)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetTotalRevenue(fromDate, toDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<User> GetAllUsers()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.UserRepository.GetAll();
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
    }
}
