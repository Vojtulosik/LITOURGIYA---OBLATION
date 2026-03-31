using LITOURGIYA___OBLATION.EngineClasses;
using NAudio.Codecs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LITOURGIYA___OBLATION
{
    internal class EmilyBaseUI
    {
        private int TextChosenColor;
        private int HighlightChosenColor;
        private string SaveFile;
        public EmilyBaseUI(int textchosencolor, int hightlightchosencolor, string savefile)
        {
            TextChosenColor = textchosencolor;
            HighlightChosenColor = hightlightchosencolor;
            SaveFile = savefile;
        }
        SoundHub SoundHub = new SoundHub();
        FileManagement FileManager = new FileManagement();
        Random Rand = new Random();
        AssetStation AssetStation = new AssetStation();
        //DATA
        private int BulletsInMag;
        private int MoodStatus;
        private int HungerStatus;
        private int ThirstStatus;
        private int HeatStatus;
        private int ColdStatus;
        private int EnergyStatus;
        private int PainStatus;
        public void Hideout() 
        {
            LoadNotes NoteLoader = new LoadNotes(HighlightChosenColor, TextChosenColor);
            //SoundHub.PlayFromFile();
            DataStructure Data = UpdateData();
            Data = SetData(Data);
            while (true)
            {
                string[] thoughts =
                {
                    "   The lights flicker, humming quietly above my head, halls are quiet today.\n",
                    "   I should test the rifle before going out.\n"
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
                if (Data.FirstAimPractise == true)
                {
                    optioncolors[9] = 7;
                    specialsymbol[19] = 6;
                    optioncolors[optioncolors.Length - 1] = 5;
                    specialsymbol[specialsymbol.Length - 1] = 8;
                }
                MoodStatus = 3;
                string Sensations = GrabSensations();
                string[] prompts =
                {
                    "ORCHIDEJ POWER PLANT", " - ", "Hideout\n", "Day 1, 9:00 AM\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], "\n", "Sensations :\n", "  " + Sensations + "\n", "\n"
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
                            options = new string[] { "Unholster rifle", "Load mag", "Back" };
                            specialsymbol = new int[] { 0, 1, 0, 1, 0, 1 };
                            optioncolors = new int[] { 6, 6, 6 };
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
                                        string BulletsInMagRange = "";
                                        int leftOffset = Rand.Next(0, 4);
                                        int rightOffset = Rand.Next(0, 4);
                                        int min = BulletsInMag - leftOffset;
                                        int max = BulletsInMag + rightOffset;
                                        if (min < 0)
                                        {
                                            min = 0;
                                            leftOffset = 0;
                                        }
                                        if (max > 15) max = 15;
                                        if (leftOffset + rightOffset == 0) max = max + Rand.Next(1, 4);

                                        BulletsInMagRange = (min) + " to " + (max);

                                        prompts = new string[] { "[Mag Checking : Uncertain]", ", By the weight of the rifle's mag, I guess I still have ", BulletsInMagRange, " bullets left.\n", "[Precision : Uncertain]", ", Hopefully I'll hit something this time.\n", "\n" };
                                        textcolors = new int[] { 5, 7, 1, 7, 5, 7, 7 };
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
                                                if (BulletsInMag > 0)
                                                {
                                                    bool TargetHit = CombatInput.PrecisionBar(7, 30, 5);
                                                    if (TargetHit == true)
                                                    {
                                                        prompts = new string[] { "It's a hit! Sand shoots out into the air from the new bullethole and the dummy falls back-first against the wall.\n", "\n" };
                                                        textcolors = new int[] { 7, 7 };
                                                        options = new string[] { "Back" };
                                                        specialsymbol = new int[] { 0, 1 };
                                                        optioncolors = new int[] { 6 };
                                                        ConsoleOutput.RenderText(prompts, textcolors);
                                                        ConsoleOutput.RenderOptions(options, specialsymbol);
                                                        ConsoleOutput.Run();
                                                        if (Data.FirstAimPractise == false)
                                                        {
                                                            Data.FirstAimPractise = true;
                                                            Console.Clear();
                                                            NoteLoader.Render(1);
                                                            options = new string[] { "Close" };
                                                            specialsymbol = new int[] { 0, 1 };
                                                            ConsoleOutput.UpdateValues(null, null, options, specialsymbol);
                                                            ConsoleOutput.RenderOptions(options, specialsymbol);
                                                            ConsoleOutput.Run();
                                                        }
                                                        BulletsInMag--;
                                                    }
                                                    else
                                                    {
                                                        prompts = new string[] { "The rifle punches me into my shoulder, and the bullet strikes into the wall with a loud bang. Dang it.\n", "\n" };
                                                        textcolors = new int[] { 7, 7 };
                                                        options = new string[] { "Back" };
                                                        specialsymbol = new int[] { 0, 1 };
                                                        optioncolors = new int[] { 6 };
                                                        ConsoleOutput.RenderText(prompts, textcolors);
                                                        ConsoleOutput.RenderOptions(options, specialsymbol);
                                                        ConsoleOutput.Run();
                                                    }
                                                }
                                                else
                                                {
                                                    prompts = new string[] { "A loud metallic click is heard, but no bullet is discharged. Did I forget to load my mags?\n", "\n" };
                                                    textcolors = new int[] { 7, 7 };
                                                    options = new string[] { "Back" };
                                                    specialsymbol = new int[] { 0, 1 };
                                                    optioncolors = new int[] { 6 };
                                                    ConsoleOutput.RenderText(prompts, textcolors);
                                                    ConsoleOutput.RenderOptions(options, specialsymbol);
                                                    ConsoleOutput.Run();
                                                }
                                                break;
                                            case 1:
                                                esc = true;
                                                break;
                                        }
                                    }
                                    esc = false;
                                    break;
                                case 1:
                                    BulletsInMag += 5;
                                    prompts = new string[] { "Loaded five 7.62s into the mag.\n", "\n" };
                                    textcolors = new int[] { 1, 7 };
                                    options = new string[] { "Back" };
                                    specialsymbol = new int[] { 0, 1 };
                                    optioncolors = new int[] { 6 };
                                    ConsoleOutput.OptionIndexPlacement = 0;
                                    SelectedIndex = 0;
                                    ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol, optioncolors);
                                    ConsoleOutput.RenderText(prompts, textcolors);
                                    ConsoleOutput.RenderOptions(options, specialsymbol);
                                    ConsoleOutput.Run();
                                    break;
                                case 2:
                                    esc = true;
                                    break;
                            }
                        }
                        break;
                    case 14:
                        prompts = new string[] { "Test", "\n" };
                        textcolors = new int[] { 6, 7 };
                        options = new string[] { "Back" };
                        specialsymbol = new int[] { 0, 1 };
                        optioncolors = new int[] { 6 };
                        ConsoleOutput.OptionIndexPlacement = 0;
                        SelectedIndex = 0;
                        ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol, optioncolors);
                        ConsoleOutput.RenderText(prompts, textcolors);
                        ConsoleOutput.RenderOptions(options, specialsymbol);
                        ConsoleOutput.Run();
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
        private DataStructure UpdateData()
        {
            DataStructure data = FileManager.LoadData(SaveFile);
            return data;
        }
        private DataStructure SetData(DataStructure Data)
        {
            BulletsInMag = Data.BulletsInMag;
            MoodStatus = Data.MoodStatus;
            PainStatus = Data.PainStatus;
            HungerStatus = Data.HungerStatus;
            ThirstStatus = Data.ThirstStatus;
            HeatStatus = Data.HeatStatus;
            ColdStatus = Data.ColdStatus;
            return Data;
        }
        private string GrabSensations()
        {
            string sensations = AssetStation.SanityParameters[MoodStatus] + " | " + AssetStation.EnergyParameters[EnergyStatus];
            if (PainStatus > 0) sensations += " | " + AssetStation.PainParameters[PainStatus];
            if (ThirstStatus > 0) sensations += " | " + AssetStation.ThirstParameters[ThirstStatus];
            if (HungerStatus > 0) sensations += " | " + AssetStation.HungerParameters[HungerStatus];
            if (ColdStatus > 0) sensations += " | " + AssetStation.ColdParameters[HeatStatus];
            if (HeatStatus > 0) sensations += " | " + AssetStation.HeatParameters[HeatStatus];
            return sensations;
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