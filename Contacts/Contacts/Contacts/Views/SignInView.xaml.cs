using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();

            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            { NumberOfTapsRequired = 1 };
            tapGesture.Tapped += async (s, e) =>
            { await Navigation.PushAsync(new SignUpView()); };
            labelUP.GestureRecognizers.Add(tapGesture);
        }

        private async void ButtonMain_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainListView());
        }
    }
}