namespace zintersid.Services.AuditLogs.Dtos
{
    public class AuditLogDto
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string MessageTemplate { get; set; }
        public string TraceId { get; set; }
        public string SpanId { get; set; }
        public AuditLogProperties Properties { get; set; }
    }
}