using Contacts.Services.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private IAllSetting _allSetting { get; set; }
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }


        public SettingsViewModel(INavigationService navigationService, IAllSetting allSetting)
        {
            _allSetting = allSetting;
            Languages = _allSetting.GetLanguages();
            CurrentLanguage = Languages[_allSetting.LangSet];
            _navigationService = navigationService;
            SortSet = _allSetting.SortSet;
            Theme = _allSetting.ThemeSet == (int)ThemeType.LightTheme? false : true;

            switch ((SortType)SortSet)
            {
                case SortType.SortByName:
                    SortName = true; 
                    break;
                case SortType.SortByNickname:
                    SortNickName = true; 
                    break;
                case SortType.SortByData:
                    SortDate = true; 
                    break;
            }
            MainListCommand = new DelegateCommand(OnMainListCommand);
        }

        #region -- Public properties --
        private int _sortSet;
        public int SortSet
        {
            get { return _sortSet; }
            set { SetProperty(ref _sortSet, value); }
        }

        private bool _sortName;
        public bool SortName
        {
            get { return _sortName; }
            set { SetProperty(ref _sortName, value); }
        }

        private bool _sortNickName;
        public bool SortNickName
        {
            get { return _sortNickName; }
            set { SetProperty(ref _sortNickName, value); }
        }

        private bool _sortDate;
        public bool SortDate
        {
            get { return _sortDate; }
            set { SetProperty(ref _sortDate, value); }
        }

        private bool _theme;
        public bool Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        private LangModel _currentLanguage;
        public LangModel CurrentLanguage
        {
            get { return _currentLanguage; }
            set { SetProperty(ref _currentLanguage, value); }
        }

        private ObservableCollection<LangModel> _languages;
        public ObservableCollection<LangModel> Languages
        {
            get { return _languages; }
            set { SetProperty(ref _languages, value); }
        }

        public DelegateCommand MainListCommand { get; private set; }
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SortName):
                    if (SortName) SortSet = (int)SortType.SortByName;
                    break;
                case nameof(SortNickName):
                    if (SortNickName) SortSet = (int)SortType.SortByNickname;
                    break;
                case nameof(SortDate):
                    if (SortDate) SortSet = (int)SortType.SortByData;
                    break;
            }
        }
        #endregion

        #region -- Private helpers --
        private async void OnMainListCommand()
        {
            _allSetting.SortSet = SortSet;
            _allSetting.LangSet = CurrentLanguage.Key;
            _allSetting.ThemeSet = _allSetting.ChangeTheme(Theme);
            var p = new NavigationParameters { { "sSet", SortSet } };
            await _navigationService.NavigateAsync("SignInView", p);
        }

        #endregion
    }

}
