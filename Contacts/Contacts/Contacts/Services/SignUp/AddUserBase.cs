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
        private UserModel _user;
        private List<UserModel> _users;

        public List<UserModel> Users
        {
            get => _users;
            set => _users = value;
        }
        public UserModel User 
        { 
            get => _user; 
            set => _user = value; 
        }

        public AddUserBase(IRepository repository, UserModel user)
        {
            _repository = repository;
            _user = user;
            _users = _repository.GetAllAsync<UserModel>().Result;
        }

        public async Task<int> AddUserBaseAsync(UserModel entity)
        {
            if (Users.Find(x => x.Login == User.Login) != null)
                return 1;
            await _repository.AddAsync<UserModel>(User);
            return 0;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            
        }
    }
}
