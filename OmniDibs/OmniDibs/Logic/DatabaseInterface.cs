using Microsoft.EntityFrameworkCore;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Logic {
    internal class DatabaseInterface {
        internal static Account? VerifyLogin(string username, string password) {
            using OmniDibsContext context = new();
            var account = context.Accounts
                                 .Where(x => x.UserName
                                              .Equals(username) &&
                                             x.Password
                                              .Equals(password))
                                 .Include(x => x.Person)
                                 .ThenInclude(x => x.Country)
                                 .Include(x => x.Bookings);
            return account.FirstOrDefault();

        }
        internal static List<T> GetListOf<T>() where T : class {
            using OmniDibsContext context = new();
            return context.Set<T>().ToList();
        }
        internal static ImmutableList<T> GetImmutableListOf<T>() where T : class {
            using OmniDibsContext context = new();
            return context.Set<T>().ToImmutableList();
        }

        internal static void AddToDatabase<T>(T item) where T : class {
            using OmniDibsContext context = new();
            //HACK: Attach fixes "Cannot insert explicit value for identity" when object has references to other tracked items
            context.Attach(item);
            context.Add<T>(item);
            context.SaveChanges();
        }
        internal static bool RemoveFromDatabase<T>(T item) where T : class {
            using OmniDibsContext context = new();
            context.Remove<T>(item);
            int affected = context.SaveChanges();
            return affected != 0;
        }
        internal static void UpdateInDatabase<T>(T item) where T : class {
            using OmniDibsContext context = new();
            context.Update<T>(item);
            context.SaveChanges();
        }
        internal static T? GetItemByKey<T>(int key) where T : class {
            using OmniDibsContext context = new();
            T? alts = context.Find<T>(key);
            return alts;
        }

        internal static ISet<Booking> GetBookings(Account account) {
            using (OmniDibsContext context = new()) {
                HashSet<Booking> bookings = new HashSet<Booking>();
                if (account.Bookings.Any()) {
                    var charters = context.AirplaneBookings.Where(x => x.OrderAccount.Equals(account)).Include(x => x.Airplane).ThenInclude(x => x.Seats).ToList();
                    var tickets = context.Bookings.Where(x => x.OrderAccount.Equals(account) && x is Ticket).ToList();
                    foreach (var x in charters) {
                        bookings.Add(x);
                    }
                    foreach (var x in tickets) {
                        bookings.Add(x);
                    }
                    return bookings;
                }
                return account.Bookings;
            }
        }
    }
}
