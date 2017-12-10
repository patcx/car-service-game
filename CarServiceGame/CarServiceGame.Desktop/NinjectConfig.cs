using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Desktop.Abstract;
using CarServiceGame.Desktop.Factories;
using CarServiceGame.Desktop.Services;
using CarServiceGame.Domain.Concrete;
using CarServiceGame.Domain.Contracts;
using Ninject.Modules;

namespace CarServiceGame.Desktop
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IGarageRepository>().To<GarageService>();
            Bind<IWorkerRepository>().To<WorkerService>();
            Bind<IOrderRepository>().To<OrderService>();
            Bind<IHttpClientFactory>().To<HttpClientFactory>().InSingletonScope();
        }
    }
}
