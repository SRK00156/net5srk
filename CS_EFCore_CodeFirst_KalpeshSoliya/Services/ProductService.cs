using CS_EFCore_CodeFirst_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Services
{
    public class ProductService: IServices<Products,int>
    {
        private readonly ScikeyLabDbContext ctx;
        public ProductService(ScikeyLabDbContext _ctx)
        {
            this.ctx = _ctx;
        }

        public async Task<Products> CreateAsync(Products entity)
        {
            var res = await ctx.Products.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task DeletebyIdAsync(int _id)
        {
            var _product = await GetbyIdAsync(_id);
            ctx.Products.Remove(_product);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Products> UpdateByIdAsync(int _id, Products entiry)
        {
            var _product = await GetbyIdAsync(_id);
            _product.ProductId = entiry.ProductId;
            _product.ProductName = entiry.ProductName;
            await ctx.SaveChangesAsync();
            return _product;
        }

        public async Task<Products> GetbyIdAsync(int _id)
        {
            return await ctx.Products.FindAsync(_id);
        }

        public async Task<List<Products>> GetbyPIdAsync(string _pid)
        {
            List<string> PidStr = _pid.Split(',').ToList();
            //return await ctx.Products.Where(p => PidStr.Any(str => p.ProductId.Contains(str))).ToListAsync();
            return await ctx.Products.Where(p => PidStr.Contains(p.ProductId)).ToListAsync();
        }
    }
}
