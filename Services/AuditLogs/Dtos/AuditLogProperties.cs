namespace zintersid.Services.AuditLogs.Dtos
{
    public class AuditLogProperties
    {
        public string EndpointName { get; set; }
        public AuditLogEventId EventId { get; set; }
        public string SourceContext { get; set; }
        public string RequestId { get; set; }
        public string RequestPath { get; set; }
        public string ConnectionId { get; set; }
        // Add other properties as needed
    }
}