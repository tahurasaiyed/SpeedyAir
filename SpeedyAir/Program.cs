using Microsoft.Extensions.DependencyInjection;
using SpeedyAir.Services;
using SpeedyAir.Services.Interfaces;
using SpeedyAir.Utilities;
using SpeedyAir.Utilities.Interfaces;

namespace SpeedyAir
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<IFlightSchedule, FlightSchedule>();
            services.AddTransient<IOrderLoader, OrderLoader>();
            services.AddTransient<IOrderScheduler, OrderScheduler>();

            services.AddTransient<App>();
        }
    }
}