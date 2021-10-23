using Contacts.Models;
using Contacts.Services.Repository;
using System.Text.RegularExpressions;
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
            const string validLogin = @"^\D";
            const string validPassword = @"^(?=.*[A-ZА-ЯЁҐЄЇІ])(?=.*[a-zа-яёґєїі])(?=.*\d)[\d\D]+$";
            CheckEnter check = CheckEnter.ChecksArePassed;

            if (!Regex.IsMatch(Password, validPassword))
            {
                check = CheckEnter.PasswordBigSmallLetterAndDigit;
            }
            if (!Regex.IsMatch(Login, validLogin))
            {
                check = CheckEnter.LoginNotDigitalBegin;
            }
            var user = await _repository.FindAsync<UserModel>(x => x.Login == Login);
            if (user != null)
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
