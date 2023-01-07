using OmniDibs.Models;
using OmniDibs.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal class RootMenu : DefaultMenu<LoginOptions> {
        public RootMenu(string title = "OmniDibs Login Options") : base(title) {
        }

        protected override ReturnType ExecuteMappedAction(LoginOptions e) => e switch {
            LoginOptions.Login => (new LoginPage()).Run(),
            LoginOptions.Register => (new RegisterPage().Run()),
            _ => ReturnType.CONTINUE
        };

        protected override LoginOptions GetE(int i) => i switch {
            _ => (LoginOptions)i
        };
    }
}
