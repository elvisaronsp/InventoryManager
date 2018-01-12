namespace InventoryManager.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClothesService clothes;

        public HomeController(IClothesService clothes)
        {
            this.clothes = clothes;
        }

        public async Task<IActionResult> Index(string sort, string order)
        {
            var model = await this.clothes.AllClothesAsync(sort, order);

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
