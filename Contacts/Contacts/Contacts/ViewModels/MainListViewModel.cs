using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.ViewModels
{
    public class MainListViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogs { get; }

        public MainListViewModel(INavigationService navigationService, IPageDialogService dialogs)
        {
            _navigationService = navigationService;
            _dialogs = dialogs;

            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        #region Public
        public DelegateCommand<string> NavigateCommand { get; }

        #endregion


        #region Overrides
        #endregion

        #region Private


        private async void OnNavigateCommandExecuted(string path)
        {
            var result = await _navigationService.NavigateAsync(path);

            if (!result.Success)
            {
                await _dialogs.DisplayAlertAsync("Error", result.Exception.Message, "Ok");
            }
        }

        #endregion
    }
}
