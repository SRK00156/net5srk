using Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Services
{
	public class DepartmentService : IService<Department, int>
	{
		private readonly CompanyContext ctx;
		public DepartmentService(CompanyContext ctx)
		{
			this.ctx = ctx;
		}

		public async Task<Department> CreateAsync(Department entity)
		{
			try
			{
				var res = await ctx.Departments.AddAsync(entity);
				await ctx.SaveChangesAsync();
				return res.Entity;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task DeleteAsync(int id)
		{
			// search record based on id.
			var dept = await ctx.Departments.FindAsync(id);
			ctx.Departments.Remove(dept);
			await ctx.SaveChangesAsync();
		}

		public async Task<IEnumerable<Department>> GetAsync()
		{
			return await ctx.Departments.ToListAsync();
		}

		public async Task<Department> GetAsync(int id)
		{
			return await ctx.Departments.FindAsync(id);
		}

		public async Task<Department> UpdateAsync(int id, Department entity)
		{
			// 1 serach recorde
			var dept = await ctx.Departments.FindAsync(id);
			// Pass the P.K> SO that Record will be Search
			dept.DeptNo = entity.DeptNo;
			dept.DeptName = entity.DeptName;
			dept.Location = entity.Location;
			await ctx.SaveChangesAsync();
			return dept;
		}
	}
}
