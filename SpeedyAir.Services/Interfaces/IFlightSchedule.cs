using SpeedyAir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Services.Interfaces
{
    /// <summary>
    /// Interface for flight schedule.
    /// </summary>
    public interface IFlightSchedule
    {
        /// <summary>
        /// Get scheduled flights.
        /// </summary>
        /// <returns>List of scheduled flights.</returns>
        public List<Flight> GetScheduledFlights();

        /// <summary>
        /// Prints the flight schedule.
        /// </summary>
        public void PrintSchedule();
    }
}
