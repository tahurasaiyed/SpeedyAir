using Moq;
using Newtonsoft.Json;
using SpeedyAir.Domain;
using SpeedyAir.Services;
using SpeedyAir.Services.Interfaces;
using SpeedyAir.Utilities;
using SpeedyAir.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SpeedyAir.Tests
{
    public class OrderLoaderTests
    {
        private readonly Mock<IFileReader> _mockFileReader;
        private readonly IOrderLoader _orderLoader;

        public OrderLoaderTests()
        {
            _mockFileReader = new Mock<IFileReader>();
            _orderLoader = new OrderLoader(_mockFileReader.Object);
        }

        [Fact]
        public void LoadOrders_ValidFilePath_ReturnsListOfOrders()
        {
            string orderJson = @"
            {
                'order-001': {
                    'destination': 'YYZ'
                },
                'order-002': {
                    'destination': 'YYC'
                }
            }";

            _mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(orderJson);

            List<Order>? orders = _orderLoader.LoadOrders();

            Assert.NotNull(orders);
            Assert.Equal(2, orders.Count);

            Order firstOrder = orders.First();
            Assert.Equal("order-001", firstOrder.Name);
            Assert.Equal("YYZ", firstOrder.Destination);

            Order secondOrder = orders.Last();
            Assert.Equal("order-002", secondOrder.Name);
            Assert.Equal("YYC", secondOrder.Destination);
        }

        [Fact]
        public void LoadOrders_MalformedJson_ThrowsJsonException()
        {
            string orderJson = "{abc";

            _mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(orderJson);
            Assert.Throws<JsonReaderException>(() => _orderLoader.LoadOrders());
        }

        [Fact]
        public void LoadOrders_WhenOrderDictionaryIsNull_ThrowsInvalidOperationException()
        {
            string jsonWithNull = "null";
            _mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>())).Returns(jsonWithNull);

            var exception = Assert.Throws<InvalidOperationException>(() => _orderLoader.LoadOrders());
            Assert.Equal("Failed to deserialize orders from JSON.", exception.Message);
        }
    }
}
