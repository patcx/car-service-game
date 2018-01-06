using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarServiceGame.WebAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class PaymentController : Controller
    {
        protected readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("api/v{version:apiVersion}/Payment/Send")]
        public IActionResult SendCode([ModelBinder(typeof(GarageIdBinder))]Guid garageId, string email)
        {
            dynamic result = new ExpandoObject();
            try
            {
                _paymentRepository.SendMail(garageId, email);
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

        [HttpPost("api/v{version:apiVersion}/Payment/Process")]
        public IActionResult ProcessCode([ModelBinder(typeof(GarageIdBinder))]Guid garageId, string code)
        {
            dynamic result = new ExpandoObject();
            try
            {
                _paymentRepository.ProcessCode(garageId, code);
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