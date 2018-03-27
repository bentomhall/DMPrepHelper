using System.Collections.Generic;
using System;

namespace LibGenerator.Dungeon
{

    public class LocationData
    {
        public string Name { get; set; } = "";
        public string Scale { get; set; } = "";
        public List<int> Sizes { get; set; } = new List<int>();
        public List<string> Subtypes { get; set; } = new List<string>();
        public List<string> Ages { get; set; } = new List<string>();
        public double HasBoss { get; set; } = 0.0;
        public Dictionary<string, double> LairChance { get; set; } = new Dictionary<string, double>() { {"1", 0.0 }, { "2", 0.0 }, { "3", 0.0 }, { "4", 0.0 } };
        public bool HasSublocations { get; set; } = false;

        public int GetSize()
        {
            var index = r.Next(0, Sizes.Count);
            return Sizes[index];
        }

        public bool GetBoss()
        {
            var x = r.NextDouble();
            return x < HasBoss;
        }

        public bool HasLair(string tier)
        {
            var chance = LairChance[tier];
            return (r.NextDouble() < chance);
        }

        public string GetAge()
        {
            var index = r.Next(0, Ages.Count);
            return Ages[index];
        }

        public string GetSubtype()
        {
            var index = r.Next(0, Subtypes.Count);
            return Subtypes[index];
        }

        private Random r = new Random();
    }
}