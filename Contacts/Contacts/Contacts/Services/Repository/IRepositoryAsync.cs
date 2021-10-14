﻿using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    public interface IRepositoryAsync
    {
        Task<int> AddAsync<T>(T entity) where T : IEntity, new();
        Task<List<T>> GetAllAsync<T>() where T : IEntity, new();
        Task<int> RemoveAsync<T>(T entity) where T : IEntity, new();
        Task<int> UpdateAsync<T>(T entity) where T : IEntity, new();
    }
}

