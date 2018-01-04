using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Database;
using CarServiceGame.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CarServiceGame.WebAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class GarageController : Controller
    {
        private IGarageRepository garageRepository;

        public GarageController(IGarageRepository garageRepository)
        {
            this.garageRepository = garageRepository;
        }

        [AllowAnonymous]
        [HttpPost("api/v{version:apiVersion}/Garage")]
        public IActionResult Index(string name, string password)
        {
            dynamic result = new ExpandoObject();

            try
            {
                var garage = garageRepository.GetGarage(name, password);
                if (garage != null)
                {
                    string token = GetToken(garage);
                    result.status = "ok";
                    result.token = token;
                    result.garage = garage;

                    return Ok(JsonConvert.SerializeObject(result));
                }
                else
                {
                    result.status = "error";
                    return Ok(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return Ok(JsonConvert.SerializeObject(result));
            }

        }


        [AllowAnonymous]
        [HttpPost("api/v{version:apiVersion}/Garage/Register")]
        public IActionResult Register(string name, string password)
        {
            dynamic result = new ExpandoObject();

            try
            {
                var garage = garageRepository.CreateGarage(name, password);
                if (garage != null)
                {
                    string token = GetToken(garage);

                    result.status = "ok";
                    result.token = token;
                    result.garage = garage;
                    return Ok(JsonConvert.SerializeObject(result));
                }
                else
                {
                    result.status = "error";
                    return BadRequest(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(JsonConvert.SerializeObject(result));
            }

        }

        private string GetToken(Domain.Entities.Garage garage)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret_key_car_service_game@!"));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddDays(1),
               claims: new Claim[]
               {
                   new Claim("GarageId", garage.GarageId.ToString()),
               },
               signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var token = tokenHandler.WriteToken(jwt);
            return token;
        }

        [HttpGet("api/v{version:apiVersion}/Garage/Balance")]
        public IActionResult GarageBalance([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                var balance = garageRepository.GetGarageBalance(garageId);
                result.status = "ok";
                result.balance = balance;
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(JsonConvert.SerializeObject(result));
            }

        }

        [HttpGet("api/v{version:apiVersion}/Garage/Ranking")]
        public IActionResult GetGarageSRanking(int count = 20)
        {
            dynamic result = new ExpandoObject();
            try
            {
                var Ranking = garageRepository.GetGaragesRanking(count);
                return Ok(JsonConvert.SerializeObject(Ranking));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(JsonConvert.SerializeObject(result));
            }
        }

        [HttpPost("api/v{version:apiVersion}/Garage/Upgrade")]
        public IActionResult UpgradeGarage([ModelBinder(BinderType = typeof(GarageIdBinder))]Guid garageId, decimal cost)
        {
            dynamic result = new ExpandoObject();
            try
            {
                garageRepository.UpgradeGarage(garageId, cost);
                result.status = "ok";
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.status = "error";
                return BadRequest(JsonConvert.SerializeObject(result));
            }
        }
    }
}