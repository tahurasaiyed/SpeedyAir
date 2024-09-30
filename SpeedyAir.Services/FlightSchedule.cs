using SpeedyAir.Domain;
using SpeedyAir.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Services
{
    /// <summary>
    /// Contains all operations associated with scheduling a flight.
    /// </summary>
    public class FlightSchedule : IFlightSchedule
    {
        /// <summary>
        /// Private variable that stores the list of flights.
        /// </summary>
        private List<Flight> _flights;

        /// <summary>
        /// Constructor
        /// </summary>
        public FlightSchedule()
        {
            _flights = new List<Flight>
            {
                new Flight(1, "YUL", "YYZ", 1),
                new Flight(2, "YUL", "YYC", 1),
                new Flight(3, "YUL", "YVR", 1),
                new Flight(4, "YUL", "YYZ", 2),
                new Flight(5, "YUL", "YYC", 2),
                new Flight(6, "YUL", "YVR", 2)
            };
        }

        /// <summary>
        /// Get scheduled flights.
        /// </summary>
        /// <returns></returns>
        public List<Flight> GetScheduledFlights()
        {
            return _flights;
        }

        /// <summary>
        /// Prints the flight schedule.
        /// </summary>
        public void PrintSchedule()
        {
            foreach (var flight in _flights)
            {
                Console.WriteLine($"Flight: {flight.Number}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {flight.Day}");
            }
        }
    }
}
