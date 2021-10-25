using Contacts.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    public interface IRepository
    {
        Task<int> AddAsync<T>(T entity) where T : IEntity, new();
        Task<List<T>> GetAllAsync<T>() where T : IEntity, new();
        Task<T> GetByIdAsync<T>(int id) where T : IEntity, new();
        Task<int> RemoveAsync<T>(T entity) where T : IEntity, new();
        Task<int> UpdateAsync<T>(T entity) where T : IEntity, new();
        Task<T> FindAsync<T>(Expression<Func<T, bool>> expression) where T : IEntity, new();
        Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : IEntity, new();
    }
}

