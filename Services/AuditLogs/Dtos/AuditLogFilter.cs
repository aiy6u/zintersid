namespace zintersid.Services.AuditLogs.Dtos
{
    public class AuditLogFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Keyword { get; set; }
        public string? User { get; set; }
    }
}