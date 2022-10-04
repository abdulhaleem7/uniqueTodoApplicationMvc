using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface IRepository<T>
    {
        Task<T> Get(Expression<Func<T,bool>> expression);

        Task<T> Create(T entity);
        Task<bool> AddRange(List<T> entities);

        Task<T> Update(T entity);

        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> expression);

        Task<bool> Exists(Expression<Func<User,bool>> expression);

        Task<bool> Exists(int id);

        Task<int> SaveChanges();
    }
}