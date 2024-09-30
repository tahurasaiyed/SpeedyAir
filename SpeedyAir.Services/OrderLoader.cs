using SpeedyAir.Domain;
using SpeedyAir.Services.Interfaces;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using SpeedyAir.Utilities.Interfaces;

namespace SpeedyAir.Services
{
    /// <summary>
    /// Contains all operations associated with loading orders.
    /// </summary>
    public class OrderLoader : IOrderLoader
    {
        private readonly string _filePath;
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileReader">The file reader service used to read files.</param>
        public OrderLoader(IFileReader fileReader)
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Orders.json");
            _fileReader = fileReader;
        }

        /// <summary>
        /// Fetches orders.
        /// </summary>
        /// <returns>List of orders.</returns>  
        public List<Order> LoadOrders()
        {
            string json = _fileReader.ReadAllText(_filePath);
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);

            if (orderDictionary == null)
            {
                throw new InvalidOperationException("Failed to deserialize orders from JSON.");
            }

            var orders = new List<Order>();
            foreach (var kvp in orderDictionary)
            {
                var order = kvp.Value;
                order.Name = kvp.Key;
                orders.Add(order);
            }

            return orders;
        }
    }
}