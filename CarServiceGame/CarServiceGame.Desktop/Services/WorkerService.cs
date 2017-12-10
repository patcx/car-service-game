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
    public class WorkerService : IWorkerRepository
    {
        private IHttpClientFactory httpClientFactory;

        public WorkerService(IHttpClientFactory clientFactory)
        {
            this.httpClientFactory = clientFactory;
        }

        public IEnumerable<Worker> GetUnemployedWorkers(int skip, int take)
        {
            using (var client = httpClientFactory.GetClient())
            {
                var responseTask = client.GetAsync($"{Config.Domain}api/v1/Workers");
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                return JsonConvert.DeserializeObject<IEnumerable<Worker>>(responseString);
            }
        }

        public void FireWorker(Guid garageId, Guid workerId)
        {
            using (var client = httpClientFactory.GetClient())
            {
                HttpContent content = new StringContent($"workerId={workerId}", Encoding.ASCII, "application/x-www-form-urlencoded");
                var responseTask = client.PostAsync($"{Config.Domain}api/v1/Workers/Fire", content);
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    throw new Exception("Worker cannot be fired");
            }
        }

        public void EmployWorker(Guid garageId, Guid workerId)
        {
            using (var client = httpClientFactory.GetClient())
            {
                HttpContent content = new StringContent($"workerId={workerId}", Encoding.ASCII, "application/x-www-form-urlencoded");
                var responseTask = client.PostAsync($"{Config.Domain}api/v1/Workers/Employ", content);
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    throw new Exception("Worker cannot be employed");
            }
        }

        public void UpgradeWorker(Guid garageId, Guid workerId, decimal cost)
        {
            throw new NotImplementedException();
        }
    }
}
