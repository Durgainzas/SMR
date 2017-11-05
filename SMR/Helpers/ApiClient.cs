﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace SMR.Helpers
{
    public class ApiClient
    {
        private const string ApiKey = "0GdSrwLGGC1SDN7YpHwwcQPCVF1ROooi";

        private static readonly HttpClient _client = new HttpClient();


        public ApiClient()
        {

        }

        public async Task<HttpResponseMessage> GetAsync(string baseUri, string requestUri, bool addApiKey = true)
        {
            string uri = $"{baseUri}{requestUri}";

            if (addApiKey) uri += $"apikey={ApiKey}";

            var response = await _client.GetAsync(uri).ConfigureAwait(false);

            return response;
        }

    }
}
