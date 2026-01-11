using Microsoft.AspNetCore.Mvc;
using Models;
using ApiClient;
using System.Net;

namespace WebSmartRig.Controllers
{
    public class GuestController : Controller //תמונות מחלקים לשתי קבוצות תמונות של מידע ושל עיצוב מידע שומרים בתיקיה wwwroot of webservice design save in webapp
    {
        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCatalog(string? operatingSystemId = null, string? typeId = null, int? minPrice = null, int? maxPrice = null
            , int? priceSort = null,int? companyId = null)
        {
            // 1. get data from webservice
            // 2. 
            WebClient<CatalogViewModel> webClient = new WebClient<CatalogViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
            webClient.Path = "api/Guest/GetCatalog";

            // 3. Add optional filters if they are provided
            if (operatingSystemId != null)
                webClient.AddParameter("operatingSystemId", operatingSystemId);

            if (companyId.HasValue)
            {
                webClient.AddParameter("companyId", companyId.ToString());
            }
            if (typeId != null)
                webClient.AddParameter("typeId", typeId);

            if (minPrice.HasValue)
                webClient.AddParameter("minPrice", minPrice.Value.ToString());

            if (maxPrice.HasValue)
                webClient.AddParameter("maxPrice", maxPrice.Value.ToString());

            if (priceSort.HasValue)
                webClient.AddParameter("priceSort", priceSort.Value.ToString()); // 1 = ascending, 2 = descending


            CatalogViewModel viewModel = webClient.Get();
            
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetComputer(string computerId)
        {
            // 1. get data from webservice
            // 2. 
            WebClient<ComputerDetailsViewModel> webClient = new WebClient<ComputerDetailsViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
            webClient.Path = "api/Guest/GetComputerDetails";
            webClient.AddParameter("computerId", computerId);
            ComputerDetailsViewModel computerDetailsViewModel = webClient.Get();

            return View(computerDetailsViewModel);
        }
        [HttpGet]
        public IActionResult ViewRegistrationForm()
        {
            WebClient<RegistrationViewModel> webClient = new WebClient<RegistrationViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
            webClient.Path = "api/Guest/RegistrationViewModel";
            RegistrationViewModel vm= webClient.Get();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Registrations(User user) // can add iformfile to add pictures
        {
            if(ModelState.IsValid == false)
            {
            return View("ViewRegistrationForm", GetRegistrationView(user));
            }
            bool ok = PostUser(user);
            if (ok)
            {
                HttpContext.Session.SetString("userId", user.UserId.ToString());
                return RedirectToAction("GetCatalog", "guest");
            }
            ViewBag.Massage = "Registration failed. Try again";
            return View(GetRegistrationView(user));
        }
        private RegistrationViewModel GetRegistrationView(User user)
        {
            
            WebClient<RegistrationViewModel> webClient = new WebClient<RegistrationViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
            webClient.Path = "api/Guest/RegistrationViewModel";
            RegistrationViewModel vm = webClient.Get();
            vm.User = user;
            return vm;
        }
        private bool PostUser(User user)
        {
            WebClient<User> clientUser = new WebClient<User>();
            clientUser.Schema = "http";
            clientUser.Host = "localhost";
            clientUser.Port = 7249;
            clientUser.Path = "api/Guest/Registration";
            return clientUser.Post(user);
        }
    }
}
