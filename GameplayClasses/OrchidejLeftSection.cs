using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.EnvironmentGeneration;
using LITOURGIYA___OBLATION.GameplayClasses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Markup;
using static System.Net.Mime.MediaTypeNames;
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

        public OrchidejLeftSection(int textchosencolor, int hightlightchosencolor, DataStructure data)
        {
            TextChosenColor = textchosencolor;
            HighlightChosenColor = hightlightchosencolor;
            Data = data;
        }
        BodyStatus BodyStatus = new BodyStatus();
        LootInteractionGen LootInter = new LootInteractionGen();
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
                if (Data.TinyStockroomExplored != false)
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
                if (Data.EnvironmentalStatusData["TinyStockroomExplored"] == true) options[0] = "Side greasy door - Tiny Stockroom";
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
            bool esc2 = false;
            while (esc == false)
            {
                location = "Tiny Stockroom";
                string[] thoughts =
                {
                    "   It's a tiny mess of a room with rotting trash and racks, dirty water leaking from the ceiling.\n",
                    "   There's a metal box hanging on the wall too, with a sticker that's too scraped to tell what it means.\n",
                    "   It seriously stinks in here."
                };
                string[] options =
                {
                //"<<  ", "  >>", "(  ", "  )", "  ", "[", "]", ""
                "Rack - Left wall", "Rack - In between", "Rack - Right wall", "Wall - hanging metal box", "Floor - Scattered trash", "back"
                };
                int[] optioncolors =
                {
                6, 6, 6, 6, 6, 6
                };
                int[] specialsymbol =
                {
                5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6
                };
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