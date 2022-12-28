using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal class Menu {
        private string title;
        private string[] actions;
        private int highlight = 0;
        private int maxlength;
        private int length;
        private ConsoleColor highlightColor;
        private ConsoleColor defaultColor = ConsoleColor.DarkGray;

        public Menu(string title, List<string> actions, int highlight = 0, ConsoleColor highlightColor = ConsoleColor.Gray) {
            this.title = title;
            this.actions = actions.ToArray();
            this.highlight = highlight;
            this.highlightColor = highlightColor;
            maxlength = Math.Max(actions.Max(s => s.Length), title.Length);
            length = maxlength + 8; //Junkspaces, box and comma
        }

        internal void ShowNumberedActionsMenu() {
            Console.Write('╔' + new string('═', title.Length + 2) + '╗' + Environment.NewLine + '║' + $" {title} " + "║" + Environment.NewLine + '╠' + new string('═', title.Length + 2) + '╩' + new string('═', length - title.Length - 3) + '╗' + Environment.NewLine);
            for (int index = 0; index < actions.Length; index++) {
                Console.Write('║');
                Console.ForegroundColor = index == highlight? highlightColor : Console.ForegroundColor = defaultColor;
                Console.Write($" [{index}] : {actions[index]} " + new string(' ', maxlength - actions[index].Length));
                Console.ResetColor();
                Console.Write('║' + Environment.NewLine);
            }
            Console.WriteLine('╚' + new string('═', length) + '╝');
        }

    }
}
