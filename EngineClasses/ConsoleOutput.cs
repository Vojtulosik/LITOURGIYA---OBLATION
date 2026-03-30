using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Tracing;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LITOURGIYA___OBLATION
{
    internal class ConsoleOutput
    {
        public ConsoleColor[] availabletextcolors =
        {
            ConsoleColor.Red, //0
            ConsoleColor.Yellow, //1
            ConsoleColor.Green, //2
            ConsoleColor.Blue, //3
            ConsoleColor.Magenta, //4
            ConsoleColor.Cyan, //5
            ConsoleColor.White, //6
            ConsoleColor.Gray, //7
            ConsoleColor.DarkGray, //8
            ConsoleColor.Black, //9
            ConsoleColor.DarkRed, //10
            ConsoleColor.DarkYellow, //11
            ConsoleColor.DarkGreen, //12
            ConsoleColor.DarkBlue, //13
            ConsoleColor.DarkMagenta, //14
            ConsoleColor.DarkCyan, //15
        };
        public string[] availabletextcolorsname =
        {
            "red", "yellow", "green", "blue", "magenta", "cyan", "white", "gray", "darkgray", "black", "darkred", "darkyellow", "darkgreen", "darkblue", "darkmagenta", "darkcyan"
        };
        private string[] Options;
        private string[] Prompts;
        private int[] Textcolors;
        private int[] TextDelay;
        private bool[] TextSound;
        private string[] Symbols =
        {
                "<<  ", "  >>", "(  ", "  )", "  ", "[", "]", "", " (!)]"
                //0    1       2      3      4     5    6    7   8
        };
        private int[] SpecialSymbol;
        private int[] OptionSpecialColor;
        private string[] OptionsText;
        public ConsoleColor TextHighlightColor = ConsoleColor.White;
        public ConsoleColor TextColor = ConsoleColor.Black;
        private string UserInput = "";
        public int OptionIndexPlacement = 0;

        public ConsoleOutput(string[] options, string[] prompts, int[] textcolors, int[] delay, bool[] sound, int textcolor, int highlightcolor, int[] specialsymbol, int[] optiontextcolor, string[] optiontext)
        {
            Options = options;
            Prompts = prompts;
            Textcolors = textcolors;
            TextDelay = delay;
            TextSound = sound;
            TextHighlightColor = availabletextcolors[highlightcolor];
            TextColor = availabletextcolors[textcolor];
            SpecialSymbol = specialsymbol;
            OptionSpecialColor = optiontextcolor;
            OptionsText = optiontext;
            
        }
        FileManagement FileManager = new FileManagement();
        SoundHub soundhub = new SoundHub();
        char IndexSymbol;
        public void UpdateValues(string[] prompts, int[] colors)
        {
            Prompts = prompts;
            Textcolors = colors;
        }
        public void UpdateValues(string[] prompts, int[] colors, string[] options, int[] specialsymbol)
        {
            Prompts = prompts;
            Textcolors = colors;
            Options = options;
            SpecialSymbol = specialsymbol;
        }
        public void UpdateValues(string[] prompts, int[] colors, string[] options, int[] specialsymbol, int[] optionscolors)
        {
            Prompts = prompts;
            Textcolors = colors;
            Options = options;
            SpecialSymbol = specialsymbol;
            OptionSpecialColor = optionscolors;
        }
        public void RenderText(string[] prompt, int[] colors)
        {
            Console.Clear();
            for (int u = 0; u < prompt.Length; u++)
            {
                Console.ForegroundColor = availabletextcolors[colors[u]];
                Console.Write(prompt[u]);
            }
        }
        public void RenderText(string[] prompt, int[] colors, int individualsymboldelay)
        {
            for (int u = 0; u < prompt.Length; u++)
            {
                Console.ForegroundColor = availabletextcolors[colors[u]];
                string currentword = prompt[u];
                for (int i = 0; i < prompt[u].Length; i++)
                {
                    Console.Write(currentword[i]);
                    Thread.Sleep(individualsymboldelay);
                }
            }
        }
        public void RenderText(string[] prompt, int[] colors, int[] delay, bool[] sound)
        {
            Console.Clear();
            for (int u = 0; u < prompt.Length; u++)
            {
                Console.ForegroundColor = availabletextcolors[colors[u]];
                if (sound[u] == true)
                {
                    soundhub.PlaySound();
                }
                Console.Write(prompt[u]);
                Thread.Sleep(delay[u]);
            }
        }
        public void RenderOptions(string[] options, int[] SpecialSymbol)
        {
            int SpecialSymbolIndex = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (OptionIndexPlacement == i)
                {
                    Console.BackgroundColor = TextHighlightColor;
                    Console.ForegroundColor = TextColor;
                    IndexSymbol = '*';
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    IndexSymbol = ' ';
                }
                if (options[i] != "")
                {
                    if (SpecialSymbol[SpecialSymbolIndex] != 7)
                    {
                        Console.WriteLine($"{IndexSymbol} {Symbols[SpecialSymbol[SpecialSymbolIndex]]}{options[i]}{Symbols[SpecialSymbol[SpecialSymbolIndex + 1]]}");
                    }
                    else
                    {
                        Console.WriteLine($"{options[i]}");
                    }
                }
                else
                {
                    Console.WriteLine("");
                }
                Console.ResetColor();
                SpecialSymbolIndex += 2;
            }
        }
        public void RenderOptions(string[] options, int delay, int onebyonedelay)
        {
            Thread.Sleep(delay);
            for (int i = 0; i < options.Length; i++)
            {
                Thread.Sleep(onebyonedelay);
                if (OptionIndexPlacement == i)
                {
                    Console.BackgroundColor = TextHighlightColor;
                    Console.ForegroundColor = TextColor;
                    IndexSymbol = '*';
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    IndexSymbol = ' ';
                }
                if (options[i] != "")
                {
                        Console.WriteLine($"{IndexSymbol} <<  {options[i]}  >>");
                }
                else
                {
                    Console.WriteLine("");
                }
                Console.ResetColor();
            }
        }
        public void RenderOptions(string[] options, int[] SpecialSymbol, int[] optioncolors)
        {
            int SpecialSymbolIndex = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (OptionIndexPlacement == i)
                {
                    Console.BackgroundColor = TextHighlightColor;
                    Console.ForegroundColor = TextColor;
                    IndexSymbol = '*';
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = availabletextcolors[optioncolors[i]];
                    IndexSymbol = ' ';
                }
                if (options[i] != "")
                {
                    if (SpecialSymbol[SpecialSymbolIndex] != 7)
                    {
                        Console.WriteLine($"{IndexSymbol} {Symbols[SpecialSymbol[SpecialSymbolIndex]]}{options[i]}{Symbols[SpecialSymbol[SpecialSymbolIndex + 1]]}");
                    }
                    else
                    {
                        Console.WriteLine($"{options[i]}");
                    }
                }
                else
                {
                    Console.WriteLine("");
                }
                Console.ResetColor();
                SpecialSymbolIndex += 2;
            }
        }
        public void RenderAnimatedText(string[] prompt, int[] colors, int SelectAnimation)
        {
            Console.Clear();
            for (int i = 0; i < prompt.Length; i++)
            {
                Console.ForegroundColor = availabletextcolors[colors[i]];
                Console.Write(prompt[i]);
            }
            switch (SelectAnimation)
            {
                case 1: 
                    Console.ResetColor();
                    string[] AnimationSymbols =
                    {
                            "\\", "|", "/", "-"
                    };
                    Console.Write("   ");
                    for (int u = 0; u < 5; u++)
                    {
                        for (int i = 0; i < AnimationSymbols.Length; i++)
                        {
                            Thread.Sleep(120);
                            Console.Write(AnimationSymbols[i]);
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }
                    break;
            }
        }
        public string RenderInputOption()
        {
            Console.BackgroundColor = availabletextcolors[9];
            Console.ForegroundColor = availabletextcolors[5];
            Console.CursorVisible = true;
            string input = "";
            input = Console.ReadLine();
            Console.ResetColor();
            Console.CursorVisible = false;
            return input;
        }
        public void RenderNote(string[] prompt, bool delay)
        {
            string text = string.Join(null, prompt);
            string[] words = text.Split(' ');
            int WindowWidth = Console.WindowWidth - 1;
            int CurrentLineLength = 0;
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (delay == true)
                {
                    Thread.Sleep(70);
                }
                string currentword = words[i];
                if (currentword[0] == '<')
                {
                    int start = currentword.IndexOf('<');
                    int end = currentword.IndexOf('>');
                    string color = currentword.Substring(start + 1, end - 1);
                    Console.ForegroundColor = availabletextcolors[availabletextcolorsname.IndexOf(color)];
                    currentword = currentword.Substring(end + 1);
                }
                if (currentword.Length + CurrentLineLength <= WindowWidth)
                {
                    if (currentword == "#")
                    {
                        Console.Write("\n");
                        CurrentLineLength = 0;
                    }
                    else
                    {
                        Console.Write(currentword + ' ');
                        CurrentLineLength = CurrentLineLength + currentword.Length + 1;
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.Write("\n");
                    CurrentLineLength = 0;
                    if (currentword == "#")
                    {
                        Console.Write("\n");
                        CurrentLineLength = 0;
                    }
                    else
                    {
                        Console.Write(currentword + ' ');
                        CurrentLineLength = CurrentLineLength + currentword.Length + 1;
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            //# Signals for another line in the .txt files
        }
        public int Run()
        {
            ConsoleKey KeyPressed;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                KeyPressed = keyInfo.Key;
                if (Options.Length > 1)
                {
                    if (KeyPressed == ConsoleKey.UpArrow)
                    {
                        if (Options.Length > 1)
                        {
                            OptionIndexPlacement--;
                            if (OptionIndexPlacement == -1)
                            {
                                OptionIndexPlacement = Options.Length - 1;
                            }
                        }
                        if (SpecialSymbol[OptionIndexPlacement * 2] == 7)
                        {
                            OptionIndexPlacement--;
                            if (OptionIndexPlacement == -1)
                            {
                                OptionIndexPlacement = Options.Length - 1;
                            }
                        }
                        if (Options[OptionIndexPlacement] == "")
                        {
                            OptionIndexPlacement--;
                        }
                        if (OptionIndexPlacement == -1)
                        {
                            OptionIndexPlacement = Options.Length - 1;
                        }
                    }
                    else if (KeyPressed == ConsoleKey.DownArrow)
                    {
                        if (Options.Length > 1)
                        {
                            OptionIndexPlacement++;
                            if (OptionIndexPlacement == Options.Length)
                            {
                                if (SpecialSymbol[0] == 7)
                                {
                                    OptionIndexPlacement = 1;
                                }
                                else
                                {
                                    OptionIndexPlacement = 0;
                                }
                            }
                        }
                        if (Options[OptionIndexPlacement] == "")
                        {
                            OptionIndexPlacement++;
                        }
                        if (SpecialSymbol[OptionIndexPlacement * 2] == 7)
                        {
                            OptionIndexPlacement++;
                        }
                        if (OptionIndexPlacement == Options.Length)
                        {
                            if (SpecialSymbol[0] == 7)
                            {
                                OptionIndexPlacement = 1;
                            }
                            else
                            {
                                OptionIndexPlacement = 0;
                            }
                        }
                    }

                    if (KeyPressed == ConsoleKey.UpArrow || KeyPressed == ConsoleKey.DownArrow)
                    {
                        Console.Clear();
                        if (Prompts != null)
                        {
                            RenderText(Prompts, Textcolors);
                        }
                        if (OptionSpecialColor != null)
                        {
                            RenderOptions(Options, SpecialSymbol, OptionSpecialColor);
                        }
                        else
                        {
                            RenderOptions(Options, SpecialSymbol);
                        }
                    }
                }
            } while (KeyPressed != ConsoleKey.Enter);
            return OptionIndexPlacement;
        }
    }
}
