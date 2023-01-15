using Microsoft.EntityFrameworkCore;
using OmniDibs.Interfaces;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace OmniDibs.Pages {
    internal class BookingPage : DefaultMenu<BookingAlternatives>, IRunnable {
        private readonly Account _account;
        internal BookingPage(Account account) : base("Booking Alternatives") { _account = account; }

        public ReturnType Run() {

            while (true) {
                GUI.ClearWindow();
                GUI.printWindow($"| {_title} |", 0, 0, 100, 20);
                return this.RunMenu();
            }
        }

        protected override ReturnType ExecuteMappedAction(BookingAlternatives e) => e switch {
            BookingAlternatives.Account_Details => DisplayAccountDetails(),
            BookingAlternatives.Display_Bookings => DisplayBookings(),
            BookingAlternatives.Make_a_Booking => MakeBooking(),
            BookingAlternatives.Remove_a_Booking => RemoveBooking(),
            BookingAlternatives.Logout => ReturnType.HARDRETURN,
            _ => ReturnType.CONTINUE
        };


        protected override BookingAlternatives GetE(int i) => i switch {
            _ => (BookingAlternatives)i
        };

        private ReturnType DisplayAccountDetails() {
            IEnumerable<string> accountFields = new List<string> { "UserName", "Privileges" };
            IEnumerable<string> personFields = new List<string> { "FirstName", "LastName", "Country", "MailAdress" };
            GUI.ClearWindow();
            GUI.printWindow($"| Account Details |", 0, 0, 100, 25);
            var accountDictionary = GetPropertyPairs<Account>(_account);
            var personDictionary = GetPropertyPairs<Person>(_account.Person);
            var accountResult = GetMatches<string>(accountFields, accountDictionary);
            var personResult = GetMatches<string>(personFields, personDictionary);
            Console.SetCursorPosition(3, 3);
            GUI.PrintList(Compress(accountResult), "Account Information");
            Console.SetCursorPosition(3, 9);
            GUI.PrintList(Compress(personResult), "Personal Information");
            GUI.PromtUserInput();
            return ReturnType.CONTINUE;
        }

        private static Dictionary<string, string> GetPropertyPairs<T>(T instance) {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                keyValuePairs.Add(info.Name, info.GetValue(instance, null)?.ToString()!);
            }
            return keyValuePairs;
        }
        private static List<(T, T)> GetMatches<T>(IEnumerable<T> keys, Dictionary<T, T> dictionary) where T : class {
            return keys.Where(dictionary.ContainsKey).Select(x => (x, dictionary[x])).ToList();
        }
        private static List<string> Compress(List<(string, string)> list, string separator = " : ") {
            List<string> result = new List<string>();
            foreach (var (first, second) in list) {
                result.Add(first + separator + second);
            }
            return result;
        }
        private ReturnType DisplayBookings() {
            var bookings = _account.Bookings; //DatabaseInterface.GetBookings(_account);
            GUI.ClearWindow();
            GUI.printWindow($"| Account Details |", 0, 0, 100, 25);
            Console.SetCursorPosition(3, 3);
            GUI.PrintList(bookings.Any() ?
                          bookings.Select(x => x.GetBookingInfo()).ToList()! :
                          new List<string> { "You have not made any bookings you cheap SOB" },
                          "Your Bookings");
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 40, Console.GetCursorPosition().Top + 2);
            Console.Write($"Grand Total : {bookings.Sum(x => x.GetCost())} §");
            Console.SetCursorPosition(3, Console.GetCursorPosition().Top + 2);
            GUI.PromtUserInput();
            return ReturnType.CONTINUE;
        }
        private ReturnType MakeBooking() {

            //var bookables = from assembly in AppDomain.CurrentDomain.GetAssemblies()
            //                from type in assembly.GetTypes()
            //                where type.IsSubclassOf(typeof(Booking))
            //                select type.Name;
            //var bookingType = ItemSelectorMenu<string>.SelectItemFromList(bookables.ToList());

            var bookables = new List<Booking> { new Ticket(), new AirplaneBooking() };
            IndexMenu indexMenu = new(typeof(Booking).Name,
                                       bookables.Select(x => x.GetType().Name).ToList()!,
                                       0);
            int index = indexMenu.RunMenu();
            var bookingType = bookables[index];

            var booking = bookingType switch {
                Ticket => MakeTicketBooking(),
                AirplaneBooking => MakeAirPlaneBooking(),
                _ => null,
            };
            return ReturnType.CONTINUE;
        }

        private Booking? MakeAirPlaneBooking() {
            var plane = ChoosePlane();
            var dates = ChooseDate(plane);
            throw new NotImplementedException();
        }

        private object ChooseDate(object plane) {
            throw new NotImplementedException();
        }

        private object ChoosePlane() {
            throw new NotImplementedException();
        }

        private Booking? MakeTicketBooking() {
            List<Flight> flights; // = DatabaseInterface.GetListOf<Flight>();
            using (var db = new OmniDibsContext()) {
                flights = db.Flights.Include(x => x.Origin).Include(x => x.Destination).AsNoTracking().ToList();
                db.Dispose();
            }

            var destination = ChooseAvailableDestination(flights);
            var flight = SelectFlight(flights, destination);
            var tickets = SelectSeatOptions(flight);
            var booking = SelectTicket(tickets);

            //if(booking != null) {
            //    _account.Bookings.Add(booking);
            //    DatabaseInterface.UpdateInDatabase(booking);
            //}

            return booking;
        }

        private Ticket? SelectTicket(List<Ticket> tickets) {
            Ticket? chosen = ItemSelectorMenu<Ticket>.SelectItemFromList(tickets);
            int affected;

            IndexMenu indexMenu = new("Confirm Order",
                                       new List<string>() { chosen.ToString(), "Dont Book Ticket" },
                                       0);
            int choice = indexMenu.RunMenu();

            //if(choice == 0) {
            //    chosen.Account = _account;
            //    DatabaseInterface.UpdateInDatabase(chosen);
            //    _account.Bookings.Add(chosen);
            //    return chosen;
            //} else {
            //    return null;
            //}
            Ticket? newTicket;
            if (choice == 1) {
                return null;
            } else {
                using (var db = new OmniDibsContext()) {
                    db.ChangeTracker.Clear();
                    var ticket = db.Tickets.First(x => x.Id == chosen.Id);
                    var account = db.Accounts.First(x => x.Id == _account.Id);
                    ticket.Account = account;
                    db.Update<Ticket>(ticket);
                    affected = db.SaveChanges();
                    db.ChangeTracker.Clear();
                    if (affected > 0) {
                        _account.Bookings.Add(chosen);
                        chosen.Account = _account;
                        newTicket = chosen;
                    } else {
                        newTicket = null;
                    }
                    db.Dispose();
                }
            }
            return newTicket;
        }

        private List<Ticket> SelectSeatOptions(Flight flight) {
            IndexMenu indexMenu = new("Select Standard",
                                       Enum.GetNames(typeof(Standard)).ToList(),
                                       0);
            int choice = indexMenu.RunMenu();
            Standard standard = (Standard)choice;
            indexMenu = new("WindowSeat?",
                                      new List<string> { "Yes", "No" },
                                      0);
            choice = indexMenu.RunMenu();
            bool windowSeat = choice == 0;
            List<Ticket> tickets = new();
            using (var db = new OmniDibsContext()) {
                tickets = db.Tickets
                    .Include(x => x.Seat)
                    .Where(x => x.Flight == flight && x.Seat.IsWindowSeat == windowSeat && x.Seat.Class == standard && x.Account == null)
                    .AsNoTracking()
                    .ToList();
                db.Dispose();
            }

            return tickets;
        }

        private Flight SelectFlight(List<Flight> flights, Country destination) {
            flights = flights.Where(f => f.Destination == destination).ToList();
            return ItemSelectorMenu<Flight>.SelectItemFromList(flights);
        }

        private Country? ChooseAvailableDestination(List<Flight> flights) {
            List<Country> destinations = flights.Select(f => f.Destination).Distinct().ToList();
            if (destinations.Any()) {
                return ItemSelectorMenu<Country>.SelectItemFromList(destinations);
            }
            return null;
        }

        private ReturnType RemoveBooking() {
            Booking booking = ItemSelectorMenu<Booking>.SelectItemFromList(_account.Bookings.ToList())!;
            if ((booking?.Unbook()).HasValue) {
                _account.Bookings.Remove(booking!);
            }
            return ReturnType.CONTINUE;
        }
    }
}
