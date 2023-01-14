using OmniDibs.Data;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.Pages;
using OmniDibs.UI;

namespace OmniDibs {
    internal class Program {
        static void Main(string[] args) {
            //ClassCreator.CreateFromInput<Person>();

            Console.CursorVisible = false;
            RootPage root = new RootPage();
            root.Run();


            //Console.WriteLine("Hello, World!");
            //MainMenu main = new MainMenu();
            //main.DisplayMenu();

            //Menu menu = new Menu("Second Menu", new List<string> { "Default", "Enter", "Exit" });
            //Console.WriteLine(Environment.NewLine);
            //menu.ShowNumberedActionsMenu();

            //LoginField login = new LoginField("login",1,1,12,ConsoleColor.Blue, true);
            //string name = login.GetContinousInput();
            //Console.WriteLine(name);
            //for (int i = 0; i < 10; i++) {
            //    Console.WriteLine(PersonGenerator.GetBirthDate());
            //}

            //MainMenu menu = new();
            //menu.RunMenu();

            //LoginMenu login = new LoginMenu();
            //login.RunMenu();

            //LoginPage page = new LoginPage();
            //page.Run();


            //List<Person> persons = new List<Person>() { PersonGenerator.GetPerson(), PersonGenerator.GetPerson(), 
            //    PersonGenerator.GetPerson(), PersonGenerator.GetPerson(), PersonGenerator.GetPerson(), PersonGenerator.GetPerson(), PersonGenerator.GetPerson(), };

            //Account account = PersonGenerator.GetAccount(persons.First());

            //DataIniter.InitData();

            //Country country = new Country() { CountryName = "Turkey", Languages = {"Turkish","English"}, ContinentsString = "EUROPE,ASIA", Climate = Climate.TEMPERATE, BigMacIndex = 2.68F };
            //foreach(var str in country.Languages) {
            //    Console.WriteLine(str);
            //}
            //foreach (var str in country.Continents) {
            //    Console.WriteLine(str);
            //}
            //Console.WriteLine($"{country.CountryName} is Located in {country.ContinentsString}");
            //Console.WriteLine($"In {country.CountryName} common languages are {country.LanguagesString}");
            //LoginMenu2 start = new LoginMenu2();
            //start.RunMenu();
        }
    }
}