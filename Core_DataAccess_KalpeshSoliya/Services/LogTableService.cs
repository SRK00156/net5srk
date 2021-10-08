using Core_DataAccess_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_DataAccess_KalpeshSoliya.Services
{
    public class LogTableService:IServiceLogTable<LogTable>
    {
        private readonly SRKCompContext ctx;
        public LogTableService(SRKCompContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<LogTable> InsertLogAsync(LogTable entity)
        {
            var res = await ctx.LogTables.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }
    }
}
