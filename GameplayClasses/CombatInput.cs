using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Threading;


namespace LITOURGIYA___OBLATION
{
    internal class CombatInput
    {
        public bool PrecisionBar(int barlength, int refreshspeed)
        {

            ConsoleKeyInfo KeyInput;
            ConsoleKey Keypressed;
            int CursorIndex = 1;
            char CursorSymbol = '-';
            char[] bar = new char[5 + barlength * 2];
            while (true)
            {
                for (int i = 0; i < (5 + (barlength * 2) - 1); i++)
                {
                    bar[i] = ' ';
                }
                if (CursorIndex == bar.Length - 1)
                {
                    CursorIndex = 1;
                }
                if (CursorIndex == barlength + 1 || CursorIndex == barlength + 3)
                {
                    CursorIndex++;
                }
                bar[CursorIndex] = CursorSymbol;
                bar[0] = '{';
                bar[bar.Length - 1] = '}';
                bar[1 + barlength] = '[';
                bar[2 + barlength] = ' ';
                bar[3 + barlength] = ']';

                Console.Clear();

                bar[CursorIndex] = CursorSymbol;
                string output = new string(bar);
                Console.WriteLine(output);

                var stopwatch = Stopwatch.StartNew();
                while (stopwatch.ElapsedMilliseconds < refreshspeed)
                {
                    if (Console.KeyAvailable)
                    {
                        KeyInput = Console.ReadKey(true);
                        Keypressed = KeyInput.Key;
                        if (Keypressed == ConsoleKey.Enter)
                        {
                            if (CursorIndex == 2 + barlength)
                            {
                                return true;
                                break;
                            }
                            else
                            {
                                return false;
                                break;
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
                CursorIndex++;
            }
        }
    }
}