namespace InventoryManager.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClothesService clothes;
        private readonly UserManager<User> userManager;

        public HomeController(
            IClothesService clothes,
            UserManager<User> userManager)
        {
            this.clothes = clothes;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string sort, string order)
        {
            var user = await this.userManager.GetUserAsync(User);

            var model = await this.clothes.AllClothesAsync(user.Id, sort, order);

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
