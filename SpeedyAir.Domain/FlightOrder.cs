using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Domain
{
    /// <summary>
    /// Represents a flight order and its associated flight information in the SpeedyAir.ly air freight service.
    /// </summary>
    public class FlightOrder
    {
        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// Gets or sets number for the flight. 
        /// If flight is not scheduled, it returns the text "not scheduled"
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the departure city code for the flight.
        /// </summary>
        public string Departure { get; set; }

        /// <summary>
        /// Gets or sets the arrival city code for the flight.
        /// </summary>
        public string Arrival { get; set; }

        /// <summary>
        /// Gets or sets the day of the flight's schedule.
        /// </summary>
        public int? Day { get; set; }
    }
}
