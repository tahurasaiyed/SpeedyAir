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
    /// Contains all operations associated with scheduling orders.
    /// </summary>
    public class OrderScheduler : IOrderScheduler
    {
        private readonly IFlightSchedule _flightSchedule;
        private readonly IOrderLoader _orderLoader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flightSchedule">The flight schedule service used to get scheduled flights.</param>
        /// <param name="orderLoader">The order loader service used to load orders from a file.</param>
        public OrderScheduler(IFlightSchedule flightSchedule, IOrderLoader orderLoader)
        {

            _flightSchedule = flightSchedule;
            _orderLoader = orderLoader;
        }

        /// <summary>
        /// Schedules orders for flights based on the flight capacity and the destination.
        /// </summary>
        /// <returns>Returns orders and their schedule.</returns>
        public List<FlightOrder> ScheduleOrders()
        {
            List<Flight> flights = _flightSchedule.GetScheduledFlights();
            List<Order> orders = _orderLoader.LoadOrders();
            List<FlightOrder> flightOrders = new();

            var groupedOrders = orders.GroupBy(o => o.Destination).ToDictionary(g => g.Key, g => new Queue<Order>(g));
            AssignOrdersToFlights(flights, flightOrders, groupedOrders);
            HandleUnassignedOrders(flightOrders, groupedOrders);

            return flightOrders.OrderBy(x => x.OrderName).ToList();
        }

        /// <summary>
        /// Handles any orders that could not be assigned to flights.
        /// </summary>
        /// <param name="flightOrders">The list of flight orders that have been scheduled.</param>
        /// <param name="groupedOrders">A dictionary of grouped orders by their destination.</param>
        private void HandleUnassignedOrders(List<FlightOrder> flightOrders, Dictionary<string, Queue<Order>> groupedOrders)
        {
            foreach (var queue in groupedOrders.Values)
            {
                while (queue.Any())
                {
                    var order = queue.Dequeue();
                    flightOrders.Add(new FlightOrder
                    {
                        OrderName = order.Name,
                        FlightNumber = "not scheduled",
                    });
                }
            }
        }

        /// <summary>
        /// Assigns orders to flights based on available capacity and destination.
        /// </summary>
        /// <param name="flights">The list of scheduled flights.</param>
        /// <param name="flightOrders">The list of flight orders that will be populated.</param>
        /// <param name="groupedOrders">A dictionary of grouped orders by their destination.</param>
        private void AssignOrdersToFlights(List<Flight> flights, List<FlightOrder> flightOrders, Dictionary<string, Queue<Order>> groupedOrders)
        {
            foreach (var flight in flights)
            {
                if (!groupedOrders.ContainsKey(flight.Arrival))
                    continue;

                var totalOrders = groupedOrders[flight.Arrival];
                int availableCapacity = flight.Capacity - flight.LoadedOrders;
                int ordersToProcess = Math.Min(availableCapacity, totalOrders.Count);

                for (int i = 0; i < ordersToProcess; i++)
                {
                    Order order = totalOrders.Dequeue();
                    flightOrders.Add(new FlightOrder
                    {
                        OrderName = order.Name,
                        FlightNumber = flight.Number.ToString(),
                        Departure = flight.Departure,
                        Arrival = flight.Arrival,
                        Day = flight.Day
                    });
                    flight.LoadedOrders++;
                }
            }
        }

        /// <summary>
        /// Prints schedule of each order
        /// </summary>
        public void PrintSchedule()
        {
            List<FlightOrder> flightOrders = ScheduleOrders();
            foreach(var flightOrder in flightOrders) 
            {
                if (flightOrder.Day is null)
                    Console.WriteLine($"order: {flightOrder.OrderName}, flightNumber: {flightOrder.FlightNumber}");
                else
                    Console.WriteLine($"order: {flightOrder.OrderName}, flightNumber: {flightOrder.FlightNumber}, departure: {flightOrder.Departure}, arrival: {flightOrder.Arrival}, day: {flightOrder.Day}");
            }
        }
    }
}
