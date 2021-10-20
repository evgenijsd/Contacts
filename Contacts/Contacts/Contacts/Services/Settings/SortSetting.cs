using Xamarin.Essentials;

namespace Contacts.Services.Settings
{
    public class SortSetting : ISortSetting
    {
        public int SortSet
        {
            get => Preferences.Get(nameof(SortSet), 0);
            set => Preferences.Set(nameof(SortSet), value);
        }
    }
}
