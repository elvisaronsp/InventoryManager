namespace InventoryManager.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models.Clothes;
    using System.Threading.Tasks;

    [Authorize]
    public class ClothesController : Controller
    {
        private readonly IClothesService clothes;
        private readonly UserManager<User> userManager;

        public ClothesController(
            IClothesService clothes,
            UserManager<User> userManager)
        {
            this.clothes = clothes;
            this.userManager = userManager;
        }

        public IActionResult Add()
            => View();

        [HttpPost]
        public async Task<IActionResult> Add(ClothesFormServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.GetUserAsync(User);

            await this.clothes.AddAsync(
                user.Id,
                model.Name,
                model.Type,
                model.Quantity,
                model.Size,
                model.SinglePrice,
                model.PictureUrl,
                model.Description);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await this.userManager.GetUserAsync(User);

            if (!await this.clothes.ProductExistForUserAsync(id, user.Id))
            {
                return NotFound();
            }

            var model = await this.clothes.DetailsAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(User);

            if (!await this.clothes.ProductExistForUserAsync(id, user.Id))
            {
                return NotFound();
            }

            await this.clothes.DeleteAsync(id);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(User);

            if (!await this.clothes.ProductExistForUserAsync(id, user.Id))
            {
                return NotFound();
            }

            var model = await this.clothes.GetProductByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClothesFormServiceModel model)
        {
            await this.clothes.EditAsync(
                id,
                model.Name,
                model.Type,
                model.Quantity,
                model.Size,
                model.SinglePrice,
                model.PictureUrl,
                model.Description);

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}