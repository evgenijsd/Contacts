using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            new Contact {ProfileId = 0, Image="user.png", NickName="UserNick 1", Name="UserName", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="UserNick 2", Name="UserName", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="UserNick 3", Name="UserName", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="UserNick 4", Name="UserName", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="UserNick 5", Name="UserName", Description="48000",  Date=DateTime.Now}
            };

            this.BindingContext = this;
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

        protected void LogOut_ClickedAsync(object sender, EventArgs e)
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
    }
}