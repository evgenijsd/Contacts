using Acr.UserDialogs;
using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.SignIn;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }
        private IRepository _repository { get; }
        private IAuthenticationId _checkAuthorization { get; set; }

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

        private int _id = 0;
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



        public AddEditProfileViewModel(INavigationService navigationService, IPageDialogService dialogs, IRepository repository, IAuthenticationId checkAuthorization)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;
            _checkAuthorization = checkAuthorization;
            UserId = _checkAuthorization.UserId;
            _repository = repository;
        }

        public ICommand OpenSaveCommand => new Command(OnOpenSaveCommandAsync, () => CanSave);
        public ICommand ImageCommand => new Command(OnImageCommand);

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
            ContactModel contact = new ContactModel
            {
                Id = Id,
                UserId = UserId,
                Image = Image,
                Name = Name,
                Nickname = Nickname,
                Description = Description,
                Date = DateTime.Now
            };
            if (Title == "Add Profile") {
                int result = await _repository.AddAsync<ContactModel>(contact);
            }
            else {
                int result = await _repository.UpdateAsync<ContactModel>(contact);
                //await _dialogs.DisplayAlertAsync("Alert", $"login id {contact.UserId} contact id  {contact.Id}, result -{result}", "Ok");
            }
            //await _dialogs.DisplayAlertAsync("Alert", $"login id {contact.UserId} contact id  {contact.Id}, result -{result}", "Ok");
            var p = new NavigationParameters();
            p.Add("aId", contact.Id);
            await _navigationService.GoBackAsync(p);
        }

        private async void Result(int i)
        {
            if (i == 0)
            {
                Image = (await MediaPicker.PickPhotoAsync()).FullPath;
            }
            else
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                Image = photo.FullPath;
            }
            //_dialogs.DisplayAlertAsync("Alert", $"login id {i}", "Ok");
        }

        private void OnImageCommand()
        {
            var actionSheetConfig = new ActionSheetConfig()
                .SetTitle("Choose Type")
                .SetMessage("Image")
                .Add("Pick at Gallery", () => this.Result(0), "gallery.png")
                .Add("Take photo with camera ", () => this.Result(1), "photo.png");
            var confirm =  UserDialogs.Instance.ActionSheet(actionSheetConfig);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            Title = "Add Profile";

            string parameterName = "maUserId";
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
                UserId = Contact.UserId;
                Image = Contact.Image;
                Name = Contact.Name;
                Nickname = Contact.Nickname;
                Description = Contact.Description;
                Date = Contact.Date;
            }

        }

        #endregion

        #region -- Public properties --


        #endregion

        #region -- Overrides --


        #endregion

        #region -- Private helpers --


        #endregion
    }
}
