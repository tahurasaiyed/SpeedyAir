using SpeedyAir.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir
{
    public class App
    {
        private readonly IFlightSchedule _flightSchedule;
        private readonly IOrderScheduler _orderScheduler;

        public App(IFlightSchedule flightSchedule, IOrderScheduler orderScheduler)
        {
            _flightSchedule = flightSchedule;
            _orderScheduler = orderScheduler;
        }

        public void Run()
        {
            Console.WriteLine("User story 1 -  Flight schedule:");
            _flightSchedule.PrintSchedule();

            Console.WriteLine("\nUser story 2 -  Orders associated with flights:");
            _orderScheduler.PrintSchedule();
        }
    }
}
