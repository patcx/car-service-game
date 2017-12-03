using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace CarServiceGame.WebAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet("api/v{version:apiVersion}/Orders")]
        public IActionResult Index([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId, int skip = 0, int take = 20)
        {
            dynamic result = new ExpandoObject();

            try
            {
                string orders = JsonConvert.SerializeObject(orderRepository.GetAvailableOrders(skip, take));
                return Ok(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(result);
            }
        }

        [HttpGet("api/v{version:apiVersion}/Orders/History")]
        public IActionResult HistoryOrders([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId, int skip = 0, int take = 20)
        {
            dynamic result = new ExpandoObject();

            try
            {
                string historyOrders = JsonConvert.SerializeObject(orderRepository.GetHistoryOrders(garageId, skip, take));
                return Ok(historyOrders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(result);
            }
        }

        [HttpPost("api/v{version:apiVersion}/Orders/Assign")]
        public IActionResult AssignOrder([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId, Guid orderId, Guid workerId, int stallNumber)
        {
            dynamic result = new ExpandoObject();

            try
            {
                orderRepository.AssignOrder(garageId, orderId, workerId, stallNumber);
                result.status = "ok";
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(result);
            }
        }


        [HttpPost("api/v{version:apiVersion}/Orders/Finish")]
        public IActionResult FinishOrder([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId, Guid orderId)
        {
            dynamic result = new ExpandoObject();

            try
            {
                orderRepository.FinishOrder(orderId);
                result.status = "ok";
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(result);
            }
        }

    }
}