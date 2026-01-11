using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.ViewModels;
using System;

namespace SmartRigWeb
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
        public CatalogViewModel GetCatalog(
    int? minPrice = null,
    int? maxPrice = null,
    int? companyId = null,
    int? operatingSystemId = null,
    int? typeId = null,
    int? priceSort = null) // 1 = ascending, 2 = descending
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();

            try
            {
                // Assign filter values to ViewModel so the view can use them
                catalogViewModel.MinPrice = minPrice;
                catalogViewModel.MaxPrice = maxPrice;
                catalogViewModel.CompanyId = companyId;
                catalogViewModel.OperatingSystemId = operatingSystemId;
                catalogViewModel.TypeId = typeId;
                catalogViewModel.PriceSort = priceSort;

                this.repositoryFactory.ConnectDbContext();

                catalogViewModel.Types = this.repositoryFactory.TypeRepository.GetAllByTypeCode(1);
                catalogViewModel.Companys = this.repositoryFactory.CompanyRepository.GetAll();
                catalogViewModel.operatingSystems = this.repositoryFactory.OperatingSystemRepository.GetAll();

                List<Computer> computers = new List<Computer>();

                // -------- FILTERS --------
                if (minPrice.HasValue && maxPrice.HasValue && !companyId.HasValue && !operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRange(minPrice.Value, maxPrice.Value);

                else if (!minPrice.HasValue && !maxPrice.HasValue && companyId.HasValue && !operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyId(companyId.Value);

                else if (!minPrice.HasValue && !maxPrice.HasValue && !companyId.HasValue && operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemId(operatingSystemId.Value);

                else if (!minPrice.HasValue && !maxPrice.HasValue && !companyId.HasValue && !operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetComputerByType(typeId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && companyId.HasValue && !operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyId(minPrice.Value, maxPrice.Value, companyId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && !companyId.HasValue && operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemId(minPrice.Value, maxPrice.Value, operatingSystemId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && !companyId.HasValue && !operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndTypeId(minPrice.Value, maxPrice.Value, typeId.Value);

                else if (!minPrice.HasValue && !maxPrice.HasValue && companyId.HasValue && operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndOperatingSystemId(companyId.Value, operatingSystemId.Value);

                else if (!minPrice.HasValue && !maxPrice.HasValue && companyId.HasValue && !operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByCompanyIdAndTypeId(companyId.Value, typeId.Value);

                else if (!minPrice.HasValue && !maxPrice.HasValue && !companyId.HasValue && operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetComputersByOperatingSystemIdAndTypeId(operatingSystemId.Value, typeId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && companyId.HasValue && operatingSystemId.HasValue && !typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemId(
                        minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && companyId.HasValue && !operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndTypeId(
                        minPrice.Value, maxPrice.Value, companyId.Value, typeId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && !companyId.HasValue && operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndOperatingSystemIdAndTypeId(
                        minPrice.Value, maxPrice.Value, operatingSystemId.Value, typeId.Value);

                else if (minPrice.HasValue && maxPrice.HasValue && companyId.HasValue && operatingSystemId.HasValue && typeId.HasValue)
                    computers = this.repositoryFactory.ComputerRepository.GetByPriceRangeAndCompanyIdAndOperatingSystemIdAndTypeId(
                        minPrice.Value, maxPrice.Value, companyId.Value, operatingSystemId.Value, typeId.Value);

                else
                    computers = this.repositoryFactory.ComputerRepository.GetAll();

                // -------- SORTING --------
                if (priceSort.HasValue)
                {
                    for (int i = 0; i < computers.Count - 1; i++)
                    {
                        for (int j = i + 1; j < computers.Count; j++)
                        {
                            if (priceSort.Value == 1 && computers[i].Price > computers[j].Price ||
                                priceSort.Value == 2 && computers[i].Price < computers[j].Price)
                            {
                                Computer temp = computers[i];
                                computers[i] = computers[j];
                                computers[j] = temp;
                            }
                        }
                    }
                }

                // -------- MAPPING TO VIEW MODEL --------
                List<ComputerCatalogViewModel> catalogComputers = new List<ComputerCatalogViewModel>();

                foreach (Computer computer in computers)
                {
                    Cpu cpu = this.repositoryFactory.CpuRepository.GetById(computer.CpuId);
                    Gpu gpu = this.repositoryFactory.GpuRepository.GetById(computer.GpuId);
                    Ram ram = this.repositoryFactory.RamRepository.GetById(computer.RamId);
                    Storage storage = this.repositoryFactory.StorageRepository.GetById(computer.StorageId);
                    Models.OperatingSystem os = this.repositoryFactory.OperatingSystemRepository.GetById(computer.OperatingSystemId);

                    ComputerCatalogViewModel vm = new ComputerCatalogViewModel
                    {
                        Id = computer.ComputerId,
                        ComputerName = computer.ComputerName,
                        ComputerPicture = computer.ComputerPicture,
                        Price = computer.Price,
                        Cpu = cpu.CpuName,
                        Gpu = gpu.GpuName,
                        Ram = ram.RamName,
                        Storage = storage.StorageName,
                        OperatingSystem = os.OperatingSystemName
                    };

                    catalogComputers.Add(vm);
                }

                catalogViewModel.Computers = catalogComputers;
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
        public ComputerDetailsViewModel GetComputerDetails(int computerId)
        {
            ComputerDetailsViewModel cDVM = new ComputerDetailsViewModel();
            try
            {
                this.repositoryFactory.ConnectDbContext();
                Computer computer = this.repositoryFactory.ComputerRepository.GetById(computerId);
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
                cDVM.operatingSystem = OS;
                cDVM.storage = storage;
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

        [HttpPost]
        public bool Registration([FromBody] User user)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.UserRepository.Create(user);
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
        public RegistrationViewModel RegistrationViewModel()
        {
            List<Cities> cities = new List<Cities>();
            RegistrationViewModel viewModel = new RegistrationViewModel();
            try
            {
                this.repositoryFactory.ConnectDbContext();
                viewModel.User = null;
                viewModel.Cities = this.repositoryFactory.CitiesRepository.GetAll();
                return viewModel;
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
