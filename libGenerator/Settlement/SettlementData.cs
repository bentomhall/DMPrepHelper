using System.Collections.Generic;
using LibGenerator.Core;
using LibGenerator.NPC;

namespace LibGenerator.Settlement
{
    public class SettlementData
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public List<string> NPCs { get; set; }
        public Dictionary<string, int> TechCaps { get; set; }
        public Dictionary<string, double> Roles { get; set; }

        private WeightedChoiceSet roles;

        public string GetRole(double x)
        {
            if (roles == null)
            {
                roles = new WeightedChoiceSet(Roles);
            }
            return roles.Match(x);
        }

        public int GetPopulation(double x)
        {
            var multiplier = x * 0.2 + 0.9;
            return (int)(Population * multiplier);
        }

        public List<PersonData> GetNPCs(CityData city, NPCGenerator gen)
        {
            var nation = new NationData() { Name = city.Nation, Races = city.Races };
            var output = new List<PersonData>();
            foreach (var prof in NPCs)
            {
                var npc = gen.GenerateNPCforCity(nation);
                npc.Profession = prof;
                output.Add(npc);
            }
            return output;
        }

    }
}