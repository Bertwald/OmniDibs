using Microsoft.Data.SqlClient;
using OmniDibs.Interfaces;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using Dapper;
using OmniDibs.Data;
using Microsoft.EntityFrameworkCore;

namespace OmniDibs.Pages {
    internal class AdminPage : DefaultMenu<AdminAlternatives>, IRunnable {
        static readonly string s_connString = "data source=.\\SQLEXPRESS; initial catalog=OmniDibs; persist security info=True; Integrated Security=True";
        private Account _account;
        internal AdminPage(Account account) : base("AdminPage") { _account = account; }

        public ReturnType Run() {
            while (true) {
                GUI.ClearWindow();
                GUI.printWindow($"| {_title} |", 0, 0, 100, 20);
                return RunMenu();
            }
        }

        protected override ReturnType ExecuteMappedAction(AdminAlternatives e) => e switch {
            AdminAlternatives.Booking_Statistics => ShowBookingStatistics(),
            AdminAlternatives.Customer_Statistics => ShowCustomerStatistics(),
            AdminAlternatives.Business_Configurations => ConfigureBusinessObjects(),
            AdminAlternatives.Handle_Accounts => HandleAccounts(),
            AdminAlternatives.Logout => ReturnType.HARDRETURN,
            _ => ReturnType.CONTINUE
        };

        private ReturnType HandleAccounts() {
            string action = ItemSelector<string>.SelectItemFromList(new List<string>() { "Delete Account", "Alter Account", "Back" }, "Account Actions");
            return action switch {
                "Delete Account" => DeleteAccount(),
                "Alter Account" => AlterAccount(),
                "Back" => ReturnType.CONTINUE,
                _ => ReturnType.CONTINUE,
            };
        }

        private ReturnType AlterAccount() {
            List<Account> accounts = GetAccounts();
            if (!accounts.Any()) {
                return ReturnType.CONTINUE;
            }
            Account selectedAccount = ItemSelector<Account>.SelectItemFromList(accounts);
            List<string> fields = GetAccountFieldNames();
            //List<Type> types = GetAccountFieldTypes();
            string chosenField = ItemSelector<string>.SelectItemFromList(fields, "Column");
            Console.WriteLine("Input new value");
            string newVal = Console.ReadLine();
            if (GUI.Confirm("ALTER")) {
                AlterItem(selectedAccount.Id, "Accounts", chosenField, newVal);
            }
            return ReturnType.CONTINUE;
        }

        private static List<Account> GetAccounts() {
            Console.WriteLine("Narrow down potential results by giving input string, i.e. username");
            Console.Write("Selector: ");
            string selector = Console.ReadLine();
            selector ??= "";
            List<Account> accounts = new();
            using (var db = new OmniDibsContext()) {
                if (selector.Any()) {
                    accounts = db.Accounts.Where(x => x.UserName.Contains(selector)).ToList();
                } else {
                    accounts = db.Accounts.ToList();
                }
            }
            return accounts;
        }

        private static ReturnType DeleteAccount() {
            List<Account> accounts = GetAccounts();
            if (!accounts.Any()) {
                return ReturnType.CONTINUE;
            }
            Account selectedAccount = ItemSelector<Account>.SelectItemFromList(accounts);
            if (GUI.Confirm("DELETE ACCOUNT")) {
                using (var db = new OmniDibsContext()) {
                    // Find() felt like a better option, but include is not available as part of find
                    var account = db.Accounts.Include(x => x.Bookings).FirstOrDefault(x => x.Id == selectedAccount.Id);
                    if (!account.Bookings.Any()) {
                        db.Accounts.Remove(account);
                        db.SaveChanges();
                    } else {
                        Console.WriteLine("Account has bookings and cannot be safely deleted: Consider Upgrading to OmniDibs 1.02");
                        GUI.Delay();
                    }
                }
            }
            return ReturnType.CONTINUE;
        }

        private List<string> GetAccountFieldNames() {
            return typeof(Account).GetProperties().Select(x => x.Name).ToList();
        }

