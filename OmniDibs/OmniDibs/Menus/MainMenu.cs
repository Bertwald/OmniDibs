using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using OmniDibs.Interfaces;
using OmniDibs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace OmniDibs.Menus
{
    public class MainMenu : IMenu<Models.Privileges> {
        private readonly string title = "Main Menu";
        private int activeChoice;
        private string[] completeMenu;
        private int numberOfChoices;
        private int maxLength;
        private int length;
        private ConsoleColor highlightColor = ConsoleColor.Gray;
        private ConsoleColor defaultColor = ConsoleColor.DarkGray;

        public MainMenu() {
            string[] choices = System.Enum.GetNames(typeof(Models.Privileges));
            maxLength = Math.Max(choices.Max(s => s.Length), title.Length);
            length = maxLength + 8;
            numberOfChoices = choices.Length;
            List<string> buildingMenu = new();
            buildingMenu.Add(('╔' + new string('═', title.Length + 2) + '╗' + Environment.NewLine + '║' + $" {title} " + "║" + Environment.NewLine + '╠' + new string('═', title.Length + 2) + '╩' + new string('═', length - title.Length - 3) + '╗') + Environment.NewLine);
            for (int index = 0; index < choices.Length; index++) {
                buildingMenu.Add($"║ [{index}] : {choices[index]} " + new string(' ', maxLength - choices[index].Length) + '║' + Environment.NewLine);
            }
            buildingMenu.Add('╚' + new string('═', length) + '╝');
            completeMenu = buildingMenu.ToArray();
        }

        public void DisplayMenu() {
            for (int i = 0; i < completeMenu.Length; i++) {
                if (i < completeMenu.Length - numberOfChoices - 1 || i == completeMenu.Length - 1) {
                    Console.Write(completeMenu[i]);
                } else {
                    Console.ResetColor();
                    Console.Write(completeMenu[i].First());
                    Console.ForegroundColor = i == activeChoice + 1 ? highlightColor : Console.ForegroundColor = defaultColor;
                    Console.Write(completeMenu[i][1..(completeMenu[i].Length - 3)]);
                    Console.ResetColor();
                    Console.Write(completeMenu[i][(completeMenu[i].Length - 3)..(completeMenu[i].Length)]);
                }

            }
        }

        public ReturnType RunMenu() {
            ReturnType @return;
            while (true) {
                DisplayMenu();
                @return = PerformAction(Console.ReadKey(true).Key);


            }
        }
        private ReturnType PerformAction(ConsoleKey key) => key switch {
            ConsoleKey.UpArrow => ChangeActiveChoice(-1),
            ConsoleKey.DownArrow => ChangeActiveChoice(1),
            //ConsoleKey.Enter => PerformAction(Enum.TryParse<ConsoleKey>(actions[activeChoice][0].ToString(), true, out ConsoleKey highlightedAction) ? highlightedAction : ConsoleKey.Q),
            _ => ReturnType.CONTINUE
        };
        private ReturnType ChangeActiveChoice(int step) {
            activeChoice = (activeChoice + step + numberOfChoices) % numberOfChoices;
            return ReturnType.CONTINUE;
        }
    }
}
*/


//private static void ShowNumberedActionsMenu(string title, List<string> actions, int highlight = -1, ConsoleColor highlightColor = ConsoleColor.Gray) {
//    var maxlength = Math.Max(actions.Max(s => s.Length), title.Length);
//    var length = maxlength + 8; //Junkspaces, box and comma
//    Console.Write('╔' + new string('═', title.Length + 2) + '╗' + Environment.NewLine + '║' + $" {title} " + "║" + Environment.NewLine + '╠' + new string('═', title.Length + 2) + '╩' + new string('═', length - title.Length - 3) + '╗' + Environment.NewLine);
//    for (int index = 0; index < actions.Count; index++) {
//        Console.Write('║');
//        if (index == highlight) Console.ForegroundColor = highlightColor; else Console.ForegroundColor = ConsoleColor.DarkGray;
//        Console.Write($" [{index}] : {actions[index]} " + new string(' ', maxlength - actions[index].Length));
//        Console.ResetColor();
//        Console.Write('║' + Environment.NewLine);
//    }
//    Console.WriteLine('╚' + new string('═', length) + '╝');
//}
