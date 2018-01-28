using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.I2c;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Core;
using Core.Model;


namespace Martj14UWP
{
   
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Savebtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (Email.Text.Contains("@") && Email.Text.Contains("."))
            {
                int result = 0;
                result =
                    Controller.Instance.AddSubmission(new Submission(First.Text, Last.Text, Email.Text, Phone.Text,
                        BirthDate.Date, Password.Text, Serial.Text));
                switch (result)
                {
                    //Already registred
                    case 0:
                        PopupText.Text = "User Already Registred, Login to Claim More Serial Numbers";
                        Popup.IsOpen = true;
                        break;
                    //Invalid Serial
                    case 1:
                        PopupText.Text = "Serial Number Is Invalid or Already Claimed";
                        Popup.IsOpen = true;
                        break;
                    //Submission and Serial valid registration complete
                    case 2:
                        PopupText.Text = First.Text + " Registred, Login to Claim More Serial Numbers";
                        Popup.IsOpen = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                PopupText.Text = "Email Is Invalid";
                Popup.IsOpen = true;
            }

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.Instance.LoginChecker(User.Text, UserPassword.Text) && User.Text.Equals("admin"))
            {
                this.Frame.Navigate(typeof(AdminPage));
            }
            else if (Controller.Instance.LoginChecker(User.Text, UserPassword.Text))
            {
                this.Frame.Navigate(typeof(UserPage));
            }
            else
            {
                PopupText.Text = "User Email or Password Incorrect";
                Popup.IsOpen = true;
            }
        }

        private void PopupBtn_OnClick_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = false;
        }
    }
}
