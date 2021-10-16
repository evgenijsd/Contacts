using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.SignIn;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
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
        public DelegateCommand<string> EditCommand { get; set; }
        public DelegateCommand<string> DeleteCommand { get; set; }


        public MainListViewModel(INavigationService navigationService, IPageDialogService dialogs, ICheckAuthorization checkAuthorization, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
            _dialogs = dialogs;
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

        #region Public
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("aId"))
            {
            }
        }
        public async void Initialize(INavigationParameters parameters)
        {
            var contacts = await _repository.GetAllAsync<ContactModel>();
            ContactList = new ObservableCollection<ContactView>(contacts.FindAll(x => x.UserId == UserId).Select(x => x.ToContactView()));

            var deleteCommand = new Command(OnDeleteCommand);
            var editCommand = new Command(OnEditCommand);

            foreach (var contact in ContactList)
            {
                contact.DeleteCommand = deleteCommand;
                contact.EditCommand = editCommand;
            }
            IsNull = ContactList.Count == 0;
        }

        private async void OnEditCommand(object contactObj)
        {
            ContactView contact = contactObj as ContactView;
            int index = ContactList.IndexOf((ContactView)contactObj);
            if (contact != null) await _dialogs.DisplayAlertAsync("Alert", $"login id {index} - {contact.Id} - {contact.UserId}", "Ok");
            var p = new NavigationParameters();
            p.Add("maId", contact.Id);
            await _navigationService.NavigateAsync("AddEditProfileView");
        }

        public async void OnDeleteCommand(object contactObj)
        {
            await _dialogs.DisplayAlertAsync("Alert", $"login id {UserId}", "Ok");
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
        }
        #endregion

        #region Private
        private async void OnAddCommand(string parameter)
        {
            //await _dialogs.DisplayAlertAsync("Alert", $"Count {ContactList.Count}", "Ok");
            var p = new NavigationParameters();
            p.Add("maUserId", UserId);
            p.Add("maId", 7);
            await _navigationService.NavigateAsync("AddEditProfileView");
        }
        #endregion
    }
}
