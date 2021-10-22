using Acr.UserDialogs;
using Contacts.Models;
using Contacts.Services.MainList;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Contacts.Services.SignIn;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Contacts.Services.Settings.SettingsType;

namespace Contacts.ViewModels
{
    public class MainListViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IAllSetting _allSetting { get; set; }
        private IMainListService _mainList;
        private int _index;


        public MainListViewModel(INavigationService navigationService, IPageDialogService dialogs, IAllSetting allSetting, IMainListService mainList)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            _mainList = mainList;
            _allSetting = allSetting;

            AddCommand = new DelegateCommand(OnAddCommand);
        }

        #region Public
        public async void Initialize(INavigationParameters parameters)
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            ContactList = await _mainList.GetCollectionAsync(UserId);

            var deleteCommand = new Command(OnDeleteCommand);
            var editCommand = new Command(OnEditCommand);

            foreach (var contact in ContactList)
            {
                contact.DeleteCommand = deleteCommand;
                contact.EditCommand = editCommand;
            }
            IsNull = ContactList.Count == 0;
            Settings = _allSetting.SortSet;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var parameterName = "aId";
            if (parameters.ContainsKey(parameterName))
            {
                ContactView contact = parameters.GetValue<ContactModel>(parameterName).ToContactView();
                if (contact != null)
                {
                    contact.DeleteCommand = new Command(OnDeleteCommand);
                    contact.EditCommand = new Command(OnEditCommand);
                    if (ContactList[_index].Id == contact.Id)
                    {
                        ContactList[_index] = contact;
                    }
                    else
                    {
                        ContactList.Add(contact);
                    }
                }
            }

            parameterName = "sSet";
            if (parameters.ContainsKey(parameterName))
            {
                Settings = parameters.GetValue<int>(parameterName);
            }

            parameterName = "smUserId";
            if (parameters.ContainsKey(parameterName))
            {
                UserId = parameters.GetValue<int>(parameterName);
            }
        }

        #endregion

        #region -- Public properties --
        private ObservableCollection<ContactView> _contactList;
        public ObservableCollection<ContactView> ContactList
        {
            get => _contactList;
            set => SetProperty(ref _contactList, value);
        }

        private int _settings = (int)SetE.SortByName;
        public int Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        private bool _isNull;
        public bool IsNull
        {
            get { return _isNull; }
            set { SetProperty(ref _isNull, value); }
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public DelegateCommand AddCommand { get; set; }

        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(ContactList))
            {
                IsNull = ContactList.Count == 0;
            }

            if (args.PropertyName == nameof(Settings))
            {
                ContactList = _mainList.SortCollection(ContactList, (SetE)Settings);
            }
        }

        #endregion

        #region -- Private helpers --
        private async void OnAddCommand()
        {
            var p = new NavigationParameters { { "maUserId", UserId } };
            await _navigationService.NavigateAsync("AddEditProfileView", p);
        }

        private async void OnEditCommand(object contactObj)
        {
            if (contactObj != null)
            {
                ContactView contact = contactObj as ContactView;
                _index = ContactList.IndexOf(contact);
                var p = new NavigationParameters { { "maContact", contact.ToContact() } };
                await _navigationService.NavigateAsync("AddEditProfileView", p);
            }
        }

        private async void OnDeleteCommand(object contactObj)
        {
            if (contactObj != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Are you sure you want to remove contact?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };
                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                if (confirm)
                {
                    await _mainList.DeleteContactAsync(ContactList, contactObj);
                }
            }
        }
        #endregion
    }
}

