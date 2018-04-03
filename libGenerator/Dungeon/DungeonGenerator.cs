using System;
using System.Collections.Generic;
using System.Linq;
using libGenerator.Core;

namespace LibGenerator.Dungeon
{
    public class DungeonGenerator
    {

        public DungeonGenerator(IEnumerable<RegionData> r, IEnumerable<LocationData> l)
        {
            regions = r.ToList();
            locations = l.ToList();
        }

        public AdventureData GenerateAdventure(RegionData selectedRegion)
        {
            var randomType = selectedRegion.GetRandomLocationType();
            LocationData type = locations.First(x => x.Name == randomType);
            var adventure = new AdventureData() { Region = selectedRegion.Name, AdventureType = type.Name, Scale = type.Scale };
            try
            {

                adventure.Level = GetRandomLevel(selectedRegion.GetTier());
                adventure.PrimaryMonster = selectedRegion.GetRandomMonster(r.NextDouble());
                adventure.Size = type.GetSize();
                adventure.HasBoss = type.GetBoss();
                adventure.SubType = type.GetSubtype();
            } catch (Exception e)
            {
                var exc = new ConfigException("Creating adventure failed due to configuration issues. Check the format of region and adventure types", e);
                throw exc;
            }
            return adventure;
        }

        public AdventureData GenerateAdventure(string regionName)
        {
            RegionData region;
            try
            {
                region = regions.First(x => x.Name == regionName);
            } catch (Exception e)
            {
                throw new ConfigException("Error retrieving region. Check that region names are properly specified.", e);
            }
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
            {"4", new List<int>() {17,20}},
            {"Epic", new List<int>() {21, 30 } }
        };
        private Random r = new Random();
    }
}
