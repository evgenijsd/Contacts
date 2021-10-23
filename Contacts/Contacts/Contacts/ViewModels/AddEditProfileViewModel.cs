using Acr.UserDialogs;
using Contacts.Models;
using Contacts.Services.AddEditProfile;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService { get; }
        private IAddEditService _addEdit;
        enum ImageChoise { gallery, camera }

        public AddEditProfileViewModel(INavigationService navigationService, IAddEditService addEdit)
        {
            _navigationService = navigationService;
            _addEdit = addEdit;
        }


        #region -- Public properties --

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

        private string _title = "Add Profile";
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

        public ICommand OpenSaveCommand => new Command(OnSaveCommandAsync, () => CanSave);
        public ICommand ImageCommand => new Command(OnImageCommand);
        #endregion

        #region Public 
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            string parameterName = "maUserId";
            if (parameters.ContainsKey(parameterName))
            {
                UserId = parameters.GetValue<int>(parameterName);
            }

            parameterName = "maContact";
            if (parameters.ContainsKey(parameterName))
            {
                Title = "Edit Profile";
                Contact = parameters.GetValue<ContactModel>(parameterName);
                {
                    Id = Contact.Id;
                    UserId = Contact.UserId;
                    Image = Contact.Image;
                    Name = Contact.Name;
                    Nickname = Contact.Nickname;
                    Description = Contact.Description;
                    Date = Contact.Date;
                }
            }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Name):
                case nameof(Nickname):
                    CanSave = !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Nickname);
                    break;
            }

        }
        #endregion

        #region -- Private helpers --
        private void OnImageCommand()
        {
            var actionSheetConfig = new ActionSheetConfig()
                .SetTitle("Choose Type")
                .SetMessage("Image")
                .Add("Pick at Gallery", () => this.ResultGalleryPhoto(ImageChoise.gallery), "gallery.png")
                .Add("Take photo with camera ", () => this.ResultGalleryPhoto(ImageChoise.camera), "photo.png");
            var confirm = UserDialogs.Instance.ActionSheet(actionSheetConfig);
        }

        private async void ResultGalleryPhoto(ImageChoise choise)
        {
            switch (choise)
            {
                case ImageChoise.gallery:
                    Image = (await MediaPicker.PickPhotoAsync()).FullPath;
                    break;
                case ImageChoise.camera:
                    Image = await _addEdit.Photo();
                    break;
            }
        }

        private async void OnSaveCommandAsync()
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
            await _addEdit.AddEditExecute(Title, contact);
            var p = new NavigationParameters { { "amContact", contact } };
            await _navigationService.GoBackAsync(p);
        }
        #endregion
    }
}
