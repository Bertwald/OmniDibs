using OmniDibs.Models;
using OmniDibs.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal class LoginMenu2 : DefaultMenu<LoginOptions> {
        internal LoginMenu2(string title = "LoginMenu") : base(title) {
        }
        protected override ReturnType ExecuteMappedAction(LoginOptions options) => options switch {
            LoginOptions.Login => new LoginPage().Run(),
            _ => ReturnType.CONTINUE
        };
        protected override LoginOptions GetE(int i) {
            return (LoginOptions)i;
        }
    }
}
