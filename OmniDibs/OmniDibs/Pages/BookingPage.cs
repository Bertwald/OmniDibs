using OmniDibs.Interfaces;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            BookingAlternatives.Logout => ReturnType.HARDRETURN
        };

        protected override BookingAlternatives GetE(int i) => i switch {
            _ => (BookingAlternatives)i
        };

        private ReturnType DisplayAccountDetails() {
            IEnumerable<string> accountFields = new List<string> { "UserName", "Privileges" };
            IEnumerable<string> personFields = new List<string> { "FirstName" , "LastName", "Country", "MailAdress" };
            GUI.ClearWindow();
            GUI.printWindow($"| Account Details |", 0, 0, 100, 20);
            var accountDictionary = GetPropertyPairs<Account>(_account);
            var accountResult = GetMatches<string>(accountFields, accountDictionary);
            foreach(var (name, value) in accountResult) {
                GUI.PrintPropertyPair(name, value);
            }

            var personDictionary = GetPropertyPairs<Person>(_account.Person);
            var personResult = GetMatches<string>(personFields, personDictionary);
            foreach (var (name, value) in personResult) {
                GUI.PrintPropertyPair(name, value);
            }

            GUI.PromtUserInput();
            return ReturnType.CONTINUE;
        }

        private Dictionary<string , string> GetPropertyPairs<T>(T instance) {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                keyValuePairs.Add(info.Name, info.GetValue(instance, null)?.ToString()!);
            }
            return keyValuePairs;
        }
        private List<(T,T)> GetMatches<T>(IEnumerable<T> keys, Dictionary<T, T> dictionary) {
            return keys.Where(dictionary.ContainsKey).Select(x => (x, dictionary[x])).ToList();
        }
        private ReturnType DisplayBookings() {

            return ReturnType.CONTINUE;
        }
        private ReturnType MakeBooking() {
            return ReturnType.CONTINUE;
        }
    }
}
