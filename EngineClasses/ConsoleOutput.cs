using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.GameplayClasses;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
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
                "<<  ", "  >>", "(  ", "  )", "  ", "[", "]", "", " (!)]", "] - ", ",  ", "/", "  < [", ">", "  ["
                //0    1       2      3      4     5    6    7   8        9       10     11   12       13   14
        };
        private int[] SpecialSymbol;
        private int[] OptionSpecialColor;
        private string[] OptionsText;
        public ConsoleColor TextHighlightColor = ConsoleColor.White;
        public ConsoleColor TextColor = ConsoleColor.Black;
        private string UserInput = "";
        public int OptionIndexPlacement = 0;
        public char IndexSymbol = '*';
        public int OptionHorizontalIndexPlacement = 0;
        public int InventoryPage = 0;
        BodyStatus BodyStatus = new BodyStatus();
        private string[] HorizontalOptions;
        private int[] HorizontalOptionColors;
        private int[] HorizontalOptionSymbols;
        private int[] HorizontalLineStructure;
        public int[] SelectedInventoryFilters = new int[6];

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
        public void UpdateValues(string[] prompts, int[] colors, string[] options, int[] specialsymbol, int[] optionscolors, string[] horizontaloptions, int[] horizontaloptioncolors, int[] horizontaloptionsymbols, int[] horizontallinestructure)
        {
            Prompts = prompts;
            Textcolors = colors;
            Options = options;
            SpecialSymbol = specialsymbol;
            OptionSpecialColor = optionscolors;
            HorizontalOptions = horizontaloptions;
            HorizontalOptionColors = horizontaloptioncolors;
            HorizontalOptionSymbols = horizontaloptionsymbols;
            HorizontalLineStructure = horizontallinestructure;
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
                    IndexSymbol = FileManager.LoadIndexChar();
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
                    IndexSymbol = FileManager.LoadIndexChar();
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
                    IndexSymbol = FileManager.LoadIndexChar();
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
        public void RenderInventoryFilters()
        {
            string[] HorizontalOptions = {

                "", "R", " - Resources", ",",
                "   ", "C", " - Consumables", ",",
                "   ", "T", " - Tools", ",",
                "   ", "M", " - Medical", ",",
                "   ", "E", " - Expendables", ",",
                "   ", "G", " - Gear", " >"

            };
            int[] HorizontalOptionColors = {
                7, 1, 7, 7,
                7, 2, 7, 7,
                7, 4, 7, 7,
                7, 0, 7, 7,
                7, 3, 7, 7,
                7, 5, 7, 7
            };
            Console.Write("   < ");
            int SelectedOption = OptionHorizontalIndexPlacement * 4 + 1;
            if (SelectedOption == 0) SelectedOption++;
            bool HighlightOption = false;
            int counter = 0;
            int OptionRenderIndex = 0;
            int OptionRenderCount = 0;
            for (int i = 0; i < HorizontalOptions.Length; i++)
            {
                OptionRenderCount++;
                if ((SelectedOption == i || HighlightOption == true) && OptionIndexPlacement == 0)
                {
                    Console.BackgroundColor = TextHighlightColor;
                    Console.ForegroundColor = TextColor;
                    IndexSymbol = FileManager.LoadIndexChar();
                    HighlightOption = true;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = availabletextcolors[HorizontalOptionColors[i]];
                    IndexSymbol = ' ';
                }
                if (SelectedInventoryFilters[OptionRenderIndex] == 1 && OptionRenderCount != 1 && OptionRenderCount != 4)
                {
                    Console.BackgroundColor = TextHighlightColor;
                    Console.ForegroundColor = TextColor;
                }
                if (SelectedOption == i && OptionIndexPlacement == 0)
                {
                    Console.Write($"{IndexSymbol}{HorizontalOptions[i]}");
                }
                else
                {
                    Console.Write($"{HorizontalOptions[i]}");
                }
                if (HighlightOption == true) counter++;
                if (counter == 2) HighlightOption = false;
                if (OptionRenderCount == 4)
                {
                    OptionRenderIndex += 1;
                    OptionRenderCount = 0;
                }
            }
            Console.Write("\n");
        }
        public void RenderHorizontalOptions()
        {
            int LineStructureCount = 0;
            int LineStructureIndex = 1;
            int SpecialSymbolIndex = 0;
            Console.Write("  ");
            for (int i = 0; i < HorizontalOptions.Length; i++)
            {
                if (OptionHorizontalIndexPlacement == LineStructureCount && OptionIndexPlacement == LineStructureIndex)
                {
                    Console.BackgroundColor = TextHighlightColor;
                    Console.ForegroundColor = TextColor;
                    IndexSymbol = FileManager.LoadIndexChar();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = availabletextcolors[HorizontalOptionColors[i]];
                    IndexSymbol = ' ';
                }
                if (OptionHorizontalIndexPlacement == LineStructureCount && OptionIndexPlacement == LineStructureIndex)
                {
                    Console.Write($"{Symbols[HorizontalOptionSymbols[SpecialSymbolIndex]]}{IndexSymbol}{HorizontalOptions[i]}{Symbols[HorizontalOptionSymbols[SpecialSymbolIndex + 1]]}");
                }
                else
                {
                    Console.Write($"{Symbols[HorizontalOptionSymbols[SpecialSymbolIndex]]}{HorizontalOptions[i]}{Symbols[HorizontalOptionSymbols[SpecialSymbolIndex + 1]]}");
                }
                LineStructureCount++;
                if (HorizontalLineStructure[LineStructureIndex] == LineStructureCount)
                {
                    Console.Write("\n");
                    LineStructureIndex++;
                    LineStructureCount = 0;
                }
                SpecialSymbolIndex += 2;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = availabletextcolors[HorizontalOptionColors[i]];
                Console.Write("  ");
            }
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
            //# >> Signals for another line in the .txt files
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
        public int Run(bool ObjectExists, DataStructure Data)
        {
            ConsoleKey KeyPressed;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                KeyPressed = keyInfo.Key;
                if (KeyPressed == ConsoleKey.B)
                {
                    if (!ObjectExists)
                    {
                        BodyStatus.BodyCheckUI(this, Data);
                        return 9999;
                    }
                }
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
        public int Run(bool ObjectExists, out ConsoleKey KeyPressed, DataStructure Data)
        {
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                KeyPressed = keyInfo.Key;
                if (KeyPressed == ConsoleKey.B)
                {
                    if (!ObjectExists)
                    {
                        BodyStatus.BodyCheckUI(this, Data);
                        return 9999;
                    }
                }
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
            } while (KeyPressed != ConsoleKey.Enter && KeyPressed != ConsoleKey.Spacebar && KeyPressed != ConsoleKey.Backspace);
            return OptionIndexPlacement;
        }
        public int RunHorizontal(bool ObjectExists, DataStructure Data)
        {
            ConsoleKey KeyPressed;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                KeyPressed = keyInfo.Key;
                if (KeyPressed == ConsoleKey.B)
                {
                    if (!ObjectExists)
                    {
                        BodyStatus.BodyCheckUI(this, Data);
                        return 9999;
                    }
                }
                if (HorizontalLineStructure.Length > 1)
                {
                    if (KeyPressed == ConsoleKey.UpArrow)
                    {
                        if (OptionIndexPlacement == 0) OptionIndexPlacement = HorizontalLineStructure.Length - 1;
                        else OptionIndexPlacement--;
                        OptionHorizontalIndexPlacement = 0;
                    }
                    if (KeyPressed == ConsoleKey.DownArrow)
                    {
                        if (OptionIndexPlacement == HorizontalLineStructure.Length - 1) OptionIndexPlacement = 0;
                        else OptionIndexPlacement++;
                        OptionHorizontalIndexPlacement = 0;
                    }
                }
                if (HorizontalLineStructure[OptionIndexPlacement] > 1)
                {
                    if (KeyPressed == ConsoleKey.RightArrow)
                    {
                        if (OptionHorizontalIndexPlacement == HorizontalLineStructure[OptionIndexPlacement] - 1) OptionHorizontalIndexPlacement = 0;
                        else OptionHorizontalIndexPlacement++;
                    }
                    if (KeyPressed == ConsoleKey.LeftArrow)
                    {
                        if (OptionHorizontalIndexPlacement == 0) OptionHorizontalIndexPlacement = HorizontalLineStructure[OptionIndexPlacement] - 1;
                        else OptionHorizontalIndexPlacement--;
                    }
                }
                if (KeyPressed == ConsoleKey.UpArrow || KeyPressed == ConsoleKey.DownArrow || KeyPressed == ConsoleKey.RightArrow || KeyPressed == ConsoleKey.LeftArrow || KeyPressed == ConsoleKey.Enter)
                {
                    RenderText(Prompts, Textcolors);
                    RenderInventoryFilters();
                    RenderHorizontalOptions();
                }
            } while (KeyPressed != ConsoleKey.Enter);
            int store = OptionIndexPlacement;
            OptionIndexPlacement = 0;
            return store;
        }
    }
}
