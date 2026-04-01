using LITOURGIYA___OBLATION.EngineClasses;
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
    }
}
