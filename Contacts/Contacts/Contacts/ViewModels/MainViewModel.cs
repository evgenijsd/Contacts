using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Contacts.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware, IInitialize
    {
        private INavigationService _navigationService { get; }
        public DelegateCommand AddCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddCommand = new DelegateCommand(AddAction);
        }

        public void Initialize(INavigationParameters parameters)
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }



        private async void AddAction()
        {
            int id = 5;
            await _navigationService.NavigateAsync("AddEditProfileView?maId="+id.ToString());
        }
    
    }
}
