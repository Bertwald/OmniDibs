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
        //private Menu _menu;
        //private List<string> actions = new List<string>() { "Login", "Register" };

        internal LoginPage() {
            _userField = new InputField("Username", 45, 10, 12, ConsoleColor.White, false);
            _passwordField = new InputField("Password", 45, 15, 12, ConsoleColor.White, true);
            //_menu = new Menu("User Login", actions, 45, 10);
        }

        public ReturnType Run() {
            Console.Clear();
            GUI.printWindow("|OmniDibs System Login|", 0, 0, 100, 20);
            GUI.printLogo(17, 2);

            _userField.PrintField();
            _passwordField.PrintField();
            string username = _userField.GetContinousInput();
            string password = _passwordField.GetContinousInput();
            Account? user = DatabaseInterface.VerifyLogin(username, password);
            if (user != null) {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Welcome {user.Privileges} : {user.UserName} {user.Password}");
                Console.ReadKey();
            }
            return user != null ? Redirect(user) : ReturnType.CONTINUE;
        }

        internal ReturnType Redirect(Account user) => user.Privileges switch {
            Privileges.USER => (new BookingPage(user)).Run(),
            Privileges.ADMIN => (new AdminPage(user)).Run(),
            _ => ReturnType.CONTINUE,
        };
    }
}
