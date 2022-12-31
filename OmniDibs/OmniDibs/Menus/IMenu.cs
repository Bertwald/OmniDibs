using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal interface IMenu<E> where E : System.Enum {
        internal void DisplayMenu();
        internal ReturnType RunMenu();
    }
}
