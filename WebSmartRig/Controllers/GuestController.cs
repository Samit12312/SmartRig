using ApiClient;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Models.ViewModels;

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
        public IActionResult GetCatalog(string? operatingSystemId = null,
            string? typeId = null, int? minPrice = null, int? maxPrice = null,
            int? priceSort = null, int? companyId = null, string? currencyCode = "ILS")
        {
            // 1. get data from webservice
            // 2. 
            WebClient<CatalogViewModel> webClient = new WebClient<CatalogViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
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
            if (currencyCode != null)
                webClient.AddParameter("currencyCode", currencyCode);

            CatalogViewModel viewModel = webClient.Get();
            
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetComputer(string computerId)
        {
            if (!int.TryParse(computerId, out int id))
            {
                return RedirectToAction("GetCatalog", "Guest");
            }

            WebClient<ComputerDetailsViewModel> webClient = new WebClient<ComputerDetailsViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/Guest/GetComputerDetails";
            webClient.AddParameter("computerId", id.ToString());

            ComputerDetailsViewModel computerDetailsViewModel = webClient.Get();

            if (computerDetailsViewModel == null || computerDetailsViewModel.computer == null)
            {
                return RedirectToAction("GetCatalog", "Guest");
            }

            return View(computerDetailsViewModel);
        }
        [HttpGet]
        public IActionResult ViewLoginForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Message = "Email and password are required";
                return View("ViewLoginForm");
            }

            LoginResponse response = GetLoginResponse(email, password);

            if (response != null && response.Success)
            {
                HttpContext.Session.Clear();

                HttpContext.Session.SetString("userId", response.UserId.ToString());
                HttpContext.Session.SetString("userName", response.UserName);
                HttpContext.Session.SetString("Manager", response.Manager.ToString());

                return RedirectToAction("GetCatalog", "Guest");
            }

            ViewBag.Message = "Invalid email or password";
            return View("ViewLoginForm");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return RedirectToAction("ViewUpdateProfileForm", "Guest");
        }

        private LoginResponse GetLoginResponse(string email, string password)
        {
            WebClient<LoginResponse> webClient = new WebClient<LoginResponse>();

            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/Guest/Login";

            webClient.AddParameter("email", email);
            webClient.AddParameter("password", password);

            LoginResponse response = webClient.Get();

            return response;
        }
        [HttpGet]
        public IActionResult ViewRegistrationForm()
        {
            WebClient<RegistrationViewModel> webClient = new WebClient<RegistrationViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;

            string userId = HttpContext.Session.GetString("userId");

            if (userId != null)
            {
                ViewBag.IsUpdate = true;
                webClient.Path = "api/User/GetUpdateProfileViewModel";
                webClient.AddParameter("userId", userId);
            }
            else
            {
                ViewBag.IsUpdate = false;
                webClient.Path = "api/Guest/RegistrationViewModel";
            }

            RegistrationViewModel ufvm = webClient.Get();

            if (ufvm.User == null)
            {
                ufvm.User = new User();
            }

            return View("ViewRegistrationForm", ufvm);
        }
        [HttpPost]
        public IActionResult Registrations(User user)
        {
            ViewBag.IsUpdate = false;

            if (ModelState.IsValid == false)
            {
                return View("ViewRegistrationForm", GetRegistrationView(user));
            }

            if (EmailExists(user.UserEmail))
            {
                ModelState.AddModelError("User.UserEmail", "This email already exists");
                return View("ViewRegistrationForm", GetRegistrationView(user));
            }

            bool ok = PostUser(user);

            if (ok)
            {
                TempData["Message"] = "Registration successful. Please login.";
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            ViewBag.Message = "Registration failed. Try again";
            return View("ViewRegistrationForm", GetRegistrationView(user));
        }
        [HttpGet]
        public IActionResult ViewUpdateProfileForm()
        {
            string sessionId = HttpContext.Session.GetString("userId");

            if (sessionId == null)
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            ViewBag.IsUpdate = true;

            WebClient<RegistrationViewModel> webClient = new WebClient<RegistrationViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/GetUpdateProfileViewModel";
            webClient.AddParameter("userId", sessionId);

            RegistrationViewModel ufvm = webClient.Get();

            return View("ViewRegistrationForm", ufvm);
        }
        private RegistrationViewModel GetRegistrationView(User user)
        {
            ViewBag.IsUpdate = false;

            WebClient<RegistrationViewModel> webClient = new WebClient<RegistrationViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/Guest/RegistrationViewModel";

            RegistrationViewModel vm = webClient.Get();
            vm.User = user;

            return vm;
        }
        private bool EmailExists(string email)
        {
            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/Guest/EmailExists";
            webClient.AddParameter("email", email);

            return webClient.Get();
        }
        private bool PostUser(User user)
        {
            WebClient<User> clientUser = new WebClient<User>();
            clientUser.Schema = "http";
            clientUser.Host = "localhost";
            clientUser.Port = 5195;
            clientUser.Path = "api/Guest/Registration";
            return clientUser.Post(user);
        }

    }
}
