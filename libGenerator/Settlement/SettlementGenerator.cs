using System;
using System.Collections.Generic;
using System.Linq;
using LibGenerator.Core;
using LibGenerator.NPC;

namespace LibGenerator.Settlement
{
    public class SettlementGenerator
    {
        /*
        public SettlementGenerator()
        {
            npcGenerator = new NPCGenerator();
            cities = ConfigurationLoader.GetConfiguration<CityData>("data/city_data.json");
            items = ConfigurationLoader.GetConfiguration<ItemData>("data/itemRanks.json");
            settlements = ConfigurationLoader.GetConfiguration<SettlementData>("data/settlementTypes.json");
            roles = ConfigurationLoader.GetConfiguration<SettlementRole>("data/settlementRoles.json"); 
        }*/

        public SettlementGenerator(List<CityData> c, List<ItemData> i, List<SettlementData> s, List<SettlementRole> r, NPCGenerator gen)
        {
            npcGenerator = gen;
            cities = c;
            items = i;
            settlements = s;
            roles = r;
        }

        public IEnumerable<string> GetPossibleSettlementTypes()
        {
            return settlements.Select(x => x.Name);
        }

        public IEnumerable<string> GetPossibleCities() => cities.Select(x => x.Name);

        public Settlement GenerateSettlement(string size, string nearestCity)
        {
            var nearest = cities.First(x => x.Name.ToLower() == nearestCity.ToLower());
            var name = nearest.GetName();
            var sInfo = settlements.First(x => x.Name.ToLower() == size.ToLower());
            var pop = sInfo.GetPopulation(random.NextDouble());
            var demographics = nearest.GetDemographics(pop);
            var sNPCs = sInfo.GetNPCs(nearest, npcGenerator);
            var role = sInfo.GetRole(random.NextDouble());
            var tech = ApplyRole(sInfo, roles.First(x => x.Name.ToLower() == role.ToLower()));
            var sItems = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string,int> kv in tech)
            {
                var data = items.Where(x => x.Category == kv.Key);
                foreach (ItemData item in data) {
                    sItems[kv.Key + $" : {item.Subcategory}"] = item.NotAvailable(kv.Value).Select(x => x.Name).ToList();    
                }

            }
            return new Settlement()
            {
                Name = name,
                NPCs = sNPCs,
                Population = pop,
                Role = role,
                Demographics = demographics,
                Size = size,
                NearestCity = nearest.Name,
                TechLevels = tech,
                UnavailableItems = sItems
            };
        }

        private Dictionary<string, int> ApplyRole(SettlementData data, SettlementRole role)
        {
            var tech = data.TechCaps;
            foreach (Specialty s in role.Specialties)
            {
                var current = tech[s.Name];
                tech[s.Name] = Math.Min(4, Math.Max(0, current + s.Modifier));
            }
            return tech;
        }

        private NPCGenerator npcGenerator;
        private Random random = new Random();
        private List<CityData> cities;
        private List<ItemData> items;
        private List<PersonData> npcs = new List<PersonData>();
        private List<SettlementData> settlements;
        private List<SettlementRole> roles;
        private List<Settlement> generatedSettlements = new List<Settlement>();
    }
}
