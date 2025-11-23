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

                catalogViewModel.types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);
                catalogViewModel.Companys = this.repositoryFactory.CompanyRepository.GetAll();
                catalogViewModel.operatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();

                // Initialize immediately
                List<Computer> computers = new List<Computer>();


                // Price Range Filter
                if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == false && operatingSystemId.HasValue == false &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRange(minPrice.Value, maxPrice.Value);

                // Company Filter
                else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
                    companyId.HasValue == true && operatingSystemId.HasValue == false &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyId(companyId.Value);

                // Operating System Filter
                else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
                    companyId.HasValue == false && operatingSystemId.HasValue == true &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemId(operatingSystemId.Value);

                // Type Filter
                else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
                    companyId.HasValue == false && operatingSystemId.HasValue == false &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetComputerByType(typeId.Value);

                // Price + Company Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == true && operatingSystemId.HasValue == false &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyId(minPrice.Value, maxPrice.Value, companyId.Value);

                // Price + Operating System Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == false && operatingSystemId.HasValue == true &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemId(minPrice.Value, maxPrice.Value, operatingSystemId.Value);

                // Price + Type Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == false && operatingSystemId.HasValue == false &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndTypeId(minPrice.Value, maxPrice.Value, typeId.Value);

                // Company + Operating System Filter
                else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
                    companyId.HasValue == true && operatingSystemId.HasValue == true &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndOperatingSystemId(companyId.Value, operatingSystemId.Value);

                // Company + Type Filter
                else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
                    companyId.HasValue == true && operatingSystemId.HasValue == false &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndTypeId(companyId.Value, typeId.Value);

                // Operating System + Type Filter
                else if (minPrice.HasValue == false && maxPrice.HasValue == false &&
                    companyId.HasValue == false && operatingSystemId.HasValue == true &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemIdAndTypeId(operatingSystemId.Value, typeId.Value);

                // Price + Company + Operating System Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == true && operatingSystemId.HasValue == true &&
                    typeId.HasValue == false)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemId(minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value);

                // Price + Company + Type Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == true && operatingSystemId.HasValue == false &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndTypeId(minPrice.Value, maxPrice.Value, companyId.Value, typeId.Value);

                // Price + Operating System + Type Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == false && operatingSystemId.HasValue == true &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemIdAndTypeId(minPrice.Value, maxPrice.Value, operatingSystemId.Value, typeId.Value);

                // Price + Company + Operating System + Type Filter
                else if (minPrice.HasValue == true && maxPrice.HasValue == true &&
                    companyId.HasValue == true && operatingSystemId.HasValue == true &&
                    typeId.HasValue == true)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemIdAndTypeId(minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value, typeId.Value);
                // If no filters are applied, get all computers
                else
                {
                    computers = this.repositoryFactory.ComputerRepository.GetAll();
                }

                catalogViewModel.Computers = computers;
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
        [HttpGet]
        public ComputerDetailsViewModel GetComputerDetails(int Id)
        {
            ComputerDetailsViewModel cDVM = new ComputerDetailsViewModel(); // cDVM = ComputerDetailsViewModel

            //open connection :O
            try
            {
                this.repositoryFactory.ConnectDbContext();
                cDVM.computer = this.repositoryFactory.ComputerRepository.GetById(Id);
                cDVM.company = this.repositoryFactory.CompanyRepository.GetById(Id);
                cDVM.type = this.repositoryFactory.TypeRepository.GetById(Id);
                cDVM.operatingSystem = this.repositoryFactory.OperatingSystemRepository.GetById(Id);
                cDVM.cpu = this.repositoryFactory.CpuRepository.GetById(Id);
                cDVM.gpu = this.repositoryFactory.GpuRepository.GetById(Id);
                cDVM.ram = this.repositoryFactory.RamRepository.GetById(Id);
                cDVM.powerSupply = this.repositoryFactory.PowerSupplyRepository.GetById(Id);
                cDVM.cpuFan = this.repositoryFactory.CpuFanRepository.GetById(Id);
                cDVM.computerCase = this.repositoryFactory.CaseRepository.GetById(Id);
                return cDVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                //close connection :D
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
