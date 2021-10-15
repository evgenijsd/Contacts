using Contacts.Models;
using Contacts.Services.Repository;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.SignUp
{
    public class AddUserBase : IAddUserBase
    {
        private IRepository _repository;

        public AddUserBase(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddUserBaseAsync(UserModel User)
        {
            var id = await _repository.AddAsync<UserModel>(User);
            return id;
        }


    }
}