        private static ReturnType ConfigureBusinessObjects() {
            GUI.ClearWindow();
            Console.WriteLine("OmniDibs 0.92 Only supports handling of Flights, Upgrade to our Premium Product 1.02");
            Console.WriteLine("To Access our full set of functionality");
            GUI.Delay();
            //case create Flight
            int plane = ItemSelector<Airplane>.SelectDatabaseItemFromMenu().Id;
            int origin = ItemSelector<Country>.SelectDatabaseItemFromMenu().Id;
            int destination = ItemSelector<Country>.SelectDatabaseItemFromMenu().Id;
            Console.WriteLine("Base Cost of flight");
            if (!float.TryParse(Console.ReadLine(), out float basecost)) {
                basecost = 0.0f;
            }
            Console.WriteLine("Departure ex:YYYY-MM-DD");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime departure)) {
                Console.WriteLine("Incorrect input: departure defaulted to today + 30 days");
                departure = DateTime.Now.AddDays(30);
            }
            Console.WriteLine("Arrival ex:YYYY-MM-DD");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime arrival)) {
                Console.WriteLine("Incorrect input: arrival defaulted to departure + 4hours");
                arrival = departure;
                arrival.AddHours(4);
            }
            Console.WriteLine("Flight Name");
            string name = Console.ReadLine();
            if (GUI.Confirm("Create new Flight")) {
                using (var db = new OmniDibsContext()) {
                    var airplane = db.Airplanes.Find(plane);
                    var o = db.Countries.Find(origin);
                    var d = db.Countries.Find(destination);
                    Flight newFlight = new Flight() { Airplane = airplane, Origin = o, Destination = d, BaseCost = basecost, Name = name, Arrival = arrival, Departure = departure };
                    db.Add(newFlight);
                    db.SaveChanges();
                    var flight = db.Flights.Where(x => x.Name == name).Include(x => x.Tickets).Include(x => x.Airplane).ThenInclude(x => x.Seats).ToList().First();
                    List<Ticket> tickets = TicketGenerator.GenerateTicketsForFlight(flight);
                    foreach (Ticket ticket in tickets) {
                        flight.Tickets.Add(ticket);
                    }
                    db.AddRange(tickets);
                    db.SaveChanges();
                }
            }
            return ReturnType.CONTINUE;
        }

        private static ReturnType ShowCustomerStatistics() {
            GUI.ClearWindow();
            using (var db = new OmniDibsContext()) {
                var accountBookings = db.Bookings
                                        .Where(x => x.Account != null && x is Ticket)
                                        .Include(x => x.Account)
                                        .GroupBy(x => x.Account.UserName)
                                        .Select(group => new { accountName = group.Key, nrBookings = group.Sum(x => 1) })
                                        .OrderByDescending(x => x.nrBookings)
                                        .Take(3).ToList();
                Console.WriteLine("Our Top Travellers");
                foreach (var accountBooking in accountBookings) {
                    Console.WriteLine($"{accountBooking.accountName} has a total of {accountBooking.nrBookings} flight tickets in their pocket");
                }
                Console.WriteLine();
                var accountGroup = db.Bookings
                                        .Where(x => x.Account != null)
                                        .Include(x => x.Account)
                                        .GroupBy(x => x.Account.UserName)
                                        .Select(x => new { accountName = x.Key, accountBookings = x.ToList() }).ToList();
                var accountMaxes = accountGroup
                                .Select(group => new {
                                    userName = group.accountName,
                                    userMax = group.accountBookings.MaxBy(x => x.GetCost())
                                })
                                .OrderByDescending(x => x.userMax.GetCost())
                                .Take(3);
                Console.WriteLine("Our most well filled purses");
                foreach (var accountMax in accountMaxes) {
                    Console.WriteLine($"{accountMax.userName} has an expensive charter: {accountMax.userMax}");
                }
                Console.WriteLine();
                GUI.Delay();
            }
            return ReturnType.CONTINUE;
        }

        private static ReturnType ShowBookingStatistics() {
            GUI.ClearWindow();
            using (var db = new OmniDibsContext()) {
                var flightsTickets = db.Flights.Include(x => x.Tickets).ThenInclude(x => x.Account).OrderBy(x => x.Departure).Take(10).ToList();
                foreach (var flightTicket in flightsTickets) {
                    float percentage = 100f * (float)flightTicket.Tickets.Where(x => x.Account != null).Count() / (float)flightTicket.Tickets.Count;
                    if (percentage < 20f) {
                        Console.ForegroundColor = ConsoleColor.Red;
                    } else if (percentage < 70f) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    } else {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine($"Flight {flightTicket.Name} has {percentage}% Occupation");
                }
                Console.ResetColor();

            }
            GUI.Delay();

                return ReturnType.CONTINUE;
        }

        protected override AdminAlternatives GetE(int i) {
            return (AdminAlternatives)i;
        }


        public static bool AlterItem(int id, string tableName, string column, string newValue) {
            int affectedRow = 0;

            string sql = $"UPDATE [{tableName}] SET [{column}] = '{newValue}' WHERE Id = {id}";

            using (var connection = new SqlConnection(s_connString)) {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow > 0;
        }
    }
}
