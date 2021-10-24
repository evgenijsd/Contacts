using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
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
            get => Preferences.Get(nameof(ThemeSet), (int)ThemeType.LightTheme);
            set => Preferences.Set(nameof(ThemeSet), value);
        }

        public int LangSet
        {
            get => Preferences.Get(nameof(LangSet), (int)LangType.English);
            set => Preferences.Set(nameof(LangSet), value);
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
            switch (language)
            {
                case LangType.English:
                    System.Globalization.CultureInfo.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en");
                    break;
                case LangType.Russian:
                    System.Globalization.CultureInfo.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("ru");
                    break;
                    /*System.Resources.Configuration.Locale = new Locale(lang);
                    Resources.UpdateConfiguration(Resources.Configuration, Resources.DisplayMetrics);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                    Resurces.Culture = culture;*/
            }
            
        }

        public ObservableCollection<LangModel> GetLanguages()
        {
            var Lang = new ObservableCollection<LangModel>()
            {
                new LangModel {Key=(int)LangType.English, Lang="English"},
                new LangModel {Key=(int)LangType.Russian, Lang="Russian"}
            };
            return Lang;     
        }
    }
}
