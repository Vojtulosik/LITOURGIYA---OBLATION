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
        public bool PrecisionBar(int barlength, int refreshspeed, int targetsize)
        {

            ConsoleKeyInfo KeyInput;
            ConsoleKey Keypressed;
            int CursorIndex = 1;
            char CursorSymbol = '-';
            char[] bar = new char[4 + targetsize + barlength * 2];
            while (true)
            {
                for (int i = 0; i < (5 + targetsize + (barlength * 2) - 1); i++)
                {
                    bar[i] = ' ';
                }
                if (CursorIndex == bar.Length - 1)
                {
                    CursorIndex = 1;
                }
                if (CursorIndex == barlength + 1 || CursorIndex == barlength + targetsize + 2)
                {
                    CursorIndex++;
                }
                bar[CursorIndex] = CursorSymbol;
                bar[0] = '{';
                bar[bar.Length - 1] = '}';
                bar[1 + barlength] = '[';
                for (int i = 0; i < targetsize; i++)
                {
                    bar[barlength + 2 + i] = ' ';
                }
                bar[2 + targetsize + barlength] = ']';

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
                            if (CursorIndex > 1 + barlength && CursorIndex < barlength + targetsize + 2)
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