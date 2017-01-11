using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BasicControlsTutorial.Features
{
    public partial class PopupPage : ContentPage
    {

        public PopupPage()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {

        }

        /// <summary>
        /// Create PopupLayout to put in content page
        /// </summary>
        /// <returns></returns>
        private StackLayout CreatePopupContent()
        {
            // Create Button Login
            var btLogin = new Button
            {
                Text = "Login"
            };
            btLogin.Clicked += (s, e) => OnClickBtLogin();

            // Create Popup Content
            var _PopupContent = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Gray,
                Padding = 10,
                Children =
                {
                    new Label
                    {
                        Text = "Login:",
                        FontSize = 20
                    },

                    new Entry
                    {
                        Placeholder = "Email",
                        Keyboard = Keyboard.Email
                    },

                    new Entry
                    {
                        Placeholder = "Password",
                        IsPassword = true
                    }
                }
            };

            return _PopupContent;
        }

        private void OnClickBtLogin()
        {

        }
    }
}
