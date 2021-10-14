using Contacts.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    public class RepositoryAsync : IRepositoryAsync
    {
        private SQLiteAsyncConnection _database;

        public RepositoryAsync()
        {
            _database = new Lazy<Task<SQLiteAsyncConnection>>(async () =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.db3");
                var database = new SQLiteAsyncConnection(path);

                await database.CreateTableAsync<UserModel>();
                await database.CreateTableAsync<ContactModel>();

                return database;
            }).Value.Result;
        }

        public async Task<int> AddAsync<T>(T entity) where T : IEntity, new()
        {
            return await _database.InsertAsync(entity);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : IEntity, new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<int> RemoveAsync<T>(T entity) where T : IEntity, new()
        {
            return await _database.DeleteAsync(entity);
        }

        public async Task<int> UpdateAsync<T>(T entity) where T : IEntity, new()
        {
            return await _database.UpdateAsync(entity);
        }
    }
}
