using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.EnvironmentGeneration;
using LITOURGIYA___OBLATION.GameplayClasses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LITOURGIYA___OBLATION.InGameUIClasses
{
    class OrchidejLeftSection
    {
        private int TextChosenColor;
        private int HighlightChosenColor;
        private DataStructure Data;
        private string location = "Enterance corridor";
        private int SelectedIndex = 0;
        LootInteractionGen LootInter = new LootInteractionGen();

        public OrchidejLeftSection(int textchosencolor, int hightlightchosencolor, DataStructure data)
        {
            TextChosenColor = textchosencolor;
            HighlightChosenColor = hightlightchosencolor;
            Data = data;
        }
        BodyStatus BodyStatus = new BodyStatus();
        public void LeftCorridor()
        {
            bool esc = false;
            while (esc == false)
            {
                location = "Enterance corridor";
                string Sensations = BodyStatus.GrabSensations(Data);
                string[] thoughts =
                    {
                    "   The concrete corridor stretches into the dark, loose wiring hanging overhead and a greasy wooden door faintly\n   visible on the far left.\n",
                    "   If only I had a flashlight.\n"
                };
                string[] options = { "Side greasy door - ???", "Into the dark", "Back" };
                if (Data.EnvironmentalStatusData["TinyStockroomExplored"] != false)
                {
                    options[0] = "Side greasy door - Tiny stockroom";
                }
                int[] optioncolors =
                {
                    6, 6, 6
                };
                int[] specialsymbol =
                {
                    5, 6, 5, 6, 5, 6
                };
                string[] prompts =
                {
                    "ORCHIDEJ POWER PLANT", " - ", "Left section" + "\n", "Day " + Data.InGameDay + ", " + Data.Time + "\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], "\n", "Sensations :\n", "  " + Sensations + "\n", "\n", "Location : ", location + "\n", "\n"
                };
                int[] textcolors =
                {
                    4, 7, 6, 6, 7, 1, 7, 7, 7, 1, 7, 7, 1, 5, 7
                };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, optioncolors, null);
                ConsoleOutput.RenderText(prompts, textcolors);
                ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
                SelectedIndex = ConsoleOutput.Run();
                switch (SelectedIndex)
                {
                    case 0:
                        TinyStockroom();
                        break;
                    case 1:
                        break;
                    case 2:
                        esc = true;
                        break;
                }
            }
        }
        private void TinyStockroom()
        {
            string Sensations = BodyStatus.GrabSensations(Data);
            bool esc = false;
            while (esc == false)
            {
                location = "Tiny Stockroom";
                string[] thoughts =
                {
                "   It's a tiny mess of a room with rotting trash and racks, dirty water leaking from the ceiling.\n",
                "   There's a metal box hanging on the wall too, with a sticker that's too scraped to tell what it means.\n",
                "   It seriously stinks in here."
                };
                string[] options;
                int[] optioncolors;
                int[] specialsymbol;
                if (Data.EnvironmentalStatusData["TinyStockroomExplored"] == true)
                {
                    options = new string[]
                    {
                        //"<<  ", "  >>", "(  ", "  )", "  ", "[", "]", ""
                        "Rack - Left wall", "Rack - In between", "Rack - Right wall", "Wall - hanging metal box", "Floor - Scattered trash", "back"
                    };
                    optioncolors = new int[]
                    {
                        6, 6, 6, 6, 6, 6
                    };
                    specialsymbol = new int[]
                    {
                        5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6
                    };
                }
                else
                {
                    options = new string[]
                    {
                        //"<<  ", "  >>", "(  ", "  )", "  ", "[", "]", ""
                        "Rack - Left wall", "Rack - In between", "Rack - Right wall", "Wall - hanging metal box", "Floor - Scattered trash", "Spraypaint - Navigation symbol", "back"
                    };
                    optioncolors = new int[]
                    {
                        6, 6, 6, 6, 6, 6, 6
                    };
                    specialsymbol = new int[]
                    {
                        5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 0, 1, 5, 6
                    };
                }
                string[] prompts =
                {
                "ORCHIDEJ POWER PLANT", " - ", "Left section" + "\n", "Day " + Data.InGameDay + ", " + Data.Time + "\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], thoughts[2], "\n", "Sensations :\n", "  " + Sensations + "\n", "\n", "Location : ", location + "\n", "\n"
                };
                int[] textcolors =
                {
                    4, 7, 6, 6, 7, 1, 7, 7, 7, 7, 1, 7, 7, 1, 5, 7
                };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, optioncolors, null);
                ConsoleOutput.RenderText(prompts, textcolors);
                ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
                SelectedIndex = ConsoleOutput.Run();
                switch (SelectedIndex)
                {
                    case 0:
                        bool esc2 = false;
                        int[] LootData = Data.EnvironmentalLootData["TinyStockroomLeftRackLoot"];
                        string[] LootDataNames = Data.EnvironmentalLootDataNames["TinyStockroomLeftRackLoot"];
                        prompts = new string[] { "Tiny Stockroom ", "- ", "Left Rack\n\n", "Thoughts ", ":\n", "  I'm surprised this thing still stands, the wood platform literally bends when I press down on it.\n\n" };
                        textcolors = new int[] { 4, 7, 6, 1, 7, 7 };
                        LootInter.FormLists(Data, out List<string> LootOptions, out List<int> LootOptionsValues, out List<int> LootOptionsStoreValues);

                        while (esc2 == false)
                        {
                            options = LootInter.Generate(LootOptionsValues.ToArray(), LootOptions.ToArray(), Data, out specialsymbol, out optioncolors);
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
                            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol, optioncolors);
                            SelectedIndex = ConsoleOutput.Run();
                            if (SelectedIndex != options.Length - 1)
                            {
                                Data = LootInter.LootInteract(Data, SelectedIndex, LootOptionsValues, LootOptions, LootDataNames, LootData, LootOptionsStoreValues, "TinyStockroomLeftRackLoot");
                            }
                            else
                            {
                                esc2 = true;
                            }
                        }
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        if (Data.EnvironmentalStatusData["TinyStockroomExplored"] == true)
                        {
                            esc = true;
                        }
                        else
                        {
                            prompts = new string[] { "I grab a spraypaint can from my toolbelt, spraying a little doodle on the enterance's door. No way I'll forget about this room now.\n", "\n" };
                            textcolors = new int[] { 7, 7 };
                            options = new string[] { "Confirm" };
                            specialsymbol = new int[] { 0, 1 };
                            optioncolors = new int[] { 6 };
                            ConsoleOutput.OptionIndexPlacement = 0;
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol);
                            ConsoleOutput.Run();
                            Data.EnvironmentalStatusData["TinyStockroomExplored"] = true;
                        }
                        break;
                    case 6:
                        esc = true;
                        break;
                }
            }
        }
    }
}

/*
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
*/