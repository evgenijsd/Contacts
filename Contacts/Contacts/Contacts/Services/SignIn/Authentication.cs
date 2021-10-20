using Contacts.Models;
using Contacts.Services.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Services.SignIn
{
    public class Authentication : IAuthentication
    {
        private IRepository _repository { get; }
        public Authentication(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CheckAsync(string Login, string Password)
        {
            var userList = await _repository.GetAllAsync<UserModel>();
            var user = userList.FirstOrDefault(x => x.Login == Login);
            if (user != null && user.Login == Login && user.Password == Password)
            {
                return user.Id;
            }
            return 0;
        }
    }
}
