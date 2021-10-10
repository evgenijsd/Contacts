using Contacts.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage
    {
        public List<Contact> Contacts { get; set; }

        public MainListView()
        {
            InitializeComponent();

            Contacts = new List<Contact>
            {
            new Contact {ProfileId = 0, Image="user.png", NickName="Galaxy 1", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="Galaxy 1", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="Galaxy 1", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="Galaxy 1", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="Galaxy 1", Name="Samsung", Description="48000",  Date=DateTime.Now}
            };

            this.BindingContext = this;
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInView());

        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsView());
        }

        private void AddProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEditProfileView());
        }

        public class Contact
        {
            public int ProfileId { get; set; }
            public string Image { get; set; }
            public string NickName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
        }
    }
}