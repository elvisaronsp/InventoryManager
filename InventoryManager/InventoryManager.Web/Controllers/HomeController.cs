namespace InventoryManager.Web.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
