
namespace OmniDibs.Menus {
    internal class IndexMenu {
        private readonly string _title;
        private readonly string[] _actions;
        private int _highlightedIndex;
        private readonly int _maxStringLength;
        private readonly int _boxLength;
        private readonly int _extraLength;
        private readonly ConsoleColor _highlightColor;
        private readonly ConsoleColor _defaultColor = ConsoleColor.DarkGray;
        public IndexMenu(string title, List<string> actions, int highlight = 0, ConsoleColor highlightColor = ConsoleColor.Gray) {
            _title = title;
            _actions = actions.ToArray();
            _highlightedIndex = highlight;
            _highlightColor = highlightColor;
            _maxStringLength = Math.Max(actions.Max(s => s.Length), title.Length);
            _extraLength = ((int)Math.Log10(actions.Count())); //For very large Lists
            _boxLength = _maxStringLength + 8 + _extraLength; //Junkspaces, box and comma
        }
        internal void ShowNumberedActionsMenu() {
            Console.SetCursorPosition(0, 0);
            Console.Write('╔' + new string('═', _title.Length + 2) + '╗' + Environment.NewLine + 
                          '║' + $" {_title} " +          "║" + Environment.NewLine + 
                          '╠' + new string('═', _title.Length + 2) + '╩' + new string('═', _boxLength - _title.Length - 3) + '╗' + Environment.NewLine);
            for (int index = 0; index < _actions.Length; index++) {
                Console.Write('║');
                Console.ForegroundColor = index == _highlightedIndex? _highlightColor : Console.ForegroundColor = _defaultColor;
                Console.Write($" [{index}] : {_actions[index]} " + new string(' ', _boxLength - 7 - index.ToString().Length - _actions[index].Length));
                Console.ResetColor();
                Console.Write('║' + Environment.NewLine);
            }
            Console.WriteLine('╚' + new string('═', _boxLength) + '╝');
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
            ConsoleKey.Enter => _highlightedIndex,
            >= ConsoleKey.D0 and <= ConsoleKey.D9 => (int)key - 48,
            _ => -1
        };
        private int ChangeActiveChoice(int step) {
            _highlightedIndex = (_highlightedIndex + step + _actions.Length) % _actions.Length;
            return -1;
        }

    }
}
