using Contacts.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;

        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.db3");
                SQLiteAsyncConnection database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<UserModel>();
                database.CreateTableAsync<ContactModel>();

                return database;
            });
        }

        public Task<int> AddAsync<T>(T entity) where T : IEntity, new()
        {
            return _database.Value.InsertAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>() where T : IEntity, new()
        {
            return _database.Value.Table<T>().ToListAsync();
        }

        public Task<int> RemoveAsync<T>(T entity) where T : IEntity, new()
        {
            return _database.Value.DeleteAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntity, new()
        {
            return _database.Value.UpdateAsync(entity);
        }
    }
}
