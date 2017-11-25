using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceGame.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace CarServiceGame.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet("api/v{version:apiVersion}/Order")]
        public string Index(int skip = 0, int take = 20)
        { 
            return JsonConvert.SerializeObject(orderRepository.GetAvailableOrders(skip, take));
        }
    }
}