using System;
using System.Collections.Generic;
using libGenerator.Core;

namespace libGenerator.NPC
{
    public class NationData
    {
        public string Name { get; set; } = "";
        public Dictionary<string, double> Races { get; set; } = new Dictionary<string, double>();
        private WeightedChoiceSet<string> races;

        public string GetRace()
        {
            if (races == null)
            {
                races = new WeightedChoiceSet<string>(Races);
            }
            var roll = r.NextDouble();
            return races.Match(roll);
        }

        private Random r = new Random();
    }
}
