using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public interface IRepositoryAsync
    {
        Task<int> AddAsync<T>(T entity) where T : new();
        Task<List<T>> GetAllAsync<T>() where T : new();
        Task<int> RemoveAsync<T>(T entity) where T : new();
        Task<int> UpdateAsync<T>(T entity) where T : new();
    }
}
