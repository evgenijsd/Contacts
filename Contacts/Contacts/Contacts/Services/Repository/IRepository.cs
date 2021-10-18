using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}

