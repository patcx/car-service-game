using System;
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
    public class OrderService : IOrderRepository
    {
        private IHttpClientFactory httpClientFactory;

        public OrderService(IHttpClientFactory clientFactory)
        {
            this.httpClientFactory = clientFactory;
        }

        public IEnumerable<RepairOrder> GetAvailableOrders(int skip, int take)
        {
            using (var client = httpClientFactory.GetClient())
            {
                var responseTask = client.GetAsync($"{Config.Domain}api/v1/Orders");
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                return JsonConvert.DeserializeObject<IEnumerable<RepairOrder>>(responseString);
            }
        }

        public IEnumerable<RepairProcess> GetHistoryOrders(Guid garageId, int skip, int take)
        {
            using (var client = httpClientFactory.GetClient())
            {
                var responseTask = client.GetAsync($"{Config.Domain}api/v1/Orders/History");
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                return JsonConvert.DeserializeObject<IEnumerable<RepairProcess>>(responseString);
            }
        }

        public void AssignOrder(Guid garageId, Guid orderId, Guid workerId, int stallNumber)
        {
            using (var client = httpClientFactory.GetClient())
            {
                HttpContent content = new StringContent($"workerId={workerId}&orderId={orderId}&stallNumber={stallNumber}", Encoding.ASCII, "application/x-www-form-urlencoded");
                var responseTask = client.PostAsync($"{Config.Domain}api/v1/Orders/Assign", content);
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    throw new Exception("Worker cannot be employed");
            }
        }

        public void FinishOrder(Guid garageId, Guid orderId)
        {
            using (var client = httpClientFactory.GetClient())
            {
                HttpContent content = new StringContent($"orderId={orderId}", Encoding.ASCII, "application/x-www-form-urlencoded");
                var responseTask = client.PostAsync($"{Config.Domain}api/v1/Orders/Finish", content);
                responseTask.Wait();
                var response = responseTask.Result;
                var responseString = response.Content.ReadAsString();
                dynamic obj = JsonConvert.DeserializeObject(responseString);
                if (obj.status == "error")
                    throw new Exception("Order cannot be finished");
            }
        }

        public void CancelOrder(Guid garageId, Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
