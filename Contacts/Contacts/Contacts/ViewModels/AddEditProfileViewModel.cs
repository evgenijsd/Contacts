using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.SignIn;
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
    public class AddEditProfileViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IRepository _repository { get; }
        private ICheckAuthorization _checkAuthorization { get; set; }

        private bool _CanSave;
        public bool CanSave
        {
            get => _CanSave;
            set
            {
                if (SetProperty(ref _CanSave, value))
                {
                    RaisePropertyChanged(nameof(OpenSaveCommand));
                }
            }
        }

        private ObservableCollection<ContactModel> _contactList;
        public ObservableCollection<ContactModel> ContactList
        {
            get => _contactList;
            set => SetProperty(ref _contactList, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ContactModel _contact;
        public ContactModel Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _image = "user.png";
        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }
        

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }



        public AddEditProfileViewModel(INavigationService navigationService, IPageDialogService dialogs, IRepository repository, ICheckAuthorization checkAuthorization)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            _checkAuthorization = checkAuthorization;
            UserId = _checkAuthorization.UserId;
            _repository = repository;
        }

        public ICommand OpenSaveCommand => new Command(OnOpenSaveCommandAsync, () => CanSave);

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Name))
            {
                CanSave = Name != string.Empty && Nickname != string.Empty;
            }
            if (args.PropertyName == nameof(Nickname))
            {
                CanSave = Name != string.Empty && Nickname != string.Empty;
            }

        }

        #endregion

        #region -- Private helpers --

        private async void OnOpenSaveCommandAsync()
        {
            ContactModel contact = new ContactModel();
            contact.UserId = UserId;
            contact.Image = Image;
            contact.Name = Name;
            contact.Nickname = Nickname;
            contact.Description = Description;
            contact.Date = DateTime.Now;
            var result = await _repository.AddAsync<ContactModel>(contact);
            await _dialogs.DisplayAlertAsync("Alert", $"login id {contact.UserId} contact id  {contact.Id}, result -{result}", "Ok");
            var p = new NavigationParameters();
            p.Add("aId", contact.Id);
            
            await _navigationService.GoBackAsync(p);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            Title = "Add Profile";

            var parameterName = "maUserId";
            if (parameters.ContainsKey(parameterName))
            {
                UserId = parameters.GetValue<int>(parameterName);
            }

            parameterName = "maId";
            if (parameters.ContainsKey(parameterName))
            {
                Title = "Edit Profile";
                Id = parameters.GetValue<int>(parameterName);
                Contact = await _repository.GetByIdAsync<ContactModel>(Id);
                Nickname = Contact.Nickname;
                Name = Contact.Nickname;
                Description = Contact.Description;
            }

        }

        public async void Initialize(INavigationParameters parameters)
        {
            //Id = parameters.GetValue<int>("maId");
            if (Id != 0)
            {
                var contactList = await _repository.GetAllAsync<ContactModel>();
                ContactList = new ObservableCollection<ContactModel>(contactList);
                Contact  = ContactList.FirstOrDefault(x => x.Id == Id);
                UserId = Contact.UserId;
                Image = Contact.Image;
                Name = Contact.Name;
                Nickname = Contact.Nickname;
                Description = Contact.Description;
                Date = Contact.Date;
            }
        }

        #endregion

    }
}
