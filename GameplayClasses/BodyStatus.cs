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
        public void BodyCheckUI(ConsoleOutput ConsoleOutput)
        {
            string[] prompts = { "test\n\n" };
            int[] textcolors = { 6 };
            string[] options = { "back" };
            int[] specialsymbol = { 0, 1 };
            ConsoleOutput.OptionIndexPlacement = 0;
            ConsoleOutput.UpdateValues(prompts, textcolors, options, specialsymbol);
            ConsoleOutput.RenderText(prompts, textcolors);
            ConsoleOutput.RenderOptions(options, specialsymbol);
            ConsoleOutput.Run(true);
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
    }
}
