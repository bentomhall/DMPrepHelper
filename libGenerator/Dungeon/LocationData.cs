using System.Collections.Generic;
using System;

namespace LibGenerator.Dungeon
{

    public class LocationData
    {
        public string Name { get; set; }
        public string Scale { get; set; }
        public List<int> Sizes { get; set; }
        public List<string> Subtypes { get; set; }
        public List<string> Ages { get; set; }
        public double HasBoss { get; set; }
        public Dictionary<string, double> LairChance { get; set; }
        public bool HasSublocations { get; set; }

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