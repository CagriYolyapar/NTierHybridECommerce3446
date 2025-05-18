using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Managers.Abstracts
{
    public interface IManager<T> where T:class,IEntity
    {
        //Business Logic For Queries
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();
        List<T> Where(Expression<Func<T, bool>> exp);

        //Business Logic for Commands

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<string> RemoveAsync(T entity);
        Task MakePassiveAsync(T entity);
        
    }
}
