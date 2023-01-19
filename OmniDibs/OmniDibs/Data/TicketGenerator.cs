using OmniDibs.Models;

namespace OmniDibs.Data {
    internal class TicketGenerator {
        internal static List<Ticket> GenerateTicketsForFlights(List<Flight> flights) {
            List<Ticket> tickets = new ();
            foreach (Flight flight in flights) {
                tickets.AddRange(GenerateTicketsForFlight(flight));
            }
            return tickets;
        }

        internal static List<Ticket> GenerateTicketsForFlight(Flight flight) {
            List<Ticket> tickets = new ();
            foreach (Seat seat in flight.Airplane.Seats) {
                tickets.Add(new() { Flight = flight, 
                                    Seat = seat, 
                                    Cost = flight.BaseCost * (1.0f - (0.2f * (int)seat.Class)), 
                                    StartDate = flight.Departure, 
                                    EndDate = flight.Arrival });
            }
            return tickets;
        }
    }
}
