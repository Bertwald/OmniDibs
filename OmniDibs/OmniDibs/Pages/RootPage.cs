using OmniDibs.Interfaces;
using OmniDibs.Menus;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Pages {
    internal class RootPage : IRunnable {
        RootMenu menu = new RootMenu();
        public ReturnType Run() {
            while (true) {
                menu.RunMenu();
            }
        }
    }
}
