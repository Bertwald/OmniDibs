using OmniDibs.Data;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Logic {
    internal class DataIniter {
        private static Random random= new Random();
        public static void InitData() {
            List<Person> persons = PersonManager.GetAllPersons();
            List<Country> countries = CountryManager.GetCountries();
            List<Account> accounts = new();
            foreach (Person person in persons) {
                Country country = countries[random.Next()%countries.Count];
                person.Country = country;
                Account account = PersonGenerator.GetAccount(person);
                person.Accounts.Add(account);
                accounts.Add(account);
            }

            using var db = new OmniDibsContext();
            foreach (var country in countries) {
                db.Add(country);
            }
            foreach (var account in accounts) {
                db.Add(account);
            }
            foreach (var person in persons) {
                db.Add(person);
            }
            Account admin = new Account() {
                Person = persons.Last(),
                Privileges = Privileges.OWNER | Privileges.ADMIN | Privileges.CREATE | Privileges.READ | Privileges.UPDATE | Privileges.DELETE,
                UserName = "BigBoss",
                Password = "Admin"
            };
            db.Add(admin);
            persons.Last().Accounts.Add(admin);

            db.SaveChanges();
        }
    }
}
