using Contacts.Models;
using Contacts.Services.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Services.SignUp
{
    public class AddUserBase : CheckType, IAddUserBase
    {

        private IRepository _repository;
        public AddUserBase(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> UserAddAsync(UserModel user)
        {
            return await _repository.AddAsync(user);
        }

        public async Task<int> CheckTheCorrectnessAsync(string Login, string Password, string ConfirmPassword)
        {
            CheckEnter check = CheckEnter.ChecksArePassed;
            var userList = await _repository.GetAllAsync<UserModel>();
            if (userList.FirstOrDefault(x => x.Login == Login) != null)
            {
                check = CheckEnter.LoginExist;
            }
            if (Password != ConfirmPassword)
            {
                check = CheckEnter.PasswordsNotEqual;
            }
            if (Password.Length < 8 || Password.Length > 16)
            {
                check = CheckEnter.PasswordLengthNotValid;
            }
            if (Login.Length < 4 || Login.Length > 16)
            {
                check = CheckEnter.LoginLengthNotValid;
            }
            return (int)check;
        }


    }
}
