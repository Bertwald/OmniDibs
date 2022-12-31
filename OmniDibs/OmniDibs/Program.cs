using OmniDibs.Data;
using OmniDibs.Menus;
using OmniDibs.Pages;
using OmniDibs.UI;

namespace OmniDibs {
    internal class Program {
        static void Main(string[] args) {
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
            Console.CursorVisible = false;

            LoginMenu2 start = new LoginMenu2();
            start.RunMenu();
        }
    }
}