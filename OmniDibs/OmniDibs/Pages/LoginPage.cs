using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Pages {
    internal class LoginPage {

        private LoginField _userField;
        private LoginField _passwordField;
        private Menu _menu;
        private List<string> actions = new List<string>() { "Login", "Register" };

        internal LoginPage() {
            _userField = new LoginField("Username", 45, 10, 12, ConsoleColor.White, false);
            _passwordField = new LoginField("Password", 45, 15, 12, ConsoleColor.White, true);
            _menu = new Menu("User Login", actions, 45, 10);
        }

        internal ReturnType Run() {
            Console.Clear();
            GUI.printWindow("|OmniDibs System Login|", 0, 0, 100, 20);
            GUI.printLogo(17, 2);

            _userField.PrintField();
            _passwordField.PrintField();
            string username = _userField.GetContinousInput();
            string password = _passwordField.GetContinousInput();
            return ReturnType.CONTINUE;
        }
    }
}
