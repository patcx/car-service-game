using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarServiceGame.WebAPI.Helpers
{
    public class GarageIdBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var garageId = bindingContext.HttpContext.User.Claims.Where(x => x.Type == "GarageId").FirstOrDefault()?.Value;
            bindingContext.Result = ModelBindingResult.Success(Guid.Parse(garageId));
            return Task.CompletedTask;
        }
    }

}
