using AcentemOto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcentemOto.Data
{
    public interface IMessageLogRepository
    {
        Task AddLogAsync(MessageLog log);
        Task AddLogsBulkAsync(IEnumerable<MessageLog> logs);
        Task UpdateLogAsync(MessageLog log);
        Task<List<MessageLog>> GetAllLogsAsync();
        Task<List<MessageLog>> GetLogsByStatusAsync(MessageStatus status);
    }
}
