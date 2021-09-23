using Core_DataAccess_KalpeshSoliya.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_DataAccess_KalpeshSoliya.Services
{
	public class EmpService : IService<Emp, int>, IServiceEmpDept
	{
		private readonly SRKCompContext ctx;
		public EmpService(SRKCompContext ctx)
		{
			this.ctx = ctx;
		}

        public async Task<bool> IsDepartmentCapable(int _deptId)
        {
			var dept = await ctx.Depts.FindAsync(_deptId);
			int total_emp = await ctx.Emps.CountAsync(i => i.DeptId == _deptId);
			if (total_emp + 1 > dept.Capacity)
				return false;
			else
				return true;
        }

        public async Task<Emp> CreateAsync(Emp entity)
		{
			DeptService deptSrv = new(ctx);
			var res = await ctx.Emps.AddAsync(entity);
			await ctx.SaveChangesAsync();
			return res.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			// search record based on id.
			var emps = await ctx.Emps.FindAsync(id);
			ctx.Emps.Remove(emps);
			await ctx.SaveChangesAsync();
		}

		public async Task<IEnumerable<Emp>> GetAsync()
		{
			return await ctx.Emps.Include(a => a.Dept).ToListAsync();
		}

		public async Task<Emp> GetAsync(int id)
		{
			return await ctx.Emps.Include(a=>a.Dept).FirstOrDefaultAsync(i => i.EmpId == id);
		}

		public async Task<Emp> UpdateAsync(int id, Emp entity)
		{
			// 1 serach recorde
			var emp = await ctx.Emps.FindAsync(id);
			// Pass the P.K> SO that Record will be Search
			emp.EmpId = entity.EmpId;
			emp.EmpName = entity.EmpName;
			emp.EmpSalary = entity.EmpSalary;
			emp.EmpDesignation = entity.EmpDesignation;
			emp.DeptId = entity.DeptId;
			await ctx.SaveChangesAsync();
			return emp;
		}

    }
}
