using Contacts.Services.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using static Contacts.Services.Settings.SettingsType;

namespace Contacts.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private IPageDialogService _dialogs { get; }
        private IAllSetting _sortSetting;
        private INavigationService _navigationService { get; }
        

        public SettingsViewModel(INavigationService navigationService, IAllSetting sortSetting)
        {
            _sortSetting = sortSetting;
            _navigationService = navigationService;
            SortSet = _sortSetting.SortSet;
            switch ((SetE)SortSet)
            {
                case SetE.SortByName:
                    SortName = true; break;
                case SetE.SortByNickname:
                    SortNickName = true; break;
                case SetE.SortByData:
                    SortDate = true; break;
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

        public DelegateCommand MainListCommand { get; private set; }
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SortName):
                    if (SortName) SortSet = (int)SetE.SortByName;
                    break;
                case nameof(SortNickName):
                    if (SortNickName) SortSet = (int)SetE.SortByNickname;
                    break;
                case nameof(SortDate):
                    if (SortDate) SortSet = (int)SetE.SortByData;
                    break;
            }
        }
        #endregion

        #region -- Private helpers --
        private async void OnMainListCommand()
        {
            _sortSetting.SortSet = SortSet;
            var p = new NavigationParameters { { "sSet", SortSet } };
            await _navigationService.GoBackAsync(p);
        }

        #endregion
    }

}
