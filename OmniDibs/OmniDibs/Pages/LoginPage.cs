using OmniDibs.Interfaces;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Pages {
    internal class LoginPage : IRunnable {

        private InputField _userField;
        private InputField _passwordField;

        internal LoginPage() {
            _userField = new InputField("Username", 45, 10, 12, ConsoleColor.White, false);
            _passwordField = new InputField("Password", 45, 15, 12, ConsoleColor.White, true);
        }

        public ReturnType Run() {
            while (true) {
                Console.Clear();
                GUI.PrintWindow("|OmniDibs System Login|", 0, 0, 100, 20);
                GUI.PrintLogo(17, 2);
                _userField.PrintField();
                _passwordField.PrintField();
                string username = _userField.GetContinousInput();
                string password = _passwordField.GetContinousInput();
                Account? user = DatabaseFacade.VerifyLogin(username, password);
                return user != null ? Redirect(user) : ReturnType.CONTINUE;
            }
        }

        internal static ReturnType Redirect(Account user) {
            if (user.Privileges.HasFlag(Privileges.USER)) {
                return (new BookingPage(user)).Run();
            } else if (user.Privileges.HasFlag(Privileges.ADMIN)) {
                return (new AdminPage(user)).Run();
            } else {
                return ReturnType.CONTINUE;
            }
        }
    }
}
