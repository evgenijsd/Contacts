using Xamarin.Essentials;
using static Contacts.Services.Settings.SettingsType;

namespace Contacts.Services.Settings
{
    public class AllSetting : IAllSetting
    {
        public int SortSet
        {
            get => Preferences.Get(nameof(SortSet), (int)SettingsType.SortByName);
            set => Preferences.Set(nameof(SortSet), value);
        }
    }
}
