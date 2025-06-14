using Microsoft.AspNetCore.Mvc;

namespace zintersid.Areas.Admin.Controllers
{
    [Area("UserManagement")]
    [Route("Admin/[controller]")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}