using OmniDibs.Menus;

namespace OmniDibs {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            MainMenu main = new MainMenu();
            main.DisplayMenu();

            Menu menu = new Menu("Second Menu", new List<string> { "Default", "Enter", "Exit" });
            Console.WriteLine(Environment.NewLine);
            menu.ShowNumberedActionsMenu();
        }
    }
}