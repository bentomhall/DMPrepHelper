using System;
using System.Collections.Generic;
using LibGenerator.Core;

namespace LibGenerator.NPC
{
    public class NationData
    {
        public string Name { get; set; } = "";
        public Dictionary<string, double> Races { get; set; } = new Dictionary<string, double> { { "human", 1.0 } };
        private WeightedChoiceSet races;

        public string GetRace()
        {
            if (races == null)
            {
                races = new WeightedChoiceSet(Races);
            }
            var roll = r.NextDouble();
            return races.Match(roll);
        }

        private Random r = new Random();
    }
}
