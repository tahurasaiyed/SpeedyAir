using Moq;
using SpeedyAir.Domain;
using SpeedyAir.Services;
using SpeedyAir.Services.Interfaces;
using System.IO;
using Xunit;

namespace SpeedyAir.Tests
{
    public class FlightScheduleTests
    {
        private readonly List<Flight> _flights;
        private readonly IFlightSchedule _flightSchedule;

        public FlightScheduleTests()
        {
            _flightSchedule = new FlightSchedule();
            _flights = new List<Flight>
            {
                new Flight(1, "YUL", "YYZ", 1, 20),
                new Flight(2, "YUL", "YYC", 1, 20),
                new Flight(3, "YUL", "YVR", 1, 20),
                new Flight(4, "YUL", "YYZ", 2, 20),
                new Flight(5, "YUL", "YYC", 2, 20),
                new Flight(6, "YUL", "YVR", 2, 20)
            };
        }

        [Fact]
        public void GetScheduledFlights_ShouldReturnScheduledFlights()
        {
            List<Flight> flights = _flightSchedule.GetScheduledFlights();

            Assert.Equal(6, flights.Count);

            for (int i = 0; i < flights.Count; i++)
            {
                Assert.Equal(_flights[i].Number, flights[i].Number);
                Assert.Equal(_flights[i].Departure, flights[i].Departure);
                Assert.Equal(_flights[i].Arrival, flights[i].Arrival);
                Assert.Equal(_flights[i].Day, flights[i].Day);
                Assert.Equal(_flights[i].Capacity, flights[i].Capacity);
            }
        }

        [Fact]
        public void PrintSchedule_ShouldPrintScheduleOfFlights()
        {
            var expectedOutput =
            $"Flight: {_flights[0].Number}, departure: {_flights[0].Departure}, arrival: {_flights[0].Arrival}, day: {_flights[0].Day}{Environment.NewLine}" +
            $"Flight: {_flights[1].Number}, departure: {_flights[1].Departure}, arrival: {_flights[1].Arrival}, day: {_flights[1].Day}{Environment.NewLine}" +
            $"Flight: {_flights[2].Number}, departure: {_flights[2].Departure}, arrival: {_flights[2].Arrival}, day: {_flights[2].Day}{Environment.NewLine}" +
            $"Flight: {_flights[3].Number}, departure: {_flights[3].Departure}, arrival: {_flights[3].Arrival}, day: {_flights[3].Day}{Environment.NewLine}" +
            $"Flight: {_flights[4].Number}, departure: {_flights[4].Departure}, arrival: {_flights[4].Arrival}, day: {_flights[4].Day}{Environment.NewLine}" +
            $"Flight: {_flights[5].Number}, departure: {_flights[5].Departure}, arrival: {_flights[5].Arrival}, day: {_flights[5].Day}{Environment.NewLine}";

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            _flightSchedule.PrintSchedule();
            Assert.Equal(expectedOutput, stringWriter.ToString());
        }
    }
}