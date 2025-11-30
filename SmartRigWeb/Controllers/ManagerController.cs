using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models;

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
        public bool AddComputer(Computer computer)
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.ComputerRepository.Create(computer);
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
        public bool ChangeCartStatus(Cart cart)
        {
            try
            {
                // Connect to the database
                this.repositoryFactory.ConnectDbContext();

                // Call the Update method on CartRepository, passing the cartId and isPayed status
                return this.repositoryFactory.CartRepository.Update(cart);
            }
            catch (Exception ex)
            {
                // Log the error message to console
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally
            {
                // Ensure the database connection is closed
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Computer> GetAllComputers()
        {
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
        public List<CartComputer> GetAllOrders()
        {
            try
            {
                this.repositoryFactory.ConnectDbContext();
                return this.repositoryFactory.CartRepository.GetOrdersByUserId(0); // maybe create a function for all users
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

    }
}
