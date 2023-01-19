using Microsoft.EntityFrameworkCore;
using OmniDibs.Interfaces;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using System.Globalization;
using System.Reflection;

namespace OmniDibs.Pages {
    internal class BookingPage : DefaultMenu<BookingAlternatives>, IRunnable {
        private enum Month {
            January =  1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
        private readonly Account _account;
        internal BookingPage(Account account) : base("Booking Alternatives") { _account = account; }

        public ReturnType Run() {

            while (true) {
                GUI.ClearWindow();
                GUI.printWindow($"| {_title} |", 0, 0, 100, 20);
                return RunMenu();
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
            GUI.Delay();
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
            var bookings = DatabaseFacade.GetBookings(_account);
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
            GUI.Delay();
            return ReturnType.CONTINUE;
        }
        private ReturnType MakeBooking() {
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
            (DateTime start, DateTime end) = ChooseDate(plane);
            if(start == DateTime.MinValue || end == DateTime.MinValue) {
                GUI.ClearWindow();
                Console.WriteLine($"Sorry, no Available dates for {plane.Name + " : " + plane.Model} on chosen month");
                GUI.Delay();
                return null;
            }
            BookPlaneBetweenDates(plane, start, end);
            return null;
        }

        private void BookPlaneBetweenDates(Airplane plane, DateTime start, DateTime end) {
            using (var db = new OmniDibsContext()) {
                var account = db.Accounts.First(x => x.Id == _account.Id);
                var airplane = db.Airplanes.Include(x => x.Seats).First(x => x.Id == plane.Id);
                AirplaneBooking booking = new AirplaneBooking() { Account = account, 
                                                                  Airplane = airplane, 
                                                                  StartDate = start, 
                                                                  EndDate = end, 
                                                                  Cost = (float) (airplane.Seats.Count*15000 * (1 + (end-start).TotalDays )) };
                account.Bookings.Add(booking);
                db.Add(booking);
                db.SaveChanges();
            }
        }

        private (DateTime start, DateTime end) ChooseDate(Airplane plane) {
            int startDay, endDay;
            //There is a 13th "null" month in MonthNames array, we only need 12 here
            List<string> months = (DateTimeFormatInfo.InvariantInfo.MonthNames).Take(12).ToList();
            string startMonth = ItemSelector<string>.SelectItemFromList(months);
            int monthNumber = ((int)Enum.Parse(typeof(Month), startMonth));
            List<int> availableDays;
            availableDays = GetAvailableDaysInMonth(plane, monthNumber);
            if (!availableDays.Any()) {
                return (DateTime.MinValue, DateTime.MinValue);
            }
            startDay = SelectDay($"From which available day do you want to charter the {plane.Model}", startMonth, monthNumber, availableDays);
            availableDays = GetListOfContigous(startDay, monthNumber, availableDays);
            endDay = SelectDay($"On which day do you want the booking to end", startMonth, monthNumber, availableDays);
            return (new DateTime(2023, monthNumber, startDay), new DateTime(2023, monthNumber, endDay));
        }

        private static List<int> GetAvailableDaysInMonth(Airplane plane, int monthNumber) {
            var days = Enumerable.Range(1, DateTime.DaysInMonth(2023, monthNumber));
            List<int> availableDays;
            using (var db = new OmniDibsContext()) {
                var usedByFlight = db.Flights.Where(x => x.Airplane == plane && x.Departure.Month == monthNumber).Select(x => Enumerable.Range(CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(x.Departure),
                                                                           1 + CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(x.Arrival) -
                                                                           CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(x.Departure))).ToList();
                var daysBooked = db.AirplaneBookings.Where(x => x.Airplane == plane && x.StartDate.Month == monthNumber).Select(x => Enumerable.Range(CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(x.StartDate),
                                                                           1 + CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(x.EndDate) -
                                                                           CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(x.StartDate))).ToList();
                foreach (var range in usedByFlight) {
                    days = days.Except(range);
                }
                foreach (var range in daysBooked) {
                    days = days.Except(range);
                }
                availableDays = days.ToList();
            }
            return availableDays;
        }

        private static List<int> GetListOfContigous(int start, int monthNumber, List<int> available) {
            int offset = 0;
            for (int day = start; day < DateTime.DaysInMonth(2023, monthNumber); day++) {
                if (!available.Contains(day)) {
                    break;
                }
                offset++;
            }
            available = available.Where(x => x >= start && x <= start + offset).ToList();
            return available;
        }

        private static int SelectDay(string choiceString, string startMonth, int monthNumber, List<int> availableDays) {
            int startDay;
            GUI.ClearWindow();
            GUI.PrintBookingDays(startMonth, DateTime.DaysInMonth(2023, monthNumber), availableDays);
            Console.WriteLine(choiceString);
            Console.Write("Choice: ");
            startDay = InputModule.GetValidatedInt(availableDays);
            return startDay;
        }

        private Airplane ChoosePlane() {
            Airplane chosenPlane = ItemSelector<Airplane>.SelectDatabaseItemFromMenu();
            return chosenPlane;
        }

        private Booking? MakeTicketBooking() {
            List<Flight> flights;
            using (var db = new OmniDibsContext()) {
                flights = db.Flights.Include(x => x.Origin).Include(x => x.Destination).AsNoTracking().ToList();
                db.Dispose();
            }

            var destination = ChooseAvailableDestination(flights);
            var flight = SelectFlight(flights, destination);
            var tickets = SelectSeatOptions(flight);
            var booking = SelectTicket(tickets);

            return booking;
        }

        private Ticket? SelectTicket(List<Ticket> tickets) {
            Ticket? chosen = ItemSelector<Ticket>.SelectItemFromList(tickets);
            int affected;

            IndexMenu indexMenu = new("Confirm Order",
                                       new List<string>() { chosen.ToString(), "Dont Book Ticket" },
                                       0);
            int choice = indexMenu.RunMenu();

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

        private Flight? SelectFlight(List<Flight> flights, Country destination) {
            flights = flights.Where(f => f.Destination == destination).ToList();
            return ItemSelector<Flight>.SelectItemFromList(flights);
        }

        private Country? ChooseAvailableDestination(List<Flight> flights) {
            List<Country> destinations = flights.Select(f => f.Destination).Distinct().ToList();
            if (destinations.Any()) {
                return ItemSelector<Country>.SelectItemFromList(destinations);
            }
            return null;
        }

        private ReturnType RemoveBooking() {
            Booking booking = ItemSelector<Booking>.SelectItemFromList(_account.Bookings.ToList())!;
            if ((booking?.Unbook()).HasValue) {
                _account.Bookings.Remove(booking!);
            }
            return ReturnType.CONTINUE;
        }
    }
}
