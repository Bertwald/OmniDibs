using OmniDibs.Interfaces;
using OmniDibs.Models;
using OmniDibs.UI;

namespace OmniDibs.Menus
{
    internal abstract class DefaultMenu<E> : IMenu<E> where E : System.Enum {
        protected readonly string _title;
        private int _activeChoice;
        private string[] _completeMenu;
        private int _numberOfChoices;
        private int _maxLength;
        private int _length;
        private ConsoleColor _highlightColor = ConsoleColor.Gray;
        private ConsoleColor _defaultColor = ConsoleColor.DarkGray;
        protected DefaultMenu(string title) {
            _title = title;
            string[] choices = Enum.GetNames(typeof(E));
            choices = choices.Select(x => x.Replace('_', ' ')).ToArray();
            _maxLength = Math.Max(choices.Max(s => s.Length), title.Length);
            _length = _maxLength + 8;
            _numberOfChoices = choices.Length;
            List<string> buildingMenu = new();
            buildingMenu.Add(('╔' + new string('═', title.Length + 2) + '╗' + Environment.NewLine + 
                              '║' +             $" {title} " +          "║" + Environment.NewLine + 
                              '╠' + new string('═', title.Length + 2) + '╩' + new string('═', _length - title.Length - 3) + '╗') + Environment.NewLine);
            for (int index = 0; index < choices.Length; index++) {
                buildingMenu.Add($"║ [{index}] : {choices[index]} " + new string(' ', _maxLength - choices[index].Length) + '║' + Environment.NewLine);
            }
            buildingMenu.Add('╚' + new string('═', _length) + '╝');
            _completeMenu = buildingMenu.ToArray();
        }

        public virtual void DisplayMenu() {
            for (int i = 0; i < _completeMenu.Length; i++) {
                if (i < _completeMenu.Length - _numberOfChoices - 1 || i == _completeMenu.Length - 1) {
                    Console.Write(_completeMenu[i]);
                } else {
                    Console.ResetColor();
                    Console.Write(_completeMenu[i].First());
                    Console.ForegroundColor = i == _activeChoice + 1 ? _highlightColor : _defaultColor;
                    Console.Write(_completeMenu[i][1..(_completeMenu[i].Length - 3)]);
                    Console.ResetColor();
                    Console.Write(_completeMenu[i][(_completeMenu[i].Length - 3)..(_completeMenu[i].Length)]);
                }
            }
        }
        public virtual ReturnType RunMenu() {
            ReturnType @return = ReturnType.CONTINUE;
            while (@return == ReturnType.CONTINUE) {
                Console.SetCursorPosition(0, 0);
                GUI.ClearWindow();
                DisplayMenu();
                @return = PerformAction(Console.ReadKey(true).Key);
            }
            return @return == ReturnType.HARDRETURN ? ReturnType.HARDRETURN : ReturnType.CONTINUE;
        }
        protected virtual ReturnType PerformAction(ConsoleKey key) => key switch {
            ConsoleKey.UpArrow => ChangeActiveChoice(-1),
            ConsoleKey.DownArrow => ChangeActiveChoice(1),
            ConsoleKey.Escape => ReturnType.HARDRETURN,
            ConsoleKey.Backspace => ReturnType.SOFTRETURN,
            ConsoleKey.Enter => ExecuteMappedAction(GetE(_activeChoice)),
            >= ConsoleKey.D0 and <= ConsoleKey.D9 => ExecuteMappedAction(GetE((int)key - 48)),
            _ => ReturnType.CONTINUE
        };
        protected virtual ReturnType ChangeActiveChoice(int step) {
            _activeChoice = (_activeChoice + step + _numberOfChoices) % _numberOfChoices;
            return ReturnType.CONTINUE;
        }
        protected abstract ReturnType ExecuteMappedAction(E e);
        protected abstract E GetE(int i);
    }
}
