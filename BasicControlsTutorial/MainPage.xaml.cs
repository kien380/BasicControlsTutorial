using BasicControlsTutorial.Features;
using System;
using Xamarin.Forms;

namespace BasicControlsTutorial
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_DialogPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PopupPage());
        }
    }
}
