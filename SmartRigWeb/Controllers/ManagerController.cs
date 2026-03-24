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
                string format = data.ComputerPicture;
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.OpenTransaction();
                this.repositoryFactory.ComputerRepository.Create(data);
                int newComputerId = this.repositoryFactory.ComputerRepository.GetLastComputerId();
                data.ComputerPicture = newComputerId + format;
                data.ComputerId = newComputerId;
                this.repositoryFactory.ComputerRepository.Update(data);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Computers", data.ComputerPicture);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


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
        public bool EditComputer([FromForm] string data, IFormFile file)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                Computer computer = JsonSerializer.Deserialize<Computer>(data, options);

                this.repositoryFactory.ConnectDbContext();

                if (file != null)
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Computers",
                        computer.ComputerId + ".jpg");
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                }

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
        [HttpPost]
        public bool EditUser([FromBody] EditUserViewModel data)
        {
            try
            {
                User user = new User();
                user.UserId = data.UserId;
                user.UserName = data.UserName;
                user.UserEmail = data.UserEmail;
                user.UserAddress = data.UserAddress;
                user.UserPhoneNumber = data.UserPhoneNumber;
                user.CityId = data.CityId;
                user.Manager = data.Manager;

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
        [HttpGet]
        public bool RemoveComputer(string computerId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                bool result = this.repositoryFactory.ComputerRepository.DeleteWithCartItems(computerId);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting computer: {ex.Message}");
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
        public List<ComputersViewModel> GetAllComputers()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                List<Computer> computers = this.repositoryFactory.ComputerRepository.GetAll();
                List<Cpu> cpus = this.repositoryFactory.CpuRepository.GetAll();
                List<Gpu> gpus = this.repositoryFactory.GpuRepository.GetAll();
                List<Ram> rams = this.repositoryFactory.RamRepository.GetAll();
                List<Storage> storages = this.repositoryFactory.StorageRepository.GetAll();
                List<MotherBoard> motherboards = this.repositoryFactory.MotherBoardRepository.GetAll();
                List<PowerSupply> powerSupplies = this.repositoryFactory.PowerSupplyRepository.GetAll();
                List<CpuFan> fans = this.repositoryFactory.CpuFanRepository.GetAll();
                List<Case> cases = this.repositoryFactory.CaseRepository.GetAll();
                List<Company> companies = this.repositoryFactory.CompanyRepository.GetAll();
                List<Models.OperatingSystem> operatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();
                List<Models.Type> types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);

                List<ComputersViewModel> result = new List<ComputersViewModel>();

                foreach (Computer c in computers)
                {
                    ComputersViewModel vm = new ComputersViewModel();
                    vm.computer = c;
                    vm.cpu = this.repositoryFactory.CpuRepository.GetById(c.CpuId);
                    vm.gpu = this.repositoryFactory.GpuRepository.GetById(c.GpuId);
                    vm.ram = this.repositoryFactory.RamRepository.GetById(c.RamId);
                    vm.storage = this.repositoryFactory.StorageRepository.GetById(c.StorageId);
                    vm.motherBoard = this.repositoryFactory.MotherBoardRepository.GetById(c.MotherBoardId);
                    vm.powerSupply = this.repositoryFactory.PowerSupplyRepository.GetById(c.PowerSupplyId);
                    vm.cpuFan = this.repositoryFactory.CpuFanRepository.GetById(c.CpuFanId);
                    vm.computerCase = this.repositoryFactory.CaseRepository.GetById(c.CaseId);
                    vm.company = this.repositoryFactory.CompanyRepository.GetById(c.CompanyId);
                    vm.operatingSystem = this.repositoryFactory.OperatingSystemRepository.GetById(c.OperatingSystemId);
                    vm.type = this.repositoryFactory.TypeRepository.GetById(c.ComputerTypeId);
                    result.Add(vm);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ComputersViewModel>();
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
        [HttpGet]
        public List<Company> GetAllCompanies()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CompanyRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Company>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Cpu> GetAllCpus()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CpuRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Cpu>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Gpu> GetAllGpus()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.GpuRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Gpu>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Ram> GetAllRams()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.RamRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Ram>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Storage> GetAllStorages()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.StorageRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Storage>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<CpuFan> GetAllCpuFans()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CpuFanRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CpuFan>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        [HttpGet]
        public List<PowerSupply> GetAllPowerSupplies()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.PowerSupplyRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PowerSupply>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<MotherBoard> GetAllMotherBoards()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.MotherBoardRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<MotherBoard>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Models.OperatingSystem> GetAllOperatingSystems()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.OperatingSystemRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Models.OperatingSystem>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
    }
}
