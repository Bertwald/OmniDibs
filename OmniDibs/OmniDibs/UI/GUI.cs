using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.UI {
    internal class GUI {
        private static string[] _Logo = {
                " #######  #     #  #     #  ###         ######   ###  ######    #####  ",
                " #     #  ##   ##  ##    #   #          #     #   #   #     #  #     # ",
                " #     #  # # # #  # #   #   #          #     #   #   #     #  #       ",
                " #     #  #  #  #  #  #  #   #   #####  #     #   #   ######    #####  ",
                " #     #  #     #  #   # #   #          #     #   #   #     #        # ",
                " #     #  #     #  #    ##   #          #     #   #   #     #  #     # ",
                " #######  #     #  #     #  ###         ######   ###  ######    #####  "   
        };
        internal static void printLogo(int positionX, int positionY) {
            Console.SetCursorPosition(positionX, positionY);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor= ConsoleColor.Yellow;
            for(int line = 0; line < _Logo.Length; line++) {
                Console.SetCursorPosition(positionX, positionY+line);
                Console.Write(_Logo[line]);
            }
            Console.ResetColor();

        }

        internal static void printWindow(string title, int positionX, int positionY, int width, int height, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine('╔' + new string('═', (width - title.Length + 2) / 2) + title + new string('═', width-title.Length + 2 - (width - title.Length + 2) / 2) + '╗');
            for (int offset = 1; offset < height; offset++) {
                Console.SetCursorPosition(positionX, positionY + offset);
                Console.WriteLine("║" + new string(' ', width + 2) + "║");
            }
            Console.SetCursorPosition(positionX, positionY + height);
            Console.WriteLine('╚' + new string('═', width + 2) + '╝');
            Console.ResetColor();
        }
        internal static void PrintTextCentered(string text, int centerX, int centerY, bool isHidden = false) {
            Console.SetCursorPosition((centerX + 1 - text.Length / 2) - 1, centerY);
            Console.Write($"{(isHidden ? new string('*', text.Length-1) + text.Last() : text)}");

        }
    }
}
