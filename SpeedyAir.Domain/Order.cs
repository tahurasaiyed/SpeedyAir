using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAir.Domain
{
    /// <summary>
    /// Represents an order in the SpeedyAir.ly air freight service.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the destination of the order.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Initializes a new instance of the Order class.
        /// </summary>
        /// <param name="name">Order name</param>
        /// <param name="destination">Destination city of the order</param>
        public Order(string name, string destination)
        {
            Name = name;
            Destination = destination;
        }
    }
}
