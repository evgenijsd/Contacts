using Acr.UserDialogs;
using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Contacts.Services.SignIn;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class MainListViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IRepository _repository { get; }
        public DelegateCommand<string> AddCommand { get; private set; }
        private ICheckAuthorization _checkAuthorization { get; set; }
        private ISortSetting _sortSetting { get; set; }
        public DelegateCommand<string> EditCommand { get; set; }
        public DelegateCommand<string> DeleteCommand { get; set; }



        public MainListViewModel(INavigationService navigationService, IPageDialogService dialogs, ICheckAuthorization checkAuthorization, IRepository repository, ISortSetting sortSetting)
        {
            _navigationService = navigationService;
            _repository = repository;
            _dialogs = dialogs;
            _sortSetting = sortSetting;
            _checkAuthorization = checkAuthorization;
            UserId = _checkAuthorization.UserId;

            AddCommand = new DelegateCommand<string>(OnAddCommand);
            EditCommand = new DelegateCommand<string>(OnEditCommand);
            DeleteCommand = new DelegateCommand<string>(OnDeleteCommand);
        }

        private ObservableCollection<ContactView> _contactList;
        public ObservableCollection<ContactView> ContactList
        {
            get => _contactList;
            set => SetProperty(ref _contactList, value);
        }

        private int _settings = 100;
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

        private int _index;
        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        #region Public
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            var parameterName = "aId";
            if (parameters.ContainsKey(parameterName))
            {
                int id = parameters.GetValue<int>(parameterName);
                if (id != 0)
                {
                    var contact = (await _repository.GetByIdAsync<ContactModel>(id)).ToContactView();
                    contact.DeleteCommand = new Command(OnDeleteCommand);
                    contact.EditCommand = new Command(OnEditCommand);
                    if (ContactList[_index].Id == contact.Id)
                    {
                        ContactList[_index] = contact;
                    }
                    else { 
                        ContactList.Add(contact);
                    }
                }
            }

            parameterName = "sSet";
            if (parameters.ContainsKey(parameterName))
            {
                Settings = parameters.GetValue<int>(parameterName);
            }
        }

        public async void Initialize(INavigationParameters parameters)
        {
            var contacts = await _repository.GetAllAsync<ContactModel>();
            ContactList = new ObservableCollection<ContactView>(contacts.FindAll(x => x.UserId == UserId).Select(x => x.ToContactView()).OrderBy(x => x.Name));

            var deleteCommand = new Command(OnDeleteCommand);
            var editCommand = new Command(OnEditCommand);

            foreach (var contact in ContactList)
            {
                contact.DeleteCommand = deleteCommand;
                contact.EditCommand = editCommand;
            }
            IsNull = ContactList.Count == 0;
            Settings = _sortSetting.SortSet;

            //ContactList.Sort
        }

        private async void OnEditCommand(object contactObj)
        {
            if (contactObj != null)
            {
                ContactView contact = contactObj as ContactView;
                _index = ContactList.IndexOf(contact);
                //if (contact != null) await _dialogs.DisplayAlertAsync("Alert", $"login id {index} - {contact.Id} - {contact.UserId}", "Ok");
                var p = new NavigationParameters();
                p.Add("maId", contact.Id);
                //if (contact != null) await _dialogs.DisplayAlertAsync("Alert", $"contact - {contact.Id}", "Ok");
                await _navigationService.NavigateAsync("AddEditProfileView", p);
            }
        }

        public async void OnDeleteCommand(object contactObj)
        {
            if (contactObj != null)
            {
                ContactView contact = contactObj as ContactView;
                ContactModel contactdel = contact.ToContact();
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Are you sure you want to remove contact?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };
                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                if (confirm)
                {
                    await _repository.RemoveAsync<ContactModel>(contactdel);
                    ContactList.Remove(contact);
                    //await _dialogs.DisplayAlertAsync("Alert", $"login id {UserId}", "Ok");
                }
            }
        }

        #endregion


        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(ContactList))
            {
                IsNull = ContactList.Count == 0;
            }

            if (args.PropertyName == nameof(Settings))
            {
                switch (Settings)
                {
                    case 0: ContactList = new ObservableCollection<ContactView>(ContactList.OrderBy(x => x.Name)); break;
                    case 1: ContactList = new ObservableCollection<ContactView>(ContactList.OrderBy(x => x.Nickname)); break;
                    case 2: ContactList = new ObservableCollection<ContactView>(ContactList.OrderBy(x => x.Date)); break;
                    default: break;
                }
            }

        }
        #endregion

        #region Private
        private async void OnAddCommand(string parameter)
        {
            var p = new NavigationParameters();
            p.Add("maUserId", UserId);
            await _navigationService.NavigateAsync("AddEditProfileView", p);
        }

        #endregion
    }
}
