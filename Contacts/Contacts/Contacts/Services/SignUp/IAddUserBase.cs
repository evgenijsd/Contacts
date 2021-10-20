using Contacts.Models;
using System.Threading.Tasks;

namespace Contacts.Services.SignUp
{
    public interface IAddUserBase
    {
        Task<int> UserAddAsync(UserModel user);
        Task<int> CheckTheCorrectnessAsync(string Login, string Password, string ConfirmPassword);
    }
}
