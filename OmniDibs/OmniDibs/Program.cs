using OmniDibs.Data;
using OmniDibs.Logic;
using OmniDibs.Menus;
using OmniDibs.Models;
using OmniDibs.Pages;
using OmniDibs.UI;

namespace OmniDibs {
    internal class Program {
        static void Main(string[] args) {
            Console.Title = "OmniDibs 0.92";
            Console.CursorVisible = false;
            RootPage root = new RootPage();
            root.Run();

            //DataIniter.InitData();
            //DataIniter.InitPlanes();
            //DataIniter.InitFlights();
            //DataIniter.InitCharters();
            //DataIniter.InitTickets();

        }
}
}