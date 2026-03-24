using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LITOURGIYA___OBLATION.EngineClasses
{
    internal class AssetStation
    {
        public string[] ThirstParameters = { "Dry mouth", "Parched", "Dehydrated", "Burning throat" };//Level 0 is blank
        public string[] HungerParameters = { "Rumbling stomach", "Stomach cramping", "Starving", "Faint" };//Level 0 is blank
        public string[] MildInjuryParameters = { "Bruise", "Cut", "Burned", "Infected" }; //These will appear in body status menu on each part
        public string[] SevereInjuryParameters = { "Deep cut", "Bleeding", "Severely burned", "Frostbite", "Broken arm", "Broken leg", "Broken rib", "Sprained knee", "Severely infected" }; //These as well
        public string[] MildSicknessParameters = { "Fever", "Nausea" }; //Level 0 is blank
        public string[] SevereSicknaessParameters = { "Vomitting blood", "Food Sickness" };
        public string[] EnergyParameters = { "Energetic", "Rested", "Tired", "Exhausted", "Fatigued" };
        public string[] SanityParameters = { "Content", "Stable", "Uneasy", "Disturbed", "Panicking" };
        public string[] ColdParameters = { "Cold", "Freezing" }; //Level 0 is blank
        public string[] HeatParameters = { "Sweating", "Overheating" }; //Level 0 is blank
    }
}
