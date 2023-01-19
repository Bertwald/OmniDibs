using Microsoft.EntityFrameworkCore;
using OmniDibs.Models;
using System.Collections.Immutable;

namespace OmniDibs.Logic {
    internal class DatabaseFacade {
        internal static Account? VerifyLogin(string username, string password) {
            Account? account;
            using (OmniDibsContext context = new()) {
                account = context.Accounts
                                     .Where(x => x.UserName
                                                  .Equals(username) &&
                                                 x.Password
                                                  .Equals(password))
                                     .Include(x => x.Person)
                                     .ThenInclude(x => x.Country)
                                     .AsNoTracking()
                                     .FirstOrDefault();
                context.Dispose();
            }

            return account;

        }
        internal static List<T> GetListOf<T>() where T : class {
            List<T> list;
            using (OmniDibsContext context = new()) {
                list = context.Set<T>().AsNoTracking().ToList();
                context.Dispose();
            }
            return list;
        }
        internal static ImmutableList<T> GetImmutableListOf<T>() where T : class {
            ImmutableList<T> list;
            using (OmniDibsContext context = new()) {
                list = context.Set<T>().AsNoTracking().ToImmutableList();
                context.Dispose();
            }
            return list;
        }

        internal static void AddToDatabase<T>(T item) where T : class {
            using (OmniDibsContext context = new()) {
                //HACK: Attach fixes "Cannot insert explicit value for identity" when object has references to other tracked items
                context.Attach(item);
                context.Add<T>(item);
                context.SaveChanges();
                context.Dispose();
            }
        }
        internal static bool RemoveFromDatabase<T>(T item) where T : class {
            int affected;
            using (OmniDibsContext context = new()) {
                context.Remove<T>(item);
                affected = context.SaveChanges();
                context.Dispose();
            }
                return affected != 0;
        }
        internal static void UpdateInDatabase<T>(T item) where T : class {
            using (OmniDibsContext context = new()) {
                //context.Attach<T>(item);
                context.Update<T>(item);
                context.SaveChanges();
                context.Dispose();
            }
        }
        internal static T? GetItemByKey<T>(int key) where T : class {
            T? alts;
            using (OmniDibsContext context = new()) {
                alts = context.Find<T>(key);
                context.Dispose();
            }
            return alts;
        }

        internal static ISet<Booking> GetBookings(Account account) {
            HashSet<Booking> bookings = new HashSet<Booking>();
            using (OmniDibsContext context = new()) {
                    var charters = context.AirplaneBookings.Where(x => x.Account.Equals(account)).Include(x => x.Airplane).ThenInclude(x => x.Seats).ToList();
                    var tickets = context.Tickets.Include(x => x.Flight).Where(x => x.Account.Equals(account)).ToList();
                    foreach (var x in charters) {
                        bookings.Add(x);
                    }
                    foreach (var x in tickets) {
                        bookings.Add(x);
                    }
                context.Dispose();
            }
            return bookings;
        }
    }
}
