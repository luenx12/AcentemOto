using AcentemOto.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcentemOto.Data
{
    public class MessageLogRepository : IMessageLogRepository
    {
        public async Task AddLogAsync(MessageLog log)
        {
            using (var context = new AppDbContext())
            {
                await context.MessageLogs.AddAsync(log);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddLogsBulkAsync(IEnumerable<MessageLog> logs)
        {
            using (var context = new AppDbContext())
            {
                await context.MessageLogs.AddRangeAsync(logs);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateLogAsync(MessageLog log)
        {
            using (var context = new AppDbContext())
            {
                context.MessageLogs.Update(log);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<MessageLog>> GetAllLogsAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.MessageLogs.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<MessageLog>> GetLogsByStatusAsync(MessageStatus status)
        {
            using (var context = new AppDbContext())
            {
                return await context.MessageLogs.AsNoTracking().Where(x => x.Status == status).ToListAsync();
            }
        }
    }
}
