using OmniDibs.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal class ItemSelectorMenu<T> where T : class {
        private List<T> _items = new();
        //private IndexMenu indexMenu;

        internal void SetItems(ImmutableList<T> items) {
            _items = items.ToList();
        }
        internal T? SelectItemFromMenu(ImmutableList<T> items)  {
            _items = items.ToList();

            return _items[0];
        }
        internal T? SelectItemFromMenu() {
            if (!_items.Any()) {
                _items = DatabaseInterface.GetListOf<T>();
            }
            if (!_items.Any()) {
                return null;
            }

            return _items[0];
        }
        internal static T? SelectDatabaseItemFromMenu() {
            var items = DatabaseInterface.GetImmutableListOf<T>();
            if(items == null || !items.Any()) {
                return null;
            }
            IndexMenu indexMenu = new IndexMenu(typeof(T).Name, items.Select(x => x.ToString()).ToList()!, 2);
            int index = indexMenu.RunMenu();
            return items[index];
        }
    }
}
