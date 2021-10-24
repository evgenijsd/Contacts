using Xamarin.Essentials;

namespace Contacts.Services.SignIn
{
    public class AuthenticationId : IAuthenticationId
    {
        public int UserId 
        { 
            get => Preferences.Get(nameof(UserId), 0); 
            set => Preferences.Set(nameof(UserId), value); 
        }
    }
}
