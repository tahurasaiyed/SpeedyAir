using SpeedyAir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Services.Interfaces
{
    /// <summary>
    /// Interface for order scheduler,
    /// </summary>
    public interface IOrderScheduler
    {
        /// <summary>
        /// Prints order schedule.
        /// </summary>
        void PrintSchedule();

        /// <summary>
        /// Schedules orders for flights based on the flight's capacity and the destination.
        /// </summary>
        /// <returns>Returns orders and their schedule.</returns>
        List<FlightOrder> ScheduleOrders();
    }
}
