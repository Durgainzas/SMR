using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace SMR.Helpers
{
    public class RSSClient
    {
        private static readonly SyndicationClient _client = new SyndicationClient();

        public RSSClient()
        {

        }

        public async Task<SyndicationFeed> GetFeedAsync(string feedUri)
        {

            if (!Uri.TryCreate(feedUri.Trim(), UriKind.Absolute, out Uri uri))
                return null;

            _client.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            var currentFeed = await _client.RetrieveFeedAsync(uri);

            return currentFeed;
        }

    }
}
