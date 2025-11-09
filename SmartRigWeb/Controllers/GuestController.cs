using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using SmartRigWeb.ModelCreator;

namespace SmartRigWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        RepositoryFactory repositoryFactory;
        public GuestController()
        {
            repositoryFactory = new RepositoryFactory();
        }


        [HttpGet]
        public CatalogViewModel GetCatalog()
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            //open connection :O
            try
            {
                this.repositoryFactory.ConnectDbContext();
                catalogViewModel.types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);
                catalogViewModel.Computers = this.repositoryFactory.ComputerRepository.GetAll();
                catalogViewModel.Companys = this.repositoryFactory.CompanyRepository.GetAll();
                catalogViewModel.operatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();
                return catalogViewModel;
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
        public ComputerDetailsViewModel GetComputerDetails(int Id)
        {
            ComputerDetailsViewModel cDVM  = new ComputerDetailsViewModel(); // cDVM = ComputerDetailsViewModel

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
    }
}
