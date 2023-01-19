
namespace OmniDibs.UI {
    internal class GUI {
        private static string[] _logo = {
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int line = 0; line < _logo.Length; line++) {
                Console.SetCursorPosition(positionX, positionY + line);
                Console.Write(_logo[line]);
            }
            Console.ResetColor();

        }

        internal static void printWindow(string title, int positionX, int positionY, int width, int height, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine('╔' + new string('═', (width - title.Length + 2) / 2) + title + new string('═', width - title.Length + 2 - (width - title.Length + 2) / 2) + '╗');
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
            Console.Write($"{(isHidden ? new string('*', text.Length - 1) + text.Last() : text)}");

        }

        internal static void ClearWindow() {
            Console.Clear();
        }

        internal static void Delay() {
            Console.WriteLine("Press ANY key to Continue");
            Console.ReadKey();
        }

        internal static void PrintPropertyPair(string name, string? v) {
            Console.WriteLine($"{name} \t : \t {v}");
        }

        internal static void PrintList(List<string> list, string title) {
            int length = Math.Max(title.Length, list.Select(x => x.Length).Max()) + 2;
            int leftFillerLength = (length - title.Length) / 2;
            var position = Console.GetCursorPosition();
            Console.Write(new string('-', leftFillerLength) + title + new string('-', length - title.Length - leftFillerLength));
            for (int row = 0; row < list.Count; row++) {
                Console.SetCursorPosition(position.Left, position.Top + row + 1);
                Console.Write(list[row]);
            }
            Console.SetCursorPosition(position.Left, Console.GetCursorPosition().Top + 2);
            Console.Write(new string('-', length));
            Console.SetCursorPosition(position.Left, Console.GetCursorPosition().Top + 1);
        }

        internal static void PrintBookingDays(string startMonth, int daysInMonth, List<int> availableDays) {
            Console.WriteLine("Bookings for " + startMonth);
            for (int day = 1; day <= daysInMonth; day++) {
                if (availableDays.Contains(day)) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{day} : Available    ");
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{day} : Not Available");
                }
                if (day % 5 == 0) {
                    Console.WriteLine();
                } else {
                    Console.Write("\t");
                }
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
