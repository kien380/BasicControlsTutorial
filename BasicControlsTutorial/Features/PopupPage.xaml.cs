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
        private Button _MainButtonLogin;
        private Label _MainLabel;
        private PopupLayout _PopupContent;
        private Frame _FramePopup;
        private Entry _PopupEntry;

        public PopupPage()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            // Init Main Label
            _MainLabel = new Label { Text = "you are not login"};

            // Init Main Button Login
            _MainButtonLogin = new Button { Text = "Click to Login" };
            _MainButtonLogin.Clicked += (s, e) => OnClickMainBtLogin();

            _FramePopup = CreatePopupView();

            _PopupContent = new PopupLayout();
            _PopupContent.Content = CreatePopupContent();

        }

        /// <summary>
        /// Create PopupLayout to put in content page
        /// </summary>
        /// <returns></returns>
        private StackLayout CreatePopupContent()
        {
            // Create main button click
            _MainButtonLogin.Clicked += (s, e) => OnClickMainBtLogin();
            _MainButtonLogin.Text = "Login";
            
            // Create Popup Content
            var popupContent = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    _MainButtonLogin, 
                    _MainLabel
                }
            };

            return popupContent;
        }

        /// <summary>
        /// Create PopupView to show when click Button Main Login
        /// </summary>
        /// <returns></returns>
        private Frame CreatePopupView()
        {
            // Create Button Login
            var btLogin = new Button
            {
                Text = "Login"
            };
            btLogin.Clicked += (s, e) => OnClickBtLogin();

            // Init Popup Entry Email
            _PopupEntry = new Entry
            {
                Placeholder = "Email"
            };

            var _Popup = new StackLayout
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
                        Text = "Login",
                        FontSize = 20
                    },

                    _PopupEntry,

                    new Entry
                    {
                        Placeholder = "Password",
                        IsPassword = true
                    }
                }
            };

            // Create Frame to make Popup nicer
            var _FramePopup = new Frame
            {
                Content = _Popup,
                HasShadow = true,
                Padding = 2,
                BackgroundColor = Color.Purple
            };


            return _FramePopup;
        }

        private void OnClickBtLogin()
        {
            _PopupContent.DismissPopup();

            _MainLabel.Text = "Hello " + _PopupEntry.Text + "!";
        }

        private void OnClickMainBtLogin()
        {
            // Set position when Popup is showed
            double ParentWidth = this.Width;    // Screen Horizontal Size
            double ParentHeight = this.Height;  // Screen Vertical Size
            double PopupWidth = _FramePopup.Width;
            double PopupHeight = _FramePopup.Height;
            double XPosition = (ParentWidth / 2) - (PopupWidth / 2);
            double YPosition = (ParentHeight / 2) - (PopupHeight / 2);

            _PopupContent.ShowPopup(_FramePopup, Constraint.Constant(XPosition), Constraint.Constant(YPosition));
        }
    }
}
