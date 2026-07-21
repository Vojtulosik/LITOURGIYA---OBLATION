using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.EnvironmentGeneration;
using LITOURGIYA___OBLATION.GameplayClasses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
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
        private LootInteractionGen LootInter = new LootInteractionGen();
        private BasicGameplayFunctions BasicFunc = new BasicGameplayFunctions();
        private List<string> LootOptions = new List<string>();
        private List<int> LootOptionsValues = new List<int>();
        private List<int> LootOptionsStoreValues = new List<int>();
        private int[] LootData;
        private string[] LootDataNames;
        SoundHub SoundHub = new SoundHub();

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
                SelectedIndex = ConsoleOutput.Run(false, Data);
                switch (SelectedIndex)
                {
                    case 0:
                        TinyStockroom();
                        break;
                    case 1:
                        CorridorToCafeteria();
                        break;
                    case 2:
                        esc = true;
                        break;
                    default:
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
                SelectedIndex = ConsoleOutput.Run(false, Data);
                bool esc2 = false;
                switch (SelectedIndex)
                {
                    case 0:
                        ResetLootOptions("TinyStockroomLeftRackLoot");
                        ConsoleOutput.OptionIndexPlacement = 0;
                        Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                        prompts = new string[] { "Tiny Stockroom ", "- ", "Left Rack\n\n", "Thoughts ", ":\n", "  I'm surprised this thing still stands, the wood platform literally bends when I press down on it.\n", "  Carrying weight : ", Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n" };
                        textcolors = new int[] { 4, 7, 6, 1, 7, 7, 7, 5 };
                        LootInter.FormLists(Data, out LootOptions, out LootOptionsValues, out LootOptionsStoreValues, "TinyStockroomLeftRackLoot");

                        while (esc2 == false)
                        {
                            esc2 = CreateLootUI(ConsoleOutput, prompts, textcolors, LootOptions, LootOptionsValues, specialsymbol, optioncolors, LootDataNames, LootData, LootOptionsStoreValues, "TinyStockroomLeftRackLoot");
                            Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                            prompts[7] = Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n";
                        }
                        break;
                    case 1:
                        ResetLootOptions("TinyStockroomMiddleRackLoot");
                        ConsoleOutput.OptionIndexPlacement = 0;
                        Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                        prompts = new string[] { "Tiny Stockroom ", "- ", "Middle Rack\n\n", "Thoughts ", ":\n", "  So much dust. It's stability isn't any better.\n", "  Carrying weight : ", Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n" };
                        textcolors = new int[] { 4, 7, 6, 1, 7, 7, 7, 5 };
                        LootInter.FormLists(Data, out LootOptions, out LootOptionsValues, out LootOptionsStoreValues, "TinyStockroomMiddleRackLoot");

                        while (esc2 == false)
                        {
                            esc2 = CreateLootUI(ConsoleOutput, prompts, textcolors, LootOptions, LootOptionsValues, specialsymbol, optioncolors, LootDataNames, LootData, LootOptionsStoreValues, "TinyStockroomMiddleRackLoot");
                            Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                            prompts[7] = Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n";
                        }
                        break;
                    case 2:
                        ResetLootOptions("TinyStockroomRightRackLoot");
                        ConsoleOutput.OptionIndexPlacement = 0;
                        Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                        prompts = new string[] { "Tiny Stockroom ", "- ", "Right Rack\n\n", "Thoughts ", ":\n", "  This one's preserved quite well. Obviously though, the weight of the stuff on it would have collapsed it long ago.\n", "  Carrying weight : ", Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n" };
                        textcolors = new int[] { 4, 7, 6, 1, 7, 7, 7, 5 };
                        LootInter.FormLists(Data, out LootOptions, out LootOptionsValues, out LootOptionsStoreValues, "TinyStockroomRightRackLoot");

                        while (esc2 == false)
                        {
                            esc2 = CreateLootUI(ConsoleOutput, prompts, textcolors, LootOptions, LootOptionsValues, specialsymbol, optioncolors, LootDataNames, LootData, LootOptionsStoreValues, "TinyStockroomRightRackLoot");
                            Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                            prompts[7] = Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n";
                        }
                        break;
                    case 3:
                        ResetLootOptions("TinyStockroomHangingMetalBox");
                        ConsoleOutput.OptionIndexPlacement = 0;
                        Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                        prompts = new string[] { "Tiny Stockroom ", "- ", "Hanging metal box\n\n", "Thoughts ", ":\n", "  Oh, it's a first aid kit. I knew the scratched sticker looked like a cross.\n", "  Carrying weight : ", Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n" };
                        textcolors = new int[] { 4, 7, 6, 1, 7, 7, 7, 5 };
                        LootInter.FormLists(Data, out LootOptions, out LootOptionsValues, out LootOptionsStoreValues, "TinyStockroomHangingMetalBox");

                        while (esc2 == false)
                        {
                            esc2 = CreateLootUI(ConsoleOutput, prompts, textcolors, LootOptions, LootOptionsValues, specialsymbol, optioncolors, LootDataNames, LootData, LootOptionsStoreValues, "TinyStockroomHangingMetalBox");
                            Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                            prompts[7] = Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n";
                        }
                        break;
                    case 4:
                        ResetLootOptions("TinyStockroomFloorScatteredTrash");
                        ConsoleOutput.OptionIndexPlacement = 0;
                        Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                        prompts = new string[] { "Tiny Stockroom ", "- ", "Scattered floor trash\n\n", "Thoughts ", ":\n", "  Ew, yeah. That's where the smell comes from.\n", "  Carrying weight : ", Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n" };
                        textcolors = new int[] { 4, 7, 6, 1, 7, 7, 7, 5 };
                        LootInter.FormLists(Data, out LootOptions, out LootOptionsValues, out LootOptionsStoreValues, "TinyStockroomFloorScatteredTrash");

                        while (esc2 == false)
                        {
                            esc2 = CreateLootUI(ConsoleOutput, prompts, textcolors, LootOptions, LootOptionsValues, specialsymbol, optioncolors, LootDataNames, LootData, LootOptionsStoreValues, "TinyStockroomFloorScatteredTrash");
                            Data.InventoryTotalWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                            prompts[7] = Data.InventoryTotalWeight + "/" + Data.MaxCarryingWeight + "KG\n\n";
                        }
                        break;
                    case 5:
                        if (Data.EnvironmentalStatusData["TinyStockroomExplored"] == true)
                        {
                            esc = true;
                        }
                        else
                        {
                            prompts = new string[] { "I grab a spraypaint can from my toolbelt, spraying a little doodle on the enterance's door. No way I'll forget about\n this room now.\n", "\n" };
                            textcolors = new int[] { 7, 7 };
                            options = new string[] { "Confirm" };
                            specialsymbol = new int[] { 0, 1 };
                            optioncolors = new int[] { 6 };
                            ConsoleOutput.OptionIndexPlacement = 0;
                            ConsoleOutput.RenderText(prompts, textcolors);
                            ConsoleOutput.RenderOptions(options, specialsymbol);
                            ConsoleOutput.Run(false, Data);
                            Data.EnvironmentalStatusData["TinyStockroomExplored"] = true;
                        }
                        break;
                    case 6:
                        esc = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private bool CreateLootUI(ConsoleOutput ConsoleOutput, string[] prompts, int[] textcolors, List<string> LootOptions, List<int> LootOptionsValues, int[] specialsymbol, int[] optioncolors, string[] LootDataNames, int[] LootData, List<int> LootOptionsStoreValues, string DictionaryKey)
        {
            string[] options = LootInter.Generate(LootOptionsValues.ToArray(), LootOptions.ToArray(), Data, out specialsymbol, out optioncolors);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol, optioncolors);
            SelectedIndex = ConsoleOutput.Run(false, out ConsoleKey KeyPressed, Data);
            bool esc2 = false;
            if (SelectedIndex != options.Length - 1)
            {
                Data = LootInter.LootInteract(Data, SelectedIndex, LootOptionsValues, LootOptions, LootDataNames, LootData, LootOptionsStoreValues, DictionaryKey, KeyPressed);
                esc2 = false;
            }
            else if (SelectedIndex == 999)
            {
                esc2 = false;
            }
            else
            {
                esc2 = true;
            }
            return esc2;
        }
        private void ResetLootOptions(string ObjectName)
        {
            LootOptions.Clear();
            LootOptionsValues.Clear();
            LootOptionsStoreValues.Clear();
            LootData = Data.EnvironmentalLootData[ObjectName];
            LootDataNames = Data.EnvironmentalLootDataNames[ObjectName];
        }
        private void CorridorToCafeteria()
        {
            bool esc = true;
            while (esc)
            {
                location = "Enterance corridor";
                string Sensations = BodyStatus.GrabSensations(Data);
                string[] thoughts =
                    {
                    "   Yeah, it just continues into total darkness. No sign of working ceiling lights. I should use a flashlight \n   or something.",
                };
                string[] options = { "Back" };
                bool ToolFound = false;
                foreach(string tool in Data.Inventory)//Change to holsteredtools array later
                {
                    string refined = "";
                    foreach(char c in tool)
                    {
                        if (!char.IsNumber(c)) refined += c;
                    }
                    if (refined == "Flashlight")
                    {
                        options = new string[] { "Use Flashlight", "Back" };
                        ToolFound = true;
                    }
                }
                int[] optioncolors =
                {
                    6, 6
                };
                int[] specialsymbol =
                {
                    5, 6, 0, 1
                };
                string[] prompts =
                {
                    "ORCHIDEJ POWER PLANT", " - ", "Left section" + "\n", "Day " + Data.InGameDay + ", " + Data.Time + "\n", "\n", "Thoughts :\n", thoughts[0], "\n", "Sensations :\n", "  " + Sensations + "\n", "\n", "Location : ", location + "\n", "\n"
                };
                int[] textcolors =
                {
                    4, 7, 6, 6, 7, 1, 7, 7, 7, 1, 7, 1, 5, 7
                };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, optioncolors, null);
                ConsoleOutput.RenderText(prompts, textcolors);
                ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
                SelectedIndex = ConsoleOutput.Run(false, Data);
                switch (SelectedIndex)
                {
                    case 0:
                        if (ToolFound) Cafeteria();
                        else esc = false;
                        break;
                    case 1:
                        esc = false;
                        break;
                }
            }
        }
        private void Cafeteria()
        {
            bool esc = false;
            while (esc == false)
            {
                Data.Inventory.Add("Can of mystery meat");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("Can of chicken stock");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("Can of Vegetable mix");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("Can of Tomato sauce");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("Bag of Tagliatelle");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("Bag of grated cheese");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("SNWLEO");
                Data.InventoryValues.Add(1);
                Data.Inventory.Add("Noisemaker bait");
                Data.InventoryValues.Add(1);
                Data.MoodStatus = 2;
                location = "Cafeteria"; //Entering it for the first time plays a sound effect of someone running
                string Sensations = BodyStatus.GrabSensations(Data);
                string[] thoughts =
                    {
                    "   A fishy odor always means there's food to be discovered. And it's especially strong towards the kitchen.\n",
                    "   Though, it could still be rotteh- ...What was that..?\n"
                };
                string[] options = { "Center - Bench tables", "Facing wall door - ???", "Left wall - Kitchen", "Right wall - Janitor cart", "Right wall - Barricaded double door", "Back" };
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
                    "ORCHIDEJ POWER PLANT", " - ", "Left section" + "\n", "Day " + Data.InGameDay + ", " + Data.Time + "\n", "\n", "Thoughts :\n", thoughts[0], thoughts[1], "\n", "Sensations :\n", "  " + Sensations + "\n", "\n", "Location : ", location + "\n", "\n"
                };
                int[] textcolors =
                {
                    4, 7, 6, 6, 7, 1, 7, 7, 7, 1, 7, 7, 1, 5, 7
                };
                ConsoleOutput ConsoleOutput = new ConsoleOutput(options, prompts, textcolors, null, null, TextChosenColor, HighlightChosenColor, specialsymbol, optioncolors, null);
                ConsoleOutput.RenderText(prompts, textcolors);
                ConsoleOutput.RenderOptions(options, specialsymbol, optioncolors);
                SelectedIndex = ConsoleOutput.Run(false, Data);
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