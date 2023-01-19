using Microsoft.EntityFrameworkCore.Storage;
using OmniDibs.Data;
using OmniDibs.Interfaces;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private InputField _idNumber;

        internal RegisterPage() {
            _userField = new InputField("Username", 5, 2, 12, ConsoleColor.White, false);
            _passwordField = new InputField("Password", 35, 2, 12, ConsoleColor.White, true);
            _passwordField2 = new InputField("Repeat", 65, 2, 12, ConsoleColor.White, true);
            _lastname = new InputField("LastName", 35, 8, 12, ConsoleColor.White, false);
            _firstname = new InputField("Name", 5, 8, 12, ConsoleColor.White, false);
            _emailField = new InputField("Mail", 5, 14, 24, ConsoleColor.White, false);
            _emailField.SetInputPattern(@"[A-Z0-9-_@.]");
            _alias = new InputField("Alias", 65, 8, 12, ConsoleColor.White, false);

            _idNumber = new InputField("YYYYMMDD-XXXX", 5, 2, 13, ConsoleColor.White, false);
            _idNumber.SetInputPattern(@"[0-9-]");

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

            if (password.Equals(passwordcheck)) {
                ClearWindow();
                _idNumber.PrintField();
                string id= _idNumber.GetContinousInput();
                bool verified = VerifyIdNumber(id);
                if (verified) {
                    Country? country = ItemSelector<Country>.SelectDatabaseItemFromMenu();
                    Person accountOwner = new() {FirstName = firstname, LastName = lastname, Alias = alias, BirthDate = id, Country = country, MailAdress = email };
                    Account account = new() {UserName=username, Password=password, Privileges = Privileges.USER | Privileges.READ | Privileges.UPDATE, Person = accountOwner};
                    accountOwner.Accounts.Add(account);
                    DatabaseFacade.AddToDatabase<Account>(account);
                } else {
                    Console.WriteLine("IDNUMBER AOK");
                }

            }
            Console.ReadKey();
            return ReturnType.CONTINUE;
        }

        private static void ClearWindow() {
            Console.Clear();
            GUI.printWindow("|OmniDibs Account Creation Step 1/2|", 0, 0, 100, 20);
        }

        private bool VerifyIdNumber(string idNumber) {
            return VerifyIdLength(idNumber) && VerifyIdFormat(idNumber) && VerifyIdChecksum(idNumber) && VerifyIdDate(idNumber);
        }

        private static bool VerifyIdDate(string idNumber) {
            return DateTime.TryParse(idNumber[0..4] + "-" + idNumber[4..6] + "-" + idNumber[6..8], out _);
        }

        private static bool VerifyIdChecksum(string idNumber) {
            return (int)Char.GetNumericValue(idNumber.Last()) == PersonGenerator.GetControlNumber(idNumber[0..13]);
        }
        private static bool VerifyIdFormat(string idNumber) {
            string format = @"[0-9]{8}-[0-9]{4}";
            return Regex.IsMatch(idNumber,format);
        }
        private static bool VerifyIdLength(string idNumber) {
            return idNumber.Length == 13;
        }
    }
}
