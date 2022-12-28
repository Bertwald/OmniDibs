using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    public interface IMenu<E> where E : System.Enum {
        public void DisplayMenu();
    }
}
