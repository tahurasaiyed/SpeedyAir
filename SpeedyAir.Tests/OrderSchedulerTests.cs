using Moq;
using SpeedyAir.Domain;
using SpeedyAir.Services;
using SpeedyAir.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpeedyAir.Tests
{
    public class OrderSchedulerTests
    {
        private readonly Mock<IFlightSchedule> _mockFlightSchedule;
        private readonly Mock<IOrderLoader> _mockOrderLoader;
        private readonly IOrderScheduler _orderScheduler;

        public OrderSchedulerTests()
        {
            _mockFlightSchedule = new Mock<IFlightSchedule>();
            _mockOrderLoader = new Mock<IOrderLoader>();
            _orderScheduler = new OrderScheduler(_mockFlightSchedule.Object, _mockOrderLoader.Object);
        }

        private List<Flight> GetFlights()
        {
            return new List<Flight>()
            {
                new Flight(1, "YUL", "YYZ", 1, 2),
                new Flight(2, "YUL", "YYC", 1, 2),
                new Flight(3, "YUL", "YYZ", 2, 2)
            };
        }

        private List<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order("order-001", "YYZ"),
                new Order("order-002", "YYZ"),
                new Order("order-003", "YYZ"),
                new Order("order-004", "YYC")
            };
        }

        private List<Order> GetHugeOrders()
        {
            return new()
            {
                new Order("order-001", "YYZ"),
                new Order("order-002", "YYZ"),
                new Order("order-003", "YYZ"),
                new Order("order-004", "YYC"),
                new Order("order-005", "YYC"),
                new Order("order-006", "YYZ"),
                new Order("order-007", "YYZ")
            };
        }

        private void AssertFlightOrder(FlightOrder flightOrder, string expectedOrderName, string expectedFlightNumber,
            string? expectedDeparture, string? expectedArrival, int? expectedDay)
        {
            Assert.Equal(expectedOrderName, flightOrder.OrderName);
            Assert.Equal(expectedFlightNumber, flightOrder.FlightNumber);
            Assert.Equal(expectedDeparture, flightOrder.Departure);
            Assert.Equal(expectedArrival, flightOrder.Arrival);
            Assert.Equal(expectedDay, flightOrder.Day);
        }

        private void MockFlightSchedule()
        {
            _mockFlightSchedule.Setup(x => x.GetScheduledFlights()).Returns(GetFlights());
        }

        [Fact]
        public void ScheduleOrders_WithEnoughFlightCapacity_ShouldScheduleAllOrders()
        {
            MockFlightSchedule();

            _mockOrderLoader.Setup(x => x.LoadOrders()).Returns(GetOrders());

            List<FlightOrder> flightOrders = _orderScheduler.ScheduleOrders();
            Assert.Equal(4, flightOrders.Count);

            AssertFlightOrder(flightOrders[0], "order-001", "1", "YUL", "YYZ", 1);
            AssertFlightOrder(flightOrders[1], "order-002", "1", "YUL", "YYZ", 1);
            AssertFlightOrder(flightOrders[2], "order-003", "3", "YUL", "YYZ", 2);
            AssertFlightOrder(flightOrders[3], "order-004", "2", "YUL", "YYC", 1);
        }

        [Fact]
        public void ScheduleOrders_WhenFlightIsNotAvailable_ShouldNotScheduleThoseOrders()
        {
            MockFlightSchedule();

            List<Order> orders = new()
            {
                new Order("order-001", "YYZ"),
                new Order("order-002", "YYZ"),
                new Order("order-003", "YYE"),
                new Order("order-004", "YYC")
            };
            _mockOrderLoader.Setup(x => x.LoadOrders()).Returns(orders);

            List<FlightOrder> flightOrders = _orderScheduler.ScheduleOrders();
            Assert.Equal(4, flightOrders.Count);

            AssertFlightOrder(flightOrders[0], "order-001", "1", "YUL", "YYZ", 1);
            AssertFlightOrder(flightOrders[1], "order-002", "1", "YUL", "YYZ", 1);
            AssertFlightOrder(flightOrders[2], "order-003", "not scheduled", null, null, null);
            AssertFlightOrder(flightOrders[3], "order-004", "2", "YUL", "YYC", 1);
        }

        [Fact]
        public void ScheduleOrders_WithOrderExceedingFlightCapacity_DoesNotScheduleExceededOrders()
        {
            MockFlightSchedule();

            _mockOrderLoader.Setup(x => x.LoadOrders()).Returns(GetHugeOrders());

            List<FlightOrder> flightOrders = _orderScheduler.ScheduleOrders();
            Assert.Equal(7, flightOrders.Count);

            AssertFlightOrder(flightOrders[0], "order-001", "1", "YUL", "YYZ", 1);
            AssertFlightOrder(flightOrders[1], "order-002", "1", "YUL", "YYZ", 1);
            AssertFlightOrder(flightOrders[2], "order-003", "3", "YUL", "YYZ", 2);
            AssertFlightOrder(flightOrders[3], "order-004", "2", "YUL", "YYC", 1);
            AssertFlightOrder(flightOrders[4], "order-005", "2", "YUL", "YYC", 1);
            AssertFlightOrder(flightOrders[5], "order-006", "3", "YUL", "YYZ", 2);
            AssertFlightOrder(flightOrders[6], "order-007", "not scheduled", null, null, null);
        }

        [Fact]
        public void PrintSchedule_ShouldPrintScheduleOfFlights()
        {
            MockFlightSchedule();

            List<Order> orders = GetHugeOrders();
            _mockOrderLoader.Setup(x => x.LoadOrders()).Returns(orders);

            var expectedOutput =
            $"order: order-001, flightNumber: 1, departure: YUL, arrival: YYZ, day: 1{Environment.NewLine}" +
            $"order: order-002, flightNumber: 1, departure: YUL, arrival: YYZ, day: 1{Environment.NewLine}" +
            $"order: order-003, flightNumber: 3, departure: YUL, arrival: YYZ, day: 2{Environment.NewLine}" +
            $"order: order-004, flightNumber: 2, departure: YUL, arrival: YYC, day: 1{Environment.NewLine}" +
            $"order: order-005, flightNumber: 2, departure: YUL, arrival: YYC, day: 1{Environment.NewLine}" +
            $"order: order-006, flightNumber: 3, departure: YUL, arrival: YYZ, day: 2{Environment.NewLine}" +
            $"order: order-007, flightNumber: not scheduled{Environment.NewLine}";

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            _orderScheduler.PrintSchedule();
            Assert.Equal(expectedOutput, stringWriter.ToString());
        }
    }
}
