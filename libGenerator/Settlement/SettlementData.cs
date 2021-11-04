using System.Collections.Generic;
using libGenerator.Core;
using libGenerator.NPC;

namespace libGenerator.Settlement
{
    public class SettlementData
    {
        public string Name { get; set; } = "";
        public int PopulationMax { get; set; } = 0;
        public int PopulationMin { get; set; } = 0;
        public List<string> NPCs { get; set; } = new List<string>();
        public Dictionary<string, int> TechCaps { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, double> Roles { get; set; } = new Dictionary<string, double>();

        private WeightedChoiceSet<string> roles;

        public string GetRole(double x)
        {
            if (roles == null)
            {
                roles = new WeightedChoiceSet<string>(Roles);
            }
            return roles.Match(x);
        }

        public int GetPopulation(double x)
        {
            int difference = PopulationMax - PopulationMin;
            return (int)(difference * x) + PopulationMin;
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