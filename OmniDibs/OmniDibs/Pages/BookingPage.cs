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
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Pages {
    internal class BookingPage : DefaultMenu<BookingAlternatives>, IRunnable {
        private Account _account;
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

        private Dictionary<string, string> GetPropertyPairs<T>(T instance) {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                keyValuePairs.Add(info.Name, info.GetValue(instance, null)?.ToString()!);
            }
            return keyValuePairs;
        }
        private List<(T, T)> GetMatches<T>(IEnumerable<T> keys, Dictionary<T, T> dictionary) where T : class {
            return keys.Where(dictionary.ContainsKey).Select(x => (x, dictionary[x])).ToList();
        }
        private List<string> Compress(List<(string, string)> list, string separator = " : ") {
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

            return ReturnType.CONTINUE;
        }
        private ReturnType RemoveBooking() {
            Booking booking = ItemSelectorMenu<Booking>.SelectItemFromList(_account.Bookings.ToList())!;
            bool success = DatabaseInterface.RemoveFromDatabase(booking);
            if (success) {
                _account.Bookings.Remove(booking);
            }
            return ReturnType.CONTINUE;
        }
    }
}
