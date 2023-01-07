using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Logic {
    internal class DatabaseInterface {
        internal static Account? VerifyLogin(string username, string password) {
            using OmniDibsContext context = new();
            var account = context.Accounts.Where(x => x.UserName.Equals(username) && x.Password.Equals(password));
            return account.FirstOrDefault();

        }

        internal static void AddToDatabase<T>(T item) where T : class {
            using OmniDibsContext context = new();
            context.Add<T>(item);
            context.SaveChanges();
        }
        internal static void RemoveFromDatabase<T>(T item) where T : class {
            using OmniDibsContext context = new();
            context.Remove<T>(item);
            context.SaveChanges();
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
    }
}
