using OmniDibs.Logic;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Data {
    internal class FlightManager {
        private static readonly Random random = new();
        internal static List<Flight> GetFlights() {
            List<Country> countries = DatabaseInterface.GetListOf<Country>();
            List<Airplane> airplanes = DatabaseInterface.GetListOf<Airplane>();
            var ret = new List<Flight>() {
                new Flight() { Name = "CAF1337", BaseCost = 56000, Departure = PersonGenerator.GetRandomDateInRange(new (2023,04,30),new (2023,02,01)), Destination = countries.Where(x => x.CountryName.Equals("Antarctica")).Single(), Origin = countries.Where(x => x.CountryName.Equals("Sweden")).Single()},
                new Flight() { Name = "RAF007", BaseCost = 1500, Departure = PersonGenerator.GetRandomDateInRange(new (2023,04,30),new (2023,02,01)), Destination = countries.Where(x => x.CountryName.Equals("England")).Single(), Origin = countries.Where(x => x.CountryName.Equals("Sweden")).Single()},
                new Flight() { Name = "RF212", BaseCost = 16500, Departure = PersonGenerator.GetRandomDateInRange(new (2023,04,30),new (2023,02,01)), Destination = countries.Where(x => x.CountryName.Equals("Canada")).Single(), Origin = countries.Where(x => x.CountryName.Equals("Sweden")).Single()},
                new Flight() { Name = "DUC5000", BaseCost = 2000, Departure = PersonGenerator.GetRandomDateInRange(new (2023,04,30),new (2023,02,01)), Destination = countries.Where(x => x.CountryName.Equals("Turkey")).Single(), Origin = countries.Where(x => x.CountryName.Equals("Sweden")).Single()},
                new Flight() { Name = "M1LF23", BaseCost = 1000, Departure = PersonGenerator.GetRandomDateInRange(new (2023,04,30),new (2023,02,01)), Destination = countries.Where(x => x.CountryName.Equals("France")).Single(), Origin = countries.Where(x => x.CountryName.Equals("Sweden")).Single()},
            };
            ret[0].Airplane = airplanes[0];
            foreach(var flight in ret) {
                if (flight.Airplane == null) {
                    flight.Airplane = airplanes.Last();
                }
            }
            foreach (var flight in ret) {
                int h = random.Next(1, 10);
                flight.Arrival = flight.Departure.AddHours(h);
                /*
                foreach (Seat seat in flight.Airplane.Seats) {
                    flight.Tickets.Add(new() { Flight = flight, Seat = seat, Cost = flight.BaseCost * (1.0f - (0.2f * (float)seat.Class)) });
                }*/
            }
            return ret;
        }
    }
}
