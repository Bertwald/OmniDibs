using OmniDibs.Models;

namespace OmniDibs.Data {
    internal class PersonManager {
        public static List<Person> Persons { get; set; } = new();
        public static List<Person> GetAllPersons() {
            if (Persons == null || !Persons.Any()) {
                Persons = new List<Person>() {
                    new () { BirthDate = "19780215-1234", FirstName  = "John", LastName = "Andersson", MailAdress = "john.andersson@gmail.com"  },
                    new () { BirthDate = "19650306-1342", FirstName  = "Maria", LastName = "Claesson", MailAdress = "maria.claesson@gmail.com"  },
                    new () { BirthDate = "19831112-1367", FirstName = "Ida", LastName = "Spjut", MailAdress = "idaspjut@hotmail.com"},
                    new () { BirthDate = "19921220-1538", FirstName = "Felix", LastName = "Persson", MailAdress = "felixp@gmail.com"},
                    new () { BirthDate = "19810112-1284", FirstName = "Christina", LastName = "Holm", MailAdress = "c.holm@hotmail.com"},
                };
            }
            var persons = new List<Person>();
            for (int n = 0; n < 300; n++) {
                Persons.Add(PersonGenerator.GetPerson());
            }
            Persons.Add(new Person() { BirthDate = "19481232-1284", FirstName = "Rune", LastName = "Boss", MailAdress = "BigBoss@TheBoss.is", Alias = "El Bosso" });
            return Persons;
        }
    }
}
