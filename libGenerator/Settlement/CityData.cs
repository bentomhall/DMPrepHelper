using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using libGenerator.Core;
using System;

namespace libGenerator.Settlement
{
    public class CityData
    {
        public string Name { get; set; } = "";
        public string Nation { get; set; } = "";
        public int Population { get; set; } = 0;
        public string Size { get; set; } = "";
        public string Region { get; set; } = "";
        public string Terrain { get; set; } = "";
        public Dictionary<string, double> Races { get; set; } = new Dictionary<string, double>();
        public int Tech { get; set; } = 0;
        public List<string> Prefixes { get; set; } = new List<string>();
        public List<string> Infixes { get; set; } = new List<string>();
        public List<string> Suffixes { get; set; } = new List<string>();
        public string Combiner { get; set; } = "";

        private WeightedChoiceSet<string> races;

        public string GetRace()
        {
            if (races == null)
            {
                races = new WeightedChoiceSet<string>(Races);
            }
            var r = random.NextDouble();
            return races.Match(r);
        }

        private Random random = new Random();
        public string GetName()
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var prefix = Prefixes.Count == 0 ? "" : random.Choice(Prefixes)+Combiner;
            var infix = Infixes.Count == 0 ? "" : random.Choice(Infixes) + Combiner;
            var suffix = random.Choice(Suffixes);
            return textInfo.ToTitleCase(prefix+infix+suffix);
        }

        public Dictionary<string, int> GetDemographics(int population)
        {
            return Races.ToDictionary(o => o.Key, o => (int)(o.Value * population));
        }
    }
}