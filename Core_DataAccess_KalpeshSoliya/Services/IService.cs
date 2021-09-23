using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_DataAccess_KalpeshSoliya.Services
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TEntity">Generic Parameter which is Constraints to be always class using where TEntity:class</typeparam>
	/// <typeparam name="TPk">Is s generic Parameter which will always be input parameter using 'in'</typeparam>
	public interface IService<TEntity, in TPk> where TEntity:class
	{
		Task<IEnumerable<TEntity>> GetAsync();
		Task<TEntity> GetAsync(TPk id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TPk id, TEntity entity);
		Task DeleteAsync(TPk id);
	}

	public interface IServiceEmpDept
	{
		Task<bool> IsDepartmentCapable(int _deptId);
	}
}
