using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using SmartRigWeb.ModelCreator;

namespace SmartRigWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        RepositoryFactory repositoryFactory = new RepositoryFactory();
        

        [HttpGet]
        public CatalogViewModel GetCatalog()
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            catalogViewModel.types = repositoryFactory.TypeRepository.GetAllByTypeCode(1);
            catalogViewModel.Computers = repositoryFactory.ComputerRepository.GetAll();
            catalogViewModel.Companys = repositoryFactory.CompanyRepository.GetAll();
            catalogViewModel.operatingSystems = repositoryFactory.OperatingSystemRepository.GetAll();
            return catalogViewModel;
        }
    }
}
