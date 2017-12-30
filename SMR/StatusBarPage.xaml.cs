using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatusBarPage : Page
    {
        DateTime dateTime = DateTime.Now;
        DispatcherTimer timer = new DispatcherTimer();


        public StatusBarPage()
        {
            this.InitializeComponent();
        }



        private void TextBlockDateTime_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlockDateTime.Text = "Loading...";
            Timer_Start();
            
        }

        private void Timer_Start()
        {
            timer.Tick += (ss, ee) =>
            {
                if (timer.Interval.Ticks == 1000)
                {
                    DateTime dateTime = DateTime.Now;
                    var actualTimeFormated = String.Format("{0:d.M.yyyy HH:mm:ss}", dateTime);
                    TextBlockDateTime.Text = actualTimeFormated;
                }

            };
            timer.Interval = new TimeSpan(1000);
            timer.Start();
        }

        private void Timer_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }
}
