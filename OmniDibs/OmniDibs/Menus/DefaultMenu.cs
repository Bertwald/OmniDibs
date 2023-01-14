using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.Menus {
    internal abstract class DefaultMenu<E> : IMenu<E> where E : System.Enum {
        protected readonly string _title;
        private int activeChoice;
        private string[] completeMenu;
        private int numberOfChoices;
        private int maxLength;
        private int length;
        private ConsoleColor highlightColor = ConsoleColor.Gray;
        private ConsoleColor defaultColor = ConsoleColor.DarkGray;
        protected DefaultMenu(string title) {
            _title = title;
            string[] choices = System.Enum.GetNames(typeof(E));
            choices = choices.Select(x => x.Replace('_', ' ')).ToArray();
            maxLength = Math.Max(choices.Max(s => s.Length), title.Length);
            length = maxLength + 8;
            numberOfChoices = choices.Length;
            List<string> buildingMenu = new();
            buildingMenu.Add(('╔' + new string('═', title.Length + 2) + '╗' + Environment.NewLine + '║' + $" {title} " + "║" + Environment.NewLine + 
                              '╠' + new string('═', title.Length + 2) + '╩' + new string('═', length - title.Length - 3) + '╗') + Environment.NewLine);
            for (int index = 0; index < choices.Length; index++) {
                buildingMenu.Add($"║ [{index}] : {choices[index]} " + new string(' ', maxLength - choices[index].Length) + '║' + Environment.NewLine);
            }
            buildingMenu.Add('╚' + new string('═', length) + '╝');
            completeMenu = buildingMenu.ToArray();
        }

        public virtual void DisplayMenu() {
            for (int i = 0; i < completeMenu.Length; i++) {
                if (i < completeMenu.Length - numberOfChoices - 1 || i == completeMenu.Length - 1) {
                    Console.Write(completeMenu[i]);
                } else {
                    Console.ResetColor();
                    Console.Write(completeMenu[i].First());
                    Console.ForegroundColor = i == activeChoice + 1 ? highlightColor : defaultColor;
                    Console.Write(completeMenu[i][1..(completeMenu[i].Length - 3)]);
                    Console.ResetColor();
                    Console.Write(completeMenu[i][(completeMenu[i].Length - 3)..(completeMenu[i].Length)]);
                }
            }
        }
        public virtual ReturnType RunMenu() {
            ReturnType @return;
            while (true) {
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                DisplayMenu();
                @return = PerformAction(Console.ReadKey(true).Key);
            }
        }
        protected virtual ReturnType PerformAction(ConsoleKey key) => key switch {
            ConsoleKey.UpArrow => ChangeActiveChoice(-1),
            ConsoleKey.DownArrow => ChangeActiveChoice(1),
            ConsoleKey.Escape => ReturnType.HARDRETURN,
            ConsoleKey.Backspace => ReturnType.SOFTRETURN,
            ConsoleKey.Enter => ExecuteMappedAction(GetE(activeChoice)),
            >= ConsoleKey.D0 and <= ConsoleKey.D9 => ExecuteMappedAction(GetE((int)key - 48)),
            _ => ReturnType.CONTINUE
        };
        protected virtual ReturnType ChangeActiveChoice(int step) {
            activeChoice = (activeChoice + step + numberOfChoices) % numberOfChoices;
            return ReturnType.CONTINUE;
        }
        protected abstract ReturnType ExecuteMappedAction(E e);
        protected abstract E GetE(int i);
    }
}
