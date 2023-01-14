﻿using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Data {
    internal class TicketGenerator {
        public static List<Ticket> GenerateTicketsForFlights(List<Flight> flights) {
            List<Ticket> tickets = new List<Ticket>();
            foreach (Flight flight in flights) {
                tickets.AddRange(GenerateTicketsForFlight(flight));
            }
            return tickets;
        }

        private static List<Ticket> GenerateTicketsForFlight(Flight flight) {
            List<Ticket> tickets = new List<Ticket>();
            foreach (Seat seat in flight.Airplane.Seats) {
                tickets.Add(new() { Flight = flight, Seat = seat, Cost = flight.BaseCost * (1.0f - (0.2f * (float)seat.Class)) });
            }
            return tickets;
        }
    }
}