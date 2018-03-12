﻿using System.Collections.Generic;
using System;
using LibGenerator.Core;

namespace LibGenerator.Dungeon
{
    public class RegionData
    {
        public string Name { get; set; }
        public string Tier { get; set; }
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

        private Dictionary<string, double> monsterTable;
        private WeightedChoiceSet monsters;

        public void SetMonsters(Dictionary<string, double> m)
        {
            monsters = new WeightedChoiceSet(m);
        }

        public void SetMonsters(WeightedChoiceSet m)
        {
            monsters = m;
        }

        public string GetRandomMonster(double r)
        {
            if (monsters == null)
            {
                monsters = new WeightedChoiceSet(Monsters);
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