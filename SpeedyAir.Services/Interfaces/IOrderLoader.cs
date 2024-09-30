using SpeedyAir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Services.Interfaces
{
    /// <summary>
    /// Interface for order loader.
    /// </summary>
    public interface IOrderLoader
    {
        /// <summary>
        /// Fetches orders.
        /// </summary>
        /// <returns>List of orders.</returns>
        List<Order> LoadOrders();
    }
}
