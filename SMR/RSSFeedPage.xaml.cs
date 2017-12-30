using SMR.Helpers;
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
using Windows.Web.Syndication;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RSSFeedPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        SyndicationFeed feed = new SyndicationFeed();
        RSSClient rssclient = new RSSClient();

        int counter = 0;

        private string placeholder = "\t\t\t\t\t";
        
        private string rssFeedUri = "http://servis.idnes.cz/rss.aspx?c=zpravodaj";


        public RSSFeedPage()
        {
            this.InitializeComponent();
            Feeder_Init();
        }

        private async void Feeder_Init()
        {

            try
            {
                feed = await rssclient.GetFeedAsync(rssFeedUri);
                TextBlockRSS.Text = placeholder;
                for (int i = 0; i < 5; i++)
                {
                    TextBlockRSS.Text += $"{feed.Items[i].Title.Text}. | ";
                }

                TextBlockRSS.Text += placeholder;
                scrollviewer.ChangeView(-scrollviewer.HorizontalOffset, null, null, true);
                Scrolling_Start();
            }
            catch (Exception)
            {
                TextBlockRSS.Text = "Error loading data. Please check internet connection or try again later.";
            }

            
        }

        private void Scrolling_Start()
        {
            timer.Tick += (ss, ee) =>
            {
                if (timer.Interval.Ticks == 300)
                {
                    //each time set the offset to scrollviewer.HorizontalOffset + 5
                    scrollviewer.ChangeView(scrollviewer.HorizontalOffset + 5, null, null);
                    //if the scrollviewer scrolls to the end, scroll it back to the start.
                    if (scrollviewer.HorizontalOffset == scrollviewer.ScrollableWidth)
                    {
                        scrollviewer.ChangeView(-scrollviewer.HorizontalOffset, null, null, true);
                    }
                    if (counter == 3000) //10minutes
                    {
                        counter = 0;
                        timer.Stop();
                        Feeder_Init();
                    }
                    counter += 1;
                }
            };
            timer.Interval = new TimeSpan(300);
            timer.Start();
        }

        private void Scrollviewer_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }
}
