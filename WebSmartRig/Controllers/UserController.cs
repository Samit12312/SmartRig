using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using System.Text.Json;

namespace WebAppSmartRig.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("HomePage", "Guest");
        }

        [HttpPost]
        public IActionResult UpdateProfile([Bind(Prefix = "User")] UpdateProfileViewModel model)
        {
            string userIdStr = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            model.UserId = Convert.ToInt32(userIdStr);
            model.Validate();

            if (IsEmailAllowed(model.UserEmail) == false)
            {
                ModelState.AddModelError("User.UserEmail", "Email must be like name@gmail.com, name@hotmail.com, or name@walla.co.il");
            }

            if (IsEmailUsedByAnotherUser(model.UserId, model.UserEmail))
            {
                ModelState.AddModelError("User.UserEmail", "This email is already used by another user");
            }

            if (model.HasErrors || ModelState.IsValid == false)
            {
                ViewBag.IsUpdate = true;
                return View("~/Views/Guest/ViewRegistrationForm.cshtml", GetUpdateRegistrationView(model));
            }

            WebClient<UpdateProfileViewModel> webClient = new WebClient<UpdateProfileViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/UpdateProfile";

            bool ok = webClient.Post(model);

            if (ok)
            {
                HttpContext.Session.SetString("userName", model.UserName);
                TempData["Message"] = "Profile updated successfully";
                return RedirectToAction("ViewUpdateProfileForm", "Guest");
            }

            ViewBag.IsUpdate = true;
            ViewBag.Message = "Failed to update profile";
            return View("~/Views/Guest/ViewRegistrationForm.cshtml", GetUpdateRegistrationView(model));
        }
        private bool IsEmailUsedByAnotherUser(int userId, string email)
        {
            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/IsEmailUsedByAnotherUser";
            webClient.AddParameter("userId", userId.ToString());
            webClient.AddParameter("email", email);

            return webClient.Get();
        }

        private RegistrationViewModel GetUpdateRegistrationView(UpdateProfileViewModel model)
        {
            WebClient<RegistrationViewModel> webClient = new WebClient<RegistrationViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/Guest/RegistrationViewModel";

            RegistrationViewModel vm = webClient.Get();

            User user = new User();
            user.UserId = model.UserId;
            user.UserName = model.UserName;
            user.UserEmail = model.UserEmail;
            user.UserAddress = model.UserAddress;
            user.CityId = model.CityId;
            user.UserPhoneNumber = model.UserPhoneNumber;
            user.UserPassword = "Password1";

            vm.User = user;

            return vm;
        }
        private bool IsEmailAllowed(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            email = email.ToLower();

            if (email.Contains("@") == false)
            {
                return false;
            }

            string[] parts = email.Split('@');

            if (parts.Length != 2)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(parts[0]))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(parts[1]))
            {
                return false;
            }

            if (email.EndsWith(".com"))
            {
                return true;
            }

            if (email.EndsWith(".net"))
            {
                return true;
            }

            if (email.EndsWith(".org"))
            {
                return true;
            }

            if (email.EndsWith(".edu"))
            {
                return true;
            }

            if (email.EndsWith(".co.il"))
            {
                return true;
            }

            if (email.EndsWith(".org.il"))
            {
                return true;
            }

            if (email.EndsWith(".ac.il"))
            {
                return true;
            }

            return false;
        }
        [HttpGet]
        public IActionResult GetCatalog(string? operatingSystem = null, string? typeId = null, int? minPrice = null, int? maxPrice = null, int? priceSort = null)
        {
            // 1. get data from webservice
            // 2. 
            WebClient<CatalogViewModel> webClient = new WebClient<CatalogViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/GetCatalog";

            // 3. Add optional filters if they are provided
            if (operatingSystem != null)
                webClient.AddParameter("operatingSystem", operatingSystem);

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
        public IActionResult ViewCart()
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            Console.WriteLine("MVC VIEW CART USER ID = " + userId);

            WebClient<CartViewModel> webClient = new WebClient<CartViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/GetCart";
            webClient.AddParameter("userId", userId);

            CartViewModel cart = webClient.Get();

            if (cart == null)
            {
                cart = new CartViewModel();
                cart.Computers = new List<CartComputer>();
                cart.Total = 0;
            }

            if (cart.Computers == null)
            {
                cart.Computers = new List<CartComputer>();
            }

            return View(cart);
        }

        [HttpGet]
        public IActionResult AddToCart(int computerId)
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/AddToCart";
            webClient.AddParameter("userId", userId);
            webClient.AddParameter("computerId", computerId.ToString());

            webClient.Get();

            TempData["Message"] = "Added to cart!";
            return RedirectToAction("GetCatalog", "Guest");
        }
        [HttpGet]
        public IActionResult CheckSession()
        {
            string userId = HttpContext.Session.GetString("userId");
            string userName = HttpContext.Session.GetString("userName");
            string manager = HttpContext.Session.GetString("Manager");

            return Content("userId = " + userId + " | userName = " + userName + " | Manager = " + manager);
        }
        [HttpGet]
        public IActionResult ViewCheckout()
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            WebClient<CartViewModel> webClient = new WebClient<CartViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/GetCart";
            webClient.AddParameter("userId", userId);

            CartViewModel cart = webClient.Get();

            if (cart != null)
            {
                ViewBag.Total = cart.Total;
            }
            else
            {
                ViewBag.Total = 0;
            }

            CheckoutViewModel vm = new CheckoutViewModel();

            return View("ViewCheckout", vm);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkout)
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            if (!IsCardDateValid(checkout.CardDate))
            {
                ModelState.AddModelError("CardDate", "Card date is expired");
            }

            if (ModelState.IsValid == false)
            {
                return View("ViewCheckout", checkout);
            }

            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/BuyCart";
            webClient.AddParameter("userId", userId);

            bool ok = webClient.Get();

            if (ok)
            {
                TempData["Message"] = "Payment completed successfully!";
                return RedirectToAction("OrderHistory", "User");
            }

            ViewBag.Message = "Payment failed. Try again";
            return View("ViewCheckout", checkout);
        }

        private bool IsCardDateValid(string cardDate)
        {
            try
            {
                if (string.IsNullOrEmpty(cardDate))
                    return false;

                string[] parts = cardDate.Split('/');

                if (parts.Length != 2)
                    return false;

                int month = Convert.ToInt32(parts[0]);
                int year = Convert.ToInt32(parts[1]) + 2000;

                if (month < 1 || month > 12)
                    return false;

                DateTime lastDayOfCardMonth = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

                if (lastDayOfCardMonth < DateTime.Today)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int computerId)
        {
            string userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/RemoveFromCart";
            webClient.AddParameter("userId", userId);
            webClient.AddParameter("computerId", computerId.ToString());

            webClient.Get();
            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public IActionResult BuyCart()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/BuyCart";
            webClient.AddParameter("userId", userId);

            webClient.Get();
            return RedirectToAction("GetCatalog", "Guest");
        }
        [HttpGet]
        public IActionResult OrderHistory()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }

            WebClient<OrderHistoryViewModel> webClient = new WebClient<OrderHistoryViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5195;
            webClient.Path = "api/User/GetOrderHistory";
            webClient.AddParameter("userId", userId);

            OrderHistoryViewModel viewModel = webClient.Get();
            return View(viewModel);
        }
    }
}
