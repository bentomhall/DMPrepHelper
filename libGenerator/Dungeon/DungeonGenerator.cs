using System;
using System.Collections.Generic;
using System.Linq;
using LibGenerator.Core;

namespace LibGenerator.Dungeon
{
    public class DungeonGenerator
    {
        public DungeonGenerator()
        {
            regions = ConfigurationLoader.GetConfiguration<RegionData>("data/regionData.json");
            locations = ConfigurationLoader.GetConfiguration<LocationData>("data/dungeonData.json");
        }

        public AdventureData GenerateAdventure(RegionData selectedRegion)
        {
            var randomType = selectedRegion.GetRandomLocationType();
            LocationData type = locations.First(x => x.Name == randomType);
            var adventure = new AdventureData
            {
                Region = selectedRegion.Name,
                AdventureType = type.Name,
                Level = GetRandomLevel(selectedRegion.Tier),
                PrimaryMonster = selectedRegion.GetRandomMonster(r.NextDouble()),
                Scale = type.Scale,
                Size = type.GetSize(),
                HasBoss = type.GetBoss(),
                SubType = type.GetSubtype()
            };
            return adventure;
        }

        public AdventureData GenerateAdventure(string regionName)
        {
            var region = regions.First(x => x.Name == regionName);
            return GenerateAdventure(region);
        }

        public IEnumerable<string> GetValidRegions() => regions.Select(x => x.Name);

        private int GetRandomLevel(string tier)
        {
            var tierLevels = tierMap[tier];
            return r.Next(tierLevels[0], tierLevels[1]+1);
        }

        private List<RegionData> regions = new List<RegionData>();
        private List<AdventureData> adventures = new List<AdventureData>();
        private List<LocationData> locations = new List<LocationData>();
        private Dictionary<string, List<int>> tierMap = new Dictionary<string, List<int>>()
        {
            {"1", new List<int>() {1, 4}},
            {"1.5", new List<int>(){3, 7}},
            {"2", new List<int>() {5, 11}},
            {"2.5", new List<int>() {8, 13}},
            {"3", new List<int>() {12, 16}},
            {"3.5", new List<int>() {14, 18}},
            {"4", new List<int>() {17,20}}
        };
        private Random r = new Random();
    }
}
