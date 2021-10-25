using Contacts.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    public class Repository : IRepository
    {
        private SQLiteAsyncConnection _database;

        public Repository()
        {
            _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.db3"));
            _database.CreateTableAsync<UserModel>();
            _database.CreateTableAsync<ContactModel>();
        }

        public async Task<int> AddAsync<T>(T entity) where T : IEntity, new() => 
            await _database.InsertAsync(entity);

        public async Task<List<T>> GetAllAsync<T>() where T : IEntity, new() => 
            await _database.Table<T>().ToListAsync();

        public async Task<T> GetByIdAsync<T>(int id) where T : IEntity, new() => 
            await _database.GetAsync<T>(id);

        public async Task<int> RemoveAsync<T>(T entity) where T : IEntity, new() => 
            await _database.DeleteAsync(entity);

        public async Task<int> UpdateAsync<T>(T entity) where T : IEntity, new() => 
            await _database.UpdateAsync(entity);

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> expression) where T : IEntity, new() =>
            await _database.FindAsync<T>(expression);

        public async Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : IEntity, new() =>
            await _database.Table<T>().Where(expression).ToListAsync();
    }
}
