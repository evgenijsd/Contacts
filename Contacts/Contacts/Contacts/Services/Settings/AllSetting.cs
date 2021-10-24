using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.Services.Settings
{
    public class AllSetting : IAllSetting
    {

        #region -- Public properties --
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
        #endregion

        public string Language(LangType language)
        {
            string result = "en-US";
            switch (language)
            {
                case LangType.English:
                    result = "en-US";
                    CultureInfo.CurrentCulture = new CultureInfo(result);
                    CultureInfo.CurrentUICulture = new CultureInfo(result);
                    break;
                case LangType.Russian:
                    result = "ru-RU";
                    CultureInfo.CurrentCulture = new CultureInfo(result);
                    CultureInfo.CurrentUICulture = new CultureInfo(result);
                    break;
            }
            return result;
            
        }

        public ObservableCollection<LangModel> GetLanguages()
        {
            var Lang = new ObservableCollection<LangModel>()
            {
                new LangModel {Key=(int)LangType.English, Lang=Resurces.Resource.LangEng},
                new LangModel {Key=(int)LangType.Russian, Lang=Resurces.Resource.LangRus}
            };
            return Lang;     
        }
    }
}
