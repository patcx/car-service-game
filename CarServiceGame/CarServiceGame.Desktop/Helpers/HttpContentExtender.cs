using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.Helpers
{
    public static class HttpContentExtender
    {
        public static string ReadAsString(this HttpContent content)
        {
            var resultTask = content.ReadAsStringAsync();
            resultTask.Wait();
            return resultTask.Result;
        }
    }
}
