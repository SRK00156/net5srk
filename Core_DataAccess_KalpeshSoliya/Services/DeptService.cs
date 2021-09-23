using Core_DataAccess_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_DataAccess_KalpeshSoliya.Services
{
	public class DeptService : IService<Dept, int>
	{
		private readonly SRKCompContext ctx;
		public DeptService(SRKCompContext ctx)
		{
			this.ctx = ctx;
		}

		public async Task<Dept> CreateAsync(Dept entity)
		{
			var res = await ctx.Depts.AddAsync(entity);
			await ctx.SaveChangesAsync();
			return res.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			// search record based on id.
			var dept = await ctx.Depts.FindAsync(id);
			ctx.Depts.Remove(dept);
			await ctx.SaveChangesAsync();
		}

		public async Task<IEnumerable<Dept>> GetAsync()
		{
			return await ctx.Depts.Include(a => a.Emps).ToListAsync();
		}

		public async Task<Dept> GetAsync(int id)
		{
			return await ctx.Depts.Include(a => a.Emps).FirstOrDefaultAsync(i => i.DeptId == id); ;
		}

		public async Task<Dept> UpdateAsync(int id, Dept entity)
		{
			// 1 serach recorde
			var dept = await ctx.Depts.FindAsync(id);
			// Pass the P.K> SO that Record will be Search
			dept.DeptId = entity.DeptId;
			dept.DeptName = entity.DeptName;
			dept.Capacity = entity.Capacity;
			await ctx.SaveChangesAsync();
			return dept;
		}
	}
}
