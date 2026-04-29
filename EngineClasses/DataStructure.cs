using System;
using System.Collections.Generic;
using System.Text;

namespace LITOURGIYA___OBLATION.EngineClasses
{
    internal class DataStructure
    {
        public int Version { get; set; }
        //First branch == Progression status data
        public int InGameDay { get; set; }
        public bool FirstAimPractise { get; set; }
        public string Time { get; set; }
        public Dictionary<string, int[]> EnvironmentalLootData { get; set; } = new();
        public Dictionary<string, string[]> EnvironmentalLootDataNames { get; set; } = new();
        public Dictionary<string, bool> EnvironmentalStatusData { get; set; } = new();
        //Second branch == config data
        public string FileCreationHour { get; set; }
        public string FileCreationMinutes { get; set; }
        public string FileCreationDate { get; set; }
        public string FileCreationName { get; set; }
        //Third branch == Tools status
        public int BulletsInMag { get; set; }
        //Fourth branch == Emily status
        public int MoodStatus { get; set; }
        public int PainStatus { get; set; }
        public int HungerStatus { get; set; }
        public int ThirstStatus { get; set; }
        public int HeatStatus { get; set; }
        public int ColdStatus { get; set; }
        public int EnergyStatus { get; set; }
        public List<string> Inventory { get; set; } = new();
        public List<int> InventoryValues { get; set; } = new();
        public List<int> ToolsDurability { get; set; } = new();
        public List<string> Tools { get; set; } = new();
        public List<int> CarryingTools { get; set; } = new();
        public decimal InventoryTotalWeight { get; set; } = 0m;
        public decimal MaxCarryingWeight { get; set; } = 50;
    }
}