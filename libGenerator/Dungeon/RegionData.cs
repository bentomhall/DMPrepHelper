using System.Collections.Generic;
using System;
using libGenerator.Core;

namespace libGenerator.Dungeon
{
    public class RegionData
    {
        public string Name { get; set; } = "";
        public List<string> Tier { get; set; } = new List<string>();
        public Dictionary<string, double> Monsters
        {
            get => monsterTable;
            set
            {
                monsterTable = value;
                SetMonsters(value);
            }
        }
        public float AdventuresPerHex { get; set; }
        public List<string> AdventureTypes { get; set; }

        private Dictionary<string, double> monsterTable = new Dictionary<string, double>();
        private WeightedChoiceSet<string> monsters;

        public string GetTier()
        {
            return r.Choice(Tier);
        }

        public void SetMonsters(Dictionary<string, double> m)
        {
            monsters = new WeightedChoiceSet<string>(m);
        }

        public void SetMonsters(WeightedChoiceSet<string> m)
        {
            monsters = m;
        }

        public string GetRandomMonster(double r)
        {
            if (monsters == null)
            {
                monsters = new WeightedChoiceSet<string>(Monsters);
            }
            return monsters.Match(r);
        }

        public string GetRandomLocationType()
        {
            var index = r.Next(0, AdventureTypes.Count);
            return AdventureTypes[index];
        }

        private readonly Random r = new Random();


    }
}