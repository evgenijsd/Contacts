using Contacts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView
    {
        public List<ContactModel> Contacts { get; set; }

        public MainListView()
        {
            InitializeComponent();

            Contacts = new List<ContactModel>
            {
            new ContactModel {Id = 0, Image="user.png", NickName="UserNick 1", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 1, Image="user.png", NickName="UserNick 2", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 2, Image="user.png", NickName="UserNick 3", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 3, Image="user.png", NickName="UserNick 4", Name="UserName", Description="48000",  Date=DateTime.Now},
            new ContactModel {Id = 4, Image="user.png", NickName="UserNick 5", Name="UserName", Description="48000",  Date=DateTime.Now}
            };

            this.BindingContext = this;
        }
    }
}