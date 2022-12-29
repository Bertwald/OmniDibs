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
            LoginPage page = new LoginPage();
            page.Run();
        }
    }
}