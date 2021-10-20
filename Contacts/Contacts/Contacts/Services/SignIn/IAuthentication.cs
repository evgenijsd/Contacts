using System.Threading.Tasks;

namespace Contacts.Services.SignIn
{
    public interface IAuthentication
    {
        Task<int> CheckAsync(string Login, string Password);
    }
}
