using System;
using System.Collections.Generic;
using System.Text;

namespace LITOURGIYA___OBLATION
{
    internal class EmilyBaseUI
    {
        private int TextChosenColor;
        private int HighlightChosenColor;
        public EmilyBaseUI(int textchosencolor, int highlightchosencolor)
        {
            TextChosenColor = textchosencolor;
            HighlightChosenColor = highlightchosencolor;
        }
        public void Hideout()
        {
            string[] thoughts = 
            { 
                "   The lights flicker, humming quietly above my head, halls are quiet today.\n",
                "   I should test the rifle before going out.\n" 
            };
            string[] sensations =
            {
                "   Energised  |  Content\n"
            };
            string[] options =
            {
                //"<<  ", "  >>", "(  ", "  )", "  ", "[", "]", ""
                "left wall :", "Old Worktable", "", "Right wall - desk :", "Glass box", "Deassembled radio", "Journal", "", "Right wall :", "Sandbag dummy", "", "Far corner :", "Storage drawer", "Sleeping bag", "", "Near the enterance :", "Reinforced door"
            };
            int[] specialsymbol =
            {
                7, 7, 5, 6, 4, 4, 7, 7, 5, 6, 5, 6, 5, 6, 4, 4, 7, 7, 5, 6, 4, 4, 7, 7, 5, 6, 5, 6, 4, 4, 7, 7, 5, 6
            };
            string[] prompts =
            {
                "ORCHIDEJ POWER PLANT", " - ", "Hideout\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], "\n", "Sensations :\n", sensations[0], "\n"
            };
            int[] textcolors =
            {
                4, 7, 6, 7, 1, 7, 7, 7, 1, 7, 7
            };
            ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, null, null);
            ConsoleOutput.UpdateValues(1);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderOptions(options, specialsymbol);
            ConsoleOutput.Run();
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