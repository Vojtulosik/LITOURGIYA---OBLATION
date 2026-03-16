using System;
using System.Collections.Generic;
using System.Text;

namespace LITOURGIYA___OBLATION
{
    internal class EmilyBaseUI
    {
        private int TextChosenColor;
        private int HighlightChosenColor;
        public EmilyBaseUI(int textchosencolor, int hightlightchosencolor)
        {
            TextChosenColor = textchosencolor;
            HighlightChosenColor = hightlightchosencolor;
        }
        public void Hideout()
        {
            while (true)
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
                    "left wall :", "Old Worktable",
                    "", "Right wall - desk :",
                    "Glass box", "Disassembled radio",
                    "Journal", "",
                    "Right wall :", "Sandbag dummy",
                    "Wardrobe locker", "",
                    "Far corner :", "Storage drawer",
                    "Sleeping bag", "",
                    "Near the enterance :", "Reinforced door"
                };
                int[] optioncolors =
                {
                    6, 7, 7, 6,
                    7, 7, 7, 7,
                    6, 5, 7, 7,
                    6, 7, 7, 7,
                    6, 7
                };
                int[] specialsymbol =
                {
                    7, 7, 5, 6,
                    4, 4, 7, 7,
                    5, 6, 5, 6,
                    5, 6, 4, 4,
                    7, 7, 5, 8,
                    5, 6, 4, 4,
                    7, 7, 5, 6,
                    5, 6, 4, 4,
                    7, 7, 5, 6
                };
                string[] prompts =
                {
                    "ORCHIDEJ POWER PLANT", " - ", "Hideout\n", "Day 1, 9:00 AM\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], "\n", "Sensations :\n", sensations[0], "\n"
                };
                int[] textcolors =
                {
                    4, 7, 6, 6, 7, 1, 7, 7, 7, 1, 7, 7
                };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, optioncolors, null);
                ConsoleOutput.OptionIndexPlacement = 1;
                ConsoleOutput.RenderText(prompts, textcolors);
                ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
                int SelectedIndex = ConsoleOutput.Run();
                bool esc = false;
                switch (SelectedIndex)
                {
                    case 9:
                        esc = false;
                        while (esc == false)
                        {
                            prompts = new string[] { "SANDBAG DUMMY\n", "\n", "A Burlap sack filled to the brim with sand, the top enclosed with a rope.\n", "Few bullet holes already present.\n", "\n" };
                            textcolors = new int[] { 1, 7, 7, 7, 6 };
                            options = new string[] { "Unholster rifle", "Back" };
                            specialsymbol = new int[] { 0, 1, 0, 1 };
                            optioncolors = new int[] { 6, 6 };
                            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol, optioncolors);
                            ConsoleOutput.OptionIndexPlacement = 0;
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol);
                            SelectedIndex = ConsoleOutput.Run();
                            esc = false;
                            switch (SelectedIndex)
                            {
                                case 0:
                                    while (esc == false)
                                    {
                                        prompts = new string[] { "By the weight of the rifle's mag, I guess I still have ", "3 to 5", " bullets left.\n", "Hopefully I'll hit something this time.\n", "\n" };
                                        textcolors = new int[] { 7, 1, 7, 5, 7 };
                                        options = new string[] { "Aim", "Back" };
                                        specialsymbol = new int[] { 0, 1, 0, 1 };
                                        optioncolors = new int[] { 6, 6 };
                                        ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol, optioncolors);
                                        ConsoleOutput.OptionIndexPlacement = 0;
                                        ConsoleOutput.RenderText(prompts, textcolors);
                                        ConsoleOutput.RenderOptions(options, specialsymbol);
                                        SelectedIndex = ConsoleOutput.Run();
                                        switch (SelectedIndex)
                                        {
                                            case 0:
                                                prompts = new string[] { "Sliding the mag back into the magwell after missing twice, I raise the barrel and aim towards the dummy...\n", "\n" };
                                                textcolors = new int[] { 7, 7 };
                                                options = new string[] { "Fire" };
                                                specialsymbol = new int[] { 0, 1 };
                                                optioncolors = new int[] { 6 };
                                                ConsoleOutput.RenderText(prompts, textcolors);
                                                ConsoleOutput.RenderOptions (options, specialsymbol);
                                                ConsoleOutput.Run();
                                                CombatInput CombatInput = new CombatInput();
                                                bool TargetHit = CombatInput.PrecisionBar(7, 30, 5);
                                                if (TargetHit == true)
                                                {
                                                    prompts = new string[] { "It's a hit! Sand shoots out into the air from the new bullethole and the dummy falls back-first against the wall.\n", "\n" };
                                                    textcolors = new int[] { 7, 7 };
                                                    options = new string[] { "Back" };
                                                    specialsymbol = new int[] { 0, 1 };
                                                    optioncolors = new int[] { 6 };
                                                }
                                                else
                                                {
                                                    prompts = new string[] { "The rifle punches me into my shoulder, and the bullet strikes into the wall with a loud bang. Dang it.\n", "\n" };
                                                    textcolors = new int[] { 7, 7 };
                                                    options = new string[] { "Back" };
                                                    specialsymbol = new int[] { 0, 1 };
                                                    optioncolors = new int[] { 6 };
                                                }
                                                ConsoleOutput.RenderText(prompts, textcolors);
                                                ConsoleOutput.RenderOptions(options, specialsymbol);
                                                ConsoleOutput.Run();
                                                break;
                                            case 1:
                                                esc = true;
                                                break;
                                        }
                                    }
                                    esc = false;
                                    break;
                                case 1:
                                    esc = true;
                                    break;
                            }
                        }
                        break;
                    case 17:
                        prompts = new string[] { "I'm pretty sure I wanted to try something before leaving.\n", "\n" };
                        textcolors = new int[] { 7, 7 };
                        options = new string[] { "back" };
                        specialsymbol = new int[] { 0, 1 };
                        ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
                        ConsoleOutput.OptionIndexPlacement = 0;
                        ConsoleOutput.RenderText(prompts, textcolors);
                        ConsoleOutput.RenderOptions(options, specialsymbol);
                        SelectedIndex = ConsoleOutput.Run();
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