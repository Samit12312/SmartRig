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

                    ComputerCatalogViewModel vm = new ComputerCatalogViewModel();

                    vm.Id = computer.ComputerId;
                    vm.ComputerName = computer.ComputerName;
                    vm.ComputerPicture = computer.ComputerPicture;
                    vm.Price = computer.Price;
                    vm.Cpu = cpu.CpuName;
                    vm.Gpu = gpu.GpuName;
                    vm.Ram = ram.RamName;
                    vm.Storage = storage.StorageName;
                    vm.OperatingSystem = os.OperatingSystemName;

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
        public CartViewModel GetCart(int userId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                CartViewModel cartViewModel = new CartViewModel();

                cartViewModel.Computers = this.repositoryFactory.CartRepository.GetCartById(userId);
                cartViewModel.Total = 0;

                foreach (CartComputer item in cartViewModel.Computers)
                {
                    cartViewModel.Total += item.ComputerPrice * item.ComputerQuantity;
                }

                return cartViewModel;
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
        [HttpGet]
        public RegistrationViewModel GetUpdateProfileViewModel(int userId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                RegistrationViewModel vm = new RegistrationViewModel
                {
                    User = this.repositoryFactory.UserRepository.GetById(userId),
                    Cities = this.repositoryFactory.CitiesRepository.GetAll()
                };

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
        [HttpGet]
        public bool AddToCart(int userId, int computerId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                Cart cart = this.repositoryFactory.CartRepository.GetUnpaidCart(userId);

                if (cart == null)
                {
                    Cart newCart = new Cart();
                    newCart.UserId = userId;
                    newCart.Date = DateTime.Now.ToString("yyyy-MM-dd");
                    newCart.IsPayed = false;
                    this.repositoryFactory.CartRepository.Create(newCart);
                    cart = this.repositoryFactory.CartRepository.GetUnpaidCart(userId);
                }

                return this.repositoryFactory.CartRepository.AddComputer(cart.CartId, computerId, 1);
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
        public bool RemoveFromCart(int userId, int computerId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                Cart cart = this.repositoryFactory.CartRepository.GetUnpaidCart(userId);

                if (cart != null)
                {
                    return this.repositoryFactory.CartRepository.RemoveComputer(cart.CartId, computerId);
                }

                return false;
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
        public bool BuyCart(int userId)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();

                Cart cart = this.repositoryFactory.CartRepository.GetUnpaidCart(userId);

                if (cart != null)
                {
                    return this.repositoryFactory.CartRepository.BuyCart(cart.CartId);
                }

                return false;
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
        public OrderHistoryViewModel GetOrderHistory(int userId)
        {
            OrderHistoryViewModel viewModel = new OrderHistoryViewModel();
            try
            {
                this.repositoryFactory.ConnectDbContext();

                // Get all paid carts for this user
                List<Cart> paidCarts = this.repositoryFactory.CartRepository.GetPaidCartsByUserId(userId);

                viewModel.Orders = new List<OrderViewModel>();

                foreach (Cart cart in paidCarts)
                {
                    OrderViewModel order = new OrderViewModel
                    {
                        CartId = cart.CartId,
                        Date = cart.Date,
                        Items = new List<OrderItemViewModel>(),
                        TotalPrice = 0
                    };

                    // Get computers in this cart
                    List<Computer> computers = this.repositoryFactory.ComputerRepository.GetComputersByCartId(cart.CartId);

                    foreach (Computer computer in computers)
                    {
                        OrderItemViewModel orderItem = new OrderItemViewModel
                        {
                            ComputerId = computer.ComputerId,
                            ComputerName = computer.ComputerName,
                            ComputerPicture = computer.ComputerPicture,
                            Price = computer.Price,
                            Quantity = 1 // If you don't track quantity, default to 1
                        };

                        order.Items.Add(orderItem);
                        order.TotalPrice += computer.Price;
                    }

                    viewModel.Orders.Add(order);
                }

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
