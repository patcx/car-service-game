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
    public class WorkerController : Controller
    {
        private IWorkerRepository workerRepository;

        public WorkerController(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        [HttpGet("api/v{version:apiVersion}/Workers")]
        public IActionResult Index(int skip = 0, int take = 20)
        {
            dynamic result = new ExpandoObject();
            try
            {
                string orders = JsonConvert.SerializeObject(workerRepository.GetUnemployedWorkers(skip, take));
                return Ok(orders);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(result);
            }
        }

        [HttpGet("api/v{version:apiVersion}/Workers/Fire")]
        public IActionResult FireWorker(Guid workerId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                workerRepository.FireWorker(workerId);
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

        [HttpGet("api/v{version:apiVersion}/Workers/Employ")]
        public IActionResult EmployWorker([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId, Guid workerId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                workerRepository.EmployWorker(workerId, workerId);
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