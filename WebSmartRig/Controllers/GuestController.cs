using Microsoft.AspNetCore.Mvc;
using Models;
using ApiClient;

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
        public IActionResult GetCatalog()
        {
            // 1. get data from webservice
            // 2. 
            WebClient<CatalogViewModel> webClient = new WebClient<CatalogViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 7249;
            webClient.Path = "api/Guest/GetCatalog";
            CatalogViewModel viewModel = webClient.Get();
            
            return View(viewModel);
        }

    }
}
