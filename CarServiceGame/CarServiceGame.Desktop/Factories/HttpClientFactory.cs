using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Desktop.Abstract;

namespace CarServiceGame.Desktop.Factories
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private string token;

        public void SetToken(string token)
        {
            this.token = token;
        }

        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
