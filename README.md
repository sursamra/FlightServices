# FlightServices
# FlightServices
Invoke the FlightBuilder.GetFlights method to get some test flights and filter out those that:
 
1. Depart before the current date/time
2. Have any segment with an arrival date before the departure date
3. Spend more than 2 hours on the ground – i.e. those with a total combined gap of over two hours between the arrival date of one segment and the departure date of the next
