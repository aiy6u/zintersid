using Newtonsoft.Json;
using zintersid.Services.AuditLogs.Dtos;

public class SerilogReaderHelper
{
    private readonly string _logFilePath;

    public SerilogReaderHelper(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public IEnumerable<AuditLogDto> ReadAuditLogs(params string[] yyyyMMddHHList)
    {
        var logDir = Path.GetDirectoryName(_logFilePath);
        var logFilePrefix = Path.GetFileNameWithoutExtension(_logFilePath);
        var logFileExt = Path.GetExtension(_logFilePath);

        var listFileLogPath = new List<string>();

        foreach (var yyyyMMddHH in yyyyMMddHHList)
        {
            var searchPattern = $"{logFilePrefix}{yyyyMMddHH}*{logFileExt}";
            if (Directory.Exists(logDir))
            {
                // Get all files matching the pattern in the directory
                var matchingFiles = Directory.GetFiles(logDir, searchPattern);
                listFileLogPath.AddRange(matchingFiles);
            }
            else
            {
                Console.WriteLine($"Directory does not exist: {logDir}");
            }
        }

        var auditLogs = new List<AuditLogDto>();

        foreach (var logFile in listFileLogPath)
        {
            try
            {
                using var reader = new StreamReader(logFile);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        var logEntry = JsonConvert.DeserializeObject<AuditLogDto>(line);
                        if (logEntry == null)
                        {
                            continue;
                        }


                        auditLogs.Add(logEntry);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing log entry: {ex.Message}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                continue;
            }
            catch (IOException e)
            {
                if (e.Message.Contains("it is being used by another process"))
                {
                    continue;
                }

                throw;
            }
        }

        return auditLogs;
    }
}

