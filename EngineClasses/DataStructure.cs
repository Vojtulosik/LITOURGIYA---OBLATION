using System;
using System.Collections.Generic;
using System.Text;

namespace LITOURGIYA___OBLATION.EngineClasses
{
    internal class DataStructure
    {
        //First branch == Progression status data
        public int InGameDay { get; set; }
        public bool FirstAimPractise { get; set; }
        public string Time { get; set; }
        //Second branch == config data
        public string FileCreationHour { get; set; }
        public string FileCreationMinutes { get; set; }
        public string FileCreationDate { get; set; }
        public string FileCreationName { get; set; }
        //Third branch == Loot data
        public int BulletsInMag { get; set; }
        //Fourth branch == Emily status
        public int MoodStatus { get; set; }
        public int PainStatus { get; set; }
        public int HungerStatus { get; set; }
        public int ThirstStatus { get; set; }
        public int HeatStatus { get; set; }
        public int ColdStatus { get; set; }
        public int EnergyStatus { get; set; }
        //Fifth branch == Orchidej left section data
        public bool TinyStockroomExplored { get; set; } = false;
    }
}
