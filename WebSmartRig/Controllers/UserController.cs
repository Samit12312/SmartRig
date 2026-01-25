using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        public IActionResult UpdateProfile(User user)
        {
            // Remove password validation (we're not changing it)
            ModelState.Remove("user.UserPassword");

            if (!ModelState.IsValid)
            {
                // Save the user in TempData to reload if validation fails
                TempData["user"] = JsonSerializer.Serialize(user);
                return RedirectToAction("UpdateProfile", "User");
            }

            // Dummy password required by API / DB validation
            user.UserPassword = "XXXXX5";

            // Make sure the correct user is updated from session
            string userIdStr = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("ViewLoginForm", "Guest");
            }
            user.UserId = Convert.ToInt32(userIdStr);

            // Call API
            WebClient<User> webClient = new WebClient<User>
            {
                Schema = "http",
                Host = "localhost",
                Port = 7249,
                Path = "api/User/UpdateProfile"
            };

            bool ok = webClient.Post(user);

            if (ok)
            {
                // Update session with new username
                HttpContext.Session.SetString("userName", user.UserName);
                return RedirectToAction("GetCatalog", "Guest");
            }

            ViewBag.Error = true;
            return RedirectToAction("UpdateProfile", "User");
        }

        [HttpGet]
        public IActionResult GetCatalog(string? operatingSystem = null, string? typeId = null, int? minPrice = null, int? maxPrice = null, int? priceSort = null)
        {
            // 1. get data from webservice
            // 2. 
            WebClient<CatalogViewModel> webClient = new WebClient<CatalogViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
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

    }
}
