using ApiClient;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAppSmartRig.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
