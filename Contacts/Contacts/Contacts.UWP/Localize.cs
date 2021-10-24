using Contacts.Services;
using System;
using System.Globalization;
using Xamarin.Forms;

[assembly: Dependency(typeof(Contacts.UWP.Localize))]
namespace Contacts.UWP
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return CultureInfo.CurrentUICulture;
        }
    }
}
