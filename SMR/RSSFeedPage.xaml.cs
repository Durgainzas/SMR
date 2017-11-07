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


        string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ";
        private string rssFeedUri = "http://servis.idnes.cz/rss.aspx?c=zpravodaj";


        public RSSFeedPage()
        {
            this.InitializeComponent();
            Feeder_Init();
        }

        private async void Feeder_Init()
        {
            feed = await rssclient.GetFeedAsync(rssFeedUri);
            try
            {
                foreach (var item in feed.Items)
                {
                    TextBlock.Text += $"{item.Title.Text}. ";
                }
            }
            catch (Exception e)
            {
                TextBlock.Text = e.Message;
                throw;
            }

        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
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
