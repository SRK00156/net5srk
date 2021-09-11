using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">Generic Parameter which is constraints to be always class using where TEntity:class</typeparam>
    /// <typeparam name="TPK">Is ageneric parameter which will always be input parameter using 'in'</typeparam>
    public interface IServices<TEntity, in TPK> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetbyIdAsync(TPK id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateByIdAsync(TPK id, TEntity entiry);
        Task DeletebyIdAsync(TPK id);
    }
}
