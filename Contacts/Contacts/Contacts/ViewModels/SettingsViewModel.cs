using Contacts.Services.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private IPageDialogService _dialogs { get; }
        private ISortSetting _sortSetting;
        private INavigationService _navigationService { get; }
        public DelegateCommand<string> ButtonClickCommand { get; private set; }

        public SettingsViewModel(IPageDialogService dialogs, INavigationService navigationService, ISortSetting sortSetting)
        {
            _dialogs = dialogs;
            _sortSetting = sortSetting;
            _navigationService = navigationService;
            SortSet = _sortSetting.SortSet;
            switch (SortSet)
            {
                case 0: SortName = true; break;
                case 1: SortNickName = true; break;
                case 2: SortDate = true; break;
                default: break;
            }
            ButtonClickCommand = new DelegateCommand<string>(Execute);
        }

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

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SortName))
            {
                if (SortName) SortSet = 0;
            }
            if (args.PropertyName == nameof(SortNickName))
            {
                if (SortNickName) SortSet = 1;
            }
            if (args.PropertyName == nameof(SortDate))
            {
                if (SortDate) SortSet = 2;
            }
        }

        private async void Execute(string parameter)
        {
            _sortSetting.SortSet = SortSet;
            //await _dialogs.DisplayAlertAsync("Alert", $"sortsetting {SortSet}", "Ok");
            var p = new NavigationParameters();
            p.Add("sSet", SortSet);
            await _navigationService.GoBackAsync(p);
        }
    }
}
