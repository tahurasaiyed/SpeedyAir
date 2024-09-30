namespace SpeedyAir.Domain
{
    /// <summary>
    /// Represents a flight in the SpeedyAir.ly air freight service.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Gets or sets unique indentifer for the flight.
        /// </summary>
        public int Number { get; set; }

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
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the flight.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets of sets total orders loaded in the flight.
        /// </summary>
        public int LoadedOrders { get; set; }

        /// <summary>
        /// Initializes a new instance of the Flight class.
        /// </summary>
        /// <param name="number">Flight number</param>
        /// <param name="departure">Departure city code</param>
        /// <param name="arrival">Arrival city code</param>
        /// <param name="day">Day of flight</param>
        /// <param name="capacity">Capacity of flight</param>
        public Flight(int number, string departure, string arrival, int day, int capacity = 20)
        {
            Number = number;
            Departure = departure;
            Arrival = arrival;
            Day = day;
            Capacity = capacity;
        }
    }
}