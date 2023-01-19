using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Interfaces
{
    internal interface IMenu<E> where E : Enum
    {
        internal void DisplayMenu();
        internal ReturnType RunMenu();
    }
}
