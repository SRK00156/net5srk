using CS_EFCore_CodeFirst_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Services
{
    public class VendorProductService:IServices<VendorProduct,int>
    {
        private readonly ScikeyLabDbContext ctx;
        public VendorProductService(ScikeyLabDbContext _ctx)
        {
            this.ctx = _ctx;
        }

        public async Task<VendorProduct> CreateAsync(VendorProduct entity)
        {
            var res = await ctx.VendorProduct.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task DeletebyIdAsync(int _id)
        {
            var _VenderProduct = await GetbyIdAsync(_id);
            ctx.VendorProduct.Remove(_VenderProduct);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<VendorProduct>> GetAllAsync()
        {
            return await ctx.VendorProduct.ToListAsync();
        }

        public async Task<VendorProduct> UpdateByIdAsync(int _id, VendorProduct entiry)
        {
            var _venderProduct = await GetbyIdAsync(_id);
            _venderProduct.VenderRowId = entiry.VenderRowId;
            _venderProduct.ProductRowId = entiry.ProductRowId;
            await ctx.SaveChangesAsync();
            return _venderProduct;
        }

        public async Task<VendorProduct> GetbyIdAsync(int _id)
        {
            return await ctx.VendorProduct.FindAsync(_id);
        }
    }
}
