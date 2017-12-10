using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.Abstract
{
    public interface IHttpClientFactory
    {
        void SetToken(string token);
        HttpClient GetClient();
    }
}
