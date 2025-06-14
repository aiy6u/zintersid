using Microsoft.AspNetCore.Mvc;

namespace zintersid.Areas.Admin.Controllers
{
    [Area("RoleManagement")]
    [Route("Admin/[controller]")]
    public class RoleManagementController : Controller
    {
        // GET: Role
        public IActionResult Index()
        {
            return View();
        }
    }
}