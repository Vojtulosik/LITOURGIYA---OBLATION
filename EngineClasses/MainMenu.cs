using LITOURGIYA___OBLATION.EngineClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LITOURGIYA___OBLATION
{
    internal class MainMenu
    {
        FileManagement FileManager = new FileManagement();

        public int HighlightChosenColor = 6;
        public int TextChosenColor = 9;
        private bool withdelay = true;
        ConfigFileData ConfigData;
        public void StartupMenu()
        {
            ConfigData = FileManager.LoadConfigFile();
            while (true)
            {
                HighlightChosenColor = ConfigData.TextHighlightColor;
                TextChosenColor = ConfigData.TextColor;
                Console.CursorVisible = false;
                string[] options =
                {
                    "Awake", "Credits", "Options", "Close"
                };
                int[] specialsymbol =
                {
                    0, 1, 0, 1, 0, 1, 0, 1
                };
                string[] prompts =
                {
                @"
 ██▓     ██▓▄███████▓ ▒█████   █    ██  ██▀██▄    ▄███▄  ██░░██   ██▓ ▄▄▄░  
▓██▒    ▓██▒▓  ██▒ ▓▒▒██▒  ██▒ ██  ▓██▒▓██ ▒ ██▒ ██▒ ▀█▒▓██▒ ▒██  ██▒▒████▄░▒   
▒██░    ▒██▒▒ ▓██░ ▒░▒██░  ██▒▓██  ▒██░▓██ ░▄█ ▒▒██░▄▄▄░▒██▒  ▒██ ██░▒██▒ ▀██░ 
▒██░    ░██░░ ▓██▓ ░ ▒██   ██░▓██  ░██░▒██▀▀█▄  ░██▓ ██▓░██░  ░▓███░░███▄▄███░
░██████▒░██░  ▒██▒ ░ ░ ████▓▒░▒▓█████▓ ░██▓ ▒██▒░▓████▀▒░██░  ░ ██▒▓██▓▒  ▒███▒
░ ▒░▓  ░░▓    ▒ ░░   ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ░▒▒░ ▒ ░▓   ░ ██▒▒▒  ▒▒   ▓▒█░
░ ░ ▒  ░ ▒ ░    ░      ░ ▒ ▒░ ░░▒░ ░ ░   ░▒ ░ ▒░  ░░  ░  ▒ ░ ▓██ ░▒░   ▒   ▒▒ ░
  ░ ░    ▒ ░  ░      ░ ░ ░ ▒   ░░░ ░ ░   ░░   ░ ░ ░   ░  ▒ ░ ▒ ▒ ░░    ░   ▒   
    ░  ░ ░               ░ ░     ░        ░           ░  ░   ░ ░           ░  ░
                                                             ░                " + "\n",
                "                                                           OBLATION, Build : 0.1\n"
                };
                int[] textcolors =
                {
                    7, 8
                };
                int[] delay =
                {
                    0, 0
                };
                if (withdelay == true)
                {
                    delay[0] = 1500;
                    delay[1] = 500;
                }
                else
                {
                    delay[0] = 0;
                    delay[1] = 0;
                }
                bool[] sound =
                {
                    true, true
                };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, delay, sound, TextChosenColor, HighlightChosenColor, specialsymbol, null, null);
                if (withdelay == true)
                {
                    ConsoleOutput.RenderText(prompts, textcolors, delay, sound);
                    ConsoleOutput.RenderOptions(options, 1000, 200);
                }
                else
                {
                    ConsoleOutput.RenderText(prompts, textcolors);
                    ConsoleOutput.RenderOptions(options, specialsymbol);
                }
                int SelectedIndex = ConsoleOutput.Run();
                switch (SelectedIndex)
                {
                    case 0:
                        SoulFilesUI();
                        break;
                    case 1:
                        CreditsUI();
                        break;
                    case 2:
                        OptionsUI();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public void CreditsUI()
        {
            string[] prompts =
            {
                "Created by ",
                "VOJTULOSIK",
                ", aka ",
                "FUNI\n",
                "Made using : ",
                "Visual studio, C# NET 10.0\n",
                "\n"
            };
            int[] textcolors =
            {
                7, 14, 7, 14, 7, 6, 7
            };
            string[] options =
            {
                "Back"
            };
            int[] specialsymbol =
            {
                0, 1
            };
            ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, null, null);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderOptions(options, specialsymbol);
            int SelectedIndex = ConsoleOutput.Run();
            switch (SelectedIndex)
            {
                case 0:
                    withdelay = false;
                    break;
            }
        }
        public void SoulFilesUI()
        {
            string[] prompts =
            {
               "Searching for ", "Souls", "..."
            };

            int[] textcolors =
            {
                7, 5, 7
            };

            string[] options = { null };
            string[] soulfiles = FileManager.SearchFiles();
            string[] soulfileNames = FileManager.ReadFileNames(soulfiles);
            int[] specialsymbol = { };
            if (soulfiles.Length > 0)
            {
                options = soulfileNames.Concat(new[] { "", "Create Soul", "Kill Soul", "Back" }).ToArray();
                specialsymbol = new int[options.Length * 2];
                for (int i = 0; i < specialsymbol.Length - 2; i++)
                {
                    specialsymbol[i] = 4;
                }
                specialsymbol[specialsymbol.Length - 1] = 1;
                specialsymbol[specialsymbol.Length - 2] = 0;
                specialsymbol[specialsymbol.Length - 3] = 1;
                specialsymbol[specialsymbol.Length - 4] = 0;
                specialsymbol[specialsymbol.Length - 5] = 1;
                specialsymbol[specialsymbol.Length - 6] = 0;
            }
            else
            {
                options = new string[] { "Create Soul", "Back" };
                specialsymbol = new int[options.Length * 2];
                specialsymbol[0] = 0;
                specialsymbol[1] = 1;
                specialsymbol[2] = 0;
                specialsymbol[3] = 1;
            }

            ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, null, null);
            ConsoleOutput.RenderAnimatedText(prompts, textcolors, 1);
            if (soulfiles.Length > 0)
            {
                prompts = new string[] { "Souls ", "found", "!\n"};
                textcolors = new int[] { 7, 5, 7, 7 };
                ConsoleOutput.UpdateValues(prompts, textcolors);
                ConsoleOutput.RenderText(prompts, textcolors);
            }
            else
            {
                prompts = new string[] { "No ", "Souls ", "Located. ", "Make one?\n", "\n" };
                textcolors = new int[] { 7, 5, 7, 7, 7 };
                ConsoleOutput.UpdateValues(prompts, textcolors);
                ConsoleOutput.RenderText(prompts, textcolors);
            }
            ConsoleOutput.RenderOptions(options, specialsymbol);
            int SelectedIndex = ConsoleOutput.Run();
            if (soulfiles.Length > 0)
            {
                int MenuIndex = options.Length - 1;
                if (MenuIndex == SelectedIndex)
                {
                    withdelay = false;
                }
                else if (MenuIndex - 1 == SelectedIndex)
                {
                    FileManager.DeleteSoul(HighlightChosenColor, TextChosenColor, soulfiles, soulfileNames);
                    SoulFilesUI();
                }
                else if (MenuIndex - 2 == SelectedIndex)
                {
                    FileManager.CreateSoul(HighlightChosenColor, TextChosenColor);
                    SoulFilesUI();
                }
                else
                {
                    InGameUI InGameUI = new InGameUI(soulfiles[SelectedIndex], TextChosenColor, HighlightChosenColor);
                    InGameUI.Initialisation();
                }
            }
            else
            {
                switch (SelectedIndex)
                {
                    case 0:
                        FileManager.CreateSoul(HighlightChosenColor, TextChosenColor);
                        SoulFilesUI();
                        break;
                    case 1:
                        withdelay = false;
                        break;
                }
            }
        }
        public void OptionsUI()
        {
            bool esc = false;
            while (esc == false)
            {
                string[] options =
                {
                    "SelectedOptionTextHighlightColor" , "SelectedOptionTextColor", "ChangeCursorSymbol", "", "Back"
                };
                int[] specialsymbol =
                {
                    2, 3, 2, 3, 0, 1, 0, 1, 0, 1
                };
                string[] prompts =
                {
                    "Press ", "ENTER", ", to interact.\n"
                };
                int[] textcolors =
                {
                    7, 1, 7
                };

                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, null, null);
                Console.Clear();
                options[0] = "SelectedOptionTextHighlightColor : " + ConsoleOutput.availabletextcolorsname[HighlightChosenColor];
                options[1] = "SelectedOptionTextColor : " + ConsoleOutput.availabletextcolorsname[TextChosenColor];
                ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                ConsoleOutput.RenderText(prompts, textcolors);
                ConsoleOutput.RenderOptions(options, specialsymbol);
                int SelectedIndex = 0;
                while (SelectedIndex != 3)
                {
                    SelectedIndex = ConsoleOutput.Run();
                    switch (SelectedIndex)
                    {
                        case 0:
                            if (HighlightChosenColor != ConsoleOutput.availabletextcolors.Length - 1)
                            {
                                HighlightChosenColor++;
                            }
                            else
                            {
                                HighlightChosenColor = 0;
                            }
                            ConsoleOutput.TextHighlightColor = ConsoleOutput.availabletextcolors[HighlightChosenColor];
                            ConfigData.TextHighlightColor = HighlightChosenColor;
                            Console.Clear();
                            options[0] = "SelectedOptionTextHighlightColor : " + ConsoleOutput.availabletextcolorsname[HighlightChosenColor];
                            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol);
                            break;
                        case 1:
                            if (TextChosenColor != ConsoleOutput.availabletextcolors.Length - 1)
                            {
                                TextChosenColor++;
                            }
                            else
                            {
                                TextChosenColor = 0;
                            }
                            ConsoleOutput.TextColor = ConsoleOutput.availabletextcolors[TextChosenColor];
                            ConfigData.TextColor = TextChosenColor;
                            Console.Clear();
                            options[1] = "SelectedOptionTextColor : " + ConsoleOutput.availabletextcolorsname[TextChosenColor];
                            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol);
                            break;
                        case 2:
                            prompts = new string[] { "Enter the ", "symbol", " you'd like to see next to the hovered option. Must be", " one ", "character ", "long.\n", "Press ", "ENTER", " to confirm.\n", "example : * >> \"* Awake\".\n\n" };
                            textcolors = new int[] { 7, 1, 7, 5, 1, 7, 7, 1, 7, 8 };
                            ConsoleOutput.UpdateValues(prompts, textcolors);
                            ConsoleOutput.RenderText(prompts, textcolors);
                            string UserInput = ConsoleOutput.RenderInputOption();
                            if (UserInput.Length == 1)
                            {
                                char.TryParse(UserInput, out char Symbol);
                                prompts = new string[] { "Your selected symbol : ", Symbol.ToString() + "\n\n" };
                                textcolors = new int[] { 6, 5 };
                                options = new string[] { "Confirm", "Decline" };
                                specialsymbol = new int[] { 0, 1, 0, 1 };
                                ConsoleOutput.OptionIndexPlacement = 0;
                                ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                                ConsoleOutput.RenderText(prompts, textcolors);
                                ConsoleOutput.RenderOptions(options, specialsymbol);
                                SelectedIndex = ConsoleOutput.Run();
                                switch (SelectedIndex)
                                {
                                    case 0:
                                        ConfigData.SelectionSymbol = Symbol;
                                        FileManager.UpdateConfigFile(ConfigData);
                                        SelectedIndex = 3;
                                        break;
                                    case 1:
                                        SelectedIndex = 3;
                                        break;
                                }
                            }
                            else
                            {
                                prompts = new string[] { "Invalid choice. It musn't be ", "longer", " or ", "shorter ", "than ", "one ", "character.\n\n" };
                                textcolors = new int[] { 7, 1, 7, 1, 7, 1, 5 };
                                options = new string[] { "Back" };
                                specialsymbol = new int[] { 0, 1 };
                                ConsoleOutput.OptionIndexPlacement = 0;
                                ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                                ConsoleOutput.RenderText(prompts, textcolors);
                                ConsoleOutput.RenderOptions(options, specialsymbol);
                                ConsoleOutput.Run();
                                SelectedIndex = 3;
                            }
                            break;
                        case 4:
                            withdelay = false;
                            esc = true;
                            SelectedIndex = 3;
                            FileManager.UpdateConfigFile(ConfigData);
                            break;
                    }
                }
            }
        }
    }
}