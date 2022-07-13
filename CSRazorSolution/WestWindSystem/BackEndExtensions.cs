
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WestWindSystem.DAL;
using WestWindSystem.BLL;
#endregion

namespace WestWindSystem
{
    public static class BackEndExtensions
    {
        public static void WWBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //we will register all the services that will be used by the system
            //and will be provided by the system (context setup)
            //and will be provided by the system(BLL services)

            //register the context service
            //the options contains the connection string information
            services.AddDbContext<WestWindContext>(options);

            //register each service class (BLL)
            //each service class will have an AddTransient<T>() method


            //use the AddTransient<T> method where T is  your BLL class name
            //AddTransient is called a factory
            //I read my lamda symbol => as "do the following..."
            services.AddTransient<BuildVersionServices>((serviceProvider) =>
            {   //get the connection class that was registered above in AddDbContext 
                var context = serviceProvider.GetService<WestWindContext>();

                //create an instance of the service class (BuildVersionServices)
                //supplying the context reference to the service class
                return new BuildVersionServices(context);
            });
        }
    }
}
