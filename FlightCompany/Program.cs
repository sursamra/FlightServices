using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TravelRepublic.FlightCodingTest
{
    /*
     *  1.Depart before the current date/ time
2.Have any segment with an arrival date before the departure date
3.Spend more than 2 hours on the ground – i.e.those with a total combined gap of over two hours between the arrival date of one segment and the departure date of the next

     * */
    class Program
    {
        const string file = @"C:\Samra\AON\Projects\Next X\Dnata Test\C# Test\Dnata\Dnata\Flights.txt";
        const string DepartBeforeCurrentDateTimefile = @"C:\Samra\AON\Projects\Next X\Dnata Test\C# Test\Dnata\Dnata\DepartBeforeCurrentDateTime.txt";
        const string SegmentsWithArrivalDepartureDatefile = @"C:\Samra\AON\Projects\Next X\Dnata Test\C# Test\Dnata\Dnata\SegmentsWithArrivalDepartureDate.txt";
        const string DepartureOver2HoursAfterArrivalfile = @"C:\Samra\AON\Projects\Next X\Dnata Test\C# Test\Dnata\Dnata\DepartureOver2HoursAfterArrivalfile.txt";
        static void Main(string[] args)
        {

            FlightBuilder fb = new FlightBuilder();
            IList<Flight> flights = fb.GetFlights();
            PrintFlights(flights);
            DepartBeforeCurrentDateTime(DateTime.Now, flights);
            SegmentsWithArrivalEarlierThanDepartureDate(flights);
            DepartureOver2HoursAfterArrival(flights);


        }

        public interface IFlightService
        {
            IList<Flight> GetFlights();
        }
        public class FlightService : IFlightService
        {
            public IList<Flight> GetFlights()
            {
                FlightBuilder fb = new FlightBuilder();
                return fb.GetFlights();
            }

        }
        public interface IFlightService2
        {
            IList<Flight> FlightsDepartBeforeCurrentDateTime(DateTime dateTime);
            IList<Segment> SegmentsWithArrivalEarlierThanDepartureDate(DateTime dateTime);
        }
        public static void PrintFlights(IList<Flight> flights)
        {

            List<string> message = new List<string>();
            int counter = 1;

            foreach (Flight flight in flights)
            {
                message.Add(string.Format("Flight {0}", counter++));
                message.Add(" Arrival Date\tDeparture Date");
                foreach (Segment s in flight.Segments)
                {
                    message.Add(string.Format("{0}\t{1}", s.ArrivalDate, s.DepartureDate));
                }
            }

            File.Delete(file);
            File.WriteAllLines(file, message);
        }

        //1.Depart before the current date/ time
        public static void DepartBeforeCurrentDateTime(DateTime current, IList<Flight> flights)
        {
            FlightBuilder fb = new FlightBuilder();
            List<string> message = new List<string>();
            int counter = 1;

            foreach (Flight flight in flights)
            {
                message.Add(string.Format("Flight {0}", counter++));
                message.Add(" Arrival Date\tDeparture Date");
                foreach (Segment s in flight.Segments)
                {
                    if (s.DepartureDate.CompareTo(current) < 0)
                        message.Add(string.Format("{0}\t{1}", s.ArrivalDate, s.DepartureDate));
                }
            }
            File.Delete(DepartBeforeCurrentDateTimefile);
            File.WriteAllLines(DepartBeforeCurrentDateTimefile, message);
        }
        //segment with an arrival date before the departure date
        public static void SegmentsWithArrivalEarlierThanDepartureDate(IList<Flight> flights)
        {
            FlightBuilder fb = new FlightBuilder();
            List<string> message = new List<string>();
            int counter = 1;

            foreach (Flight flight in flights)
            {
                message.Add(string.Format("Flight {0}", counter++));
                message.Add(" Arrival Date\tDeparture Date");
                foreach (Segment s in flight.Segments)
                {
                    if (s.ArrivalDate.CompareTo(s.DepartureDate) < 0)
                        message.Add(string.Format("{0}\t{1}", s.ArrivalDate, s.DepartureDate));
                }
            }
            File.Delete(SegmentsWithArrivalDepartureDatefile);
            File.WriteAllLines(SegmentsWithArrivalDepartureDatefile, message);
        }

        public static void DepartureOver2HoursAfterArrival(IList<Flight> flights)
        {
            FlightBuilder fb = new FlightBuilder();
            List<string> message = new List<string>();
            int counter = 1;

            foreach (Flight flight in flights)
            {
                message.Add(string.Format("Flight {0}", counter++));
                message.Add(" Arrival Date\tDeparture Date");
                foreach (Segment s in flight.Segments)
                {
                    TimeSpan ts = s.DepartureDate.Subtract(s.ArrivalDate);
                    if (ts.TotalDays > 1 || ts.TotalHours > 2)
                        message.Add(string.Format("{0}\t{1}", s.ArrivalDate, s.DepartureDate));
                }
            }
            File.Delete(DepartureOver2HoursAfterArrivalfile);
            File.WriteAllLines(DepartureOver2HoursAfterArrivalfile, message);
        }

    }
}
