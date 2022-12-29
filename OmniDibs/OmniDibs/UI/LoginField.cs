﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniDibs.UI {
    internal class LoginField {
        private int _positionX, _positionY;
        private string _title;
        private int _length;
        private ConsoleColor _bordercolor;
        private bool _isHidden;

        internal LoginField(string title, int x = 0, int y=0, int length = 12, ConsoleColor bordercolor = ConsoleColor.Blue, bool isHidden = false) {
            _length = Math.Max(length, title.Length); ;
            _bordercolor = bordercolor;
            _isHidden = isHidden;
            _positionX = x;
            _positionY = y;
            _title = title;
        }
        internal void PrintField() {
            Console.ForegroundColor = _bordercolor;
            Console.SetCursorPosition(_positionX, _positionY);
            Console.WriteLine('┌' + new string('─', (_length - _title.Length + 2)/2) + _title + new string('─', (_length - _title.Length + 2) / 2) + '┐');
            for (int offset = 1; offset < 4; offset++) {
                Console.SetCursorPosition(_positionX, _positionY+offset);
                Console.WriteLine("│" + new string(' ', _length + 2) + "│");
            }
            Console.SetCursorPosition(_positionX, _positionY+4);
            Console.WriteLine('└' + new string('─', _length +2) + '┘');
            Console.ResetColor();
        }
        internal string GetContinousInput() {
            Console.SetCursorPosition(_positionX + 2 + (_length / 2), _positionY + 2);
            string input = "";
            string newRead;
            do {
                newRead = InputModule.GetCharacterMatching();
                if (newRead != Environment.NewLine) {
                    if (newRead.Equals("\b") && input.Length > 0) {
                        input = input[0..(input.Length - 1)];
                    } else {
                        input += newRead;
                    }
                    ClearField();
                    GUI.PrintTextCentered(input, _positionX + 2 + (_length / 2), _positionY + 2, _isHidden);
                }
            } while (newRead != Environment.NewLine && input.Length < _length);
            return input;
        }

        private void ClearField() {
            Console.SetCursorPosition(_positionX+2,_positionY+2);
            Console.Write(new string(' ', _length));
        }


    }
}