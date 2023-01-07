using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal class IndexMenu {
        private string title;
        private string[] actions;
        private int highlight = 0;
        private int maxlength;
        private int length;
        private ConsoleColor highlightColor;
        private ConsoleColor defaultColor = ConsoleColor.DarkGray;
        public IndexMenu(string title, List<string> actions, int highlight = 0, ConsoleColor highlightColor = ConsoleColor.Gray) {
            this.title = title;
            this.actions = actions.ToArray();
            this.highlight = highlight;
            this.highlightColor = highlightColor;
            maxlength = Math.Max(actions.Max(s => s.Length), title.Length);
            length = maxlength + 8; //Junkspaces, box and comma
        }
        internal void ShowNumberedActionsMenu() {
            Console.SetCursorPosition(0, 0);
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
        internal int RunMenu() {
            int index = -1;
            while (index < 0) {
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                ShowNumberedActionsMenu();
                index = PerformAction(Console.ReadKey(true).Key);
            }
            return index;
        }
        internal int PerformAction(ConsoleKey key) => key switch {
            ConsoleKey.UpArrow => ChangeActiveChoice(-1),
            ConsoleKey.DownArrow => ChangeActiveChoice(1),
            ConsoleKey.Enter => highlight,
            >= ConsoleKey.D0 and <= ConsoleKey.D9 => (int)key - 48,
            _ => -1
        };
        private int ChangeActiveChoice(int step) {
            highlight = (highlight + step + actions.Length) % actions.Length;
            return -1;
        }

    }
}
