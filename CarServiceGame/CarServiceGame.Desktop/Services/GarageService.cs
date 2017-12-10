using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Desktop.Abstract;
using CarServiceGame.Desktop.Helpers;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Entities;
using Newtonsoft.Json;

namespace CarServiceGame.Desktop.Services
{
    public class GarageService : IGarageRepository
    {
        private IHttpClientFactory httpClientFactory;

        public GarageService(IHttpClientFactory clientFactory)
        {
            this.httpClientFactory = clientFactory;
        }

        public Garage GetGarage(string name, string password)
        {
            using (var client = httpClientFactory.GetClient())
            {
                HttpContent content = new StringContent($"name={name}&password={password}", Encoding.ASCII, "application/x-www-form-urlencoded");
                var responseTask = client.PostAsync($"{Config.Domain}api/v1/Garage", content);
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    return null;

                httpClientFactory.SetToken((string)obj.token);

                var r = JsonConvert.SerializeObject(obj.garage);
                return JsonConvert.DeserializeObject<Garage>(r);
            }
        }

        public Garage CreateGarage(string name, string password)
        {
            using (var client = httpClientFactory.GetClient())
            {
                HttpContent content = new StringContent($"name={name}&password={password}", Encoding.ASCII, "application/x-www-form-urlencoded");
                var responseTask = client.PostAsync($"{Config.Domain}api/v1/Garage/Register", content);
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    return null;

                httpClientFactory.SetToken((string)obj.token);

                var r = JsonConvert.SerializeObject(obj.garage);
                return JsonConvert.DeserializeObject<Garage>(r);
            }
        }

        public decimal GetGarageBalance(Guid garageId)
        {
            using (var client = httpClientFactory.GetClient())
            {
                var responseTask = client.GetAsync($"{Config.Domain}api/v1/Garage/Balance");
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    return 0;

                return obj.balance;
            }
        }

        public IEnumerable<GarageRanking> GetGaragesRanking(int count)
        {
            throw new NotImplementedException();
        }
    }
}
