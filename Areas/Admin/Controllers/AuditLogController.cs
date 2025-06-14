using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using zintersid.Services.AuditLogs;
using zintersid.Services.AuditLogs.Dtos;

namespace zintersid.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AuditLogController : Controller
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        // GET: Admin/AuditLog
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/AuditLog/Data
        [HttpGet("Data")]
        public IActionResult Data(
            string? keyword = null,
            string? user = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int page = 1,
            int pageSize = 10)
        {
            var filter = new AuditLogFilter
            {
                Keyword = keyword,
                User = user,
                StartDate = startDate,
                EndDate = endDate
            };

            var logs = _auditLogAppService.GetAuditLogs(filter)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Ideally, get the total count from the service for pagination
            var totalCount = _auditLogAppService.GetAuditLogs(filter).Count();

            return Json(new
            {
                data = logs,
                page,
                pageSize,
                totalCount
            });
        }
    }
}