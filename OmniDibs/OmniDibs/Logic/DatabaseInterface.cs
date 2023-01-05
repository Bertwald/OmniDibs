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
            return account != null && account.Any() ? account?.First() : null;
        }
    }
}
