using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.FlightCodingTest;

namespace FlightServices
{
    /// <summary>
    /// Interface to abstract out dependencies on external service
    /// </summary>
    public interface IFlightBuilder
    {
        IList<Flight> GetFlights();
    }
    /// <summary>
    /// factory class responsible to create external dependency
    /// </summary>
    public static class FlightBuilderFactory
    {
        public static FlightBuilder GetFlightBuilder()
        {
            return new FlightBuilder();
        }
    }

    /// <summary>
    /// Local repository for flights whcih be injected with mocks to make testing simple
    /// </summary>
    public class FlightRepository:IFlightBuilder
        {
        public virtual IList<Flight> GetFlights()
        {
            return FlightBuilderFactory.GetFlightBuilder().GetFlights();
        }

    }

    /// <summary>
    /// Actual Business Application layer
    /// </summary>
    public interface IFlightService
    {
        IList<Flight> GetFlights();
        IList<Flight> FlightsDepartBeforeDateTime(DateTime dateTime);
        IList<Flight> SegmentsWithArrivalEarlierThanDepartureDate(DateTime dateTime);
        IList<Flight> DepartureOver2HoursAfterArrival();
    }
    
    /// <summary>
    /// Implementation of the application business layer
    /// Further this code can be extended by creating domain objects to make business service indendent of FlightBuilder
    /// </summary>
    public class FlightService
    {
        IFlightBuilder service;
        public FlightService(IFlightBuilder pservice)
        {
            service = pservice;
        }
        public IList<Flight> GetFlights()
        {
            return service.GetFlights();
        }

        public IList<Flight> FlightsDepartBeforeDateTime(DateTime dateTime)
        {
             List<Flight> flights = new List<Flight>();
          
            foreach (Flight flight in service.GetFlights())
            {              
                foreach (Segment s in flight.Segments)
                {
                    if (s.DepartureDate.CompareTo(dateTime) < 0)
                        flights.Add(flight);
                }
            }
            return flights;
        }

        public IList<Flight> SegmentsWithArrivalEarlierThanDepartureDate(DateTime dateTime)
        {
            List<Flight> flights = new List<Flight>();
            foreach (Flight flight in service.GetFlights())
            {              
                foreach (Segment s in flight.Segments)
                {
                    if (s.ArrivalDate.CompareTo(s.DepartureDate) < 0)
                        flights.Add(flight);
                }
            }
            return flights;
        }

        public  IList<Flight> DepartureOver2HoursAfterArrival()
        {
            List<Flight> flights = new List<Flight>();

            foreach (Flight flight in service.GetFlights())
            {               
                foreach (Segment s in flight.Segments)
                {
                    TimeSpan ts = s.DepartureDate.Subtract(s.ArrivalDate);
                    if (ts.TotalDays > 1 || ts.TotalHours > 2)
                    {
                        flights.Add(flight);
                    }                        
                }
            }
            return flights;

        }

    }

}
