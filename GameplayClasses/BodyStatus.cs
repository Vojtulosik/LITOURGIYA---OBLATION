using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.EnvironmentGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LITOURGIYA___OBLATION.GameplayClasses
{
    internal class BodyStatus
    {
        AssetStation AssetStation = new AssetStation();

        public string GrabSensations(DataStructure data)
        {
            string sensations = AssetStation.SanityParameters[data.MoodStatus] + " | " + AssetStation.EnergyParameters[data.EnergyStatus];
            if (data.PainStatus > 0) sensations += " | " + AssetStation.PainParameters[data.PainStatus];
            if (data.ThirstStatus > 0) sensations += " | " + AssetStation.ThirstParameters[data.ThirstStatus];
            if (data.HungerStatus > 0) sensations += " | " + AssetStation.HungerParameters[data.HungerStatus];
            if (data.ColdStatus > 0) sensations += " | " + AssetStation.ColdParameters[data.ColdStatus];
            if (data.HeatStatus > 0) sensations += " | " + AssetStation.HeatParameters[data.HeatStatus];
            return sensations;
        }
        public void BodyCheckUI(ConsoleOutput ConsoleOutput, DataStructure Data)
        {
            InventoryUI(ConsoleOutput, Data);
        }
        public decimal CalcInventoryWeight(List<string> inventory, List<int> values)
        {
            decimal weight = 0;

            for (int i = 0; i < inventory.Count; i++)
            {
                decimal itemWeight = inventory[i] switch
                {
                    "Scrap polymers" => 0.2m,
                    "Wood scrap" => 0.15m,
                    "Bottle of acid" => 1.5m,
                    "Glue" => 0.6m,
                    "Toolbox" => 2.5m,
                    "Cloth fragment" => 0.05m,
                    "Nails" => 0.1m,
                    "Wood plank" => 10.0m,
                    "Bucket" => 3.3m,
                    "Corrugated panel" => 15.0m,
                    "Bolts" => 0.15m,
                    "Pipe" => 7.3m,
                    "Bandages" => 0.15m,
                    "Blood test" => 0.26m,
                    "Healing ointment" => 0.48m,
                    "Pain killers" => 0.62m,
                    "Spoiled paper" => 0.08m,
                    "Glass shard" => 0.04m,
                    "Flashlight" => 0.67m,
                    _ => 0m
                };

                weight += Math.Floor(values[i] * itemWeight * 100) / 100;
            }

            return weight;
        }
        public decimal CalcItemWeight(string item, int count)
        {
            decimal itemWeight = item switch
            {
                "Scrap polymers" => 0.2m,
                "Wood scrap" => 0.15m,
                "Bottle of acid" => 1.5m,
                "Glue" => 0.6m,
                "Toolbox" => 2.5m,
                "Cloth fragment" => 0.05m,
                "Nails" => 0.1m,
                "Wood plank" => 10.0m,
                "Bucket" => 3.3m,
                "Corrugated panel" => 15.0m,
                "Bolts" => 0.15m,
                "Pipe" => 7.3m,
                "Bandages" => 0.15m,
                "Blood test" => 0.26m,
                "Healing ointment" => 0.48m,
                "Pain killers" => 0.62m,
                "Spoiled paper" => 0.08m,
                "Glass shard" => 0.04m,
                "Flashlight" => 0.67m,
                _ => 0m
            };

            return Math.Floor(count * itemWeight * 100) / 100;
        }
        private char DefineLootType(string item)
        {
            char type = item switch
            {
                "Scrap polymers" => 'R',
                "Wood scrap" => 'R',
                "Bottle of acid" => 'R',
                "Glue" => 'R',
                "Toolbox" => 'T',
                "Cloth fragment" => 'R',
                "Nails" => 'R',
                "Wood plank" => 'R',
                "Bucket" => 'R',
                "Corrugated panel" => 'R',
                "Bolts" => 'R',
                "Pipe" => 'R',
                "Bandages" => 'M',
                "Blood test" => 'M',
                "Healing ointment" => 'M',
                "Pain killers" => 'M',
                "Spoiled paper" => 'R',
                "Glass shard" => 'R',
                "Flashlight" => 'T',
                _ => ' '
            };
            return type;
        }
        private void InventoryUI(ConsoleOutput ConsoleOutput, DataStructure Data)
        {
            string thought = "\n   Carrying the world globe on my back.";
            string[] prompts = {
                "Inventory ", "- ", Data.EquippedBackpack + "\n",
                "Carrying weight", " : " + CalcInventoryWeight(Data.Inventory, Data.InventoryValues) + "/" + Data.MaxCarryingWeight + " KG\n",
                "Thoughts", " : " + thought + "\n\n",
                "[", "Loot", "/", "Holstered Tools", "]", "  Pocket : 1/10\n"
            };
            int[] textcolors = { 
                14, 6,
                5, 1, 6,
                1, 7,
                6, 5, 6, 7, 6, 6
            };
            string[] HorizontalOptions = {
                "Next", "Previous", "View Holstered tools", "Zip up the backpack"
            };
            int[] HorizontalOptionSymbols = {
                5, 6, 5, 6, 5, 6, 5, 6
            };
            int[] HorizontalOptionColors = { 
                6, 6, 6, 6
            };
            int repeat = (Data.Inventory.Count % 15 == 0) ? 15 : Data.Inventory.Count;
            string[] InventoryOptions = new string[repeat];
            int[] InventorySpecialsymbol = new int[repeat * 2];
            int[] InventoryOptionColors = new int[repeat];
            int[] HorizontalLineStructure = new int[repeat + 3];
            HorizontalLineStructure[0] = 6;
            int SpecialSymbolIndex = 0;
            ConsoleOutput.OptionIndexPlacement = 1;
            if (Data.Inventory.Count > 0)
            {
                for (int i = 1; i < repeat + 1; i++)
                {
                    decimal ItemWeight = CalcItemWeight(Data.Inventory[i - 1], Data.InventoryValues[i - 1]);
                    InventoryOptions[i - 1] = "[" + DefineLootType(Data.Inventory[i - 1]) + "] " + Data.Inventory[i - 1] + " [" + Data.InventoryValues[i - 1] + "x] " + ItemWeight + " KG";
                    InventorySpecialsymbol[SpecialSymbolIndex] = 7;
                    InventorySpecialsymbol[SpecialSymbolIndex + 1] = 7;
                    InventoryOptionColors[i - 1] = 7;
                    SpecialSymbolIndex += 2;
                    HorizontalLineStructure[i] = 1;
                }
                HorizontalLineStructure[HorizontalLineStructure.Length - 2] = 3;
                HorizontalLineStructure[HorizontalLineStructure.Length - 1] = 1;
                HorizontalOptions = [.. InventoryOptions, .. HorizontalOptions];
                HorizontalOptionColors = [.. InventoryOptionColors, .. HorizontalOptionColors];
                HorizontalOptionSymbols = [.. InventorySpecialsymbol, .. HorizontalOptionSymbols];
            }
            ConsoleOutput.OptionIndexPlacement = 0;
            ConsoleOutput.UpdateValues(prompts, textcolors, null, null, null, HorizontalOptions, HorizontalOptionColors, HorizontalOptionSymbols, HorizontalLineStructure);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderInventoryFilters();
            ConsoleOutput.RenderHorizontalOptions();
            ConsoleOutput.RunHorizontal(true, Data);
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
