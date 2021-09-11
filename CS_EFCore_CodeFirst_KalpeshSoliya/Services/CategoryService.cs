using CS_EFCore_CodeFirst_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Services
{
    public class CategoryService : IServices<Category, int>
    {
        private readonly ScikeyLabDbContext ctx;
        public CategoryService(ScikeyLabDbContext _ctx)
        {
            this.ctx = _ctx;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            var res = await ctx.Categories.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task DeletebyIdAsync(int _id)
        {
            var _cate = await GetbyIdAsync(_id);
            ctx.Categories.Remove(_cate);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category> UpdateByIdAsync(int _id, Category entiry)
        {
            var _category = await GetbyIdAsync(_id);
            _category.CategoryId = entiry.CategoryId;
            _category.CategoryName = entiry.CategoryName;
            await ctx.SaveChangesAsync();
            return _category;
        }

        public async Task<Category> GetbyIdAsync(int _id)
        {
            return await ctx.Categories.FindAsync(_id);
        }
    }
}
