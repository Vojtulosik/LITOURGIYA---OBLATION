using LITOURGIYA___OBLATION.EngineClasses;
using LITOURGIYA___OBLATION.GameplayClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LITOURGIYA___OBLATION.EnvironmentGeneration
{
    internal class LootInteractionGen
    {
        BodyStatus BodyStatus = new BodyStatus();
        public string[] Generate(int[] values, string[] names, DataStructure Data, out int[] specialsymbol, out int[] optioncolors)
        {
            string[] options = new string[names.Length + 1];
            for (int i = 0; i < names.Length; i++)
            {
                int ListValue = Data.Inventory.IndexOf(names[i]);
                if (ListValue == -1) ListValue = 0;
                else ListValue = Data.InventoryValues[ListValue];
                char itemType = DefineLootType(names[i]);

                decimal weight = BodyStatus.CalcItemWeight(names[i], values[i]);
                options[i] = "[" + itemType + "] " + names[i] + " [" + values[i] + "x] >> Inventory : [" + ListValue + "x]" + " +" + weight + "KG";
            }
            options[names.Length] = "Back";


            int index = 0;
            specialsymbol = new int[names.Length * 2 + 2];
            for (int i = 0; i < names.Length; i++)
            {
                specialsymbol[index] = 4;
                specialsymbol[index + 1] = 4;
                index += 2;
            }
            specialsymbol[specialsymbol.Length - 1] = 6;
            specialsymbol[specialsymbol.Length - 2] = 5;


            optioncolors = new int[names.Length + 1];
            for (int i = 0; i < names.Length; i++)
            {
                if (values[i] > 0)
                {
                    optioncolors[i] = 6;
                }
                else
                {
                    optioncolors[i] = 8;
                }
            }
            optioncolors[optioncolors.Length - 1] = 6;

            return options;
        }
        public void FormLists(DataStructure Data, out List<string> LootOptions, out List<int> LootOptionsValues, out List<int> LootOptionsStoreValues, string key)
        {
            int[] LootData = (int[])Data.EnvironmentalLootData[key].Clone();
            string[] LootDataNames = (string[])Data.EnvironmentalLootDataNames[key].Clone();

            LootOptions = new List<string>();
            LootOptionsValues = new List<int>();
            LootOptionsStoreValues = new List<int>();

            for (int i = 0; i < LootData.Length; i++)
            {
                LootOptionsStoreValues.Add(LootData[i]);
                if (LootData[i] != 0)
                {
                    LootOptions.Add(LootDataNames[i]);
                    LootOptionsValues.Add(LootData[i]);
                }
            }
        }
        public DataStructure LootInteract(DataStructure Data, int SelectedIndex, List<int> LootOptionsValues, List<string> LootOptions, string[] LootDataNames, int[] LootData, List<int> LootOptionsStoreValues, string DictionaryKey, ConsoleKey KeyPressed)
        {
            if (SelectedIndex < 9999 && LootOptionsValues[SelectedIndex] > 0)
            {
                int LootTypeIndex = LootDataNames.IndexOf(LootOptions[SelectedIndex]);
                switch (KeyPressed)
                {
                    case ConsoleKey.Spacebar:
                        if (!Data.Inventory.Contains(LootOptions[SelectedIndex]))
                        {
                            Data.Inventory.Add(LootOptions[SelectedIndex]);
                            Data.InventoryValues.Add(1);
                            LootOptionsStoreValues[LootTypeIndex] -= 1;
                            LootOptionsValues[SelectedIndex] -= 1;
                            break;
                        }

                            Data.InventoryValues[Data.Inventory.IndexOf(LootOptions[SelectedIndex])] += 1;
                            LootOptionsStoreValues[LootTypeIndex] -= 1;
                            LootOptionsValues[SelectedIndex] -= 1;
                        break;
                    case ConsoleKey.Enter:
                        if (!Data.Inventory.Contains(LootOptions[SelectedIndex]))
                        {
                            Data.Inventory.Add(LootOptions[SelectedIndex]);
                            Data.InventoryValues.Add(LootOptionsValues[SelectedIndex]);
                        }
                        else
                        {
                            int invIndex = Data.Inventory.IndexOf(LootOptions[SelectedIndex]);
                            if (invIndex != -1)
                            {
                                Data.InventoryValues[invIndex] += LootOptionsValues[SelectedIndex];
                            }
                        }
                        LootOptionsStoreValues[LootTypeIndex] = 0;
                        LootOptionsValues[SelectedIndex] = 0;
                        break;
                    case ConsoleKey.Backspace:
                        if (LootOptionsValues[SelectedIndex] < LootData[LootTypeIndex])
                        {
                            Data.InventoryValues[Data.Inventory.IndexOf(LootOptions[SelectedIndex])] -= 1;
                            LootOptionsStoreValues[LootTypeIndex] += 1;
                            LootOptionsValues[SelectedIndex] += 1;
                            break;
                        }
                        break;
                }
            }
            else if (SelectedIndex == 9999)
            {
                Data.EnvironmentalLootData[DictionaryKey] = LootOptionsStoreValues.ToArray();
                return Data;
            }
            else
            {
                int LootTypeIndex = LootDataNames.IndexOf(LootOptions[SelectedIndex]);
                if (KeyPressed == ConsoleKey.Enter)
                {
                    LootOptionsValues[SelectedIndex] = LootData[LootTypeIndex];
                    LootOptionsStoreValues[LootTypeIndex] = LootData[LootTypeIndex];
                    int ListIndex = Data.Inventory.IndexOf(LootOptions[SelectedIndex]);
                    if (Data.InventoryValues[ListIndex] - LootData[LootTypeIndex] <= 0)
                    {
                        Data.Inventory.RemoveAt(ListIndex);
                        Data.InventoryValues.RemoveAt(ListIndex);
                    }
                    else
                    {
                        Data.InventoryValues[ListIndex] -= LootData[LootTypeIndex];
                    }
                }
                if (KeyPressed == ConsoleKey.Backspace)
                {
                    Data.InventoryValues[Data.Inventory.IndexOf(LootOptions[SelectedIndex])] -= 1;
                    LootOptionsStoreValues[LootTypeIndex] += 1;
                    LootOptionsValues[SelectedIndex] += 1;
                }
            }
            Data.EnvironmentalLootData[DictionaryKey] = LootOptionsStoreValues.ToArray();
            return Data;
        }
        private char DefineLootType(string item)
        {
            char type = item switch
            {
                "Scrap polymers" => 'R',
                "Wood scrap" => 'R',
                "Bottle of acid" => 'R',
                "Glue" => 'R',
                "Empty toolbox" => 'T',
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
                _ => ' '
            };
            return type;
        }
    }
}
