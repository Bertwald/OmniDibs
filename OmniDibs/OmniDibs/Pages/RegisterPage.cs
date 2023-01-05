using OmniDibs.Interfaces;
using OmniDibs.Models;
using OmniDibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Pages {
    internal class RegisterPage : IRunnable {
        private InputField _userField;
        private InputField _passwordField;
        private InputField _passwordField2;
        private InputField _emailField;
        private InputField _firstname;
        private InputField _lastname;
        private InputField _alias;

        internal RegisterPage() {
            _userField = new InputField("Username", 5, 2, 12, ConsoleColor.White, false);
            _passwordField = new InputField("Password", 35, 2, 12, ConsoleColor.White, true);
            _passwordField2 = new InputField("Repeat", 65, 2, 12, ConsoleColor.White, true);
            _lastname = new InputField("LastName", 35, 8, 12, ConsoleColor.White, false);
            _firstname = new InputField("Name", 5, 8, 12, ConsoleColor.White, false);
            _emailField = new InputField("Mail", 5, 14, 24, ConsoleColor.White, false);
            _emailField.SetInputPattern(@"[A-Z0-9-_@.]");
            _alias = new InputField("Alias", 65, 8, 12, ConsoleColor.White, false);

        }
        public ReturnType Run() {
            Console.Clear();
            GUI.printWindow("|OmniDibs Account Creation Step 1/2|", 0, 0, 100, 20);
            _userField.PrintField();
            _passwordField.PrintField();
            _passwordField2.PrintField();
            _lastname.PrintField();
            _firstname.PrintField();
            _emailField.PrintField();
            _alias.PrintField();

            string username = _userField.GetContinousInput();
            string password = _passwordField.GetContinousInput();
            string passwordcheck = _passwordField2.GetContinousInput();
            string firstname = _firstname.GetContinousInput();
            string lastname = _lastname.GetContinousInput();
            string alias = _alias.GetContinousInput();
            string email = _emailField.GetContinousInput();

            Console.ReadKey();
            //while (true) {

            //}
            return ReturnType.CONTINUE;
        }
    }
}
