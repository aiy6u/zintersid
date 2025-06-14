using zintersid.Common.IoC;
using zintersid.Services.AuditLogs.Dtos;

namespace zintersid.Services.AuditLogs
{
    public interface IAuditLogAppService: IAppService, ITransientRegister
    {
        IEnumerable<AuditLogDto> GetAuditLogs(AuditLogFilter filter);
    }
}