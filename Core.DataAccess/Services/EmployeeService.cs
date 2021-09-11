using Core.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Services
{
	public class EmployeeService : IService<Employee, int>
	{

		private readonly CompanyContext ctx;
		public EmployeeService(CompanyContext ctx)
		{
			this.ctx = ctx;
		}
		public Task<Employee> CreateAsync(Employee entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Employee>> GetAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Employee> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Employee> UpdateAsync(int id, Employee entity)
		{
			throw new NotImplementedException();
		}
	}
}
