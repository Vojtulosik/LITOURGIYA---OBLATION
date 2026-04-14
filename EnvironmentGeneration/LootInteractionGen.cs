using LITOURGIYA___OBLATION.EngineClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LITOURGIYA___OBLATION.EnvironmentGeneration
{
    internal class LootInteractionGen
    {
        public string[] Generate(int[] values, string[] names, DataStructure Data, out int[] specialsymbol, out int[] optioncolors)
        {
            string[] options = new string[names.Length + 1];
            for (int i = 0; i < names.Length; i++)
            {
                int ListValue = Data.Inventory.IndexOf(names[i]);
                if (ListValue == -1) ListValue = 0;
                else ListValue = Data.InventoryValues[ListValue];


                options[i] = names[i] + " [" + values[i] + "x] >> Inventory : [" + ListValue + "x]";
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
        public void FormLists(DataStructure Data, out List<string> LootOptions, out List<int> LootOptionsValues, out List<int> LootOptionsStoreValues)
        {
            int[] LootData = (int[])Data.EnvironmentalLootData["TinyStockroomLeftRackLoot"].Clone();
            string[] LootDataNames = (string[])Data.EnvironmentalLootDataNames["TinyStockroomLeftRackLoot"].Clone();

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
        public DataStructure LootInteract(DataStructure Data, int SelectedIndex, List<int> LootOptionsValues, List<string> LootOptions, string[] LootDataNames, int[] LootData, List<int> LootOptionsStoreValues, string DictionaryKey)
        {
            if (LootOptionsValues[SelectedIndex] > 0)
            {
                int LootTypeIndex = LootDataNames.IndexOf(LootOptions[SelectedIndex]);
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
            else
            {
                int LootTypeIndex = LootDataNames.IndexOf(LootOptions[SelectedIndex]);
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
            Data.EnvironmentalLootData[DictionaryKey] = LootOptionsStoreValues.ToArray();
            return Data;
        }
    }
}
