using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.GameplayClasses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LITOURGIYA___OBLATION.InGameUIClasses
{
    internal class OrchidejLeftSection
    {
        private int TextChosenColor;
        private int HighlightChosenColor;
        private DataStructure Data;

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
            int SelectedIndex = 0;
            while (esc == false)
            {
                string location = "Enterance corridor";
                string Sensations = BodyStatus.GrabSensations(Data);
                string[] thoughts =
                    {
                    "   The corridor stretches into the dark, loose wiring hanging overhead and a greasy wooden door faintly visible on the far left.\n",
                    "   Only if I had a flashlight.\n"
                };
                string[] options =
                {
                    //"<<  ", "  >>", "(  ", "  )", "  ", "[", "]", ""
                    "Side greasy door - ???", "Into the dark", "Back"
                };
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
                    "ORCHIDEJ POWER PLANT", " - ", "Left section\n", "Day " + Data.InGameDay + ", " + Data.Time + "\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], "\n", "Sensations :\n", "  " + Sensations + "\n", "Location : ", location + "\n", "\n"
                };
                int[] textcolors =
                {
                    4, 7, 6, 6, 7, 1, 7, 7, 7, 1, 7, 1, 5, 7
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