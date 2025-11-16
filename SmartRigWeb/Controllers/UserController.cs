using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models;

namespace SmartRigWeb
{
    [Route("api/[controller]")]
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

                List<Computer> computers = this.repositoryFactory.ComputerRepository.GetAll();

                if (minPrice.HasValue)
                    computers = computers.Where(c => c.Price >= minPrice.Value).ToList();

                if (maxPrice.HasValue)
                    computers = computers.Where(c => c.Price <= maxPrice.Value).ToList();

                if (typeId.HasValue)
                    computers = computers.Where(c => c.ComputerTypeId == typeId.Value).ToList();

                if (companyId.HasValue)
                    computers = computers.Where(c => c.CompanyId == companyId.Value).ToList();

                if (operatingSystemId.HasValue)
                    computers = computers.Where(c => c.OperatingSystemId == operatingSystemId.Value).ToList();

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
        public ShopingCartViewModel GetShoppingCart(int userId)
        {
            ShopingCartViewModel sCVM = new ShopingCartViewModel();
            try
            {
                this.repositoryFactory.ConnectDbContext();
                sCVM.cart = this.repositoryFactory.CartRepository.GetById(userId);
                sCVM.Computers = this.repositoryFactory.ComputerRepository.GetComputersByCartId(sCVM.cart.CartId);
                return sCVM;
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
        public OrdersViewModel GetOrders(int userId)
        {
            OrdersViewModel oVM = new OrdersViewModel();
            try
            {
                this.repositoryFactory.ConnectDbContext();

            }
        }
    }
}
