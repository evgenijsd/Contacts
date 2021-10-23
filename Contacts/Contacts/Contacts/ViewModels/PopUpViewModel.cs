using Contacts.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.ViewModels
{
    class PopUpViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService { get; }

        public PopUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBackCommand = new DelegateCommand(OnGoBackCommand);
        }
        public DelegateCommand GoBackCommand { get; set; }
        private async void OnGoBackCommand()
        {
            await _navigationService.GoBackAsync();
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var parameterName = "mpContact";
            if (parameters.ContainsKey(parameterName))
            {
                var contact = parameters.GetValue<ContactView>(parameterName);
                if (contact != null)
                {
                    Image = contact.Image;
                }
            }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
    }
}
