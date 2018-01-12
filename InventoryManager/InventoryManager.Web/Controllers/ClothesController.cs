﻿namespace InventoryManager.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models.Clothes;
    using System.Threading.Tasks;

    using static WebConstants;

    [Authorize(Roles = AdministratorRole)]
    public class ClothesController : Controller
    {
        private readonly IClothesService clothes;

        public ClothesController(IClothesService clothes)
        {
            this.clothes = clothes;
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

            await this.clothes.AddAsync(
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
            if (!await this.clothes.ProductExistAsync(id))
            {
                return NotFound();
            }

            var model = await this.clothes.DetailsAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.clothes.ProductExistAsync(id))
            {
                return NotFound();
            }

            await this.clothes.DeleteAsync(id);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.clothes.ProductExistAsync(id))
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