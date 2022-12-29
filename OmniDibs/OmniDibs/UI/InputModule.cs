﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OmniDibs.UI {
    internal class InputModule {
        internal static int GetValidatedInt(IEnumerable<int> validationList) {
            while (true) {
                int validint = GetInt();
                if (validationList.Contains(validint)) {
                    return validint;
                }
            }
        }

        internal static string GetString() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null) {
                    return read;
                }
            }
        }
        internal static int GetInt() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null && int.TryParse(read, out int number)) {
                    return number;
                }
            }
        }
        internal static bool GetBool() {
            while (true) {
                string? read = Console.ReadLine();
                if (read is not null && bool.TryParse(read, out bool value)) {
                    return value;
                }
            }
        }
        internal static int GetIntInRange(int lower, int upper) {
            int number = int.MaxValue;
            while (number < lower || number > upper) {
                number = GetInt();
            }
            return number;
        }
        internal static string GetCharacterMatching(string pattern = @"[A-Z0-9-_]") {
            while (true) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (Regex.IsMatch(key.KeyChar.ToString().ToUpperInvariant(), pattern) || key.Key == ConsoleKey.Enter) {
                    return key.Key == ConsoleKey.Enter ? Environment.NewLine : key.KeyChar.ToString();
                } else if (key.Key == ConsoleKey.Backspace) {
                    return key.KeyChar.ToString();
                }
            }
        }
        
        //internal static Command GetCommand() {
        //    ConsoleKey key = Console.ReadKey(true).Key;
        //    return KeyToCommand(key);
        //}
        //internal static Command KeyToCommand(ConsoleKey key) => key switch {
        //    ConsoleKey.P => Command.Park,
        //    ConsoleKey.U => Command.Unpark,
        //    ConsoleKey.Q => Command.Quit,
        //    ConsoleKey.A => Command.ToggleInput,
        //    _ => Command.DoNothing,
        //};

    }
}
