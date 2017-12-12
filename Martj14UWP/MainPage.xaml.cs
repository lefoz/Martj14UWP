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

        private async void Savebtn_OnClick(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            IController c = new Controller();
            int result = 0;
            await Task.Run(() => result = c.AddSubmission(new Submission(First.Text,Last.Text,Email.Text,Phone.Text, Bithdate.Date ,Password.Text, Serial.Text)));
            switch (result)
            {
                case 1:

                    break;
                case 2:

                    break;
                default:
                    break;
            }

        }
    }
}
