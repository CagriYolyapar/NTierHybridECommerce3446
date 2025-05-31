using Project.BLL.Managers.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Managers.MongoConcretes
{
    internal class MongoConcrete : BaseMongoManager, ICategoryManager
    {
        public Task CreateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetActives()
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetModifieds()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetPassives()
        {
            throw new NotImplementedException();
        }

        public Task MakePassiveAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public List<Category> Where(Expression<Func<Category, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
