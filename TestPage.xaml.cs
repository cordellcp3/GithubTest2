using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace WP7Test1
{
    public partial class MainPage : PhoneApplicationPage
    {

        int counter;
        CameraCaptureTask cct;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;

            //App läuft weiter, auch wenn das Gerät in Standby geht oder der LockScreen erscheint!
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            cct = new CameraCaptureTask();
            cct.Completed += new EventHandler<PhotoResult>(cct_Completed);

            counter = 0;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
        }

        void cct_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                textBox1.Text = "Scanned the Barcode!";
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
            }
            else
            {
                textBox1.Text = "ERROR";
            }
        }

        void  button1_Click(object sender, EventArgs e)
        {
 	        MessageBox.Show("...it works!");
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void OrderCoffeeClick(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Kaffee wird geordert", "Info", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                WebBrowserTask wbt = new WebBrowserTask();
                wbt.URL = "http://twitter.com";
                wbt.Show();
            }
            else 
            {
				//TODO:
                //...
            }
        }

        private void btn_Hide_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationBar.IsVisible == true)
            {
                btn_Hide.Content = "Show AppBar";
                ApplicationBar.IsVisible = false;
            }
            else
            {
                btn_Hide.Content = "Hide AppBar";
                ApplicationBar.IsVisible = true;
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            counter++;

            NavigationService.Navigate(new Uri(string.Format("/About.xaml?param={0}", counter), UriKind.Relative)); 
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            cct.Show();
        }

        private void SettingsClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }


    }
}
