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
        private string[] SupportedTools = { "Flashlight", "Toolbox" };
        BodyStatus BodyStatus = new BodyStatus();
        Random Rand = new Random();
        public string[] Generate(int[] values, string[] names, DataStructure Data, out int[] specialsymbol, out int[] optioncolors)
        {
            string[] options = new string[names.Length + 1];
            for (int i = 0; i < names.Length; i++)
            {
                int ListValue = Data.Inventory.IndexOf(names[i]);
                if (ListValue == -1) ListValue = 0;
                else ListValue = Data.InventoryValues[ListValue];
                if (SupportedTools.Contains(names[i])) ListValue = CountSpecificToolInInv(names[i], Data);
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
            decimal InventoryWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
            decimal ItemWeight = 0m;
            if (SelectedIndex < 9999 && LootOptionsValues[SelectedIndex] > 0)
            {
                int LootTypeIndex = LootDataNames.IndexOf(LootOptions[SelectedIndex]);
                InventoryWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                switch (KeyPressed)
                {
                    case ConsoleKey.Spacebar:
                        ItemWeight = BodyStatus.CalcItemWeight(LootOptions[SelectedIndex], 1);
                        char LootKind = DefineLootType(LootOptions[SelectedIndex]);
                        if (InventoryWeight + ItemWeight <= Data.MaxCarryingWeight)
                        {
                            if (LootKind == 'T')
                            {
                                int ID = GetToolID(LootOptions[SelectedIndex], Data);
                                Data.Inventory.Add(LootOptions[SelectedIndex] + ID);
                                Data.InventoryValues.Add(1);
                                LootOptionsStoreValues[LootTypeIndex] = 0;
                                LootOptionsValues[SelectedIndex] = 0;
                                if (!Data.ToolIDs.Contains(LootOptions[SelectedIndex] + ID))
                                {
                                    Data.FoundTools.Add(LootOptions[SelectedIndex]);
                                    Data.ToolIDs.Add(LootOptions[SelectedIndex] + ID);
                                    Data.ToolDurability.Add(DefineToolStartingDurability());
                                }
                                Data = IncreaseToolID(LootOptions[SelectedIndex], Data);
                            }
                            else
                            {
                                if (!Data.Inventory.Contains(LootOptions[SelectedIndex]))
                                {
                                    Data.Inventory.Add(LootOptions[SelectedIndex]);
                                    Data.InventoryValues.Add(1);
                                    LootOptionsValues[SelectedIndex] -= 1;
                                    LootOptionsStoreValues[LootTypeIndex] -= 1;
                                    break;
                                }

                                Data.InventoryValues[Data.Inventory.IndexOf(LootOptions[SelectedIndex])] += 1;
                                LootOptionsValues[SelectedIndex] -= 1;
                                LootOptionsStoreValues[LootTypeIndex] -= 1;
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        ItemWeight = BodyStatus.CalcItemWeight(LootOptions[SelectedIndex], LootData[LootTypeIndex]);
                        LootKind = DefineLootType(LootOptions[SelectedIndex]);
                        if (InventoryWeight + ItemWeight <= Data.MaxCarryingWeight)
                        {
                            if (LootKind == 'T')
                            {
                                int ID = GetToolID(LootOptions[SelectedIndex], Data);
                                Data.Inventory.Add(LootOptions[SelectedIndex] + ID);
                                Data.InventoryValues.Add(1);
                                LootOptionsStoreValues[LootTypeIndex] = 0;
                                LootOptionsValues[SelectedIndex] = 0;
                                if (!Data.ToolIDs.Contains(LootOptions[SelectedIndex] + ID))
                                {
                                    Data.FoundTools.Add(LootOptions[SelectedIndex]);
                                    Data.ToolIDs.Add(LootOptions[SelectedIndex] + ID);
                                    Data.ToolDurability.Add(DefineToolStartingDurability());
                                }
                                Data = IncreaseToolID(LootOptions[SelectedIndex], Data);
                            }
                            else
                            {
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
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        LootKind = DefineLootType(LootOptions[SelectedIndex]);
                        if (LootOptionsValues[SelectedIndex] < LootData[LootTypeIndex])
                        {
                            if (LootKind == 'T')
                            {
                                int ID = GetToolID(LootOptions[SelectedIndex], Data) - 1;
                                Data.InventoryValues.RemoveAt(Data.Inventory.IndexOf(LootOptions[SelectedIndex] + ID));
                                Data.Inventory.Remove(LootOptions[SelectedIndex] + ID);
                                LootOptionsStoreValues[LootTypeIndex] = 1;
                                LootOptionsValues[SelectedIndex] = 1;
                                DecreaseToolID(LootOptions[SelectedIndex], Data);
                            }
                            else
                            {
                                Data.InventoryValues[Data.Inventory.IndexOf(LootOptions[SelectedIndex])] -= 1;
                                LootOptionsStoreValues[LootTypeIndex] += 1;
                                LootOptionsValues[SelectedIndex] += 1;
                                break;
                            }
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
                InventoryWeight = BodyStatus.CalcInventoryWeight(Data.Inventory, Data.InventoryValues);
                int LootTypeIndex = LootDataNames.IndexOf(LootOptions[SelectedIndex]);
                char LootKind = DefineLootType(LootOptions[SelectedIndex]);
                if (KeyPressed == ConsoleKey.Enter)
                {
                    if (LootKind == 'T')
                    {
                        int ID = GetToolID(LootOptions[SelectedIndex], Data) - 1;
                        Data.InventoryValues.RemoveAt(Data.Inventory.IndexOf(LootOptions[SelectedIndex] + ID));
                        Data.Inventory.Remove(LootOptions[SelectedIndex] + ID);
                        LootOptionsStoreValues[LootTypeIndex] = 1;
                        LootOptionsValues[SelectedIndex] = 1;
                        DecreaseToolID(LootOptions[SelectedIndex], Data);
                    }
                    else
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
                }
                if (KeyPressed == ConsoleKey.Backspace)
                {
                    if (LootKind == 'T')
                    {
                        int ID = GetToolID(LootOptions[SelectedIndex], Data) - 1;
                        Data.InventoryValues.RemoveAt(Data.Inventory.IndexOf(LootOptions[SelectedIndex] + ID));
                        Data.Inventory.Remove(LootOptions[SelectedIndex] + ID);
                        LootOptionsStoreValues[LootTypeIndex] = 1;
                        LootOptionsValues[SelectedIndex] = 1;
                        DecreaseToolID(LootOptions[SelectedIndex], Data);
                    }
                    else
                    {
                        Data.InventoryValues[Data.Inventory.IndexOf(LootOptions[SelectedIndex])] -= 1;
                        LootOptionsStoreValues[LootTypeIndex] += 1;
                        LootOptionsValues[SelectedIndex] += 1;
                    }
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
                "Can of mystery meat" => 'C',
                "Can of chicken stock" => 'C',
                "Can of beans" => 'C',
                "Can of Vegetable mix" => 'C',
                "Can of Tomato sauce" => 'C',
                "Bag of Tagliatelle" => 'C',
                "Bag of grated cheese" => 'C',
                "SNWLEO" => 'G',
                "Noisemaker bait" => 'E',
                _ => ' '
            };
            return type;
        }
        private decimal DefineToolStartingDurability()
        {
            decimal Corrosion = Rand.Next(19, 100);
            return 100 * (Corrosion / 100);
        }
        private int GetToolID(string tool, DataStructure data)
        {
            int id = tool switch
            {
                "Flashlight" => data.FlashLightIDCount,
                "Toolbox" => data.ToolboxIDCount
            };
            return id;
        }
        private DataStructure DecreaseToolID(string tool, DataStructure data)
        {
            switch(tool)
            {
                case "Flashlight":
                    data.FlashLightIDCount--;
                    break;
                case "Toolbox":
                    data.ToolboxIDCount--;
                    break;
            }
            return data;
        }
        private DataStructure IncreaseToolID(string tool, DataStructure data)
        {
            switch (tool)
            {
                case "Flashlight":
                    data.FlashLightIDCount++;
                    break;
                case "Toolbox":
                    data.ToolboxIDCount++;
                    break;
            }
            return data;
        }
        private int CountSpecificToolInInv(string tool, DataStructure data)
        {
            int count = 0;
            for (int i = 0; i < data.Inventory.Count; i++)
            {
                string ToolCheck = data.Inventory[i].Substring(0, data.Inventory[i].Length - 1);
                if (tool == ToolCheck)
                {
                    if (SupportedTools.Contains(ToolCheck)) count++;
                }
            }
            return count;
        }
    }
}
