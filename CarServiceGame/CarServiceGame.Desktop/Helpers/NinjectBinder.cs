using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace CarServiceGame.Desktop.Helpers
{
    public class NinjectBinder
    {
        private static bool isLoaded = false;
        private static IKernel kernel;

        public static T Get<T>()
        {
            if (!isLoaded)
            {
                kernel = new StandardKernel(new NinjectSettings()
                {
                    AllowNullInjection = true
                });

                kernel.Load(Assembly.GetExecutingAssembly());
               
                isLoaded = true;
            }

            return kernel.Get<T>();
        }
    }
}
