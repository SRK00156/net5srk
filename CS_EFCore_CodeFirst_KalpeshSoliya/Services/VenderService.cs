using CS_EFCore_CodeFirst_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Services
{
    public class VenderService : IServices<Vender, int>
    {
        private readonly ScikeyLabDbContext ctx;
        public VenderService(ScikeyLabDbContext _ctx)
        {
            this.ctx = _ctx;
        }

        public async Task<Vender> CreateAsync(Vender entity)
        {
            var res = await ctx.Venders.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task DeletebyIdAsync(int _id)
        {
            var _Vender = await GetbyIdAsync(_id);
            ctx.Venders.Remove(_Vender);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vender>> GetAllAsync()
        {
            return await ctx.Venders.ToListAsync();
        }

        public async Task<Vender> UpdateByIdAsync(int _id, Vender entiry)
        {
            var _vender = await GetbyIdAsync(_id);
            _vender.VenderId = entiry.VenderId;
            _vender.VenderName = entiry.VenderName;
            await ctx.SaveChangesAsync();
            return _vender;
        }

        public async Task<Vender> GetbyIdAsync(int _id)
        {
            return await ctx.Venders.FindAsync(_id);
        }

        public async Task<Vender> GetbyVIDAsync(string _vid)
        {
            return await ctx.Venders.Where(v => v.VenderId == _vid).FirstOrDefaultAsync();
        }
    }
}
