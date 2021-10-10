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
            new Contact {ProfileId = 0, Image="user.png", NickName="User 1", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="User 2", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="User 3", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="User 4", Name="Samsung", Description="48000",  Date=DateTime.Now},
            new Contact {ProfileId = 0, Image="user.png", NickName="User 5", Name="Samsung", Description="48000",  Date=DateTime.Now}
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

        private async Task AddProfile_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditProfileView());
        }

        protected void LogOut_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInView());
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsView());
        }
    }
}