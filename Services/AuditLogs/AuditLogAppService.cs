using zintersid.Common.IoC;
using zintersid.Services.AuditLogs.Dtos;

namespace zintersid.Services.AuditLogs
{
    public class AuditLogAppService : AppServiceBase, IAuditLogAppService
    {
        private readonly SerilogReaderHelper _serilogReader;
        private readonly ILogger<AuditLogAppService> _logger;

        public AuditLogAppService(IConfiguration configuration, ILogger<AuditLogAppService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var logFilePath = GetLogFilePath(configuration);
            _serilogReader = new SerilogReaderHelper(logFilePath);
        }

        private string GetLogFilePath(IConfiguration configuration)
        {
            try
            {
                var logFilePath = configuration.GetSection("Serilog:WriteTo")
                                    .GetChildren()
                                    .FirstOrDefault(x => x.GetValue<string>("Name") == "File")
                                    ?.GetSection("Args")
                                    ?.GetValue<string>("path");

                if (string.IsNullOrEmpty(logFilePath))
                {
                    throw new InvalidOperationException("Log file path is not configured.");
                }

                _logger.LogInformation("Log file path configured: {LogFilePath}", logFilePath);
                return logFilePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving log file path.");
                throw;
            }
        }

        public IEnumerable<AuditLogDto> GetAuditLogs(AuditLogFilter filter)
        {
            try
            {
                var yyyyMMddHHList = GenerateHourlyTimestamps(filter.StartDate, filter.EndDate);
                var auditLogs = _serilogReader.ReadAuditLogs(yyyyMMddHHList);
                var query = FilterAuditLogs(auditLogs.AsQueryable(), filter);

                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving audit logs.");
                throw;
            }
        }

        private string[] GenerateHourlyTimestamps(DateTime? startDate, DateTime? endDate)
        {
            var yyyyMMddHHList = new List<string>();
            startDate ??= new DateTime(2025, 6, 1);
            endDate ??= DateTime.Now;

            for (DateTime yyyyMMddHH = startDate.Value; yyyyMMddHH <= endDate; yyyyMMddHH = yyyyMMddHH.AddHours(1))
            {
                yyyyMMddHHList.Add(yyyyMMddHH.ToString("yyyyMMddHH"));
            }
            return [.. yyyyMMddHHList];
        }

        private IQueryable<AuditLogDto> FilterAuditLogs(IQueryable<AuditLogDto> query, AuditLogFilter filter)
        {
            // if (!string.IsNullOrEmpty(filter.Keyword))
            // {
            //     query = query.Where(log => log.Action.Contains(filter.Keyword) || log.Details.Contains(filter.Keyword));
            // }

            // if (!string.IsNullOrEmpty(filter.User))
            // {
            //     query = query.Where(log => log.User.Equals(filter.User, StringComparison.OrdinalIgnoreCase));
            // }

            return query;
        }
    }
}