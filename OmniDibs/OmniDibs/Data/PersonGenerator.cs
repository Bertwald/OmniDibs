using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Data {
    /**
     * A class for creating randomly generated names
     * 
     */
    internal sealed class PersonGenerator {
        private static readonly Random random = new();
        private static readonly string[] firstnames = {
                "Karin",
                "Anders",
                "Johan",
                "Eva",
                "Maria",
                "Mikael",
                "Anna",
                "Sara",
                "Erik",
                "Per",
                "Christina",
                "Lena",
                "Lars",
                "Emma",
                "Kerstin",
                "Karl",
                "Marie",
                "Peter",
                "Thomas",
                "Karl",
                "Jan",
                "Sven",
                "Mikael",
                "Arne",
                "Ruben",
                "Helen",
                "Helena",
                "Isak"};
        private static readonly string[] lastnames = {
                "Andersson",
                "Johansson",
                "Karlsson",
                "Nilsson",
                "Eriksson",
                "Larsson",
                "Olsson",
                "Persson",
                "Svensson",
                "Gustafsson",
                "Pettersson",
                "Jonsson",
                "Jansson",
                "Hansson"
        };
        private static readonly string[] domains = {
            "@gmail.com",
            "@hotmail.com",
            "@qtmail.com",
        };
        internal static string GetBirthDate() {
            DateTime birthDate = GetBirthDateTime();
            string IdentityNumber = birthDate.ToString("yyyyMMdd") + "-" + string.Join("",Enumerable.Range(1, 3).Select(s => random.Next(0,9)));
            return IdentityNumber + GetControlNumber(IdentityNumber);
        }

        private static int GetControlNumber(string identityNumber) {
            int[] numbers = CharArrayToIntArray(identityNumber.Reverse().ToArray());
            string control = "";
            for (int index = 0; index < 9; index++) {
                control += (2 - index % 2) * numbers[index];
            }
            numbers = CharArrayToIntArray(control.ToArray());
            int checksum = 0;
            foreach(int number in numbers) {
                checksum += number;
            }
            return (1000-checksum) % 10;
        }
        private static int[] CharArrayToIntArray(char[] array) {
            return Array.ConvertAll(array, c => (int)Char.GetNumericValue(c));
        }

        private static DateTime GetBirthDateTime() {
            DateTime endDate = DateTime.Now;
            DateTime startDate = new(1920,01,01);
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new (0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime birthDate = startDate + newSpan;
            return birthDate;
        }

        /**
* A method that returns a full (Swedish) name 
*/
        internal static string GetName() {
            return GetFirstName() +
             " " + GetLastName();
        }
        /**
         * Returns a swedish first name
         */
        internal static string GetFirstName() {
            return firstnames[random.Next(firstnames.Length)];
        }
        internal static string GetDomain() {
            return domains[random.Next(domains.Length)];
        }
        /**
         * Returns a swedish surname
         */
        internal static string GetLastName() {
            return lastnames[random.Next(lastnames.Length)];
        }
        internal static Person GetPerson() {
            string firstname = GetFirstName();
            string lastname = GetLastName();
            string Idnumber = GetBirthDate();
            return new Person() {FirstName = firstname, LastName = lastname, BirthDate = Idnumber, 
                                 MailAdress = firstname + "_" + lastname + Idnumber[2 .. 4] + GetDomain()};
        }
        internal static Account GetAccount(Person person, Privileges privileges = Privileges.USER | Privileges.READ | Privileges.UPDATE) {
            return new Account() { Person = person, Privileges = privileges, 
                                   UserName = person.FirstName[.. Math.Min(4, person.FirstName.Length)] + 
                                   person.LastName[..Math.Min(4, person.LastName.Length)] + (person.BirthDate ?? "0123")[^4..], 
                                   Password = "User00"  };
        }
        //Good luck ever obtaining an instance of this class 
        private PersonGenerator() {; }
    }
}
