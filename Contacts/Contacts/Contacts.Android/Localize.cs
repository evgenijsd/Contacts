using Contacts.Services;
using System;
using System.Globalization;
using Xamarin.Forms;

[assembly: Dependency(typeof(Contacts.Droid.Localize))]
namespace Contacts.Droid
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");
            return new System.Globalization.CultureInfo(netLanguage);
        }
    }
}