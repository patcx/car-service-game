using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceGame.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class WorkerController : Controller
    {
        [HttpGet("")]
        public string Index()
        {
            return "";
        }
    }
}