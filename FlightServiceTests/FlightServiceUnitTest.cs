using System;
using FlightServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelRepublic.FlightCodingTest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
 

namespace FlightServiceTests
{
    [TestClass]
    public class FlightServiceUnitTest
    {
        [TestMethod]
        public void FlightsDepartBeforeDateTime()
        {
            var flightBuilder = new Mock<FlightRepository>();
            var flightService = new FlightService (flightBuilder.Object);

            var flights = new List<Flight>()
                            {
                                new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now,DepartureDate = DateTime.Now.AddDays(-1)
                                    }
                                         }
                                },
                                new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now,DepartureDate = DateTime.Now.AddDays(1)
                                    }
                                         }
                                },
                                 new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now,DepartureDate = DateTime.Now.AddDays(3)
                                    }
                                         }
                                }
                            };
            flightBuilder.Setup(r => r.GetFlights()).Returns(flights);
            var flightsDepartBeforeToday = flightService.FlightsDepartBeforeDateTime(DateTime.Now);
            Assert.IsTrue(flightsDepartBeforeToday.Count() == 1);

        }
        [TestMethod]
        public void SegmentsWithArrivalEarlierThanDepartureDate()
        {
            var flightBuilder = new Mock<FlightRepository>();
            var flightService = new FlightService(flightBuilder.Object);

            var flights = new List<Flight>()
                            {
                                new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now.AddDays(-1),DepartureDate = DateTime.Now
                                    }
                                         }
                                },
                                new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now.AddDays(-2),DepartureDate = DateTime.Now
                                    }
                                         }
                                },
                                 new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now.AddDays(1),DepartureDate = DateTime.Now
                                    }
                                         }
                                }
                            };
            flightBuilder.Setup(r => r.GetFlights()).Returns(flights);
            var flightsDepartBeforeToday = flightService.SegmentsWithArrivalEarlierThanDepartureDate(DateTime.Now);
            Assert.IsTrue(flightsDepartBeforeToday.Count() == 2);

        }

        [TestMethod]
        public void DepartureOver2HoursAfterArrival()
        {
            var flightBuilder = new Mock<FlightRepository>();
            var flightService = new FlightService(flightBuilder.Object);

            var flights = new List<Flight>()
                            {
                                new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now,DepartureDate = DateTime.Now.AddHours(1)
                                    }
                                         }
                                },
                                new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now,DepartureDate = DateTime.Now.AddHours(3)
                                    }
                                         }
                                },
                                 new Flight()
                                {  Segments = new List<Segment>()
                                    {
                                        new Segment()
                                        {
                                            ArrivalDate = DateTime.Now,DepartureDate = DateTime.Now.AddHours(4)
                                    }
                                         }
                                }
                            };
            flightBuilder.Setup(r => r.GetFlights()).Returns(flights);
            var flightsDepartBeforeToday = flightService.DepartureOver2HoursAfterArrival();
            Assert.IsTrue(flightsDepartBeforeToday.Count() == 2);

        }
    }
}
