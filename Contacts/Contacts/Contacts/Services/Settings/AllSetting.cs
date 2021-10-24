using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.Services.Settings
{
    public class AllSetting : IAllSetting
    {
        public int SortSet
        {
            get => Preferences.Get(nameof(SortSet), (int)SortType.SortByName);
            set => Preferences.Set(nameof(SortSet), value);
        }

        public int ThemeSet
        {
            get => Preferences.Get(nameof(SortSet), (int)ThemeType.LightTheme);
            set => Preferences.Set(nameof(SortSet), value);
        }

        public int LangSet
        {
            get => Preferences.Get(nameof(SortSet), (int)LangType.ChoiseEng);
            set => Preferences.Set(nameof(SortSet), value);
        }

        public int ChangeTheme(bool theme) 
        {
            int result = (int)ThemeType.LightTheme;
            if (theme)
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
                result = (int)ThemeType.DarkTheme;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Unspecified;
            }
            return result;
        }

        public void ChangeLanguage(LangType language)
        {
        }
    }
}
