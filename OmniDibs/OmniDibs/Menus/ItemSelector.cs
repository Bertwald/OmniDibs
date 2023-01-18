using OmniDibs.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal class ItemSelector<T> where T : class {
        internal static T? SelectDatabaseItemFromMenu() {
            var items = DatabaseInterface.GetImmutableListOf<T>();
            if(items == null || !items.Any()) {
                return null;
            }
            IndexMenu indexMenu = new (typeof(T).Name,
                                       items.Select(x => x.ToString()).ToList()!,
                                       0);
            int index = indexMenu.RunMenu();
            return items[index];
        }

        internal static T? SelectItemFromList(List<T> items) {
            if (items == null || !items.Any()) {
                return null;
            }
            IndexMenu indexMenu = new(typeof(T).Name,
                                       items.Select(x => x.ToString()).ToList()!,
                                       0);
            int index = indexMenu.RunMenu();
            return items[index];
        }

    }
}
