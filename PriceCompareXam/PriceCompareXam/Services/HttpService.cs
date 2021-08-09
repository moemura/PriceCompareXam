using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PriceCompareXam.Services
{
    public class HttpService
    {
        static HttpClient _client = new HttpClient();

        public static HttpClient Client
        {
            get 
            {
                return _client; 
            }
        }
    }
}
