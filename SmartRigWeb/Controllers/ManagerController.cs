using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ManageComponentsViewModel GetAllManageComponents()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                ManageComponentsViewModel vm = new ManageComponentsViewModel();

                List<Cpu> cpus = this.repositoryFactory.CpuRepository.GetAll();
                foreach (Cpu cpu in cpus)
                {
                    CpuManageViewModel item = new CpuManageViewModel();
                    item.cpu = cpu;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(cpu.CpuCompanyId);
                    vm.cpus.Add(item);
                }

                List<Gpu> gpus = this.repositoryFactory.GpuRepository.GetAll();
                foreach (Gpu gpu in gpus)
                {
                    GpuManageViewModel item = new GpuManageViewModel();
                    item.gpu = gpu;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(gpu.GpuCompanyId);
                    vm.gpus.Add(item);
                }

                List<Ram> rams = this.repositoryFactory.RamRepository.GetAll();
                foreach (Ram ram in rams)
                {
                    RamManageViewModel item = new RamManageViewModel();
                    item.ram = ram;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(ram.RamCompanyId);
                    item.type = this.repositoryFactory.TypeRepository.GetById(ram.RamTypeId);
                    vm.rams.Add(item);
                }

                List<Storage> storages = this.repositoryFactory.StorageRepository.GetAll();
                foreach (Storage storage in storages)
                {
                    StorageManageViewModel item = new StorageManageViewModel();
                    item.storage = storage;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(storage.StorageCompanyId);
                    item.type = this.repositoryFactory.TypeRepository.GetById(storage.StorageType);
                    vm.storages.Add(item);
                }

                List<MotherBoard> motherBoards = this.repositoryFactory.MotherBoardRepository.GetAll();
                foreach (MotherBoard motherBoard in motherBoards)
                {
                    MotherBoardManageViewModel item = new MotherBoardManageViewModel();
                    item.motherBoard = motherBoard;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(motherBoard.MotherBoardCompanyId);
                    vm.motherBoards.Add(item);
                }

                List<CpuFan> cpuFans = this.repositoryFactory.CpuFanRepository.GetAll();
                foreach (CpuFan cpuFan in cpuFans)
                {
                    CpuFanManageViewModel item = new CpuFanManageViewModel();
                    item.cpuFan = cpuFan;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(cpuFan.CpuFanCompanyId);
                    vm.cpuFans.Add(item);
                }

                List<PowerSupply> powerSupplies = this.repositoryFactory.PowerSupplyRepository.GetAll();
                foreach (PowerSupply powerSupply in powerSupplies)
                {
                    PowerSupplyManageViewModel item = new PowerSupplyManageViewModel();
                    item.powerSupply = powerSupply;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(powerSupply.PowerSupplyCompanyId);
                    vm.powerSupplies.Add(item);
                }

                List<Case> cases = this.repositoryFactory.CaseRepository.GetAll();
                foreach (Case computerCase in cases)
                {
                    CaseManageViewModel item = new CaseManageViewModel();
                    item.computerCase = computerCase;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(computerCase.CaseCompanyId);
                    vm.cases.Add(item);
                }

                List<Models.OperatingSystem> operatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();
                foreach (Models.OperatingSystem operatingSystem in operatingSystems)
                {
                    OperatingSystemManageViewModel item = new OperatingSystemManageViewModel();
                    item.operatingSystem = operatingSystem;
                    item.company = this.repositoryFactory.CompanyRepository.GetById(operatingSystem.OperatingSystemCompanyId);
                    vm.operatingSystems.Add(item);
                }

                return vm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ManageComponentsViewModel();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
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
        // Cpu section
        [HttpPost]
        public bool AddCpu([FromBody] Cpu cpu)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.CpuRepository.Create(cpu);
                return true;
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
        public bool EditCpu([FromBody] Cpu cpu)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CpuRepository.Update(cpu);
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
        public bool RemoveCpu(string cpuId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CpuRepository.Delete(cpuId);
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

        

        // Adding/Editing/Removing Stuff
        /// <summary>
        /// Adding/Editing/Removing components and computers is done by sending the component/computer object in the request body.
        /// For removing,
        /// only the id is sent as a query parameter.
        /// The methods return true if the operation was successful,
        /// false otherwise.
        /// </summary>
        /// <param name="gpu"></param>
        /// <returns></returns>
        // Gpu section
        [HttpPost]
        public bool AddGpu([FromBody] Gpu gpu)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.GpuRepository.Create(gpu);
                return true;
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
        public bool EditGpu([FromBody] Gpu gpu)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.GpuRepository.Update(gpu);
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
        public bool RemoveGpu(string gpuId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.GpuRepository.Delete(gpuId);
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

        // Ram section
        [HttpPost]
        public bool AddRam([FromBody] Ram ram)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.RamRepository.Create(ram);
                return true;
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
        public bool EditRam([FromBody] Ram ram)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.RamRepository.Update(ram);
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
        public bool RemoveRam(string ramId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.RamRepository.Delete(ramId);
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

        // Storage section
        [HttpPost]
        public bool AddStorage([FromBody] Storage storage)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.StorageRepository.Create(storage);
                return true;
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
        public bool EditStorage([FromBody] Storage storage)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.StorageRepository.Update(storage);
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
        public bool RemoveStorage(string storageId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.StorageRepository.Delete(storageId);
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

        // MotherBoard section
        [HttpPost]
        public bool AddMotherBoard([FromBody] MotherBoard motherBoard)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.MotherBoardRepository.Create(motherBoard);
                return true;
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
        public bool EditMotherBoard([FromBody] MotherBoard motherBoard)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.MotherBoardRepository.Update(motherBoard);
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
        public bool RemoveMotherBoard(string motherBoardId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.MotherBoardRepository.Delete(motherBoardId);
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

        // PowerSupply section
        [HttpPost]
        public bool AddPowerSupply([FromBody] PowerSupply powerSupply)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.PowerSupplyRepository.Create(powerSupply);
                return true;
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
        public bool EditPowerSupply([FromBody] PowerSupply powerSupply)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.PowerSupplyRepository.Update(powerSupply);
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
        public bool RemovePowerSupply(string powerSupplyId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.PowerSupplyRepository.Delete(powerSupplyId);
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

        // OperatingSystem section
        [HttpPost]
        public bool AddOperatingSystem([FromBody] Models.OperatingSystem operatingSystem)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.OperatingSystemRepository.Create(operatingSystem);
                return true;
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
        public bool EditOperatingSystem([FromBody] Models.OperatingSystem operatingSystem)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.OperatingSystemRepository.Update(operatingSystem);
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
        public bool RemoveOperatingSystem(string operatingSystemId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.OperatingSystemRepository.Delete(operatingSystemId);
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
        public bool AddCase([FromBody] Models.Case caseItem)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.CaseRepository.Create(caseItem);
                return true;
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
        public bool EditCase([FromBody] Models.Case caseItem)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CaseRepository.Update(caseItem);
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

        // Case remove
        [HttpGet]
        public bool RemoveCase(string caseId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CaseRepository.Delete(caseId);
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
         //CpuFan Section
        [HttpPost]
        public bool AddCpuFan([FromBody] CpuFan cpuFan)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                this.repositoryFactory.CpuFanRepository.Create(cpuFan);
                return true;
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
        public bool EditCpuFan([FromBody] CpuFan cpuFan)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CpuFanRepository.Update(cpuFan);
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
        public bool RemoveCpuFan(string cpuFanId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CpuFanRepository.Delete(cpuFanId);
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
                user.UserId = data.user.UserId;
                user.UserName = data.user.UserName;
                user.UserEmail = data.user.UserEmail;
                user.UserAddress = data.user.UserAddress;
                user.UserPhoneNumber = data.user.UserPhoneNumber;
                user.CityId = data.user.CityId;
                user.Manager = data.user.Manager;

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
        [HttpGet]
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

        [HttpGet]
        public bool DeleteOrder(int cartId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.Delete(cartId.ToString());
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
        public List<Cart> GetAllOrders()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Cart>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
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

        // Get functions for SomeStuff 

        // TOO !!! MUCH !!! STUFF !!!! 

        // WHY DID I THINK THIS WAS A GOOD IDEA !?

        // I should have just made a separate controller for each entity, but nooooo, I had to put everything in one controller, now look at this mess

        // I mean, at least I can get all the data I need for the admin panel in one request, but still, this is a mess

        // I should have at least made a separate view model for each entity, but nooooo, I had to put everything in one view model, now look at this mess

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
        [HttpGet]
        public List<Models.Type> GetRamTypes()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.TypeRepository.GetAllByTypeCode(2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Models.Type>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Models.Type> GetStorageTypes()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.TypeRepository.GetAllByTypeCode(3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Models.Type>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
        [HttpGet]
        public List<Cities> GetAllCities()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CitiesRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Cities>();
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }
    }
}
